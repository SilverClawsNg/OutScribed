using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.TalesManagement.Entities
{
    public class TaleFlag : Entity
    {
        public Guid FlaggerId { get; private set; }

        public Guid TaleId { get; private set; }


        public DateTime Date { get; private set; }


        public FlagTypes Type { get; private set; }

        private TaleFlag() : base(Guid.NewGuid()) { }


        private TaleFlag(Guid flaggerId, FlagTypes type)
         : base(Guid.NewGuid())
        {
            FlaggerId = flaggerId;
            Type = type;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a new tale flag
        /// </summary>
        /// <param name="flaggerId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Result<TaleFlag> Create(Guid flaggerId, FlagTypes? type)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                  Title: "Null Flag Type",
                                   Description: "No rate type was found.");

            if (!Enum.IsDefined(typeof(FlagTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Invalid Flag Type",
                                     Description: $"The flag type with title: {type} is invalid.");


            return new TaleFlag(flaggerId, (FlagTypes)type);

        }
    }
}
