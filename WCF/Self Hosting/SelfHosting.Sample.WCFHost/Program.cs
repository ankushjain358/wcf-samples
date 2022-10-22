using SelfHosting.Sample.WCFService;
using System;
using System.ServiceModel;

namespace SelfHosting.Sample.WCFHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(WcfEntryPoint)))
            {
                host.Open();

                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}
