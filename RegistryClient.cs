using org.kevoree;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Org.Kevoree.Registry.Client
{
    public class RegistryClient
    {
        private string baseUrl;

        public RegistryClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task publishContainerRoot(ContainerRoot containerRoot)
        {
            var saver = new org.kevoree.pmodeling.api.json.JSONModelSerializer();
            string json = saver.serialize(containerRoot);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
                var res = await client.PostAsync("/deploy", httpcontent);
                if (!res.IsSuccessStatusCode)
                {
                    var e = new HttpRequestException();
                    throw e;
                }
            }
        }
    }
}
