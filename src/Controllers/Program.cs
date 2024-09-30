using GrudgeBookMvc.src.Model.Postgres;
using GrudgeBookMvc.src.Model.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionData = builder.Configuration.GetConnectionString("PostgreSQLConnection");

builder.Services.AddDbContext<GrudgeContext>(options => options.UseNpgsql(connectionData));
builder.Services.AddTransient<IGrudgeRepository, PostgresGrudgeRepository>();
builder.Services.AddTransient<GrudgeService>();

var app = builder.Build();

app.MapControllerRoute
    (
    name: "grudging",
    pattern: "{controller}/{action}/{id?}"
    );

app.Run();