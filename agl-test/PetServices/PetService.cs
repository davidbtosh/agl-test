using DataServices;
using DataServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetServices
{
    public class PetService : IPetService
    {
        //public PetService()
        //{
        //    dataService = new DataService();
        //}
        public PetService(IDataService ds)
        {
            dataService = ds;
        }

        private readonly IDataService dataService;

        public 

        Func<PetOwner, List<Pet>> transform = (o) =>
        {
            var petList = o.Pets.Where(c => c.Type == "Cat").ToList();
            petList.ForEach(p => { p.Owner = o.Name; p.OwnerGender = o.Gender; });
            return petList;
        };

        public IList<PetOwner> TransformData()
        {
            string json = dataService.GetDataFromWebService();

            var petOwners = dataService.DeserializePetData(json);

            var sortedCats = petOwners.Where(w => w.Pets != null && w.Pets.Any()).SelectMany(transform).OrderBy(o => o.Name).ToList();

            var genders = sortedCats.GroupBy(s => s.OwnerGender, (key, cats) => new PetOwner { Gender = key, Pets = cats.ToList() }).ToList();

            return genders;
        }
    }
}
