namespace OutScribed.Client.Services.Interfaces
{
    public interface IApiGetServices<T>
    {
        Task<Result<T>> GetAsync(string url, CancellationToken cancellationToken);

    }
}
