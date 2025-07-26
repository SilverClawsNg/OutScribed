using Backend.Application.Features.UserManagement.Common;
using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Queries.LoadWriters
{

    public class LoadWritersQueryResponse
    {

        public Countries? Country { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Username { get; set; }

        public List<WriterSummary>? Writers { get; set; }

    }

}