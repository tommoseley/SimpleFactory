using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFactory.Blueprints;
using SimpleFactory.Components;
namespace SimpleFactory.Machines
{
    public class Machine
    {
        public Machine() 
        {
            Produces = new();
            Hopper = new();
            Name = string.Empty;
        }
        public string Name { get; set; }
        public List<Component> Produces { get; set; }
        public ComponentCollection Hopper { get; set; }  
        public bool CanProduce (Component item)
        {
            if (Produces == null) throw new Exception("Machine has no production list");
            Component target = Produces.Find(x => x.Name == item.Name);
            if (target != null)
            {
                return target.Blueprint.CanMake(Hopper);
            }
            return false;
        }
        public bool Make (Component item)
        {
            if (Produces == null) throw new Exception("Machine has no production list");
            Component target = Produces.Find(x => x.Name == item.Name);
            if (target != null)
            {
                return target.Blueprint.Make(Hopper);
            }
            return false;
        }
    }
}
