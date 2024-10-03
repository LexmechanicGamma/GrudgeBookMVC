using GrudgeBookMvc.src.Model.Postgres.Authentication;
using GrudgeBookMvc.src.Views.Json.AuthenticationData;

namespace GrudgeBookMvc.src.Model.Services.Authorization
{
    public interface IAuthentication
    {
        public void RegisterDwarf(UserData inputedData);
    }
}