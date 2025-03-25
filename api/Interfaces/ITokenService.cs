using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        string GenerateToken(Company company);
        ClaimsPrincipal ValidateToken(string jwtToken);
    }
}