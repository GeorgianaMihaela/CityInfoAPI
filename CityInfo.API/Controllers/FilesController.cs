using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO; 

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? 
                throw new ArgumentNullException(nameof(fileExtensionContentTypeProvider));

        }
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            // Demo code to download a file 
            // Hardcode the path 
            // I placed this file in the project folder (the one with the .csproj:
            // "C:\GEO_PERSONAL\DEV_ACADEMY\CityInfo\CityInfo.API\ASP.NET_Best_Practice_Review.pdf"
            string pathToFile = "ASP.NET_Best_Practice_Review.pdf";

            // check if the file exists 
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            // find the correct content type for our file 
            if (fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream"; 
            }

            // if the file exists, read all bytes 
            byte[] bytes = System.IO.File.ReadAllBytes(pathToFile);

            return File(bytes, contentType, Path.GetFileName(pathToFile));

        }
    }
}
