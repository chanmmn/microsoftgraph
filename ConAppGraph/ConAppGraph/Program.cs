using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConAppGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
            Console.Read();
        }

        public static async Task Run()
        {
            var clientId = "Application ID";
            var scopes = new List<string>() { "User.ReadBasic.All" }.ToArray();

            IPublicClientApplication clientApplication = InteractiveAuthenticationProvider.CreateClientApplication(clientId);
            InteractiveAuthenticationProvider authProvider = new InteractiveAuthenticationProvider(clientApplication, scopes);

            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var users = await graphClient.Users
                .Request()
                .Select(e => new {
                    e.DisplayName,
                    e.GivenName,
                    e.PostalCode
                })
                .GetAsync();

            foreach (var user in users)
            {
                Console.WriteLine(user.GivenName);
            }
        }
    }
}
