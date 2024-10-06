using GrudgeBookMvc.src.Controllers.AuthenticationController;
using GrudgeBookMvc.src.Model.Domain.Authentication;
using GrudgeBookMvc.src.Model.Services.Auth;

namespace GrudgeBookMvc.src.Model.Services.Authentication
{
    public class AuthService
    {
        private IAuthenticationRepository _dwarfRepository;

        public AuthService(IAuthenticationRepository DwarfRepo)
        {
            this._dwarfRepository = DwarfRepo;
        }

        public void RegisterDwarfData(Postgres.Authentication.UserData dwarfData)
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
        public Postgres.Authentication.UserData GetExistingAccount(string username)
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