using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests
    {

        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=david_tumpowsky_hair_salon_tests;";
        }

        [TestMethod]
        public void SearchClient()
        {
            Client result = Client.Find(3);
            string clientName = result.GetClientName();

            Assert.AreEqual("clientOne", finalResult);
        }
    }
}
