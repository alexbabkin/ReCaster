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

        public static DependencyResolver Instance => _instance ?? (_instance = new DependencyResolver());

        public static T Get<T>()
        {
            return Instance._kernel.Get<T>();
        }

        public static void Release(object instance)
        {
            Instance._kernel.Release(instance);
        }

        private DependencyResolver():this(new StandardKernel())
        {
            _kernel.Bind<IConfigManager>().To<ConfigManager>()
                .InSingletonScope();

            _kernel.Bind<IWcfService>().To<WcfService>()
                .InSingletonScope();

            _kernel.Bind<IEndpoint>().To<RecasterEndpoint>()
               .InTransientScope();

            _kernel.Bind<IReceiver>().To<MulticastReceiveManager>()
                .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulticastCatcher)
                .InTransientScope();

            _kernel.Bind<ISender>().To<TcpSender>()
                .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulticastCatcher)
                .InTransientScope();

            _kernel.Bind<IReceiver>().To<TcpReceiver>()
                .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulitcastSender)
                .InTransientScope();

            _kernel.Bind<ISender>().To<MulticastSender>()
                 .When(r => _kernel.Get<IConfigManager>().AppType == EndpointType.MulitcastSender)
                .InTransientScope();
        }

        private DependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
                
    }
}
