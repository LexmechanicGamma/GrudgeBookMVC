using GrudgeBookMvc.src.Model.Postgres.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using GrudgeBookMvc.src.Controllers.AuthenticationController;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using GrudgeBookMvc.src.Services.Auth;
using GrudgeBookMvc.src.Services.BookServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer(); /////
builder.Services.AddSwaggerGen(
    c =>
    {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "openAPI", Version = "v1" });
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, 
                        Id = "bearerAuth" }
                },
                new string[] {}
            }
        });       
    });

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

app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("swagger/v1/swagger.json","v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllerRoute
    (
    name: "Auth",
    pattern: "{controller}/{action}/{username?}"
    )
    .WithName("AuthRoute")
    .WithOpenApi(operations => new(operations)
    {
        Summary = "Route for Registartion and Authorization."
    });

app.MapControllerRoute
    (
    name: "grudging",
    pattern: "{controller = Book}/{action}/{id?}"
    )
    .WithName("BookControllerRoute")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Route For Grudge Managment."
    });


app.Run();