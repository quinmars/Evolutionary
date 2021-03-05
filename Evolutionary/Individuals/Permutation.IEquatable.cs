using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolutionary.Individuals
{
    public readonly partial struct Permutation : IEquatable<Permutation>
    {
        public bool Equals(Permutation other)
            => IsCircular == other.IsCircular
            && 
            ( 
                _elements == other._elements
                || 
                (
                    _elements != null
                    && other._elements != null
                    && _elements.SequenceEqual(other._elements)
                )
            );

        public static bool operator ==(Permutation lhs, Permutation rhs)
            => lhs.Equals(rhs);

        public static bool operator !=(Permutation lhs, Permutation rhs)
            => !lhs.Equals(rhs);
        
        public override bool Equals(object obj)
            => obj is Permutation other && this.Equals(other);

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(IsCircular);

            if (_elements != null)
            {
                hash.Add(0);
                foreach (var e in _elements)
                    hash.Add(e);
            }

            return hash.ToHashCode();
        }
    }
}
