using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Movies.Client.ApiServices;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieApiService, MovieApiService>();

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
	.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, option =>
	{
		//option.Authority = config["Authority:Url"];//
		option.Authority = "https://localhost:5005";

		option.ClientId = "movies_mvc_client";
		option.ClientSecret = "secret";
		option.ResponseType = "code";

		option.Scope.Add("openid");
		option.Scope.Add("profile");

		option.SaveTokens = true;
		option.GetClaimsFromUserInfoEndpoint = true;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
