using OutScribed.Modules.Publishing.Domain.Exceptions;
using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Exceptions;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.Modules.Publishing.Domain.Models
{
    public class Tale : AggregateRoot
    {
        public string? Slug { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public Ulid CreatorId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? Text { get; private set; }
        public Category Category { get; private set; }
        public string? Photo { get; private set; }
        public string? Summary { get; private set; }
        public TaleStatus Status { get; private set; }
        public Country? Country { get; private set; }

        private readonly List<Tag> _tags = [];
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        private readonly List<Share> _shares = [];
        public IReadOnlyCollection<Share> Shares => _shares.AsReadOnly();

        private readonly List<Rating> _rating = [];
        public IReadOnlyCollection<Rating> Ratings => _rating.AsReadOnly();

        private readonly List<Follower> _followers = [];
        public IReadOnlyCollection<Follower> Followers => _followers.AsReadOnly();

        private readonly List<Comment> _comments = [];
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

        private readonly List<Flag> _flags = [];
        public IReadOnlyCollection<Flag> Flags => _flags.AsReadOnly();

        private readonly List<TaleHistory> _histories = [];
        public IReadOnlyList<TaleHistory> Histories => [.. _histories];

        private Tale() { }

        private Tale(Ulid taleId, Ulid creatorId, string title, Category category, 
            TaleHistory history) : base(taleId)
        {
            CreatorId = creatorId;
            CreatedAt = DateTime.UtcNow;
            Title = title;
            Status = TaleStatus.Created;
            Category = category;
            _histories = [history];

        }

        public static Tale Create(Ulid creatorId, string title, Category category)
        {

            var invalidFields = Validator.GetInvalidFields(
             [
                  new("Tale Title", title, minLength: 3, maxLength: 128),
                  new("Creator ID", creatorId),
                  new("Category", category)
             ]
           );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            var taleId = Ulid.NewUlid();

            var tale = new Tale(taleId, creatorId, title, category,
                TaleHistory.Create(TaleStatus.Created, creatorId, null));

            return tale;

        }

        public void UpdateBasic(string title, Category category)
        {

            if (Status == TaleStatus.Published)
                throw new TaleUpdatesDisabledException(Id, "Basics");

            var invalidFields = Validator.GetInvalidFields(
               [
                    new("Tale Title", title, minLength: 3, maxLength: 128),
                      new("Category", category)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Title = title;

            Category = (Category)category;

        }

        public void UpdateSummary(string summary)
        {

            if (Status == TaleStatus.Published)
                throw new TaleUpdatesDisabledException(Id, "Summary");

            var invalidFields = Validator.GetInvalidFields(
              [
                   new("Tale Summary", summary, minLength: 3, maxLength: 512)
              ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Summary = summary;

        }

        public void UpdateText(string text)
        {

            if (Status == TaleStatus.Published)
                throw new TaleUpdatesDisabledException(Id, "Text");

            var invalidFields = Validator.GetInvalidFields(
           [
                new("Tale Text", text, minLength: 3, maxLength: 65535)
           ]
         );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Text = HtmlContentProcessor.Clean(text);

        }

        public void UpdatePhoto(string photo)
        {
            if (Status == TaleStatus.Published)
                throw new TaleUpdatesDisabledException(Id, "Photo URL");

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

        }

        public void UpdateCountry(Country country)
        {

            if (Status == TaleStatus.Published)
                throw new TaleUpdatesDisabledException(Id, "Country");

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

        }

        public void UpdateStatus(Ulid adminId, TaleStatus status, string? notes, string? username)
        {

            if (Status == TaleStatus.Published)
                throw new TaleUpdatesDisabledException(Id, "Status");

            var invalidFields = Validator.GetInvalidFields(
               [
                    new("Notes", notes, minLength: 3, maxLength: 2048, isOptional:true),
                    new("Tale Status", status),
                    new("Admin ID", adminId)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            if (status == TaleStatus.Submitted && (Photo == null || Text == null
                || Summary == null))
                throw new IncompleteTaleException(Id);


            if (notes == null && (status == TaleStatus.ReturnedByChecker || status == TaleStatus.ReturnedByEditor || status == TaleStatus.ReturnedByPublisher
                || status == TaleStatus.RejectedByChecker || status == TaleStatus.RejectedByEditor || status == TaleStatus.RejectedByPublisher))
                throw new NotesNotFoundException(Id);

            _histories.Add(TaleHistory.Create(status, adminId, notes));

            Status = status;

            if (status == TaleStatus.Published)
            {

                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));

                Slug = UrlSlugGenerator.Generate(CreatedAt, username, Title);

            }
            else
            {
                if (status == TaleStatus.Submitted)
                {

               
                }
                else
                {

                    string StatusToString =
                         Status == TaleStatus.Created ? "Under Creation"
                         : Status == TaleStatus.Submitted ? "Submitted For Review"
                         : Status == TaleStatus.Checked ? "Passed Legal Vetting"
                         : Status == TaleStatus.Edited ? "Passed Story Relevance Vetting"
                         : Status == TaleStatus.Published ? "Passed Publication Vetting"
                         : Status == TaleStatus.ReturnedByChecker ? "Returned For Review (Legal Vetting)"
                         : Status == TaleStatus.ReturnedByEditor ? "Returned For Review (Story Relevance Vetting)"
                         : Status == TaleStatus.ReturnedByPublisher ? "Returned For Review (Publication Vetting)"
                         : Status == TaleStatus.ResubmittedToChecker ? "Resubmitted (Legal Vetting)"
                         : Status == TaleStatus.ResubmittedToEditor ? "Resubmitted (Story Relevance Vetting)"
                         : Status == TaleStatus.ResubmittedToPublisher ? "Resubmitted (Publication Vetting)"
                         : Status == TaleStatus.RejectedByChecker ? "Rejected (Legal Vetting)"
                         : Status == TaleStatus.RejectedByEditor ? "Rejected (Story Relevance Vetting)"
                         : Status == TaleStatus.RejectedByPublisher ? "Rejected (Publication Vetting)"
                         : "Error";

                }
            }

        }

        public void AddComment(Ulid id, Ulid commentatorId, string text)
        {

            if (_comments.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Comment");

            _comments.Add(Comment.Create(id, commentatorId, text));

        }

        public void AddReply(Ulid id, Ulid commentId, Ulid commentatorId, string text)
        {

            var comment = _comments.FirstOrDefault(c => c.Id == commentId)
                ?? throw new DoesNotExistException(commentId, "Insight Comment");

            _comments.Add(comment.Reply(id, commentatorId, text));

        }

        public void AddFlag(Ulid id, Ulid flaggerId, FlagType type)
        {

            if (_flags.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Flag");

            _flags.Add(Flag.Create(id, flaggerId, type));

        }

        public void AddFollower(Ulid id, Ulid followerId)
        {

            if (_followers.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Follower");

            _followers.Add(Follower.Create(id, followerId));

        }

        public void RemoveFollower(Ulid id)
        {

            var follower = _followers.FirstOrDefault(c => c.Id == id)
                ?? throw new DoesNotExistException(id, "Insight Follower");

            _followers.Remove(follower);

        }

        public void AddRating(Ulid id, Ulid raterId, RatingType type)
        {

            if (_rating.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Rating");

            _rating.Add(Rating.Create(id, raterId, type));

        }

        public void AddTag(Ulid tagId)
        {

            if (_tags.Any(c => c.Id == tagId))
                throw new AlreadyExistsException(tagId, "Insight Tag");

            _tags.Add(Tag.Create(tagId));

        }

        public void AddShare(Ulid id, Ulid sharerId, ContactType type, string handle)
        {

            if (_shares.Any(c => c.Id == id))
                throw new AlreadyExistsException(id, "Insight Share");

            _shares.Add(Share.Create(id, sharerId, type, handle));

        }
    }
}
