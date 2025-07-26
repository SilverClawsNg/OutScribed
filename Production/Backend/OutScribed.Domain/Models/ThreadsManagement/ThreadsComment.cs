using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.Common;

namespace OutScribed.Domain.Models.ThreadsManagement
{
    public class ThreadsComment : Entity
    {

        public Guid CommentatorId { get; private set; }

        public Guid ThreadsId { get; private set; }

        public Guid? ParentId { get; private set; }


        public DateTime Date { get; private set; }

        public Label Details { get; private set; }


        private readonly List<ThreadsComment> _Responses = [];

        public IReadOnlyList<ThreadsComment> Responses => [.. _Responses];


        private readonly List<ThreadsCommentRating> _Ratings = [];

        public IReadOnlyList<ThreadsCommentRating> Ratings => [.. _Ratings];


        private readonly List<ThreadsCommentFlag> _Flags = [];

        public IReadOnlyList<ThreadsCommentFlag> Flags => [.. _Flags];


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ThreadsComment() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private ThreadsComment(Guid commentatorId, Label details)
        : base(Guid.NewGuid())
        {
            CommentatorId = commentatorId;
            Details = details;
            _Ratings = [];
            Date = DateTime.UtcNow;
        }

        public static Result<ThreadsComment> Create(Guid commentatorId, string details)
        {

            var detailsResult = Label.Create(details, "Threads Comment Details", 1024);

            if (detailsResult.IsFailure)
                return detailsResult.Error;

            return new ThreadsComment(commentatorId, detailsResult.Value);

        }

        public Result<ThreadsComment> SaveComment(Guid commentatorId, string details)
        {

            var commentResult = Create(commentatorId, details);

            if (commentResult.IsFailure)
                return commentResult.Error;

            _Responses.Add(commentResult.Value);

            return commentResult.Value;

        }

        public Result<int> SaveRating(RateTypes? type, Guid raterId)
        {

            var rateResult = ThreadsCommentRating.Create(raterId, type);

            if (rateResult.IsFailure)
                return rateResult.Error;

            _Ratings.Add(rateResult.Value);

            return _Ratings.Count(c => c.Type == type);

        }

        public Result<int> SaveFlag(FlagTypes? type, Guid flaggerId)
        {

            var flagResult = ThreadsCommentFlag.Create(flaggerId, type);

            if (flagResult.IsFailure)
                return flagResult.Error;

            _Flags.Add(flagResult.Value);

            return _Flags.Count;

        }

    }
}
