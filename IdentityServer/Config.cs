using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
	public class Config
	{
		public static IEnumerable<Client> Clients =>
			new Client[]
			{
				new Client
				{
					ClientId = "moviClient",
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets =
					{
						new Secret("secrect".Sha256())
					},
					AllowedScopes = {"moviesAPI"}
				}
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{

			};

		public static IEnumerable<ApiResource> ApiResources =>
			new ApiResource[]
			{
			};

		public static IEnumerable<IdentityResource> identityResources =>
			new IdentityResource[]
			{
			};

		public static List<TestUser> TestUsers =>
			new List<TestUser>
			{
			};
	}
}
