using OutScribed.SharedKernel.Interfaces.OutScribed.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.SharedKernel.BaseTypes
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
