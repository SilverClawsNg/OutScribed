using OutScribed.Client.Enums;
using OutScribed.Client.Responses;

namespace OutScribed.Client.Models
{
    public class ComponentParameters
    {

        public ComponentParameters()
        {
            Date = DateTime.UtcNow;

        }

        public bool HasContents { get; set; } = false;

        public string? ErrorMessage { get; set; }


        public int PageNumber { get; set; }

        public ComponentTypes ComponentType { get; set; }

        public ComponentStates ComponentState { get; set; }

        public string ComponentClass => ComponentState.ToString().ToLower();

        public string Info { get; set; } = default!;

        public DateTime Date { get; set; }

        public Guid Id { get; set; }

        public int Counts { get; set; }

        public bool Success { get; set; } = false;

        public bool Reload { get; set; } = false;

        public bool Append { get; set; } = false;

        public string Url { get; set; } = default!;

        public string? ContactValue { get; set; }

        public string Username { get; set; } = default!;

        public ContactTypes ContactType { get; set; }

        public Guid CommentatorId { get; set; }

        public TaleDraftSummary TaleDraft { get; set; } = default!;

        public ThreadDraftSummary ThreadDraft { get; set; } = default!;

        public AdminSummary Admin { get; set; } = default!;

        public List<TaleHistorySummary> TaleHistory { get; set; } = default!;

        public UserProfileResponse Profile { get; set; } = default!;

        public TaleResponse Tale { get; set; } = default!;

        public TaleCommentsResponse TaleComments { get; set; } = default!;

        public TaleCommentResponse TaleCommentsList { get; set; } = default!;

        public TaleResponsesResponse TaleResponses { get; set; } = default!;

        public List<TaleCommentSummary> TaleRecursiveComments { get; set; } = default!;

        public TaleCommentSummary TaleFocusComment { get; set; } = default!;

        public TaleCommentSummary TaleComment { get; set; } = default!;

        public TaleLinksResponse TaleLinks { get; set; } = default!;

        public TaleThreadsResponse TaleThreads { get; set; } = default!;

        public ThreadResponse Thread { get; set; } = default!;

        public ThreadCommentsResponse ThreadComments { get; set; } = default!;

        public ThreadCommentResponse ThreadCommentsList { get; set; } = default!;

        public ThreadResponsesResponse ThreadResponses { get; set; } = default!;

        public List<ThreadCommentSummary> ThreadRecursiveComments { get; set; } = default!;

        public ThreadCommentSummary ThreadFocusComment { get; set; } = default!;

        public ThreadCommentSummary ThreadComment { get; set; } = default!;

        public WatchListSummary WatchlistSummary { get; set; } = default!;

    }
}
