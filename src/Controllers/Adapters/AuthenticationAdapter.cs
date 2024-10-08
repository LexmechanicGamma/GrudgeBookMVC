using GrudgeBookMvc.src.Views.Json.AuthenticationData;
using GrudgeBookMvc.src.Model.Postgres.Authentication;
namespace GrudgeBookMvc.src.Controllers.Adapters
{
    public static class AuthenticationAdapter
    {
        public static Domain.Authentication.UserData ToDomain(RegistrationTable inputedData)
        {
            Domain.Authentication.UserData userData = new(
                inputedData.Email,
                inputedData.UserName,
                inputedData.Password
                );
            return userData;
        }

        public static Model.Postgres.Authentication.
            UserData ToPostgres(Domain.Authentication.UserData inputedData)
        {
            Model.Postgres.Authentication.
            UserData userData = new(
                inputedData.Email,
                inputedData.Id,
                inputedData.Username,
                inputedData.Salt,
                inputedData.Password
                );
            return userData;
        }
    }
}