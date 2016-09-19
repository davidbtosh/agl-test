using DataServices.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace DataServices
{
    public class DataService : IDataService
    {
        
        public string GetDataFromWebService()
        {
            using (var client = new WebClient())
            {
                string url = ConfigurationManager.AppSettings["petdataurl"];
                var json = client.DownloadString(url);
                
                return json;
            }            
        }

        public IList<PetOwner> DeserializePetData(string petData)
        {
            IList<PetOwner> petOwners = JsonConvert.DeserializeObject<List<PetOwner>>(petData);

            return petOwners;
        }
    }
}
