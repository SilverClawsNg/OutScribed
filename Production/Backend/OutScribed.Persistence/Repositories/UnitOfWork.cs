using OutScribed.Persistence.EntityConfigurations;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Domain.Repositories;
using OutScribed.Domain.Abstracts;

namespace OutScribed.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IMediator mediator, OutScribedDbContext dbContext)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public Dictionary<Type, object> repositories = [];

        private bool disposed = false;

        private readonly OutScribedDbContext _dbContext;

        private readonly IMediator _mediator;

        private ITempUserRepository? tempUserRepository;

        private IUserRepository? userRepository;

        private ITaleRepository? taleRepository;

        private IThreadsRepository? threadsRepository;

        private IWatchListRepository? watchListRepository;


        public IGenericRepository<T> RepositoryFactory<T>() where T : class
        {
            if (repositories.ContainsKey(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(_dbContext);

            repositories.Add(typeof(T), repo);

            return repo;
        }

        public IUserRepository UserRepository
        {
            get
            {
                userRepository ??= new UserRepository(_dbContext);

                return userRepository;
            }
        }

        public ITempUserRepository TempUserRepository
        {
            get
            {
                tempUserRepository ??= new TempUserRepository(_dbContext);

                return tempUserRepository;
            }
        }

        public ITaleRepository TaleRepository
        {
            get
            {
                taleRepository ??= new TaleRepository(_dbContext);

                return taleRepository;
            }
        }

        public IWatchListRepository WatchListRepository
        {
            get
            {
                watchListRepository ??= new WatchListRepository(_dbContext);

                return watchListRepository;
            }
        }

        public IThreadsRepository ThreadsRepository
        {
            get
            {
                threadsRepository ??= new ThreadsRepository(_dbContext);

                return threadsRepository;
            }
        }

        public virtual void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public virtual void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public virtual void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync();

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task DispatchDomainEventsAsync()
        {
            var domainEntities = _dbContext.ChangeTracker.Entries<Entity>()
                .Where(c => c.Entity.DomainEvents != null && c.Entity.DomainEvents.Count != 0);

            var domainEvents = domainEntities
                .SelectMany(c => c.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }

}
