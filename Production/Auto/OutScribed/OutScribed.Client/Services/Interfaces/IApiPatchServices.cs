namespace OutScribed.Client.Services.Interfaces
{
    public interface IApiPatchServices<T,R>
    {
        Task<Result<T>> PatchAsync(string url, R FormData, CancellationToken cancellationToken);

    }
}
