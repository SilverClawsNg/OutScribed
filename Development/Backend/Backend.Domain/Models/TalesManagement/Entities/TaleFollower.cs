using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;

namespace Backend.Domain.Models.TalesManagement.Entities
{
    public class TaleFollower : Entity
    {

        public Guid FollowerId { get; private set; }

        public Guid TaleId { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsActive { get; private set; }

        private TaleFollower() : base(Guid.NewGuid()) { }


        private TaleFollower(Guid followerId)
          : base(Guid.NewGuid())
        {
            FollowerId = followerId;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new tale follower
        /// </summary>
        /// <param name="followerId"></param>
        /// <returns></returns>
        public static Result<TaleFollower> Create(Guid followerId)
        {

            return new TaleFollower(followerId);

        }
    }
}
