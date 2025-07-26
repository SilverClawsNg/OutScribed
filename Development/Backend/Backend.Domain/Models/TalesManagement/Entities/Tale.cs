using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Events;
using Backend.Domain.Exceptions;
using Backend.Domain.Misc;
using Backend.Domain.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Backend.Domain.Models.TalesManagement.Entities;

public class Tale : Entity, IAggregateRoot
{

    public int Views { get; private set; }

    public Label? Url { get; private set; }

    public Label Title { get; private set; }

    public Guid CreatorId { get; private set; }

    public DateTime CreationDate { get; private set; }

    public RichText? Details { get; private set; }

    public Categories Category { get; private set; }

    public Label? PhotoUrl { get; private set; }

    public Label? Summary { get; private set; }

    public TaleStatuses Status { get; private set; }

    public Countries? Country { get; private set; }


    private readonly List<TaleComment> _Comments = [];

    public IReadOnlyList<TaleComment> Comments => [.. _Comments];


    private readonly List<TaleFollower> _Followers = [];

    public IReadOnlyList<TaleFollower> Followers => [.. _Followers];


    private readonly List<TaleShare> _Sharers = [];

    public IReadOnlyList<TaleShare> Sharers => [.. _Sharers];


    private readonly List<TaleRating> _Ratings = [];

    public IReadOnlyList<TaleRating> Ratings => [.. _Ratings];


    private readonly List<TaleFlag> _Flags = [];

    public IReadOnlyList<TaleFlag> Flags => [.. _Flags];


    private readonly List<TaleHistory> _Histories = [];

    public IReadOnlyList<TaleHistory> Histories => [.. _Histories];


    private readonly List<TaleTag> _Tags = [];

    public IReadOnlyList<TaleTag> Tags => [.. _Tags];


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Tale() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    private Tale(Guid taleId, Guid adminId, Label title, Categories category, TaleHistory history)
      : base(taleId)
    {
        CreatorId = adminId;
        CreationDate = DateTime.UtcNow;
        Title = title;
        Status = TaleStatuses.Created;
        Category = category;
        _Histories = [history];

    }

