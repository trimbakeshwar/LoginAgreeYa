using LoginAgreeYa.Model;
using LoginAgreeYa.Repository;

namespace LoginAgreeYa.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepo _UserRepo;
        public UserService(IUserRepo userRepo)
        {
            _UserRepo = userRepo;
        }


        public async Task<ResposeModel> AddUser(RegistrationModel registrationModel)
        {
            return await _UserRepo.AddUser(registrationModel).ConfigureAwait(false);
        }

        public async Task<ResposeModel> DeleteUser(int id)
        {
            return await _UserRepo.DeleteUser(id).ConfigureAwait(false);
        }

        public async Task<ResposeModel> GetAllUser()
        {
            return await _UserRepo.GetAllUser().ConfigureAwait(false);
        }

        public async Task<ResposeModel> GetUser(int id)
        {
            return await  _UserRepo.GetUser(id).ConfigureAwait(false);
        }

        public async Task<ResposeModel> UpdateUser(RegistrationModel registrationModel, int id)
        {
           return await _UserRepo.UpdateUser(registrationModel, id).ConfigureAwait(false);
        }
    }
}
