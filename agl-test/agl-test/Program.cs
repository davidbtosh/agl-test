using Ninject;
using PetServices;
using System;

namespace agl_test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                IKernel kernel = new StandardKernel(new DevTestModule());                
                IPetService petService = kernel.Get<PetService>();
                
                var genderContainer = petService.TransformData();

                //output answer
                foreach (var item in genderContainer)
                {
                    Console.WriteLine(string.Empty);
                    Console.WriteLine(item.Gender);
                    foreach(var cat in item.Pets)
                    {
                        Console.WriteLine(cat.Name);
                    }
                }

                Console.ReadKey();
            }
            catch(Exception e)
            {
                //log exception
            }
            
        }

       
    }
}
