using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.Data;
using Microsoft.Extensions.Hosting;
using Movies.API;
using Microsoft.AspNetCore.Hosting;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var config = builder.Configuration;


		// Add services to the container.
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddDbContext<MoviesContext>(options =>
					options.UseInMemoryDatabase(("Movies")));

		var configBuilder = new ConfigurationBuilder()
			.AddEnvironmentVariables()
			.AddJsonFile("appsetting.json");

		builder.Services.AddAuthentication("Bearer")
			.AddJwtBearer("Bearer", opt =>
			{
				opt.Authority = config["Bearer:Url"];
				opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateAudience = false,
				};
			});
		builder.Services.AddAuthorization(opt =>
		{
			opt.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movieClient"));
		});


		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();
		// Seed the database
		using (var scope = app.Services.CreateScope())
		{
			var serviceProvider = scope.ServiceProvider;
			var moviesContext = serviceProvider.GetRequiredService<MoviesContext>();
			MoviesContextSeed.SeedAsync(moviesContext);
		}
		app.Run();
	}

}