using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
 // Asegúrate de tener esta referencia
using System;
using api.Dtos;
using api.cloudinary;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        // Constructor con inyección de dependencias
        public FileUploadController(IConfiguration configuration)
        {
            // Configuración de Cloudinary desde appsettings.json
            var cloudinarySettings = new CloudinarySettings();
            configuration.Bind("CloudinarySettings", cloudinarySettings);

            var account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        // Endpoint para subir archivos
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileDto)
        {
            // Validar si el archivo está presente
            if (fileDto?.File == null || fileDto.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Configurar los parámetros de carga para Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileDto.File.FileName, fileDto.File.OpenReadStream()),
                PublicId = Guid.NewGuid().ToString() // Usar un ID único para evitar colisiones
            };

            try
            {
                // Subir el archivo a Cloudinary
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                // Verificar si la carga fue exitosa
                if (uploadResult.Error != null)
                {
                    return StatusCode(500, new { message = "Error uploading file to Cloudinary", details = uploadResult.Error.Message });
                }

                // Retornar la URL segura del archivo subido
                return Ok(new { url = uploadResult.SecureUrl.ToString() });
            }
            catch (Exception ex)
            {
                // Manejar errores inesperados
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }
    }
}
