using Backend.Domain.Abstracts;
using Backend.Domain.Enums;

namespace Backend.Domain.Models.ThreadsManagement
{
    public class ThreadsShare : Entity
    {
        public Guid SharerId { get; private set; }

        public Guid ThreadsId { get; private set; }


        public DateTime Date { get; private set; }


        public ContactTypes Type { get; private set; }

        private ThreadsShare() : base(Guid.NewGuid()) { }
    }
}