    public static Result<Tale> Create(Guid adminId, string title, Categories? category)
    {

        var historyResult = TaleHistory.Create(TaleStatuses.Created, adminId, null);

        if (historyResult.IsFailure)
            return historyResult.Error;

        var titleResult = Label.Create(title, "Tale Title", 128);

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

        var taleId = Guid.NewGuid();

        var tale = new Tale(taleId, adminId, titleResult.Value, (Categories)category,
            historyResult.Value);

        tale.AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, adminId, $"{title}",
           ActivityTypes.Tale, ActivityConstructorTypes.Create_Tale));

        return tale;

    }

    public Result<bool> UpdateBasic(string title, Categories? category)
    {

        if (Status == TaleStatuses.Published)
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Unallowed Operation",
                                Description: "Published tales can no longer be updated.");

        var titleResult = Label.Create(title, "Tale Title", 128);

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

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{title}",
           ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Basic));

        return true;
    }

    public Result<bool> UpdateSummary(string summary)
    {

        if (Status == TaleStatuses.Published)
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Unallowed Operation",
                                Description: "Published tales can no longer be updated.");

        var summaryResult = Label.Create(summary, "Tale Summary", 256);

        if (summaryResult.IsFailure)
            return summaryResult.Error;

        Summary = summaryResult.Value;

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
           ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Summary));

        return true;
    }

    public Result<bool> UpdateDetails(string details)
    {

        if (Status == TaleStatuses.Published)
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Unallowed Operation",
                                Description: "Published tales can no longer be updated.");

        var detailsResult = RichText.Create(details, "Tale Details", 32768);

        if (detailsResult.IsFailure)
            return detailsResult.Error;

        Details = detailsResult.Value;

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
           ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Details));

        return true;
    }

    public Result<bool> UpdatePhotoUrl(string photo)
    {

        if (Status == TaleStatuses.Published)
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Unallowed Operation",
                                Description: "Published tales can no longer be updated.");

        var photoResult = Label.Create(photo, "Tale PhotoUrl", 60);

        if (photoResult.IsFailure)
            return photoResult.Error;

        PhotoUrl = photoResult.Value;

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
         ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Photo));

        return true;
    }

    public Result<bool> UpdateCountry(Countries? country)
    {

        if (Status == TaleStatuses.Published)
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Unallowed Operation",
                                Description: "Published tales can no longer be updated.");

        if (country == null)
            return new Error(Code: StatusCodes.Status400BadRequest,
                              Title: "Null Country",
                               Description: "No country was found.");

        if (!Enum.IsDefined(typeof(Countries), country))
            return new Error(Code: StatusCodes.Status400BadRequest,
                                Title: "Invalid Country Type",
                                 Description: $"The country with title: {country} is invalid.");

        Country = (Countries)country;

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
         ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Country));

        return true;
    }

    public Result<bool> SaveTags(string tags)
    {

        if (Status == TaleStatuses.Published)
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Unallowed Operation",
                                Description: "Published tales can no longer be updated.");

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
                    var tagResult = TaleTag.Create(result.Trim().Replace(" ", "_"));

                    if (tagResult.IsFailure)
                        return tagResult.Error;

                    _Tags.Add(tagResult.Value);
                }

                i++;
            }

        }

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
       ActivityTypes.Tale, ActivityConstructorTypes.Update_Tale_Tag));

        return true;

    }

    public Result<bool> UpdateStatus(Guid adminId, TaleStatuses? status, string? reasons)
    {

        if (Status == TaleStatuses.Published)
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Unallowed Operation",
                                Description: "Published tales can no longer be updated.");

        if (status == TaleStatuses.Submitted && (PhotoUrl == null || Details == null
            || Summary == null))
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Incomplete Tale",
                                Description: "Tale cannot be submitted without summary, photo, and details.");


        if (reasons == null && (status == TaleStatuses.UnChecked || status == TaleStatuses.UnEdited || status == TaleStatuses.UnPublished
            || status == TaleStatuses.OutChecked || status == TaleStatuses.OutEdited || status == TaleStatuses.OutPublished))
            return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Reasons Required",
                                Description: "Reasons required if rejecting or returning a tale for review.");

        var historyResult = TaleHistory.Create(status, adminId, reasons);

        if (historyResult.IsFailure)
            return historyResult.Error;

        if (status == null)
            return new Error(Code: StatusCodes.Status400BadRequest,
                                Title: "Null Status",
                                 Description: "No status was found.");

        if (!Enum.IsDefined(typeof(TaleStatuses), status))
            return new Error(Code: StatusCodes.Status400BadRequest,
            Title: "Invalid Status",
                           Description: $"The status with title: '{status}' is not valid.");


        _Histories.Add(historyResult.Value);

        Status = (TaleStatuses)status;

        if (status == TaleStatuses.Published)
        {

            StringBuilder s = new();

            s = s.Append(CreationDate.Year + "-");
            s = s.Append(CreationDate.Month + "-");
            s = s.Append(CreationDate.Day + "-");
            s = s.Append(string.Join("-", [.. (Title.Value.ToUpper().RemoveSpecialCharacters()).Split(' ')]));

            var urlResult = Label.Create(s.ToString(), "Tale Url", 144);

            if (urlResult.IsFailure)
                return urlResult.Error;

            Url = urlResult.Value;

            SaveFollow(CreatorId, true);

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{s}*{Title.Value}",
            ActivityTypes.Tale, ActivityConstructorTypes.Tale_Published));
        }
        else
        {
            if (status == TaleStatuses.Submitted)
            {
                AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}",
                ActivityTypes.Tale, ActivityConstructorTypes.Tale_Submitted));
            }
            else
            {

                string StatusToString =
                     Status == TaleStatuses.Created ? "Under Creation"
                     : Status == TaleStatuses.Submitted ? "Submitted For Review"
                     : Status == TaleStatuses.Checked ? "Passed Legal Vetting"
                     : Status == TaleStatuses.Edited ? "Passed Story Relevance Vetting"
                     : Status == TaleStatuses.Published ? "Passed Publication Vetting"
                     : Status == TaleStatuses.UnChecked ? "Returned For Review (Legal Vetting)"
                     : Status == TaleStatuses.UnEdited ? "Returned For Review (Story Relevance Vetting)"
                     : Status == TaleStatuses.UnPublished ? "Returned For Review (Publication Vetting)"
                     : Status == TaleStatuses.ReChecked ? "Resubmitted (Legal Vetting)"
                     : Status == TaleStatuses.ReEdited ? "Resubmitted (Story Relevance Vetting)"
                     : Status == TaleStatuses.RePublished ? "Resubmitted (Publication Vetting)"
                     : Status == TaleStatuses.OutChecked ? "Rejected (Legal Vetting)"
                     : Status == TaleStatuses.OutEdited ? "Rejected (Story Relevance Vetting)"
                     : Status == TaleStatuses.OutPublished ? "Rejected (Publication Vetting)"
                     : "Error";

                AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{Title.Value}*{StatusToString}",
                ActivityTypes.Tale, ActivityConstructorTypes.Tale_Status_Updated));
            }
        }

        return true;
    }

    public Result<int> SaveRating(RateTypes? type, Guid raterId)
    {

        var rateResult = TaleRating.Create(raterId, type);

        if (rateResult.IsFailure)
            return rateResult.Error;

        _Ratings.Add(rateResult.Value);

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, raterId, $"{type.ToString()!.ToLower()}*{Url!.Value}*{Title.Value}",
            ActivityTypes.Tale, ActivityConstructorTypes.Rated_Tale));

        return _Ratings.Count(c => c.Type == type);

    }

    public Result<int> SaveFlag(FlagTypes? type, Guid flaggerId)
    {

        var flagResult = TaleFlag.Create(flaggerId, type);

        if (flagResult.IsFailure)
            return flagResult.Error;

        _Flags.Add(flagResult.Value);

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, flaggerId, $"{Url!.Value}*{Title.Value}*{type.ToString()!.ToLower()}",
          ActivityTypes.Tale, ActivityConstructorTypes.Flagged_Tale));

        return _Flags.Count;

    }

    public Result<Guid> SaveComment(string details, Guid commentatorId, string? username)
    {

        var commentResult = TaleComment.Create(commentatorId, details);

        if (commentResult.IsFailure)
            return commentResult.Error;

        _Comments.Add(commentResult.Value);

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, CreatorId, $"{username}*{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
          ActivityTypes.Tale, ActivityConstructorTypes.CommentedTo_Tale));

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, commentatorId, $"{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
          ActivityTypes.Tale, ActivityConstructorTypes.Commented_Tale));

        return commentResult.Value.Id;

    }

    public Result<Guid> SaveResponse(Guid parentId, string details, Guid responderId, Guid commentatorId, string? username)
    {

        var comment = _Comments.Where(c => c.Id.Equals(parentId)).FirstOrDefault();

        if (comment is null)
            return new Error(
                              Code: StatusCodes.Status400BadRequest,
                              Title: $"Null Comment",
                              Description: $"Comment could not be found."
                              );

        var commentResult = comment.SaveComment(responderId, details);

        _Comments.Add(commentResult.Value);

        var date = DateTime.UtcNow;

        AddDomainEvent(new ActivityUpdatedEvent(date, commentatorId, $"{username}*{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
          ActivityTypes.Tale, ActivityConstructorTypes.RespondedTo_Tale));

        AddDomainEvent(new ActivityUpdatedEvent(date, responderId, $"{commentResult.Value.Id}*{Url!.Value}*{Title.Value}",
          ActivityTypes.Tale, ActivityConstructorTypes.Responded_Tale));

        return commentResult.Value.Id;

    }
   
    public Result<int> SaveFollow(Guid followerId, bool option)
    {

        var followed = _Followers.Where(c => c.FollowerId == followerId).FirstOrDefault();

        if (option == false)
        {

            if(followed is null)
                return new Error(
                               Code: StatusCodes.Status400BadRequest,
                               Title: $"Operation Unallowed",
                               Description: $"User is not currently following tale."
                               );

            _Followers.Remove(followed);

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, followerId, $"{Url!.Value}*{Title.Value}",
                ActivityTypes.Tale, ActivityConstructorTypes.Unfollowing_Tale));

        }
        else
        {

            if (followed is not null)
                return new Error(
                               Code: StatusCodes.Status400BadRequest,
                               Title: $"Operation Unallowed",
                               Description: $"User is already following tale."
                               );

            var followResult = TaleFollower.Create(followerId);

            if (followResult.IsFailure)
                return followResult.Error;

            _Followers.Add(followResult.Value);

            AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, followerId, $"{Url!.Value}*{Title.Value}",
                ActivityTypes.Tale, ActivityConstructorTypes.Following_Tale));

        }

        return _Followers.Count;

    }

    public Result<int> SaveCommentRating(RateTypes? type, Guid raterId, Guid commentId)
    {

        var comment = _Comments.Where(c => c.Id.Equals(commentId)).FirstOrDefault();

        if(comment is null)
            return new Error(
                 Code: StatusCodes.Status400BadRequest,
                 Title: $"Null Comment",
                 Description: $"Comment could not be found."
                 );

        var commentResult = comment.SaveRating(type, raterId);

        if (commentResult.IsFailure)
            return commentResult.Error;

        AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, raterId, $"{type.ToString()!.ToLower()}*{commentId}*{Url!.Value}*{Title.Value}",
            ActivityTypes.Tale, ActivityConstructorTypes.Rated_Comment_Tale));

        return commentResult.Value;
    }

    public Result<int> SaveCommentFlag (FlagTypes? type, Guid flaggerId, Guid commentId)
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
        ActivityTypes.Tale, ActivityConstructorTypes.Flagged_Comment_Tale));

        return commentResult.Value;
    }

    public void UpdateViews()
    {
        Views++;
    }

}
