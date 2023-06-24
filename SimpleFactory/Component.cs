using System;
namespace SimpleFactory
{
	public class Component : IComparable<Component>
	{
		public Component()
		{
			this.Name = string.Empty;
		}
		public string Name { get; set; }
        public Dictionary<Component, int> Recipe = new ();
        public bool CanMake(ComponentCollection inventory)
        {
            foreach (Component key in Recipe.Keys)
            {
                if (inventory.ItemCount(key) < Recipe[key])
                {
                    return false;
                }

            }
            return true;
        }
        public Component Make (ComponentCollection inventory)
        {
            if (CanMake(inventory))
            {
                foreach (Component key in Recipe.Keys)
                {
                    inventory.Remove(key, Recipe[key]);
                }
                return new Component() { Name = this.Name };
            }
            return null;
        }

        int IComparable<Component>.CompareTo(Component? other)
        {
            if (other == null) return -1;
            return Name.CompareTo(other.Name);
        }
    }
}

