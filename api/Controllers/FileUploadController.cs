using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.IO;
using api.Dtos;
using api.cloudinary;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        public FileUploadController(IConfiguration configuration)
        {
            var cloudinarySettings = new CloudinarySettings();
            configuration.Bind("CloudinarySettings", cloudinarySettings);

            var account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileDto)
        {
            if (fileDto?.File == null || fileDto.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Obtener la extensión y el tipo MIME del archivo
            var fileExtension = Path.GetExtension(fileDto.File.FileName).ToLower();
            var contentType = fileDto.File.ContentType.ToLower();
            var fileStream = fileDto.File.OpenReadStream();
            var publicId = Guid.NewGuid().ToString(); // Evitar colisiones

            UploadResult uploadResult;

            try
            {
                if (contentType.StartsWith("image/"))  // 📷 Si es imagen
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(fileDto.File.FileName, fileStream),
                        PublicId = publicId,
                        UseFilename = true,
                        UniqueFilename = false
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
                else if (contentType == "application/pdf" || fileExtension == ".pdf") // 📄 Si es PDF
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(fileDto.File.FileName, fileStream),
                        PublicId = publicId
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
                else
                {
                    return BadRequest("Only images and PDFs are allowed.");
                }

                if (uploadResult.Error != null)
                {
                    return StatusCode(500, new { message = "Error uploading file to Cloudinary", details = uploadResult.Error.Message });
                }

                return Ok(new { url = uploadResult.SecureUrl.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }
    }
}
