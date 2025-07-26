using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.Common;

namespace OutScribed.Domain.Models.TalesManagement.Entities
{
    public class TaleTag : Entity
    {

        public Guid TaleId { get; private set; }

        public DateTime Date { get; private set; }

        public Label Title { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private TaleTag() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private TaleTag(Label title)
           : base(Guid.NewGuid())
        {
            Title = title;
            Date = DateTime.UtcNow;
        }

        public static Result<TaleTag> Create(string? title)
        {
            var titleResult = Label.Create(title, "Tag Title", 24);

            if (titleResult.IsFailure)
                return titleResult.Error;


            return new TaleTag(titleResult.Value);
        }

    }

}
