using System;
using System.IO;
using BlogsDEMO.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Services
{
    public class UploadBase64Service
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContext;

        public UploadBase64Service(IWebHostEnvironment environment, IHttpContextAccessor httpContext)
        {
            _environment = environment;
            _httpContext = httpContext;
        }

        public string UploadImage(string base64Image)
        {
            try
            {
                var contents = Convert.FromBase64String(base64Image);
                var fileName = $"admin_{Guid.NewGuid()}.jpg";
                var filePath = Path.Combine($"{_environment.ContentRootPath}/wwwroot/upload/admin/{fileName}");
                System.IO.File.WriteAllBytes(filePath, contents);
                var uri = $"{_httpContext.HttpContext?.Request.Scheme}://{_httpContext.HttpContext?.Request.Host}";
                var output = $"{uri}/upload/admin/{fileName}";
                return output;
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message);
            }
        }
    }
}