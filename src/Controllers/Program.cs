using GrudgeBookMvc.src.Model.Postgres.Context;
using GrudgeBookMvc.src.Model.Services.Auth;
using GrudgeBookMvc.src.Model.Services.Authentication;
using GrudgeBookMvc.src.Model.Services.BookServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using GrudgeBookMvc.src.Controllers.AuthenticationController;

var builder = WebApplication.CreateBuilder(args);

var connectionData = builder.Configuration.GetConnectionString("PostgreSQLConnection");

builder.Services.AddDbContext<DamazKronContext>(options=>options.UseNpgsql(connectionData));

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IAuthenticationRepository, UserDataRepository>();
builder.Services.AddTransient<AuthService>();

builder.Services.AddTransient<IGrudgeRepository, PostgresGrudgeRepository>();
builder.Services.AddTransient<GrudgeService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,

            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,

            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    }
);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute
    (
    name: "Auth",
    pattern: "{controller = DwarvenAuthentication}/{action}/{username?}"
    );

app.MapControllerRoute
    (
    name: "grudging",
    pattern: "{controller}/{action}/{id?}"
    );

app.Run();