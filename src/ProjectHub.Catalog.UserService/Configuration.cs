using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace ProjectHub.Catalog.UserService
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("WebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("WebAPI", "Web API", new[]
                    { JwtClaimTypes.Name })
                {
                    Scopes = { "WebAPI" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "web-api",
                    ClientName = "Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http:// .../sidnin-odic"
                    },
                    AllowedCorsOrigins =
                    {
                        "http:// ..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http:/ .../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "WebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}