using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Utilities;
using OutScribed.SharedKernel.Exceptions;

namespace OutScribed.Modules.Discovery.Domain.Models
{

    public class Watchlist : AggregateRoot
    {

        public DateTime CreatedAt { get; private set; }

        public Ulid CreatorId { get; private set; }

        public Category Category { get; private set; }

        public Country? Country { get; private set; }

        public string Summary { get; private set; } = string.Empty;

        public Source Source { get; private set; } = default!;

        public bool IsOnline { get; private set; }


        private readonly List<Tag> _tags = [];

        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();


        private readonly List<Rating> _rating = [];

        public IReadOnlyCollection<Rating> Ratings => _rating.AsReadOnly();


        private readonly List<Follower> _followers = [];

        public IReadOnlyCollection<Follower> Followers => _followers.AsReadOnly();


        private readonly List<Comment> _comments = [];

        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();


        private readonly List<Flag> _flags = [];

        public IReadOnlyCollection<Flag> Flags => _flags.AsReadOnly();


        private readonly List<LinkedTale> _linkedTales = [];

        public IReadOnlyCollection<LinkedTale> LinkedTales => _linkedTales.AsReadOnly();

        private Watchlist() { }

        private Watchlist(Guid creatorId, string summary, Source source,
            Category category, Country? country)
        {
            CreatorId = creatorId;
            CreatedAt = DateTime.UtcNow;
            Summary = summary;
            Source = source;
            Category = category;
            Country = country;
            IsOnline = false;
        }

        public static Watchlist Create(Guid creatorId, string summary, string sourceSummary, 
            string sourceUrl, Category category, Country country)
        {
          
            var invalidFields = Validator.GetInvalidFields(
                [
                      new("Creator ID", creatorId),
                      new("Country", country, isOptional: true),
                      new("Summary", summary, minLength:3, maxLength:512),
                      new("Category", category)
                ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Watchlist(creatorId, HtmlContentProcessor.Clean(summary), Source.Create(sourceSummary, sourceUrl), category, country);

        }

        public void Update(string summary, string sourceSummary,
          string sourceUrl, Category category, Country country)
        {

            var invalidFields = Validator.GetInvalidFields(
                [
                      new("Country", country, isOptional: true),
                      new("Summary", summary, minLength:3, maxLength:512),
                      new("Category", category)
                ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Summary = HtmlContentProcessor.Clean(summary);

            Source = Source.Create(sourceSummary, sourceUrl);

            Category = category;

            Country = country;

        }

        public void Publish()
        {
           
            IsOnline = true;

        }

        public void AddComment(Ulid id, Ulid commentatorId, string text)
        {

            if (_comments.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Comment");

            _comments.Add(Comment.Create(id, commentatorId, text));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

        public void AddReply(Ulid id, Ulid commentId, Ulid commentatorId, string text)
        {

            var comment = _comments.FirstOrDefault(c => c.Id == commentId)
                ?? throw new DoesNotExistException(commentId, "Insight Comment");

            _comments.Add(comment.Reply(id, commentatorId, text));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

        public void AddFlag(Ulid id, Ulid flaggerId, FlagType type)
        {

            if (_flags.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Flag");

            _flags.Add(Flag.Create(id, flaggerId, type));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

        public void AddFollower(Ulid id, Ulid followerId)
        {

            if (_followers.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Follower");

            _followers.Add(Follower.Create(id, followerId));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

        public void RemoveFollower(Ulid id)
        {

            var follower = _followers.FirstOrDefault(c => c.Id == id)
                ?? throw new DoesNotExistException(id, "Insight Follower");

            _followers.Remove(follower);

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

        public void AddRating(Ulid id, Ulid raterId, RatingType type)
        {

            if (_rating.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Rating");

            _rating.Add(Rating.Create(id, raterId, type));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

        public void AddTag(Ulid id, Ulid tagId)
        {

            if (_tags.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Tag");

            _tags.Add(Tag.Create(id, tagId));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }
    }
}
