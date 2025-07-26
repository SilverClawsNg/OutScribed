using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.Common;

namespace OutScribed.Domain.Models.TalesManagement.Entities
{
    public class TaleHistory : Entity
    {

        public Guid TaleId { get; private set; }

        public DateTime Date { get; private set; }

        public TaleStatuses Status { get; private set; }

        public Guid AdminId { get; private set; }

        public Label? Reasons { get; private set; }

        private TaleHistory() : base(Guid.NewGuid()) { }

        private TaleHistory(TaleStatuses status, Guid adminId, Label? reasons)
           : base(Guid.NewGuid())
        {
            Status = status;
            AdminId = adminId;
            Date = DateTime.UtcNow;
            Reasons = reasons;
        }

        public static Result<TaleHistory> Create(TaleStatuses? status, Guid adminId,
            string? reasons)
        {
            if (status == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Null Status",
                                     Description: "No status was found.");

            if (!Enum.IsDefined(typeof(TaleStatuses), status))
                return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Invalid Tale Status",
                               Description: $"The status with title: '{status}' is not valid.");

            Result<Label>? reasonsResult = null;

            if (reasons != null)
            {
                reasonsResult = Label.Create(reasons, "Rejection Reasons", 1024);

                if (reasonsResult.IsFailure)
                    return reasonsResult.Error;
            }

            return new TaleHistory((TaleStatuses)status, adminId, reasonsResult?.Value);
        }

    }

}
