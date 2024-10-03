using GrudgeBookMvc.src.Model.Postgres.Authentication;
using GrudgeBookMvc.src.Views.Json.AuthenticationData;

namespace GrudgeBookMvc.src.Model.Services.Authorization
{
    public class AuthService
    {
        private IAuthentication _dwarfRepository;

        public AuthService(IAuthentication DwarfRepo)
        {
            this._dwarfRepository = DwarfRepo;
        }

        public void registerDwarf(UserData dwarfData)
        {
            _dwarfRepository.RegisterDwarf(dwarfData);
        }
    }
}
