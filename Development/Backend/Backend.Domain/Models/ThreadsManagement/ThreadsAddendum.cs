using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;

namespace Backend.Domain.Models.ThreadsManagement
{
    public class ThreadsAddendum : Entity
    {

        public Guid ThreadsId { get; private set; }

        public DateTime Date { get; private set; }

        public RichText Details { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ThreadsAddendum() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private ThreadsAddendum(RichText details)
         : base(Guid.NewGuid())
        {
            Details = details;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new thread addendum
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static Result<ThreadsAddendum> Create(string details)
        {

            var detailsResult = RichText.Create(details, "Threads Addendum Details", 4096);

            if (detailsResult.IsFailure)
                return detailsResult.Error;

            return new ThreadsAddendum(detailsResult.Value);

        }
    }
}
