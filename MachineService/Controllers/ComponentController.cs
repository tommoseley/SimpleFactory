using Microsoft.AspNetCore.Mvc;
using MachineService.Models;

namespace MachineService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComponentController : ControllerBase
    {
        [HttpGet(Name = "GetComponents")]
        public IEnumerable<Component> Get()
        {
            List<Component> components = new List<Component>();
            components.Add(new Component { Name = "Iron Block" });
            components.Add(new Component { Name = "Steel Block" });
            components.Add(new Component { Name = "Steel Plate" });
            components.Add(new Component { Name = "Steel Sheet" });

            return components;
        }
    }
}
