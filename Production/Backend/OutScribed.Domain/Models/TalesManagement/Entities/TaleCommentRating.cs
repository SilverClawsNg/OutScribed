using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Domain.Models.TalesManagement.Entities
{

    public class TaleCommentRating : Entity
    {

        public Guid RaterId { get; private set; }

        public Guid TaleCommentId { get; private set; }


        public DateTime Date { get; private set; }


        public RateTypes Type { get; private set; }

        private TaleCommentRating() : base(Guid.NewGuid()) { }


        private TaleCommentRating(Guid raterId, RateTypes type)
       : base(Guid.NewGuid())
        {
            RaterId = raterId;
            Type = type;
            Date = DateTime.UtcNow;
        }

        public static Result<TaleCommentRating> Create(Guid raterId, RateTypes? type)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                  Title: "Null Rate Type",
                                   Description: "No rate type was found.");

            if (!Enum.IsDefined(typeof(RateTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Invalid Rate Type",
                                     Description: $"The rate type with title: {type} is invalid.");

            return new TaleCommentRating(raterId, (RateTypes)type);

        }

    }
}
