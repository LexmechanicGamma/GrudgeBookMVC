using GrudgeBookMvc.src.Model.Services.Authorization;

using GrudgeBookMvc.src.Controllers.Adapters;


namespace GrudgeBookMvc.src.Model.Postgres.Authentication
{
    public class UserDataRepository : IAuthentication
    {
        private UserDataContext _userDataContext;
        public UserDataRepository(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }   

        public void RegisterDwarf(UserData inputedData)
        {
            _userDataContext.usersData.Add(inputedData);

            _userDataContext.SaveChanges();
        }




        ~UserDataRepository()
        {
            if (_userDataContext != null) _userDataContext.Dispose();
        }


    }
}