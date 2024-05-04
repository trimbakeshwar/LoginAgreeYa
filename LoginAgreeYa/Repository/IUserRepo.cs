using LoginAgreeYa.Model;

namespace LoginAgreeYa.Repository
{
    public interface IUserRepo
    {
        public Task<ResposeModel> GetAllUser();
        public Task<ResposeModel> GetUser(int id);
        public Task<ResposeModel> AddUser(RegistrationModel registrationModel);
        public Task<ResposeModel> UpdateUser(RegistrationModel registrationModel, int id);
        public Task<ResposeModel> DeleteUser(int id);
    }
}
