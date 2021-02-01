using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Evolutionary.Individuals
{
    public readonly partial struct Permutation : IReadOnlyList<int>
    {
        public int this[int index] => _elements[index];

        public int Count => _elements.Length;

        public IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>)_elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
