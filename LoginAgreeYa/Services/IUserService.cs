using LoginAgreeYa.Model;

namespace LoginAgreeYa.Services
{
    public interface IUserService
    {
        public Task<ResposeModel> GetAllUser();
        public Task<ResposeModel> GetUser(int id);
        public Task<ResposeModel> AddUser(RegistrationModel registrationModel);
        public Task<ResposeModel> UpdateUser(RegistrationModel registrationModel, int id);
        public Task<ResposeModel> DeleteUser(int id);
        public ResposeModel GenerateToken(string userEmail);
        public ResposeModel Validating(string token);
        public Task<ResposeModel> Login(string username, string password);
    }
}
