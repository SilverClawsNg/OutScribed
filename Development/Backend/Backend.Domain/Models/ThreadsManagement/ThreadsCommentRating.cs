using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.ThreadsManagement
{
    public class ThreadsCommentRating : Entity
    {

        public Guid RaterId { get; private set; }

        public Guid ThreadsCommentId { get; private set; }


        public DateTime Date { get; private set; }


        public RateTypes Type { get; private set; }

        private ThreadsCommentRating() : base(Guid.NewGuid()) { }


        private ThreadsCommentRating(Guid raterId, RateTypes type)
       : base(Guid.NewGuid())
        {
            RaterId = raterId;
            Type = type;
            Date = DateTime.UtcNow;
        }

        public static Result<ThreadsCommentRating> Create(Guid raterId, RateTypes? type)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                  Title: "Null Rate Type",
                                   Description: "No rate type was found.");

            if (!Enum.IsDefined(typeof(RateTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Invalid Rate Type",
                                     Description: $"The rate type with title: {type} is invalid.");

            return new ThreadsCommentRating(raterId, (RateTypes)type);

        }

    }
}
