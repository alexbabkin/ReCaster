using System.Net;
using Ninject;
using Recaster.Multicast.Receiver;
using Recaster.Multicast.Sender;
using Recaster.Unicast.Sender;
using Recaster.Unicast.Receiver;
using Recaster.Endpoint;
using Recaster.Configuration;
using Recaster.Service;
using Recaster.WCF;
using Recaster.Common;

namespace Recaster.Dependency
{
    public class DependencyResolver
    {
        private readonly IKernel _kernel;
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
            _kernel.Bind<IConfigManager>().To<ConfigManager>()
                .InSingletonScope();

            _kernel.Bind<IWCFService>().To<WCFService>()
                .InSingletonScope();

            _kernel.Bind<IEndpoint>().To<RecasterEndpoint>()
               // .InSingletonScope();
               .InTransientScope();

            _kernel.Bind<IReceiver>().To<MulticastReceiveManager>()
                .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulticastCatcher)
                .InSingletonScope();

            _kernel.Bind<ISender>().To<TcpSender>()
                .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulticastCatcher)
                .InSingletonScope();

            _kernel.Bind<IReceiver>().To<TcpReceiver>()
                .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulitcastSender)
                .InSingletonScope();

            _kernel.Bind<ISender>().To<MulticastSender>()
                 .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulitcastSender)
                .InSingletonScope();
        }

        private DependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
                
    }
}
