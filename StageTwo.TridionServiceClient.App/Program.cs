using StageTwo.TridionServiceClient.App.Services;
using StageTwo.TridionServiceClient.App.Tridion.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace StageTwo.TridionServiceClient.App
{
    class Program
    {

        private static string _endPoint = ConfigurationManager.AppSettings["CoreService.EndPoint"] ?? "basicHttp_2013";

        private static NetworkCredential _credentials
        {
            get
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CoreService.Username"]))
                {
                    return new NetworkCredential(ConfigurationManager.AppSettings["CoreService.Username"] ?? "",
                                                       ConfigurationManager.AppSettings["CoreService.Password"] ?? "");
                }

                return System.Net.CredentialCache.DefaultNetworkCredentials;
            }
        }

        private static string _contract
        {
            get
            {
                ClientSection clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");
                string contract = String.Empty;

                for (int i = 0; i < clientSection.Endpoints.Count; i++)
                {
                    if (clientSection.Endpoints[i].Name == _endPoint)
                    {
                        contract = clientSection.Endpoints[i].Contract.ToString();
                        break;
                    }
                }

                return contract;
            }
        }

        private static readonly string CORE_SERVICE_CLIENT = "Tridion.ContentManager.CoreService.Client.ICoreService";
        private static readonly string SESSION_AWARE_CORE_SERVICE_CLIENT = "Tridion.ContentManager.CoreService.Client.ISessionAwareCoreService";

        static void Main(string[] args)
        {

            ITridionClientService service = null;

            if (_contract == CORE_SERVICE_CLIENT)
            {
                service = new TridionClientService<TridionCoreServiceClient>(_endPoint, _credentials);
            }

            if (_contract == SESSION_AWARE_CORE_SERVICE_CLIENT)
            {
                service = new TridionClientService<TridionSessionAwareCoreServiceClient>(_endPoint, _credentials);
            }

            if(service != null)
            {
                ComponentData component = service.Get<ComponentData>("tcm:5-388");
                Console.WriteLine(
                        String.Format("Component Title : {0}", component.Title)
                    );
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to end");
            Console.ReadLine();
        }
    }
}
