using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Exceptions;

namespace OutScribed.Domain.Models.ThreadsManagement
{
    public class ThreadsFollower : Entity
    {

        public Guid FollowerId { get; private set; }

        public Guid ThreadsId { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsActive { get; private set; }

        private ThreadsFollower() : base(Guid.NewGuid()) { }

        private ThreadsFollower(Guid followerId)
       : base(Guid.NewGuid())
        {
            FollowerId = followerId;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new post follower
        /// </summary>
        /// <param name="followerId"></param>
        /// <returns></returns>
        public static Result<ThreadsFollower> Create(Guid followerId)
        {

            return new ThreadsFollower(followerId);

        }

    }

}
