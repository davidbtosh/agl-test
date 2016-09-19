using DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit_tests
{
   
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void GetPetOwnersFromWebServiceTest()
        {
            DataService ds = new DataService();
            var data = ds.GetDataFromWebService();

            Assert.IsFalse(String.IsNullOrEmpty(data));

        }
    }
}
