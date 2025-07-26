using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.TalesManagement.Entities
{

    public class TaleComment : Entity
    {

        public Guid CommentatorId { get; private set; }

        public Guid TaleId { get; private set; }

        public Guid? ParentId { get; private set; }


        public DateTime Date { get; private set; }

        public RichText Details { get; private set; }


        private readonly List<TaleComment> _Responses = [];

        public IReadOnlyList<TaleComment> Responses => [.. _Responses];


        private readonly List<TaleCommentRating> _Ratings = [];

        public IReadOnlyList<TaleCommentRating> Ratings => [.. _Ratings];


        private readonly List<TaleCommentFlag> _Flags = [];

        public IReadOnlyList<TaleCommentFlag> Flags => [.. _Flags];


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private TaleComment() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        private TaleComment(Guid commentatorId, RichText details)
          : base(Guid.NewGuid())
        {
            CommentatorId = commentatorId;
            Details = details;
            _Ratings = [];
            Date = DateTime.UtcNow;
        }

        public static Result<TaleComment> Create(Guid commentatorId, string details)
        {

            var detailsResult = RichText.Create(details, "Tale Comment Details", 1024);

            if (detailsResult.IsFailure)
                return detailsResult.Error;

            return new TaleComment(commentatorId, detailsResult.Value);

        }

        public Result<TaleComment> SaveComment(Guid commentatorId, string details)
        {

            var commentResult = Create(commentatorId, details);

            if (commentResult.IsFailure)
                return commentResult.Error;

            _Responses.Add(commentResult.Value);

            return commentResult.Value;

        }

        public Result<int> SaveRating(RateTypes? type, Guid raterId)
        {

        
            var rateResult = TaleCommentRating.Create(raterId, type);

            if (rateResult.IsFailure)
                return rateResult.Error;

            _Ratings.Add(rateResult.Value);

            return _Ratings.Count(c => c.Type == type);

        }

        public Result<int> SaveFlag(FlagTypes? type, Guid flaggerId)
        {

            var flagResult = TaleCommentFlag.Create(flaggerId, type);

            if (flagResult.IsFailure)
                return flagResult.Error;

            _Flags.Add(flagResult.Value);

            return _Flags.Count;

        }

    }

}
