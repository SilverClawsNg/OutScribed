using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace OutScribed.SharedKernel.BaseTypes
{
    public abstract class Enumeration : IComparable
    {
        public int Value { get; protected set; }
        public string Name { get; protected set; } = string.Empty;

        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public override string ToString() => Name;

        public override bool Equals(object? obj) =>
            obj is Enumeration other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public int CompareTo(object? other) => Value.CompareTo(((Enumeration?)other)?.Value);
    }
}
