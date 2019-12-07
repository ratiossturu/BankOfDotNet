using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankOfDotNet.IdentitySvr
{
    public class Config
    {
        public static List<TestUser> GetUser()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="Manish",
                    Password="password"
                },
                new TestUser
                {
                    SubjectId="2",
                    Username="Bob",
                    Password="password"
                }


            };
        }



        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("bankOfDotnetApi","Customer Api for bankOfDotnet")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client{
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                      new Secret("secret".Sha256())
                    },
                     AllowedScopes={ "bankOfDotnetApi" }
                },

                new Client
                {
                    ClientId="ro.client",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                     ClientSecrets =
                    {
                      new Secret("secret".Sha256())
                    },
                    AllowedScopes={ "bankOfDotnetApi" }
                }

            };
        }

    }
}
