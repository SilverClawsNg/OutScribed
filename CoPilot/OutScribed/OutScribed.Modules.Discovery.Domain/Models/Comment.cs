using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Exceptions;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.Modules.Discovery.Domain.Models
{
    public class Comment : Entity
    {

        public DateTime CommentedAt { get; private set; }

        public Ulid CommentatorId { get; private set; }

        public string Text { get; private set; } = default!;

        public Ulid? ParentId { get; set; }

        public Ulid WatchlistId { get; private set; } = default!;



        public Comment? Parent { get; private set; }

        public Watchlist Watchlist { get; private set; } = default!;


        private readonly List<Comment> _replies = [];

        public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();


        private Comment() { }

        private Comment(Ulid id, Ulid commentatorId, string text, Ulid? parentId)
            : base(id)
        {
            CommentatorId = commentatorId;
            ParentId = parentId;
            Text = text;
            _replies = [];
            CommentedAt = DateTime.UtcNow;
        }

        public static Comment Create(Ulid id, Ulid commentatorId, string text, Ulid? parentId = null)
        {

            // Perform validation checks
            var invalidFields = Validator.GetInvalidFields(
                [
                      new("Comment ID", id),
                      new("Commentator ID", commentatorId),
                      new("Text", text, minLength:6, maxLength:1024)
                 ]
            );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Comment(id, commentatorId, HtmlContentProcessor.Clean(text), parentId);

        }

        public Comment Reply(Ulid id, Ulid commentatorId, string text)
        {

            var invalidFields = Validator.GetInvalidFields(
               [
                     new("Comment ID", id),
                      new("Commentator ID", commentatorId),
                      new("Text", text, minLength:6, maxLength:1024)
                ]
           );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            if (_replies.Any(r => r.Id == id)) 
                throw new AlreadyExistsException(id, "Watchlist Reply");

            var reply = Create(id, commentatorId, text, Id);

            _replies.Add(reply);

            return reply;

        }

    }

}
