using SimpleFactory; 
namespace SimpleTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Machine.Create();
        }

        [Test]
        public void HaveMachinesBeenCreated()
        {
            Assert.AreEqual(3, Machine.Get().Count);
        }
        [Test]
        public void CanIGetAFoundary()
        {
            Assert.IsNotNull(Machine.Get("Foundary"));
        }
    }
}