using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Events;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Misc;
using OutScribed.Domain.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace OutScribed.Domain.Models.ThreadsManagement
{
    public class Threads : Entity, IAggregateRoot
    {

        public Guid ThreaderId { get; private set; }

        public Guid TaleId { get; private set; }

        public int Views { get; private set; }


        public DateTime Date { get; private set; }

        public Label Title { get; private set; }

        public Label? Summary { get; private set; }

        public RichText? Details { get; private set; }

        public Label Url { get; private set; }

        public Categories Category { get; private set; }

        public Label? PhotoUrl { get; private set; }

        public bool IsOnline { get; private set; }

        public Countries? Country { get; private set; }


        private readonly List<ThreadsComment> _Comments = [];

        public IReadOnlyList<ThreadsComment> Comments => [.. _Comments];


        private readonly List<ThreadsFollower> _Followers = [];

        public IReadOnlyList<ThreadsFollower> Followers => [.. _Followers];


        private readonly List<ThreadsFlag> _Flags = [];

        public IReadOnlyList<ThreadsFlag> Flags => [.. _Flags];


        private readonly List<ThreadsRating> _Ratings = [];

        public IReadOnlyList<ThreadsRating> Ratings => [.. _Ratings];


        private readonly List<ThreadsShare> _Sharers = [];

        public IReadOnlyList<ThreadsShare> Sharers => [.. _Sharers];


        private readonly List<ThreadsAddendum> _Addendums = [];

        public IReadOnlyList<ThreadsAddendum> Addendums => [.. _Addendums];


        private readonly List<ThreadsTag> _Tags = [];

        public IReadOnlyList<ThreadsTag> Tags => [.. _Tags];


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Threads() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        private Threads(Guid threadId, Guid threaderId, DateTime date, Label title,
            Label url, Categories category, Guid taleId)
      : base(threadId)
        {
            ThreaderId = threaderId;
            Date = date;
            Title = title;
            Url = url;
            Category = category;
            TaleId = taleId;
            IsOnline = false;
            _Comments = [];
            _Followers = [];
            _Flags = [];
            _Ratings = [];
            _Sharers = [];
            _Addendums = [];
        }

        public static Result<Threads> Create(Guid threaderId, string title,
        Categories? category, Guid taleId, string taleTitle, string taleUrl)
        {

            var titleResult = Label.Create(title, "Threads Title", 128);

            if (titleResult.IsFailure)
                return titleResult.Error;

            if (category == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Null Category",
                                     Description: "No category was found.");

            if (!Enum.IsDefined(typeof(Categories), category))
                return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Invalid Category",
                               Description: $"The category with title: '{category}' is not valid.");


            var date = DateTime.UtcNow;

            StringBuilder url = new();

            url = url.Append(date.Year + "-");
            url = url.Append(date.Month + "-");
            url = url.Append(date.Day + "-");
            url = url.Append(string.Join("-", [.. (title.ToUpper().RemoveSpecialCharacters()).Split(' ')]));

            var urlResult = Label.Create(url.ToString(), "Threads Url", 144);

            if (urlResult.IsFailure)
                return urlResult.Error;

            var threadId = Guid.NewGuid();

            var thread = new Threads(threadId, threaderId, date, titleResult.Value,
            urlResult.Value, (Categories)category, taleId);

            thread.AddDomainEvent(new ActivityUpdatedEvent(date, threaderId, $"{title}*{taleUrl}*{taleTitle}",
             ActivityTypes.Thread, ActivityConstructorTypes.Create_Thread));

            return thread;
        }

        public Result<bool> UpdateBasic(string title, Categories? category)
        {

            if (IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Published threads can no longer be updated.");

            var titleResult = Label.Create(title, "Thread Title", 128);

            if (titleResult.IsFailure)
                return titleResult.Error;

            if (category == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Null Category",
                                     Description: "No category was found.");

            if (!Enum.IsDefined(typeof(Categories), category))
                return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Invalid Category",
                               Description: $"The category with title: '{category}' is not valid.");


            Title = titleResult.Value;

            Category = (Categories)category;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{title}",
               ActivityTypes.Thread, ActivityConstructorTypes.Update_Thread_Basic));

            return true;
        }

        public Result<bool> UpdateSummary(string summary)
        {

            if (IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Published threads can no longer be updated.");

            var summaryResult = Label.Create(summary, "Thread Summary", 256);

            if (summaryResult.IsFailure)
                return summaryResult.Error;

            Summary = summaryResult.Value;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{Title.Value}",
               ActivityTypes.Thread, ActivityConstructorTypes.Update_Thread_Summary));

            return true;
        }

        public Result<bool> UpdateCountry(Countries? country)
        {

            if (IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Published threads can no longer be updated.");

            if (country == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                  Title: "Null Country",
                                   Description: "No country was found.");

            if (!Enum.IsDefined(typeof(Countries), country))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Invalid Country Type",
                                     Description: $"The country with title: {country} is invalid.");

            Country = (Countries)country;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{Title.Value}",
             ActivityTypes.Thread, ActivityConstructorTypes.Update_Thread_Country));

            return true;
        }

        public Result<bool> UpdateDetails(string details)
        {

            if (IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Published threads can no longer be updated.");

            var detailsResult = RichText.Create(details, "Thread Details", 32768);

            if (detailsResult.IsFailure)
                return detailsResult.Error;

            Details = detailsResult.Value;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{Title.Value}",
               ActivityTypes.Thread, ActivityConstructorTypes.Update_Thread_Details));

            return true;
        }

        public Result<bool> UpdatePhotoUrl(string photo)
        {

            if (IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Published threads can no longer be updated.");

            var photoResult = Label.Create(photo, "Draft PhotoUrl", 60);

            if (photoResult.IsFailure)
                return photoResult.Error;

            PhotoUrl = photoResult.Value;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{Title.Value}",
             ActivityTypes.Thread, ActivityConstructorTypes.Update_Thread_Photo));

            return true;
        }

        public Result<bool> SaveTags(string tags)
        {

            if (IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Published threads can no longer be updated.");

            List<string> results = [.. tags.Split(',')];

            results = [.. results.Distinct()];

            if (results != null)
            {

                _Tags.Clear();

                var i = 0;

                foreach (var result in results)
                {

                    if (i < 5)
                    {
                        var tagResult = ThreadsTag.Create(result.Trim().Replace(" ", "_"));

                        if (tagResult.IsFailure)
                            return tagResult.Error;

                        _Tags.Add(tagResult.Value);
                    }

                    i++;
                }

            }

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{Title.Value}",
           ActivityTypes.Thread, ActivityConstructorTypes.Update_Thread_Tag));

            return true;

        }

        public Result<bool> Publish()
        {
            if (IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Thread has already been published.");

            if (PhotoUrl == null || Details == null || Summary == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Incomplete Thread",
                                    Description: "Thread cannot be submitted without summary, photo, and details.");


            IsOnline = true;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{Url!.Value}*{Title.Value}",
             ActivityTypes.Thread, ActivityConstructorTypes.Thread_Published));


            return true;
        }

        public Result<Guid> AddAddendum(string details)
        {

            if (!IsOnline)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Unallowed Operation",
                                    Description: "Addendums can only be added to published threads.");

            var detailsResult = ThreadsAddendum.Create(details);

            if (detailsResult.IsFailure)
                return detailsResult.Error;

            _Addendums.Add(detailsResult.Value);

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{Url!.Value}*{Title.Value}",
               ActivityTypes.Thread, ActivityConstructorTypes.Add_Thread_Addendum));

            return detailsResult.Value.Id;
        }


        public Result<int> SaveRating(RateTypes? type, Guid raterId)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Null Type",
                                     Description: "No rate type was found.");

            if (!Enum.IsDefined(typeof(RateTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                Title: "Invalid Type",
                               Description: $"The rate type with title: '{type}' is not valid.");


            var rateResult = ThreadsRating.Create(raterId, type);

            if (rateResult.IsFailure)
                return rateResult.Error;

            _Ratings.Add(rateResult.Value);

            var date = DateTime.UtcNow;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, raterId, $"{type.ToString()!.ToLower()}*{Url!.Value}*{Title.Value}",
                ActivityTypes.Thread, ActivityConstructorTypes.Rated_Thread));

            return _Ratings.Count(c => c.Type == type);

        }

        public Result<int> SaveFlag(FlagTypes? type, Guid flaggerId)
        {

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Null Type",
                                     Description: "No flag type was found.");

            if (!Enum.IsDefined(typeof(FlagTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                Title: "Invalid Type",
                               Description: $"The flag type with title: '{type}' is not valid.");


            var flagResult = ThreadsFlag.Create(flaggerId, type);

            if (flagResult.IsFailure)
                return flagResult.Error;

            _Flags.Add(flagResult.Value);

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, flaggerId, $"{Url!.Value}*{Title.Value}*{type.ToString()!.ToLower()}",
              ActivityTypes.Thread, ActivityConstructorTypes.Flagged_Thread));

            return _Flags.Count;

        }


        public Result<Guid> SaveComment(string details, Guid commentatorId, string? username)
        {

            var commentResult = ThreadsComment.Create(commentatorId, details);

            if (commentResult.IsFailure)
                return commentResult.Error;

            _Comments.Add(commentResult.Value);

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, ThreaderId, $"{username}*{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
              ActivityTypes.Thread, ActivityConstructorTypes.CommentedTo_Thread));


            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, commentatorId, $"{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
              ActivityTypes.Thread, ActivityConstructorTypes.Commented_Thread));

            return commentResult.Value.Id;

        }

        public Result<Guid> SaveResponse(Guid parentId, string details, Guid responderId, Guid commentatorId, string? username)
        {

            var comment = _Comments.Where(c => c.Id.Equals(parentId)).FirstOrDefault();

            if (comment is null)
            {

                return new Error(
                               Code: StatusCodes.Status400BadRequest,
                               Title: $"Null Comment",
                               Description: $"Comment could not be found."
                               );
            }

            var commentResult = comment.SaveComment(responderId, details);

            _Comments.Add(commentResult.Value);

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, commentatorId, $"{username}*{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
              ActivityTypes.Thread, ActivityConstructorTypes.RespondedTo_Thread));

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, responderId, $"{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
              ActivityTypes.Thread, ActivityConstructorTypes.Responded_Thread));

            return commentResult.Value.Id;

        }

        public Result<int> SaveFollow(Guid followerId, bool option)
        {

            var followed = _Followers.Where(c => c.FollowerId == followerId).FirstOrDefault();

            if (option == false)
            {

                if (followed is null)
                    return new Error(
                                   Code: StatusCodes.Status400BadRequest,
                                   Title: $"Operation Unallowed",
                                   Description: $"User is not currently following thread."
                                   );


                _Followers.Remove(followed);

                AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, followerId, $"{Url!.Value}*{Title.Value}",
                    ActivityTypes.Account, ActivityConstructorTypes.Unfollowing_Thread));

            }
            else
            {

                if (followed is not null)
                    return new Error(
                                   Code: StatusCodes.Status400BadRequest,
                                   Title: $"Operation Unallowed",
                                   Description: $"User is already following thread."
                                   );

                var followResult = ThreadsFollower.Create(followerId);

                if (followResult.IsFailure)
                    return followResult.Error;

                _Followers.Add(followResult.Value);


                AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, followerId, $"{Url!.Value}*{Title.Value}",
                    ActivityTypes.Account, ActivityConstructorTypes.Following_Thread));

            }

            return _Followers.Count;

        }

        public Result<int> SaveCommentRating(RateTypes? type, Guid raterId, Guid commentId)
        {

            var comment = _Comments.Where(c => c.Id.Equals(commentId)).FirstOrDefault();

            if (comment is null)
                return new Error(
                     Code: StatusCodes.Status400BadRequest,
                     Title: $"Null Comment",
                     Description: $"Comment could not be found."
                     );

            var commentResult = comment.SaveRating(type, raterId);

            if (commentResult.IsFailure)
                return commentResult.Error;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, raterId, $"{type.ToString()!.ToLower()}*{commentId}*{Url!.Value}*{Title.Value}",
        ActivityTypes.Thread, ActivityConstructorTypes.Rated_Comment_Thread));

            return commentResult.Value;
        }

        public Result<int> SaveCommentFlag(FlagTypes? type, Guid flaggerId, Guid commentId)
        {

            var comment = _Comments.Where(c => c.Id.Equals(commentId)).FirstOrDefault();

            if (comment is null)
                return new Error(
                     Code: StatusCodes.Status400BadRequest,
                     Title: $"Null Comment",
                     Description: $"Comment could not be found."
                     );

            var commentResult = comment.SaveFlag(type, flaggerId);

            if (commentResult.IsFailure)
                return commentResult.Error;

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, flaggerId, $"{commentId}*{Url!.Value}*{Title.Value}*{type.ToString()!.ToLower()}",
      ActivityTypes.Thread, ActivityConstructorTypes.Flagged_Comment_Thread));

            return commentResult.Value;

        }

        public void UpdateViews()
        {
            Views++;
        }
    }
}
