using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;

namespace Backend.Domain.Models.ThreadsManagement
{

    public class ThreadsTag : Entity
    {

        public Guid ThreadsId { get; private set; }

        public DateTime Date { get; private set; }

        public Label Title { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ThreadsTag() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private ThreadsTag(Label title)
           : base(Guid.NewGuid())
        {
            Title = title;
            Date = DateTime.UtcNow;
        }

        public static Result<ThreadsTag> Create(string? title)
        {
            var titleResult = Label.Create(title, "Tag Title", 24);

            if (titleResult.IsFailure)
                return titleResult.Error;


            return new ThreadsTag(titleResult.Value);
        }

    }
}
