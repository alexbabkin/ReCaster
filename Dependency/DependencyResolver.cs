using System;
using Ninject;
using Recaster.Multicast.Receiver;
using Recaster.MCatcher;
using Recaster.Unicast.Sender;
using Recaster.Unicast.Receiver;
using Recaster.MDistributor;

namespace Recaster.Dependency
{
    public class DependencyResolver
    {
        private IKernel _kernel;
        private static DependencyResolver _instance;

        public static DependencyResolver Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DependencyResolver();
                }
                return _instance;
            }
        }

        public static T Get<T>()
        {
            return DependencyResolver.Instance._kernel.Get<T>();
        }

        public static void Release(object instance)
        {
            DependencyResolver.Instance._kernel.Release(instance);
        }

        private DependencyResolver():this(new StandardKernel())
        {
            _kernel.Bind<IMulticastCatcher>().To<MulticastCatcher>()
                .InSingletonScope();
            _kernel.Bind<IMulticastReceiveManager>().To<MulticastReceiveManager>()
                .InSingletonScope();
            _kernel.Bind<ITcpSender>().To<TcpSender>()
                .InSingletonScope();
            _kernel.Bind<IMulticastDistributor>().To<MulticastDistributor>()
                .InSingletonScope();
            _kernel.Bind<ITcpReceiver>().To<TcpReceiver>()
                .InSingletonScope();      
        }

        private DependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
                
    }
}
