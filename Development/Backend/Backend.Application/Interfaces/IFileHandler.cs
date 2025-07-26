using Backend.Domain.Exceptions;

namespace Backend.Application.Interfaces
{
    public interface IFileHandler
    {

        /// <summary>
        /// Saves a photo
        /// </summary>
        /// <param name="base64string"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task SaveFileAsync(string base64string, string path);

        /// <summary>
        /// Deletes an existing object
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        void DeleteFile(string fileUrl);

    }

}
