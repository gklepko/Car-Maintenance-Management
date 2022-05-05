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
            var Volvo = new Car("Volvo", "C30", 2011, "Daniel's Car");
            var Camry = new Car("Toyota", "Camry", 2008, "Gleb's Car");
            var Repo = new CarRepository();
            Repo.SaveCarAsync(Volvo).Wait();
            Repo.SaveCarAsync(Camry).Wait();
        }

    }
}