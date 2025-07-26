using OutScribed.Client.Enums;
using OutScribed.Client.Globals;
using OutScribed.Client.Extensions;
using System.Web;

namespace OutScribed.Client.Models
{
    public class TaleCommentSummary
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public Guid TaleId { get; set; }

        public Guid? ParentId { get; set; }

        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public string LikesToString => Likes.ToString().GetCounts();

        public int Hates { get; set; }

        public string HatesToString => Hates.ToString().GetCounts();

        public RateTypes? MyRatings { get; set; }

        public string Details { get; set; } = default!;

        public string DetailsDecoded => HttpUtility.HtmlDecode(Details);

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int ResponsesCount { get; set; }

        public string ResponsesCountToString => ResponsesCount.ToString().GetCounts();

        public int Flags { get; set; }

        public bool HasFlagged { get; set; }

        public Guid CommentatorId { get; set; }

        public string CommentatorUsername { get; set; } = default!;

        public string? CommentatorDisplayPhoto { get; set; }

        public string DateToString => Helpers.GetTimeFromDate(Date);

        public string? MyRatingsToString => MyRatings == RateTypes.Like ? "You already liked this comment"
          : MyRatings == RateTypes.Hate ? "You already hated this comment"
          : null;

        public string? CommentatorDisplayPhotoToString => CommentatorDisplayPhoto == null ? null : string.Format("{0}{1}{2}", Url, "outscribed/users/", CommentatorDisplayPhoto);

        public List<TaleCommentSummary>? Responses { get; set; }

    }
}
