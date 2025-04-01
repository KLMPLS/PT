using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyApplication.Logic;
namespace TestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void AddData_AddsDataToDataLayer()
        {
            var logicLayer = new LogicLayer();
            var data = "TestData";

            logicLayer.AddData(data);

            Assert.IsTrue(logicLayer.GetData().Contains(data));
        }

        [TestMethod]
        public void GetData_ReturnsAllDataFromDataLayer()
        {
            var logicLayer = new LogicLayer();
            logicLayer.AddData("TestData1");
            logicLayer.AddData("TestData2");

            var result = logicLayer.GetData();
            Assert.AreEqual(2, result.Count);
        }
    }
}
