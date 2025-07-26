using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;

namespace Backend.Domain.Models.WatchListManagement.Entities
{
    public class LinkedTale : Entity
    {
        public Guid TaleId { get; private set; }

        public Guid WatchListId { get; private set; }

        public DateTime Date { get; private set; }

        private LinkedTale() : base(Guid.NewGuid()) { }

        private LinkedTale(Guid taleId)
        : base(Guid.NewGuid())
        {
            TaleId = taleId;
            Date = DateTime.UtcNow;
        }

        public static Result<LinkedTale> Create(Guid taleId)
        {

            return new LinkedTale(taleId);

        }
    }
}
