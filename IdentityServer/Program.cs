using IdentityServer4.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServer()
	.AddInMemoryClients(new List<Client>())
	.AddInMemoryIdentityResources(new List<IdentityResource>())
	.AddInMemoryApiResources(new List<ApiResource>())
	.AddInMemoryApiScopes(new List<ApiScope>())
	.AddTestUsers(new List<IdentityServer4.Test.TestUser>())
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
