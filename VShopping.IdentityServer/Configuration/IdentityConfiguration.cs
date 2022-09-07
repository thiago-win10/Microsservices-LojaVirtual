using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShopping.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            //VShooping é aplicação Web que vai acessar
            //O IdentitityServer para obter o token
            new ApiScope("vshopping", "VShopping Server"),
            new ApiScope(name: "read", "Read data."),
            new ApiScope(name: "write", "Write data"),
            new ApiScope(name: "delete", "Delete data."),
        };

    public static IEnumerable<Client> Clients =>
     new List<Client>
     {
               //cliente genérico
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("bababdbsa#hujgssjsm".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials, //precisa das credenciais do usuário
                    AllowedScopes = {"read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "vshopping",
                    ClientSecrets = { new Secret("bababdbsa#hujgssjsm".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code, //via codigo
                    RedirectUris = {"https://localhost:7138/signin-oidc"},//login
                    PostLogoutRedirectUris = {"https://localhost:7138/signout-callback-oidc"},//logout
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "vshopping"
                    }
                }
     };
}
