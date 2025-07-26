using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Utilities;
using OutScribed.Modules.Analysis.Domain.Exceptions;
using OutScribed.SharedKernel.Exceptions;

namespace OutScribed.Modules.Analysis.Domain.Models
{

    public class Insight : AggregateRoot
    {

        public Ulid CreatorId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public Ulid TaleId { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string? Summary { get; private set; }

        public string? Text { get; private set; }

        public string? Slug { get; private set; }

        public Category Category { get; private set; }

        public string? Photo { get; private set; }

        public bool IsOnline { get; private set; }

        public Country? Country { get; private set; }


        private readonly List<Tag> _tags = [];

        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();


        private readonly List<Share> _shares = [];

        public IReadOnlyCollection<Share> Shares => _shares.AsReadOnly();


        private readonly List<Rating> _rating = [];

        public IReadOnlyCollection<Rating> Ratings => _rating.AsReadOnly();


        private readonly List<Share> _sharers = [];

        public IReadOnlyCollection<Share> Sharers => _sharers.AsReadOnly();


        private readonly List<Follow> _followers = [];

        public IReadOnlyCollection<Follow> Follows => _followers.AsReadOnly();


        private readonly List<Comment> _comments = [];

        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();


        private readonly List<Flag> _flags = [];

        public IReadOnlyCollection<Flag> Flags => _flags.AsReadOnly();


        private readonly List<Addendum> _addendums = [];

        public IReadOnlyCollection<Addendum> Addendums => _addendums.AsReadOnly();

        private Insight() { }

        private Insight(Ulid creatorId, Ulid insightId, string title, Category category)
        {
            CreatorId = creatorId;
            CreatedAt = DateTime.UtcNow;
            TaleId = insightId;
            Title = title;
            Category = category;
            IsOnline = false;
            _addendums = [];
        }

        public static Insight Create(Ulid creatorId, Ulid insightId, string title, Category category)
        {

            var invalidFields = Validator.GetInvalidFields(
                 [
                      new("Creator ID", creatorId),
                      new("Insight ID", insightId),
                      new("Title", title, minLength:3, maxLength:128),
                      new("Category", category)
                 ]
             );
            
            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Insight(creatorId, insightId, title, category);

        }

        public void UpdateBasic(string title, Category category)
        {

            if (IsOnline == true)
                throw new InsightUpdatesDisabledException(Id, "Basics");

            var invalidFields = Validator.GetInvalidFields(
               [
                    new("Insight Title", title, minLength: 3, maxLength: 128),
                      new("Category", category)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Title = title;

            Category = (Category)category;

            //Raise event

            //AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{title}",
            //   ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Basic));

        }

        public void UpdateSummary(string summary)
        {

            if (IsOnline == true)
                throw new InsightUpdatesDisabledException(Id, "Summary");

            var invalidFields = Validator.GetInvalidFields(
              [
                   new("Insight Summary", summary, minLength: 3, maxLength: 512)
              ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Summary = summary;

            //Raise event

            //AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
            //   ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Summary));

        }

        public void UpdateText(string text)
        {

            if (IsOnline == true)
                throw new InsightUpdatesDisabledException(Id, "Text");

            var invalidFields = Validator.GetInvalidFields(
               [
                    new("Insight Text", text, minLength: 3, maxLength: 65535)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Text = HtmlContentProcessor.Clean(text);

            //Raise event

            //AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
            //   ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Details));

        }

        public void UpdatePhoto(string photo)
        {
            if (IsOnline == true)
                throw new InsightUpdatesDisabledException(Id, "Photo URL");

            var invalidFields = Validator.GetInvalidFields(
               [
                    new("Photo URL", photo, minLength: 3, maxLength: 60)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Photo = photo;

            //Raise event

            //AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
            // ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Photo));

        }

        public void UpdateCountry(Country country)
        {

            if (IsOnline == true)
                throw new InsightUpdatesDisabledException(Id, "Country");

            var invalidFields = Validator.GetInvalidFields(
               [
                    new("Country", country)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Country = country;

            //Raise event

            //AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
            // ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Country));

        }

        public Ulid AddAddendum(string text)
        {
       
            var addendum = Addendum.Create(text);

            _addendums.Add(addendum);

            return addendum.Id;

        }

        public void Publish(string username)
        {

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));

            Slug = UrlSlugGenerator.Generate(CreatedAt, username, Title);

            IsOnline = true;

            //Raise event

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

        public void AddFollow(Ulid id, Ulid followerId)
        {

            if (_followers.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Follow");

            _followers.Add(Follow.Create(id, followerId));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

        public void RemoveFollow(Ulid id)
        {

            var follower = _followers.FirstOrDefault(c => c.Id == id)
                ?? throw new DoesNotExistException(id, "Insight Follow");

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

        public void AddShare(Ulid id, Ulid sharerId, ContactType type, string handle)
        {

            if (_shares.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Share");

            _shares.Add(Share.Create(id, sharerId, type, handle));

            // Raise InsightCommentAddedEvent { InsightId, CommentId, UserId, Text, CreatedAt }
        }

    }
}
