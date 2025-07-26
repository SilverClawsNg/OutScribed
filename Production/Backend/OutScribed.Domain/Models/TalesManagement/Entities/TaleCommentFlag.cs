using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Domain.Models.TalesManagement.Entities
{
    public class TaleCommentFlag : Entity
    {
        public Guid FlaggerId { get; private set; }

        public Guid TaleCommentId { get; private set; }


        public DateTime Date { get; private set; }


        public FlagTypes Type { get; private set; }

        private TaleCommentFlag() : base(Guid.NewGuid()) { }

        private TaleCommentFlag(Guid flaggerId, FlagTypes type)
       : base(Guid.NewGuid())
        {
            FlaggerId = flaggerId;
            Type = type;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new post reaction
        /// </summary>
        /// <param name="flaggerId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Result<TaleCommentFlag> Create(Guid flaggerId, FlagTypes? type)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                  Title: "Null Flag Type",
                                   Description: "No flag type was found.");

            if (!Enum.IsDefined(typeof(FlagTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Invalid Flag Type",
                                     Description: $"The flag type with title: {type} is invalid.");


            return new TaleCommentFlag(flaggerId, (FlagTypes)type);

        }
    }
}
