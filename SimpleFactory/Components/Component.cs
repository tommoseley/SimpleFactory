using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using SimpleFactory.Blueprints;
using System.Text.Json.Serialization;

namespace SimpleFactory.Components
{

    public class Component : IComparable<Component>
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
        [JsonIgnore]
        public Blueprint? Blueprint{ get; set; }
        public override string ToString()
        {
            return Name;
        }
        public Blueprint CreateBluePrint()
        {
            Blueprint = new Blueprint();
            Blueprint.Produced = this;
            return Blueprint;
        }
    }
}

