using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.IdentityAPI
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("gatway_api","gatwat service"),
                new ApiResource("user_api","user service"),
                new ApiResource("contact_api","contact service"),
                new ApiResource("project_api","project service"),
                new ApiResource("recommend_api","recommend service")
            };

        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client{
                    ClientId ="android",
                    ClientSecrets=new List<Secret>{ new Secret("secret".Sha256()) },
                    RefreshTokenExpiration =TokenExpiration.Absolute,//滑动过期
                    AllowOfflineAccess =true,
                    RequireClientSecret =false,
                    AllowedGrantTypes =new List<string>{ "sms_auth_code" },
                    AlwaysIncludeUserClaimsInIdToken =true,
                    AllowedScopes =new List<string>
                    {

                    }
                }
            };
        }


        public static IEnumerable<IdentityResource> GetIdentity()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
