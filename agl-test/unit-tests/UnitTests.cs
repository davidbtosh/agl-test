using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataServices.Models;
using PetServices;
using Moq;
using DataServices;
using System.Linq;

namespace unit_tests
{
    [TestClass]
    public class UnitTests
    {

        private string json = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]},{'name':'Jennifer','gender':'Female','age':18,'pets':[{'name':'Garfield','type':'Cat'}]},{'name':'Steve','gender':'Male','age':45,'pets':null},{'name':'Fred','gender':'Male','age':40,'pets':[{'name':'Tom','type':'Cat'},{'name':'Max','type':'Cat'},{'name':'Sam','type':'Dog'},{'name':'Jim','type':'Cat'}]},{'name':'Samantha','gender':'Female','age':40,'pets':[{'name':'Tabby','type':'Cat'}]},{'name':'Alice','gender':'Female','age':64,'pets':[{'name':'Simba','type':'Cat'},{'name':'Nemo','type':'Fish'}]}]";


        private List<PetOwner> petOwners = new List<PetOwner>
        {
            new PetOwner
            {
                Name = "Zoe", Age = 5,
                Gender = "Female",
                Pets =  new List<Pet> { new Pet { Name = "Kobe", Type = "Cat" },
                                        new Pet { Name = "Chloe", Type = "Cat" },
                                        new Pet { Name = "Spud", Type = "Dog" } }
            },
            new PetOwner
            {
                Name = "David", Age = 47,
                Gender = "Male",
                Pets =  new List<Pet> { new Pet { Name = "Kobe", Type = "Cat" },
                                        new Pet { Name = "Bill", Type = "Dog" },
                                        new Pet { Name = "Frank", Type = "Cat" } }
            },
             new PetOwner
            {
                Name = "Allan", Age = 60,
                Gender = "Male"
            },
            new PetOwner
            {
                Name = "Greg", Age = 60,
                Gender = "Male",
                Pets =  new List<Pet> { new Pet { Name = "Kobe", Type = "Cat" },
                                        new Pet { Name = "Lisa", Type = "Dog" },
                                        new Pet { Name = "Kelli", Type = "Cat" },
                                        new Pet { Name = "Donna", Type = "Cat" } }
            }
        };

        [TestMethod]
        public void DeserializePetDataTest()
        {            
            var dataService = new DataService();
            var testOwners = dataService.DeserializePetData(json);

            Assert.IsNotNull(petOwners);
            Assert.AreEqual(6, testOwners.Count);

            Assert.IsNotNull(testOwners[0].Pets);
            Assert.AreEqual(2, testOwners[0].Pets.Count);

            Assert.IsNull(testOwners[2].Pets);

        }

        [TestMethod]
        public void TransformDataTest()
        {
            var mock = new Mock<IDataService>();
            mock.Setup(x => x.DeserializePetData(It.IsAny<string>())).Returns(petOwners);
            PetService ps = new PetService(mock.Object);

            var genders = ps.TransformData();

            //assert from petOwners above
            //1 - female owner - 2 cats (Chloe, Kobe)
            //                              ====
            //                              2 cats
            //                              ====

            //3 male owners =>  David - 2 cats (Kobe, Frank)
            //                              Allan - 0 cats (null pet list )
            //                              Greg - 3 cats (Kobe, Kelli, Donna) 
            //                              ====
            //                              5 cats
            //                              ====

            //Ensure Kobe is included twice (male)
            //once female

            Assert.IsNotNull(genders);

            Assert.AreEqual(2, genders.Count);


            var male = genders.Where(g => g.Gender == "Male").ToList();

            Assert.AreEqual(1, male.Count);

            Assert.AreEqual(5, male[0].Pets.Count);

            Assert.AreEqual(5, male[0].Pets.Count(w => w.Type == "Cat"));

            Assert.AreEqual(2, male[0].Pets.Count(w => w.Name == "Kobe"));

            //sort test
            string prev = string.Empty;
            foreach(var cat in male[0].Pets)
            {
                if(!string.IsNullOrEmpty(prev))
                {
                    Assert.IsTrue(string.Compare(prev, cat.Name) <= 0);                   
                }
                prev = cat.Name;
            }


            var female = genders.Where(g => g.Gender == "Female").ToList();

            Assert.AreEqual(1, female.Count);

            Assert.AreEqual(2, female[0].Pets.Count);

            Assert.AreEqual(2, female[0].Pets.Count(w => w.Type == "Cat"));

            Assert.AreEqual(1, female[0].Pets.Count(w => w.Name == "Kobe"));

            //sort test
            prev = string.Empty;
            foreach (var cat in female[0].Pets)
            {
                if (!string.IsNullOrEmpty(prev))
                {
                    Assert.IsTrue(string.Compare(prev, cat.Name) <= 0);
                }
                prev = cat.Name;
            }

        }

    }
}
