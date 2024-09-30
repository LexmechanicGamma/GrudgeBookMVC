using GrudgeBookMvc.src.Controllers.Adapters;
using GrudgeBookMvc.src.Model.Domain;
using GrudgeBookMvc.src.Model.Services;
using GrudgeBookMvc.src.Views.Json;
using Microsoft.AspNetCore.Mvc;

namespace GrudgeBookMvc.src.Controllers
{
    [Controller]
    public class Book : Controller 
    {
        [ActionName("PostGrudge")]
        [HttpPost]
        public async Task<IActionResult> WriteGrudge()
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<GrudgeService>();
            try
            {
                var data = await request.ReadFromJsonAsync<Views.Json.Grudge>();

                service.WriteGrudge(GrudgeAdapters.ToDomain(data));
                return StatusCode(200);
            }
            catch (InvalidUnixTimestampException e)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
                return StatusCode(400);
            }
            catch (StatusParseException e)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
                return StatusCode(400);
            }
            catch (Exception)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Internal Server Error"
                });
                return StatusCode(500);
            }
        }



        [ActionName("UpdateGrudgeStatus")]
        [HttpPut]
        public async Task<IActionResult> AdjustGrudgeStatus(string id)
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
                return StatusCode(200);
            }
            catch (StatusParseException e)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
                return StatusCode(400);
            }
            catch (IdIsNotFoundException e)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
                return StatusCode(400);
            }
            catch (Exception)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                   Error = "Internal Server Error"
                });
                return StatusCode(500);
            }

        }



        [ActionName("GetGrudge")]
        [HttpGet]
        public async Task<IActionResult> GetGrudge(string id)
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<GrudgeService>();
            try 
            {       
                await response.WriteAsJsonAsync(GrudgeAdapters.FromDomain(service.GetGrudge(id)));
                return StatusCode(200);
            }
            catch (IdIsNotFoundException e)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = e.Message
                });
                return StatusCode(400);
            }
            catch (Exception)
            {
                await response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Internal Server Error"
                });
                return StatusCode(500);
            }
        }



        [Route("Book/GetAllGrudges")]
        [ActionName("GetAllGrudges")]
        [HttpGet]
        public async Task<IActionResult> ListGrudges()
        {
            var context = ControllerContext.HttpContext;
            var request = context.Request;
            var response = context.Response;
            var service = context.RequestServices.GetService<GrudgeService>();
            await response.WriteAsJsonAsync(GrudgeAdapters.
                ListParsedGrudges(
                service.
                ListGrudges()));
            return StatusCode(200);
        }



        [ActionName("DeleteGrudge")]
        [HttpDelete]
        public string Delete() ////////////
        {
            return "ELF!";
        }
    }
}