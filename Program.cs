using System;
using Recaster.Dependency;
using Recaster.Endpoint;
using System.Linq;


namespace Recaster
{
    class Program
    {
        static void Main(string[] args)
        {
            IEndpoint endpoint = null;
            if (args.Contains("-mc"))
            {
                endpoint = DependencyResolver.Get<MulticastCatcher>();
            }
            else
            {
                endpoint = DependencyResolver.Get<MulticastDistributor>();
            }
            endpoint.Start();
            Console.ReadLine();
        }
    }
}
