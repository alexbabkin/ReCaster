using Ninject;
using Recaster.Client.ViewModels;
using Recaster.Client.SettingsProvider;
using System.Collections.ObjectModel;

namespace Recaster.Client.Dependency
{
    public class DependencyResolver
    {
        private readonly IKernel _kernel;
        private static DependencyResolver _instance;

        public static DependencyResolver Instance => _instance ?? (_instance = new DependencyResolver());

        public static T Get<T>()
        {
            return Instance._kernel.Get<T>();
        }

        public static void Release(object instance)
        {
            Instance._kernel.Release(instance);
        }

        private DependencyResolver() : this(new StandardKernel())
        {
            _kernel.Bind<MainViewModel>().ToSelf()
                .InSingletonScope();
            _kernel.Bind<MulticastSourcesSettingsViewModel>().ToSelf()
                .InSingletonScope();
            _kernel.Bind<UnicastClientSettingsViewModel>().ToSelf()
                .InSingletonScope();
            _kernel.Bind<UnicastServerSettingsViewModel>().ToSelf()
                .InSingletonScope();
            _kernel.Bind<IProvider>().To<TestProvider>()
                .InSingletonScope();
            _kernel.Bind<ReadOnlyCollection<SettingsViewModel>>().ToMethod(x => SettingsViewModel.GetTopLevelSettings())
                .InSingletonScope();
        }

        private DependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

    }
}
