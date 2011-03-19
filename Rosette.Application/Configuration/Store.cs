using System.Collections.Generic;

namespace Rosette.Configuration
{
    public class Store<T> : IList<T>
    {
        private IList<T> inner = new List<T>();
        protected IList<T> Inner { get { return inner; } set { inner = value; } }

        public int IndexOf(T item) { return Inner.IndexOf(item); }
        public void Insert(int index, T item) { Inner.Insert(index, item); }
        public void RemoveAt(int index) { Inner.RemoveAt(index); }
        public T this[int index] { get { return Inner[index]; } set { Inner[index] = value; } }
        public void Add(T item) { Inner.Add(item); }
        public void Clear() { Inner.Clear(); }
        public bool Contains(T item) { return Inner.Contains(item); }
        public void CopyTo(T[] array, int arrayIndex) { Inner.CopyTo(array, arrayIndex); }
        public int Count { get { return Inner.Count; } }
        public bool IsReadOnly { get { return Inner.IsReadOnly; } }
        public bool Remove(T item) { return Inner.Remove(item); }
        public IEnumerator<T> GetEnumerator() { return Inner.GetEnumerator(); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return ((System.Collections.IEnumerable)Inner).GetEnumerator(); }
    }
}
