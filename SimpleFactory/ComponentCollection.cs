using System;
using System.Collections;
using System.Text;

namespace SimpleFactory
{
    public class ComponentCollection
    {
        public ComponentCollection()
        {
        }
        private readonly Dictionary<string, int> Container = new();

        public int Count => Container.Count;

        public void Add(string key)
        {
            Add(key, 1);
        }
        public void Add(string key, int count)
        {
            if (Contains(key))
                Container[key] += count;
            else
            {
                Container.Add(key, count);
            }
        }

        public void Clear()
        {
            Container.Clear();
        }

        public bool Has (string key, int count)
        {
            if ( Contains (key) )
            {
                return Container[key] >= count;
            }
            return Contains(key);
        }
        public bool Contains (string key)
        {
            return Container.ContainsKey(key);
        }

        public int ItemCount(string key)
        {
            if (Contains(key) == false)
                return 0;
            return Container[key];
        }

        public bool Remove(string key)
        {
            return Remove(key, 1);
        }
        public bool Remove(string key, int count)
        {
            if (Contains(key))
            {
                Container[key] -= count;
                if (Container[key] <= 0)
                    Container.Remove(key);
                return true;
            }
            return false;
        }
        public bool RemoveAll(string key)
        {
            if (Contains(key) == false)
                return false;
            return Container.Remove(key);
        }
        public override string ToString()
        {
            StringBuilder sb = new();   
            sb.AppendLine("Inventory:");
            foreach (string key in Container.Keys)
            {
                sb.AppendLine($"{key}({Container[key]}) ");
            }
            return sb.ToString();
        }
    }
}

