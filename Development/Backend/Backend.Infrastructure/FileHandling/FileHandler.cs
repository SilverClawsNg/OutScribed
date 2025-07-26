using Amazon.S3.Model;
using Amazon.S3;
using Backend.Application.Interfaces;
using Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Backend.Infrastructure.FileHandling
{
    public class FileHandler : IFileHandler
    {

        public void DeleteFile(string fileUrl)
        {
            File.Delete(fileUrl);
        }

        public async Task SaveFileAsync(string base64string, string path)
        {

            byte[] fileBytes = Convert.FromBase64String(base64string);

            await File.WriteAllBytesAsync(path, fileBytes);

        }
    }

}
