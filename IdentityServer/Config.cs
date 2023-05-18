using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer
{
	public class Config
	{
		private static IConfiguration _configuration;
		public Config(IConfiguration configuration)
		{
			_configuration = configuration;			
		}
        
        public static IEnumerable<Client> Clients =>
			new Client[]
			{
				new Client
				{
					ClientId = "movieClient",
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes = {"movieAPI"}
				},
				new Client
				{
					ClientId = "movies_mvc_client",
					ClientName = "Movies MVC Web App",
					AllowedGrantTypes= GrantTypes.Code,
					AllowRememberConsent = false,
					RedirectUris = new List<string>()
					{
						//_configuration["Signin:Url"]
						"https://localhost:5002/signin-oidc"
					},
					PostLogoutRedirectUris = new List<string>()
					{
						//_configuration["Sigout:Url"]
						"https://localhost:5002/signout-callback-oidc"
					},
					ClientSecrets = new List<Secret>
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile
					}				
				}
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{
				new ApiScope("movieAPI", "Movie API")
			};

		public static IEnumerable<ApiResource> ApiResources =>
			new ApiResource[]
			{
			};

		public static IEnumerable<IdentityResource> identityResources =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};

		public static List<TestUser> TestUsers =>
			new List<TestUser>
			{
				new TestUser
				{
					SubjectId ="5BE86359-073C-434B-AD2D-A3932222DABE",
					Username = "jhors",
					Password = "jhors",
					Claims = new List<Claim>
					{
						new Claim(JwtClaimTypes.GivenName,"jhors"),
						new Claim(JwtClaimTypes.FamilyName,"garc")
					}
				}
			};
	}
}
