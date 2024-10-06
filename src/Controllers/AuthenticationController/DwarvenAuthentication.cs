using GrudgeBookMvc.src.Controllers.Adapters;
using GrudgeBookMvc.src.Controllers.AuthenticationController;
using GrudgeBookMvc.src.Model.Services.Auth;
using GrudgeBookMvc.src.Model.Services.Authentication;
using GrudgeBookMvc.src.Views.Json.AuthenticationData;
using GrudgeBookMvc.src.Views.Json.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GrudgeBookMvc.src.Controllers.Authentication
{   
    [Controller]
    public class DwarvenAuthentication : Controller
    {
        [AllowAnonymous]
        [ActionName("registration")]
        [HttpPost]
        public async Task Registration(string username)
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<AuthService>();

            
            try
            {
                var data = await request.ReadFromJsonAsync<RegistrationTable>();
                service.RegisterDwarfData(AuthenticationAdapter.ToPostgres(
                AuthenticationAdapter.ToDomain(data)));
                response.StatusCode = 200;
            }
            catch (SuchAccountExistsException e)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });

            }
            catch (LoginInputsException e)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
            }
            catch (Exception)
            {
                response.StatusCode = 500;
                await response.WriteAsJsonAsync("Internal Server Error.");                
            }
            
        }

        [AllowAnonymous]
        [ActionName("login")]
        [HttpPost]
        public async Task Authentication(string username)
        {

            var context = ControllerContext.HttpContext;
            var response = context.Response;
            var service = context.RequestServices.GetService<AuthService>();
            
            try
            {
                if (service.LoginAttemp(
                    username, (string)context.Request.RouteValues["password"]!)) 
                {
                    response.StatusCode = 401; 
                }
                else
                {
                    var claims = new List<Claim>() { new Claim(ClaimTypes.Name, username) };

                    var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.ISSUER,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(120)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

                    response.StatusCode = 200;
                    await context.Response.WriteAsJsonAsync(new JWToken(
                        new JwtSecurityTokenHandler().WriteToken(jwt), username));
                }
                            
            }
            catch(AccountNotFoundException e)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
            }   
        }
    }
}