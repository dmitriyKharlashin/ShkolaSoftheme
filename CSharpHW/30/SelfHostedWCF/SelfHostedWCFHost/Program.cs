using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfHostedWCFImplementations;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using SelfHostedWCF;

namespace SelfHostedWCFHost
{
    class Program
    {
        /// <summary>
        /// To check service running: http://localhost:8000/EchoWithGet?value=Hello
        /// </summary>
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            var httpUri = new Uri("http://localhost:8000/");

            //Create ServiceHost
            var host = new WebServiceHost(typeof(Service), httpUri);

            //Add a service endpoint 
            host.AddServiceEndpoint(typeof(IService), new WebHttpBinding(), "");

            ////Enable metadata exchange
            //ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior
            //{
            //    HttpGetEnabled = true
            //};
            //host.Description.Behaviors.Add(serviceMetadataBehavior);

            //Start the Service
            host.Open();
            Console.WriteLine($"Service enabled at {DateTime.Now.ToString()}");
            Console.WriteLine($"Host is running... Press any key to stop");

            Console.ReadKey();
            host.Close();
        }
    }
}
