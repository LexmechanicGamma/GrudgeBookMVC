using GrudgeBookMvc.src.Model.Services.Authentication;
using GrudgeBookMvc.src.Model.Postgres.Context;
using GrudgeBookMvc.src.Model.Postgres.Authentication;

namespace GrudgeBookMvc.src.Model.Services.Auth
{
    public class UserDataRepository : IAuthenticationRepository
    {
        private DamazKronContext _userDataContext;
        public UserDataRepository(DamazKronContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public void RegisterDwarf(UserData inputedData)
        {
            _userDataContext.UsersData.Add(inputedData);

            _userDataContext.SaveChanges();
        }

        public UserData GetAccountByUserName(string username)
        {
            return _userDataContext.UsersData.
                  Where(userData => userData.UserName == username).FirstOrDefault()!;
        }

        public bool IsExistingAccount(string username)
        {
            return _userDataContext.UsersData.
                Where(userData => userData.UserName == username).Any();
        }


        ~UserDataRepository()
        {
            if (_userDataContext != null) _userDataContext.Dispose();
        }
    }
}