using System;
using System.Collections;
using System.Dynamic;
using System.Text;

namespace SimpleFactory
{
    public class ThingCollection
    {
        public ThingCollection()
        {
            Name = string.Empty;
        }
        public string Name { get; set; }
        private readonly Dictionary<Thing, int> Container = new();

        public int Count => Container.Count;

        public void Add(Thing key)
        {
            Add(key, 1);
        }
        public void Add(Thing key, int count)
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
        public IEnumerable<Thing> Keys()
        {
            return Container.Keys;
        }
        public int GetCount(Thing key)
        {
            if (Contains(key))
                return Container[key];
            return 0;
        }
        public bool Has(Thing key, int count)
        {
            if (Contains(key))
            {
                return Container[key] >= count;
            }
            return Contains(key);
        }
        public bool Contains(Thing key)
        {
            return Container.ContainsKey(key);
        }

        public int ItemCount(Thing key)
        {
            if (Contains(key) == false)
                return 0;
            return Container[key];
        }

        public bool Remove(Thing key)
        {
            return Remove(key, 1);
        }
        public bool Remove(Thing key, int count)
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
        public bool RemoveAll(Thing key)
        {
            if (Contains(key) == false)
                return false;
            return Container.Remove(key);
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Inventory:");
            foreach (Thing key in Container.Keys)
            {
                sb.AppendLine($"{key}({Container[key]}) ");

            }
            return sb.ToString();
        }

    }
}

