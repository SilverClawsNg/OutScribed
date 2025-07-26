using OutScribed.Modules.Feedback.Domain.Exceptions;
using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Feedback.Domain.Models
{
    public class FeedbackContent : AggregateRoot
    {

        public DateTime CreatedAt { get; private set; }

        public ContentType ContentType { get; private set; } 

        public int FollowCount { get; private set; }

        public int RateCount { get; private set; }

        public int ShareCount { get; private set; }

        public int FlagCount { get; private set; }


        private readonly List<Follow> _follows = [];

        public IReadOnlyCollection<Follow> Follows => _follows.AsReadOnly();


        private readonly List<Rate> _rates = [];

        public IReadOnlyCollection<Rate> Rates => _rates.AsReadOnly();


        private readonly List<Flag> _flags = [];

        public IReadOnlyCollection<Flag> Flags => _flags.AsReadOnly();


        private readonly List<Share> _shares = [];

        public IReadOnlyCollection<Share> Shares => _shares.AsReadOnly();


        protected FeedbackContent() { } 

        private FeedbackContent(Guid contentId, ContentType type)
            : base(contentId)
        {
            ContentType = type;
            FollowCount = 0;
            RateCount = 0;
            ShareCount = 0;
            FlagCount = 0;
            CreatedAt = DateTime.UtcNow;
        }

        public static FeedbackContent Create(Guid contentId, ContentType type)
        {
          
            var invalidFields = Validator.GetInvalidFields(
           [
               new("Content ID", contentId),
                  new("Content Type", type),
             ]
          );

            if (invalidFields.Count != 0)
                throw new InvalidParametersException(invalidFields);


            return new FeedbackContent(contentId, type);
        }

        public bool AddFollow(Guid followerId)
        {

            if (_follows.Any(f => f.FollowerId == followerId))
                throw new FollowAlreadyExistsException(Id, followerId);

            var follow = Follow.Create(followerId);

            _follows.Add(follow);

            FollowCount++;

            return true;
        }

        public bool RemoveFollow(Guid followerId)
        {

            var follow = _follows.Where(c=>c.FollowerId == followerId).FirstOrDefault() 
                ?? throw new FollowNotFoundException(Id, followerId);
            
            _follows.Remove(follow);

            FollowCount--;

            return true;
        }

        public bool AddFlag(Guid flaggerId, FlagReason reason)
        {

            if (_flags.Any(f => f.FlaggerId == flaggerId))
                throw new FlagAlreadyExistsException(Id, flaggerId);

            var flag = Flag.Create(flaggerId, reason);

            _flags.Add(flag);

            FlagCount++;

            return true;
        }

        public bool AddRate(Guid raterId, RateType type)
        {

            if (_rates.Any(f => f.RaterId == raterId))
                throw new RateAlreadyExistsException(Id, raterId);

            var rate = Rate.Create(raterId, type);

            _rates.Add(rate);

            RateCount++;

            return true;
        }

        public bool AddShare(Guid sharerId, ContactType type)
        {

            var share = Share.Create(sharerId, type);

            _shares.Add(share);

            ShareCount++;

            return true;
        }




    }
}
