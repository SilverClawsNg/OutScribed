using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
namespace OutScribed.Domain.Models.TalesManagement.Entities
{
    public class TaleShare : Entity
    {

        public Guid SharerId { get; private set; }

        public Guid TaleId { get; private set; }


        public DateTime Date { get; private set; }


        public ContactTypes Type { get; private set; }

        private TaleShare() : base(Guid.NewGuid()) { }
    }
}
