using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Utilities;

namespace OutScribed.Modules.Commenting.Domain.Models
{
    public class Comment : AggregateRoot
    {

        public DateTime CommentedAt { get; private set; }

        public ContentType Content { get; private set; }

        public Guid CommentatorId { get; private set; }

        public string Text { get; private set; } = string.Empty;

        public Guid? ParentId { get; set; }

        public Comment? Parent { get; private set; }


        private readonly List<Comment> _replies = [];

        public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

        private Comment() { }

        private Comment(Guid contentId, ContentType content, Guid commentatorId, string text)
            : base(contentId)
        {
            CommentatorId = commentatorId;
            Text = text;
            Content = content;
            _replies = [];
            CommentedAt = DateTime.UtcNow;
        }

        public static Comment Create(Guid contentId, ContentType content, 
            Guid commentatorId, string text)
        {

            // Perform validation checks
            var invalidFields = Validator.GetInvalidFields(
                [
                      new("Content ID", contentId),
                      new("Content Type", content),
                      new("Commentator ID", commentatorId),
                      new("Text", text, minLength:6, maxLength:1024)
                 ]
            );
           
            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //raise event

            return new Comment(contentId, content, commentatorId, HtmlContentProcessor.Clean(text));
           
        }

        public Comment Reply(Guid commentatorId, string text)
        {
          
            var reply = Create(Id, Content, commentatorId, text);

            _replies.Add(reply);

            //raise event

            return reply;

        }

    }
}
