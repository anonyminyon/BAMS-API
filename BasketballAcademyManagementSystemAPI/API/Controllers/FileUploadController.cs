using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload/file-use-for-be-test")]
        [DisableRequestSizeLimit]
        //[Authorize]
        public async Task<IActionResult> UploadImage(IFormFile file, string url)
        {
            try
            {
                string fileUrl = await _fileUploadService.UploadFileAsync(file, url);
                return Ok(fileUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = ApiResponseStatusConstant.FailedStatus, message = ex.Message });
            }
        }

        private readonly string UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        [HttpGet("list-use-for-test")]
        public IActionResult GetFileList([FromQuery] string path = "")
        {
            try
            {
                var fullPath = Path.Combine(UploadPath, path);

                if (!Directory.Exists(fullPath))
                    return NotFound(new { message = "Directory not found" });

                var directoryInfo = new DirectoryInfo(fullPath);

                var result = new
                {
                    currentPath = path,
                    directories = directoryInfo.GetDirectories()
                        .Select(d => new
                        {
                            name = d.Name,
                            path = Path.Combine(path, d.Name).Replace("\\", "/"),
                            type = "directory"
                        }),
                    files = directoryInfo.GetFiles()
                        .Select(f => new
                        {
                            name = f.Name,
                            path = Path.Combine(path, f.Name).Replace("\\", "/"),
                            type = "file",
                            size = f.Length,
                            lastModified = f.LastWriteTime
                        })
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("tree-use-for-test")]
        public IActionResult GetFileTree()
        {
            try
            {
                var root = new DirectoryInfo(UploadPath);
                var tree = BuildDirectoryTree(root);
                return Ok(tree);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private object BuildDirectoryTree(DirectoryInfo directory)
        {
            return new
            {
                name = directory.Name,
                path = directory.FullName.Replace(UploadPath, "").TrimStart('\\').Replace("\\", "/"),
                type = "directory",
                children = directory.GetDirectories()
                    .Select(BuildDirectoryTree)
                    .Concat(directory.GetFiles()
                        .Select(f => new
                        {
                            name = f.Name,
                            path = Path.Combine(
                                directory.FullName.Replace(UploadPath, "").TrimStart('\\'),
                                f.Name).Replace("\\", "/"),
                            type = "file",
                            size = f.Length,
                            lastModified = f.LastWriteTime
                        }))
            };
        }

        [HttpPost("delete-file-by-url-for-test")]
        public IActionResult DeleteByUrl([FromQuery] string url)
        {
            try
            {
                _fileUploadService.DeleteFile(url);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
