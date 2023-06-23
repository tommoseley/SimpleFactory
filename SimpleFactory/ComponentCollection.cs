using System;
using System.Collections;
namespace SimpleFactory
{
	public class ComponentCollection : ICollection <Component>
	{
		public ComponentCollection()
		{
		}
        private readonly Dictionary<Component, int> Container = new();

        public int Count => Container.Count;

        public bool IsReadOnly => true;

        public void Add(Component item)
        {
            if (Contains(item))
                Container[item] += 1;
            else
            {
                Container.Add(item, 1);
            }
        }

        public void Clear()
        {
            Container.Clear();
        }

        public bool Contains(Component item)
        {
            return Container.ContainsKey(item);
        }

        public int ItemCount(Component item)
        {
            if (Contains(item) == false)
                return 0;
            return Container[item];
        }

        public void CopyTo(Component[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Component> GetEnumerator()
        {
            return Container.Keys.GetEnumerator();
        }

        public bool Remove(Component item)
        {
            if (Contains(item))
            {
                Container[item] -= 1;
                if (Container[item] == 0)
                    Container.Remove(item);
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Container.GetEnumerator();
        }
    }
}

