using GrudgeBookMvc.src.Model.Postgres.Authentication;
using GrudgeBookMvc.src.Model.Postgres.Book;
using GrudgeBookMvc.src.Model.Postgres.Migration;
using GrudgeBookMvc.src.Model.Services.Authorization;
using GrudgeBookMvc.src.Model.Services.BookServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionData = builder.Configuration.GetConnectionString("PostgreSQLConnection");

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<UserDataContext>(options => options.UseNpgsql(connectionData));
builder.Services.AddDbContext<GrudgeContext>(options => options.UseNpgsql(connectionData));
builder.Services.AddDbContext<DamazKronContext>(options=>options.UseNpgsql(connectionData));

builder.Services.AddTransient<IAuthentication, UserDataRepository>();
builder.Services.AddTransient<AuthService>();




builder.Services.AddTransient<IGrudgeRepository, PostgresGrudgeRepository>();
builder.Services.AddTransient<GrudgeService>();





/*builder.Services.AddAuthentication("Cookies").AddCookie
    ("AuthScheme", options =>
        {
            options.LoginPath = "/Home/Login/";
            options.LogoutPath = "/Home/Logout/";
            options.AccessDeniedPath = "/Home/AccessDenied/";
        }
    );*/

/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("login-status", policy =>
    policy.Requirements.Add(new MethodThatReturnsBool));
});*/

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

/*app.MapControllerRoute
    (
    name: "auth",
    pattern: "{controller}/{action}/{username?}"
    );*/
app.MapControllerRoute
    (
    name: "grudging",
    pattern: "{controller}/{action}/{id?}"
    );

app.Run();