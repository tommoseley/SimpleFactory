﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using SimpleFactory.Blueprints;
namespace SimpleFactory.Components
{

    public abstract class Component : IComparable<Component>
    {
        public Component()
        {
            Name = string.Empty;
            Blueprint = null;
        }
        public string Name { get; set; }

        public int CompareTo(Component? other)
        {
            if (other == null)
                return 1;
            return Name == other.Name ? 0 : 1;
        }
        public Blueprint? Blueprint{ get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}

