using GrudgeBookMvc.src.Controllers.Adapters;
using GrudgeBookMvc.src.Model.Postgres.Authentication;
using GrudgeBookMvc.src.Model.Services.Authorization;
using GrudgeBookMvc.src.Views.Json.AuthenticationData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GrudgeBookMvc.src.Controllers.Authentication
{   
    [Controller]
    public class DwarvenAuthentication : Controller
    {
        [AllowAnonymous]
        [ActionName("registration")]
        [HttpPost]
        public async Task<IActionResult> Registration()
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<AuthService>();

            var data = await request.ReadFromJsonAsync<RegistrationTable>();
            var parsedData = AuthenticationAdapter.ToPostgres(
            AuthenticationAdapter.ToDomain(data));
            service.registerDwarf(parsedData);
            try
            {
                
                

                

            }
            catch
            {
                return StatusCode(400);
            }
            return StatusCode(200);
        }

        /*[AllowAnonymous, Authorize]
        [ActionName("authorization")]
        [HttpPost]
        public async Task<IActionResult> Authentication()
        {
            var context = ControllerContext.HttpContext;
            var userFilledForm = context.Request.ReadFromJsonAsync<AuthenticationForm>();



            var claimsIdentity = new ClaimsIdentity(userFilledForm, "AuthScheme");

        }

        [AllowAnonymous]
        public async void Authorization() 
        {

        }

        [Authorize]
        [ActionName("logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _authorizationService.HttpContext.SignOutAsync();
            return StatusCode(200);
        }*/
    }
}
