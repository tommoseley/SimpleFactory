using SimpleFactory.Machines; 
namespace SimpleTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            MachineFactory.CreateMachines();
        }

        [Test]
        public void HaveMachinesBeenCreated()
        {
            Assert.AreEqual(3, MachineFactory.GetMachines().Count);
        }
        [Test]
        public void CanIGetAFoundary()
        {
            Assert.IsNotNull(MachineFactory.GetMachine("Foundary"));
        }
    }
}