using GrudgeBookMvc.src.Model.Services.Authorization;

using GrudgeBookMvc.src.Controllers.Adapters;
using GrudgeBookMvc.src.Model.Postgres.Migration;


namespace GrudgeBookMvc.src.Model.Postgres.Authentication
{
    public class UserDataRepository : IAuthentication
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




        ~UserDataRepository()
        {
            if (_userDataContext != null) _userDataContext.Dispose();
        }
    }
}