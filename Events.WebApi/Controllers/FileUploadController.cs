using Events.Application.Events.Queries.GetEventList;
using Events.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FileUploadController : BaseController
    {
        private static IWebHostEnvironment _webHostEnvironment;
        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("UploadImage")]
        public async Task<string> ImageUpload([FromForm] IFormFile file)
        {
            Guid id = Guid.NewGuid();
            if (file.Length > 0 &&
                file.Length < 5 * 1024 * 1024 &&
                file.ContentType.StartsWith("image/"))
            {
                string extension = Path.GetExtension(file.FileName);

                if(!Directory.Exists(_webHostEnvironment.ContentRootPath + "\\Images\\"))
                {
                    Directory.CreateDirectory(_webHostEnvironment.ContentRootPath + "\\Images\\");
                }

                using (FileStream fs = System.IO.File.Create(_webHostEnvironment.ContentRootPath + "\\Images\\" + id + extension))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                    return id + extension;
                }
            }
            else
            {
                throw new Exception("Upload failed");
            }
        }

        [HttpGet("GetImage/{fileName}")]
        public async Task<ActionResult> GetImage(string fileName)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath + "\\Images\\", fileName);
            var mimeType = GetMimeType(fileName);

            if (!System.IO.File.Exists(path))
            {
                return NotFound("Image not found");
            }
            var imageFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Console.WriteLine(path, mimeType);

            return File(imageFileStream, mimeType);
        }

        private string GetMimeType(string fileName)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out string mimeType))
            {
                mimeType = "application/octet-stream";
            }

            return mimeType;
        }


    }
}
