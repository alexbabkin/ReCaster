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
            IEndpoint  endpoint = DependencyResolver.Get<IEndpoint>();
            endpoint.Start();
            Console.ReadLine();
        }
    }
}
