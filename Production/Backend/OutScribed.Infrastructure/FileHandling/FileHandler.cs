using Amazon.S3.Model;
using Amazon.S3;
using OutScribed.Application.Interfaces;
using OutScribed.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Infrastructure.FileHandling
{

    public class FileHandler : IFileHandler
    {

        private static readonly string accessKey = "DO00GYRVF8A2KZCDJPBW";
        private static readonly string secretKey = "8tf2EBn0pOZ6g+wCfvi1Q9yZaLWydo5jnpXdqA+gmZg";
        private static readonly string bucketName = "coversfiles";
        private static readonly string endpointUrl = "https://nyc3.digitaloceanspaces.com";

        public async Task<Result<bool>> SaveFileAsync(string base64string, string fileUrl)
        {
            var s3Client = new AmazonS3Client(accessKey, secretKey, new AmazonS3Config
            {
                ServiceURL = endpointUrl,
                ForcePathStyle = true
            });

            byte[] fileBytes = Convert.FromBase64String(base64string);

            using var stream = new MemoryStream(fileBytes);

            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileUrl,
                InputStream = stream,
                ContentType = "application/octet-stream",
                CannedACL = S3CannedACL.PublicRead
            };

            try
            {
                var response = await s3Client.PutObjectAsync(putRequest);

                return true;
            }
            catch (AmazonS3Exception e)
            {
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                Title: "Write Error",
                                Description: $"Error encountered while writing file: {e.Message}");
            }
            catch (Exception e)
            {
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                 Title: "Server Error",
                                 Description: $"Error encountered on server while writing file: {e.Message}");
            }

        }

        public async Task<Result<bool>> DeleteFileAsync(string fileUrl)
        {

            var s3Client = new AmazonS3Client(accessKey, secretKey, new AmazonS3Config
            {
                ServiceURL = endpointUrl,
                ForcePathStyle = true
            });

            try
            {
                var response = await s3Client.DeleteObjectAsync(bucketName, fileUrl);
                return true;
            }
            catch (AmazonS3Exception e)
            {
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                Title: "Delete Error",
                                Description: $"Error encountered while delete file: {e.Message}");
            }
            catch (Exception e)
            {
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                 Title: "Server Error",
                                 Description: $"Error encountered on server while deleting file: {e.Message}");
            }

        }



    }

}
