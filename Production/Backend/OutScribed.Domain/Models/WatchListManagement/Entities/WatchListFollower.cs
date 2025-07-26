using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Exceptions;

namespace OutScribed.Domain.Models.WatchListManagement.Entities
{
    public class WatchListFollower : Entity
    {
        public Guid FollowerId { get; private set; }

        public Guid WatchListId { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsActive { get; private set; }

        private WatchListFollower() : base(Guid.NewGuid()) { }

        private WatchListFollower(Guid followerId)
         : base(Guid.NewGuid())
        {
            FollowerId = followerId;
            Date = DateTime.UtcNow;
        }

        public static Result<WatchListFollower> Create(Guid followerId)
        {

            return new WatchListFollower(followerId);

        }

    }

}
