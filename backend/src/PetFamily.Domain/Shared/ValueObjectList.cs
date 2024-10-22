using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public class ValueObjectList<T> : IReadOnlyList<T>
    {
        public IReadOnlyList<T> Values { get; } = null!;

        public T this[int index] => Values[index];

        public int Count => Values.Count;

        private ValueObjectList()
        {

        }

        public ValueObjectList(IEnumerable<T> list)
        {
            Values = new List<T>(list).AsReadOnly();
        }

        public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();

        public static implicit operator List<T>(ValueObjectList<T> list)
            => list.Values.ToList();

        public static implicit operator ValueObjectList<T>(List<T> list) =>
             new(list);
    }
}
