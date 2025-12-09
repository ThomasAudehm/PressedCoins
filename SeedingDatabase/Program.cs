using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PressedCoins.Database;

var builder = Host.CreateApplicationBuilder(args);
var config =  builder.Configuration;
builder.Services.AddTransient<SeedingDatabase>(); 
builder.Services.AddDbContext<PressedCoinsContext>(options => options.UseSqlServer(config.GetConnectionString(PressedCoinsContext.DbName)));

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var seed =  app.Services.GetRequiredService<SeedingDatabase>();
await seed.SeedAsync();
