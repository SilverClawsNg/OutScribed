namespace OutScribed.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void RollbackTransaction();

        void CommitTransaction();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        IGenericRepository<T> RepositoryFactory<T>() where T : class;

        ITempUserRepository TempUserRepository { get; }

        IUserRepository UserRepository { get; }

        ITaleRepository TaleRepository { get; }

        IThreadsRepository ThreadsRepository { get; }

        IWatchListRepository WatchListRepository { get; }

    }
}
