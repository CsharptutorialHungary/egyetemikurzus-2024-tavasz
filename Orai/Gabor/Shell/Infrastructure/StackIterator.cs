
using System.Collections;

namespace Shell.Infrastructure
{
    public class StackIterator : IEnumerator<int>
    {
        private readonly List<int> _list;
        private int index = -1;

        public int Current { get; set; }
        object IEnumerator.Current { get; }

        public StackIterator(List<int> list) 
        {
            _list = list;
        }

        public void Dispose()
        {
            //Intentionally empty
        }

        public bool MoveNext()
        {
            index++;
            if (index >= _list.Count)
            {
                return false;
            }
            Current = _list[index];
            return true;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}