using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Domain.Models.ThreadsManagement
{
    public class ThreadsRating : Entity
    {
        public Guid RaterId { get; private set; }

        public Guid ThreadsId { get; private set; }


        public DateTime Date { get; private set; }


        public RateTypes Type { get; private set; }

        private ThreadsRating() : base(Guid.NewGuid()) { }

        private ThreadsRating(Guid raterId, RateTypes type)
         : base(Guid.NewGuid())
        {
            RaterId = raterId;
            Type = type;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new thread rate
        /// </summary>
        /// <param name="raterId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Result<ThreadsRating> Create(Guid raterId, RateTypes? type)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                  Title: "Null Rate Type",
                                   Description: "No rate type was found.");

            if (!Enum.IsDefined(typeof(RateTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Invalid Rate Type",
                                     Description: $"The rate type with title: {type} is invalid.");


            return new ThreadsRating(raterId, (RateTypes)type);

        }
    }
}
