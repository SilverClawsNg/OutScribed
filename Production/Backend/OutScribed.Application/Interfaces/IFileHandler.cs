using OutScribed.Domain.Exceptions;

namespace OutScribed.Application.Interfaces
{
    public interface IFileHandler
    {

        /// <summary>
        /// Saves a photo
        /// </summary>
        /// <param name="base64string"></param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        Task<Result<bool>> SaveFileAsync(string base64string, string fileUrl);

        /// <summary>
        /// Deletes an existing object
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        Task<Result<bool>> DeleteFileAsync(string fileUrl);

    }

}
