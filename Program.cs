using System;
using Recaster.Dependency;
using Recaster.MCatcher;
using Recaster.MDistributor;
using System.Linq;


namespace Recaster
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("-mc"))
            {
                var catcher = DependencyResolver.Get<IMulticastCatcher>();
                catcher.Start();
                catcher.Stop();
            }
            else
            {
                var sender = DependencyResolver.Get<IMulticastDistributor>();
                sender.Start();
                sender.Stop();
            }
            Console.ReadLine();
        }
    }
}
