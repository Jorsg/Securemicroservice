using IdentityServer;
using IdentityServer4.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServer()
	.AddInMemoryClients(Config.Clients)
	//.AddInMemoryIdentityResources(Config.identityResources)
	//.AddInMemoryApiResources(Config.ApiResources)
	.AddInMemoryApiScopes(Config.ApiScopes)
	//.AddTestUsers(Config.TestUsers)
	.AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseIdentityServer();



app.Run();
