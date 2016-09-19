using DataServices.Models;
using System.Collections.Generic;

namespace PetServices
{
    public interface IPetService
    {        
        IList<PetOwner> TransformData();
    }
}
