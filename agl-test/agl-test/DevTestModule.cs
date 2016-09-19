using DataServices;
using Ninject.Modules;
using PetServices;

namespace agl_test
{
    public class DevTestModule : NinjectModule
    {

        public override void Load()
        {
            Bind<IDataService>().To<DataService>();
            Bind<IPetService>().ToSelf().InSingletonScope();
        }

    }
}
