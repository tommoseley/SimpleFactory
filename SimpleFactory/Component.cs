using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;

namespace SimpleFactory
{

    public class Component : IComparable<Component>
    {
        public Component()
        {
            Name = string.Empty;
        }
        public string Name { get; set; }

        public int CompareTo(Component? other)
        {
            if (other == null)
                return 1;
            return Name == other.Name ? 0 : 1;
        }
    }
}

