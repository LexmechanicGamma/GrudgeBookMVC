using GrudgeBookMvc.src.Model.Postgres.Authentication;

namespace GrudgeBookMvc.src.Model.Services.Authentication
{
    public interface IAuthenticationRepository
    {
        public void RegisterDwarf(UserData inputedData);

        public UserData GetAccountByUserName(string username);

        public bool IsExistingAccount(string username);
    }
}