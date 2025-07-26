using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.TalesManagement.Entities
{

    public class TaleRating : Entity
    {

        public Guid RaterId { get; private set; }

        public Guid TaleId { get; private set; }


        public DateTime Date { get; private set; }


        public RateTypes Type { get; private set; }

        private TaleRating() : base(Guid.NewGuid()) { }

        private TaleRating(Guid raterId, RateTypes type)
         : base(Guid.NewGuid())
        {
            RaterId = raterId;
            Type = type;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new tale rating
        /// </summary>
        /// <param name="raterId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Result<TaleRating> Create(Guid raterId, RateTypes? type)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                  Title: "Null Rate Type",
                                   Description: "No rate type was found.");

            if (!Enum.IsDefined(typeof(RateTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Invalid Rate Type",
                                     Description: $"The rate type with title: {type} is invalid.");


            return new TaleRating(raterId, (RateTypes)type);

        }
    }
}
