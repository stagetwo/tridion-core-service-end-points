using StageTwo.TridionServiceClient.App.Tridion.Service;
using System;
using System.ServiceModel;
//ICommunicationObject,
namespace StageTwo.TridionServiceClient.App.Extensions
{
    public static class WcfExtensions
    {
        public static void Using<T>(this T client, Action<T> work)
            where T :  ITridionCoreServiceClient
        {
            try
            {
                work(client);
                client.Close();
            }
            catch
            {
                client.Abort();
                throw;
            }
        }
    }
}
