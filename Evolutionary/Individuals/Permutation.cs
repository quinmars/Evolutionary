using System;
using System.Collections.Generic;
using System.Text;

namespace Evolutionary.Individuals
{
    public readonly partial struct Permutation
    {
        private readonly int[] _elements;
        public bool IsCircular { get; }

        public Permutation(int n)
        {
            var array = new int[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = i;
            }

            _elements = array;
            IsCircular = false;
        }
        
        public Permutation(int n, bool isCircular)
        {
            var array = new int[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = i;
            }

            _elements = array;
            IsCircular = isCircular;
        }
        
        public Permutation(int[] array, bool isCircular)
        {
            _elements = array;
            IsCircular = isCircular;
        }
        
        public int[] ToArray()
        {
            var n = _elements.Length;
            var ret = new int[n];
            _elements.CopyTo(ret.AsSpan());

            return ret;
        }

        public static implicit operator ReadOnlySpan<int>(Permutation permutation)
        {
            return permutation._elements;
        }
    }
}
