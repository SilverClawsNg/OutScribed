using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.SharedKernel.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}