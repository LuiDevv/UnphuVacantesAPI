using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Account;
using api.interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());
            if(user == null)
                return Unauthorized("Invalid username");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if(!result.Succeeded)
                return Unauthorized("Username or password is incorrect");
            return Ok(
                new NewUserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO registerDTO)
        {
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var appUser = new AppUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email,
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    PhoneNumber = registerDTO.PhoneNumber

                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);
                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "JobSeeker");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDTO
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Verificar autenticación primero
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("User not authenticated");
            }

            // Obtener el usuario usando el claim del token
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound("User not found in database");  
            }

            return Ok(new UserDetails
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                CV = user.CV,
                
                Token = _tokenService.CreateToken(user)
            });
        }
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
        {
            // Obtener el usuario actual
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Actualizar los campos permitidos
            user.UserName = updateProfileDto.UserName ?? user.UserName;
            user.Email = updateProfileDto.Email ?? user.Email;
            user.FirstName = updateProfileDto.FirstName ?? user.FirstName;
            user.LastName = updateProfileDto.LastName ?? user.LastName;
            user.PhoneNumber = updateProfileDto.PhoneNumber ?? user.PhoneNumber;
            user.ProfilePicture = updateProfileDto.ProfilePicture ?? user.ProfilePicture;
            user.CV = updateProfileDto.CV ?? user.CV;

            // Guardar los cambios
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Error al actualizar el perfil.");
            }

            return Ok(new { message = "Perfil actualizado correctamente." });
        }

    }
}