using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspnetCoreStarter.Data;
using AspnetCoreStarter.Common;
using AspnetCoreStarter.Models;
using AspnetCoreStarter.Endpoints;
using AspnetCoreStarter.Auth;
using AspnetCoreStarter.Extenssions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
  {
    //options.UseSqlite(
    //  builder.Configuration.GetConnectionString("ApplicationDbContext") ??
    //  throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")
    //)
    //.EnableSensitiveDataLogging()
    //.EnableDetailedErrors();
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"))
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
  },
ServiceLifetime.Scoped);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthorizationManager, AuthorizationManager>();
builder.Services.AddScoped<AutoCompeleteHelper>();
builder.Services.AddSingleton<IISOLanguages, ISOLanguages>();
builder.Services.AddScoped<DynamicQueryService>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//  var services = scope.ServiceProvider;

//  SeedData.Initialize(services);
//}
app.CreateDbIfNotExists();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// await app.UseRolesAndPermissions();

app.MapRazorPages();

app.MapUserEndpoints();

app.Run();
