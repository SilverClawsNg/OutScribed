using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Exceptions;

namespace OutScribed.Domain.Models.UserManagement.Entities
{
    public class AccountFollower : Entity
    {

        public Guid FollowerId { get; private set; }

        public Guid AccountId { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsActive { get; private set; }

        private AccountFollower() : base(Guid.NewGuid()) { }

        private AccountFollower(Guid followerId)
        : base(Guid.NewGuid())
        {
            FollowerId = followerId;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new account follower
        /// </summary>
        /// <param name="followerId"></param>
        /// <returns></returns>
        public static Result<AccountFollower> Create(Guid followerId)
        {

            return new AccountFollower(followerId);

        }
    }
}
