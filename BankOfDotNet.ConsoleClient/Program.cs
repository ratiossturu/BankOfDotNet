using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankOfDotNet.ConsoleClient
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {

            var discoRo = await DiscoveryClient.GetAsync("http://localhost:350");
            if(discoRo.IsError)
            {
                Console.WriteLine(discoRo.Error);
                return;
            }


            var tokenClientRo = new TokenClient(discoRo.TokenEndpoint, "ro.client", "secret");

            var tokentResponseRo = await tokenClientRo.RequestResourceOwnerPasswordAsync
                ("Manish","password", "bankOfDotnetApi");

            if (tokentResponseRo.IsError)
            {
                Console.WriteLine(tokentResponseRo.Error);
                return;
            }

            Console.WriteLine(tokentResponseRo.Json);
            Console.WriteLine("\n\n");




            var disco = await DiscoveryClient.GetAsync("http://localhost:350");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");

            var tokentResponse = await tokenClient.RequestClientCredentialsAsync("bankOfDotnetApi");

            if (tokentResponse.IsError)
            {
                Console.WriteLine(tokentResponse.Error);
                return;
            }

            Console.WriteLine(tokentResponse.Json);
            Console.WriteLine("\n\n");

            var client = new HttpClient();
            client.SetBearerToken(tokentResponse.AccessToken);

            var customerInfo = new StringContent(
               JsonConvert.SerializeObject(
                       new { Id = 10, FirstName = "Manish", LastName = "Narayan" }),
                       Encoding.UTF8, "application/json");

            var createCustomerResponse = await client.PostAsync("http://localhost:1219/api/Default"
                                                         , customerInfo);

            if (!createCustomerResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(createCustomerResponse.StatusCode);
            }

            var getCustomerResponse = await client.GetAsync("http://localhost:1219/api/customers");
            if (!getCustomerResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(getCustomerResponse.StatusCode);
            }
            else
            {
                var content = await getCustomerResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.Read();

        }

    }
}
