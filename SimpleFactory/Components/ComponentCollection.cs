using SimpleFactory.Machines;
using System;
using System.Collections;
using System.Dynamic;
using System.Text;

namespace SimpleFactory.Components
{
    public class ComponentCollection
    {
        public ComponentCollection()
        {
            Name = string.Empty;
        }
        public string Name { get; set; }
        private readonly Dictionary<Component, int> Container = new();

        public int Count => Container.Count;

        public void Add(Component key)
        {
            Add(key, 1);
        }
        public void Add(Component key, int count)
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
        public IEnumerable<Component> Keys()
        {
            return Container.Keys;
        }
        public int GetCount(Component key)
        {
            if (Contains(key))
                return Container[key];
            return 0;
        }
        public bool Has(Component key, int count)
        {
            if (Contains(key))
            {
                return Container[key] >= count;
            }
            return Contains(key);
        }
        public bool Contains(Component key)
        {
            return Container.ContainsKey(key);
        }

        public int ItemCount(Component key)
        {
            if (Contains(key) == false)
                return 0;
            return Container[key];
        }

        public bool Remove(Component key)
        {
            return Remove(key, 1);
        }
        public bool Remove(Component key, int count)
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
        public bool RemoveAll(Component key)
        {
            if (Contains(key) == false)
                return false;
            return Container.Remove(key);
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Inventory:");
            foreach (Component key in Container.Keys)
            {
                sb.AppendLine($"{key}({Container[key]}) ");
      
            }
            return sb.ToString();
        }
        public void ToConsole(int consoleLeft, int consoleTop, ConsoleColor consoleColor)
        {
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;

            int LineNumber = consoleTop;
            Console.SetCursorPosition(consoleLeft, LineNumber++);

            Console.WriteLine("{0}:", Name);
            Console.WriteLine("---------------");
            foreach (string key in Keys())
            {
                Console.SetCursorPosition(consoleLeft, LineNumber++);
                Console.WriteLine(string.Format("{0}: {1}", key, GetCount(key)));
            }
            Console.SetCursorPosition(originalLeft, originalTop);
            Console.ForegroundColor = originalColor;
        }
    }
}

