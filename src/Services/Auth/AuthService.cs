using GrudgeBookMvc.src.Controllers.AuthenticationController;
using GrudgeBookMvc.src.Domain.Authentication;
using GrudgeBookMvc.src.Services.Auth;

namespace GrudgeBookMvc.src.Services.Auth
{
    public class AuthService
    {
        private IAuthenticationRepository _dwarfRepository;

        public AuthService(IAuthenticationRepository DwarfRepo)
        {
            _dwarfRepository = DwarfRepo;
        }

        public void RegisterDwarfData(Model.Postgres.Authentication.UserData dwarfData)
        {
            if (_dwarfRepository.IsExistingAccount(dwarfData.UserName))
            {
                throw new SuchAccountExistsException("This UserName already exists.");
            }

            _dwarfRepository.RegisterDwarf(dwarfData);
        }

        public bool LoginAttemp(string username, string password)
        {
            if (_dwarfRepository.IsExistingAccount(username))
            {
                var dwarfAccount = _dwarfRepository.GetAccountByUserName(username);
                return dwarfAccount.SaltedPassword == Encryptor.ToSHA256(password, dwarfAccount.Salt);
            }
            else
            {
                throw new AccountNotFoundException("Username not found.");
            }
        }
        public Model.Postgres.Authentication.UserData GetExistingAccount(string username)
        {
            try
            {
                return _dwarfRepository.GetAccountByUserName(username);
            }
            catch (KeyNotFoundException e)
            {
                throw new AccountNotFoundException(e.Message + "Such Account does not exist.");
            }
            catch (ArgumentNullException e)
            {
                throw new LoginInputsException(e.Message + "Incorrect UserName input.");
            }
        }
    }
}