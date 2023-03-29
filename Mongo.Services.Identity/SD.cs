using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mongo.Services.Identity
{
    public class SD
    {
        public const string Admin = "Admin"; 
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> { 
                new ApiScope("mongo", "Mongo Server"),
                new ApiScope(name: "read", displayName: "read data"),
                new ApiScope(name: "write", displayName: "write data"),
                new ApiScope(name: "delete", displayName: "delete data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes ={"read", "write", "profile"}
                },
                new Client
                {
                    ClientId="mongo",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    
                    RedirectUris = { "https://localhost:7290/signin-oidc" },
                    PostLogoutRedirectUris={ "https://localhost:7290/signout-callback-oidc" },
                    AllowedScopes = new List<String>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mongo"
                    }
                }
            };
    }
}
