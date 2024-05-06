using LoginAgreeYa.Model;
using LoginAgreeYa.Repository;

namespace LoginAgreeYa.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepo _UserRepo;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepo userRepo, ILogger<UserService> logger)
        {
            _UserRepo = userRepo;
            _logger = logger;
        }


        public async Task<ResposeModel> AddUser(RegistrationModel registrationModel)
        {
            _logger.LogInformation("******** call AddUser Service *********");
            return await _UserRepo.AddUser(registrationModel).ConfigureAwait(false);
        }

        public async Task<ResposeModel> DeleteUser(int id)
        {
            _logger.LogInformation("******** call DeleteUser Service *********");
            return await _UserRepo.DeleteUser(id).ConfigureAwait(false);
        }

        public ResposeModel GenerateToken(string userEmail)
        {
            return _UserRepo.GenerateToken(userEmail);
        }

        public async Task<ResposeModel> GetAllUser()
        {
            _logger.LogInformation("******** call GetAllUser Service *********");
            return await _UserRepo.GetAllUser().ConfigureAwait(false);
        }

        public async Task<ResposeModel> GetUser(int id)
        {
            _logger.LogInformation("******** call GetUser Service *********");
            return await  _UserRepo.GetUser(id).ConfigureAwait(false);
        }

        public async Task<ResposeModel> Login(string username, string password)
        {
            return await _UserRepo.Login(username,password).ConfigureAwait(false);
        }
    

        public async Task<ResposeModel> UpdateUser(RegistrationModel registrationModel, int id)
        {
            _logger.LogInformation("******** call UpdateUser Service *********");
            return await _UserRepo.UpdateUser(registrationModel, id).ConfigureAwait(false);
        }

        public  ResposeModel Validating(string token)
        {
            return  _UserRepo.Validating(token);
        }

       
    }
}
