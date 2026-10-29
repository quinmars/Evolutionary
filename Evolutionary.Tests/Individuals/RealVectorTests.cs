using System;
using System.Collections.Generic;
using System.Globalization;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class RealVectorTests
    {
        [Test]
        public void New_IntCtor_AllValuesZero()
        {
            var vector = new RealVector<double>(5);

            Assert.That(vector.Length, Is.EqualTo(5));
            Assert.That(vector.Count, Is.EqualTo(5));
            Assert.That(vector.ToArray(), Is.All.EqualTo(0.0));
        }

        [Test]
        public void New_ArrayCtor_StoresValues()
        {
            var vector = new RealVector<double>(new[] { 1.5, -2.5 });

            Assert.That(vector.Length, Is.EqualTo(2));
            Assert.That(vector[0], Is.EqualTo(1.5));
            Assert.That(vector[1], Is.EqualTo(-2.5));
        }

        [Test]
        public void ToArray_ReturnsCopy()
        {
            var vector = new RealVector<double>(new[] { 1.5 });

            var array = vector.ToArray();
            array[0] = 42.0;

            Assert.That(vector[0], Is.EqualTo(1.5));
        }

        [Test]
        public void ToString_ShowsValuesWithInvariantCulture()
        {
            var vector = new RealVector<double>(new[] { 1.5, -2.5 });

            Assert.That(vector.ToString(), Is.EqualTo("[1.5, -2.5]"));
        }

        [Test]
        public void Enumeration_YieldsValues()
        {
            var vector = new RealVector<double>(new[] { 1.5, -2.5 });

            var enumerated = new List<double>();
            foreach (var value in vector)
            {
                enumerated.Add(value);
            }

            Assert.That(enumerated, Is.EqualTo(new[] { 1.5, -2.5 }));
        }

        [Test]
        public void IReadOnlyList_Interface()
        {
            IReadOnlyList<double> list = new RealVector<double>(new[] { 1.5, -2.5 });

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[1], Is.EqualTo(-2.5));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var a = new RealVector<double>(new[] { 1.5, -2.5 });
            var b = new RealVector<double>(new[] { 1.5, -2.5 });

            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var a = new RealVector<double>(new[] { 1.5 });
            var b = new RealVector<double>(new[] { -1.5 });

            Assert.That(a == b, Is.False);
            Assert.That(a != b, Is.True);
            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void Equality_NaN_ComparesEqualThroughEquals()
        {
            // The values are compared with the default equality comparer of
            // T. As with double.Equals, NaN values are considered equal -
            // even though the == operator of double would say otherwise.
            var a = new RealVector<double>(new[] { double.NaN });
            var b = new RealVector<double>(new[] { double.NaN });

            Assert.That(a.Equals(b), Is.True);
            Assert.That(a == b, Is.True);
        }

        [Test]
        public void Equals_NullObject_IsFalse()
        {
            var vector = new RealVector<double>(new[] { 1.5 });

            Assert.That(vector.Equals(null), Is.False);
        }
    }
}
