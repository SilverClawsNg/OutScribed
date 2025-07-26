using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Events;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.WatchListManagement.Entities
{
    public class WatchList : Entity, IAggregateRoot
    {

        public Categories Category { get; private set; }

        public Countries? Country { get; private set; }

        public Guid AdminId { get; private set; }

        public Label Title { get; private set; }

        public Label Summary { get; private set; }

        public Hyperlink Source { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsOnline { get; private set; }


        private readonly List<LinkedTale> _Tales = [];

        public IReadOnlyList<LinkedTale> Tales => [.. _Tales];


        private readonly List<WatchListFollower> _Followers = [];

        public IReadOnlyList<WatchListFollower> Followers => [.. _Followers];


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private WatchList() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        private WatchList(Guid adminId, DateTime date, Label title, Label summary, Hyperlink source,
         Categories category, Countries? country)
        : base(Guid.NewGuid())
        {
            AdminId = adminId;
            Date = date;
            Title = title;
            Summary = summary;
            Source = source;
            Category = category;
            Country = country;
        }

        public static Result<WatchList> Create(Guid adminId, string title,
        string summary, string sourceUrl, string sourceText, Categories? category,
        Countries? country)
        {

            var titleResult = Label.Create(title, "Wishlist Title", 128);

            if (titleResult.IsFailure)
                return titleResult.Error;

            var summaryResult = Label.Create(summary, "Wishlist Summary", 1024);

            if (summaryResult.IsFailure)
                return summaryResult.Error;

            var sourceResult = Hyperlink.Create(sourceUrl, sourceText);

            if (sourceResult.IsFailure)
                return sourceResult.Error;

            if (category == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Null Category",
                                     Description: "No category was found.");

            if (!Enum.IsDefined(typeof(Categories), category))
                return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Invalid Category",
                               Description: $"The category with title: '{category}' is not valid.");

            Countries? _country = null;

            if(country != null)
            {
                if (!Enum.IsDefined(typeof(Countries), country))
                    return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Invalid Country",
                                   Description: $"The country with title: '{country}' is not valid.");

                _country = (Countries)country;
            }

            var date = DateTime.UtcNow;

            return new WatchList(adminId, date, titleResult.Value, summaryResult.Value,
                sourceResult.Value, (Categories)category, _country);

        }

        public Result<bool> Update(string title, string summary, string sourceUrl,
          string sourceText, Categories? category, Countries? country)
        {

            var titleResult = Label.Create(title, "Wishlist Title", 128);

            if (titleResult.IsFailure)
                return titleResult.Error;

            var summaryResult = Label.Create(summary, "Wishlist Summary", 1024);

            if (summaryResult.IsFailure)
                return summaryResult.Error;

            var sourceResult = Hyperlink.Create(sourceUrl, sourceText);

            if (sourceResult.IsFailure)
                return sourceResult.Error;

            if (category == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                    Title: "Null Category",
                                     Description: "No category was found.");

            if (!Enum.IsDefined(typeof(Categories), category))
                return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Invalid Category",
                               Description: $"The category with title: '{category}' is not valid.");

            if (country != null)
            {
                if (!Enum.IsDefined(typeof(Countries), country))
                    return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Invalid Country",
                                   Description: $"The country with title: '{country}' is not valid.");

                Country = (Countries)country;
            }

            Title = titleResult.Value;

            Summary = summaryResult.Value;

            Source = sourceResult.Value;

            Category = (Categories)category;

            return true;

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
                                   Description: $"User is not currently following watchlist."
                                   );

                _Followers.Remove(followed);

                AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, followerId, $"{Id}*{Title.Value}",
                    ActivityTypes.Account, ActivityConstructorTypes.Unfollowing_Watchlist));

            }
            else
            {

                if (followed is not null)
                    return new Error(
                                   Code: StatusCodes.Status400BadRequest,
                                   Title: $"Operation Unallowed",
                                   Description: $"User is already following watchlist."
                                   );

                var followResult = WatchListFollower.Create(followerId);

                if (followResult.IsFailure)
                    return followResult.Error;

                _Followers.Add(followResult.Value);


                AddDomainEvent(new ActivityUpdatedEvent(DateTime.UtcNow, followerId, $"{Id}*{Title.Value}",
                    ActivityTypes.Account, ActivityConstructorTypes.Following_Watchlist));

            }

            return _Followers.Count;

        }

        public Result<int> SaveLink(Guid taleId)
        {

            var linkResult = LinkedTale.Create(taleId);

            if (linkResult.IsFailure)
                return linkResult.Error;

            _Tales.Add(linkResult.Value);

            return _Tales.Count;
        }

    }
}
