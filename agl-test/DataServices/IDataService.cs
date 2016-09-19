using DataServices.Models;
using System.Collections.Generic;

namespace DataServices
{
    public interface IDataService
    {
        string GetDataFromWebService();

        IList<PetOwner> DeserializePetData(string petData);
    }
}
