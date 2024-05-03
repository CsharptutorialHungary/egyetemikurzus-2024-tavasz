using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Infrastructure
{
    internal class CustumStack : IEnumerable<int>
    {
        private readonly List<int> _collection;

        public CustumStack(int length) 
        {
            _collection = new List<int>(length);
        }

        public void Push(int element)
        {
            _collection.Add(element);
        }

        public int Pop()
        {
            int last = _collection.Count - 1;
            int element = _collection[last];
            _collection.RemoveAt(last);
            return element;
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var item in _collection)
            {
                if (item % 2 == 0)
                    yield return item;
            }
            //return _collection.GetEnumerator();
            //return new StackIterator(this._collection);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
