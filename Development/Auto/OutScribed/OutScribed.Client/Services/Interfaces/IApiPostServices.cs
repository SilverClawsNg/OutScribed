namespace OutScribed.Client.Services.Interfaces
{
    public interface IApiPostServices<T,R>
    {
        Task<Result<T>> PostAsync(string url, R FormData, CancellationToken cancellationToken);

    }
}
