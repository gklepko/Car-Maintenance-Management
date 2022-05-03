using Car_Management;
using CarManagement.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var Volvo = new Car("Volvo", "C30", 2011);
            var Camry = new Car("Toyota", "Camry", 2008);
            var Repo = new Repository();
            Repo.SaveCarsAsync(new[] { Volvo, Camry}).Wait();
        }
    }
}