using GrudgeBookMvc.src.Controllers.Adapters;
using GrudgeBookMvc.src.Controllers.BookController;
using GrudgeBookMvc.src.Model.Domain.Book;
using GrudgeBookMvc.src.Model.Services.BookServices;
using GrudgeBookMvc.src.Views.Json.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrudgeBookMvc.src.Controllers.GrudgeController
{
    [Controller]
    [Authorize]
    public class Book : Controller
    {
        [Authorize]
        [ActionName("PostGrudge")]
        [HttpPost]
        public async Task WriteGrudge()
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<GrudgeService>();
            try
            {
                var data = await request.ReadFromJsonAsync<Views.Json.Book.Grudge>();

                service.WriteGrudge(GrudgeAdapters.ToDomain(data));
                response.StatusCode = 200;
            }
            catch (InvalidUnixTimestampException e)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });                
            }
            catch (StatusParseException e)
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
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Internal Server Error"
                });               
            }
        }


        [Authorize]
        [ActionName("UpdateGrudgeStatus")]
        [HttpPut]
        public async Task AdjustGrudgeStatus(string id)
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<GrudgeService>();
            try
            {

                GrudgeStatus status = GrudgeStatusBuilder.
                    FromString((string)request.Query["status"]);

                service.AdjustGrudgeStatus(id, status);
                response.StatusCode = 200;
            }
            catch (StatusParseException e)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
            }
            catch (IdIsNotFoundException e)
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
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Internal Server Error"
                });
            }
        }


        [Authorize]
        //[AllowAnonymous]
        [ActionName("GetGrudge")]
        [HttpGet]
        public async Task GetGrudge(string id)
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<GrudgeService>();
            try
            {
                response.StatusCode = 200;
                await response.WriteAsJsonAsync(GrudgeAdapters.FromDomain(service.GetGrudge(id)));               
            }
            catch (IdIsNotFoundException e)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
               
            }
            catch(ArgumentNullException e)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
            }
            catch(NullReferenceException e)
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
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Internal Server Error"
                });             
            }
        }


        [AllowAnonymous]
        [ActionName("GetAllGrudges")]
        [HttpGet]
        public async Task ListGrudges()
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<GrudgeService>();
            try
            {
                await response.WriteAsJsonAsync(GrudgeAdapters.
                ListParsedGrudges(
                service.
                ListGrudges()));              
            }
            catch
            {
                response.StatusCode = 500;
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Internal Server Error"
                });
            }
        }


        [Authorize]
        [ActionName("DeleteGrudge")]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await Response.WriteAsJsonAsync("\"An ELF!\"");
            return StatusCode(401, Results.Unauthorized());
        }
    }
}