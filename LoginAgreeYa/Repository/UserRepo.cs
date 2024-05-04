using LoginAgreeYa.AppDbContext;
using LoginAgreeYa.Model;
using Microsoft.EntityFrameworkCore;

namespace LoginAgreeYa.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContexts _db;
        public UserRepo(AppDbContexts db)
        {
            _db = db;
        }
        public async Task<ResposeModel> AddUser(RegistrationModel registrationModel)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                await _db.Registration.AddAsync(registrationModel).ConfigureAwait(false);
                var result = await _db.SaveChangesAsync().ConfigureAwait(false);
                var userDetail = await _db.Registration.FindAsync(registrationModel.Id).ConfigureAwait(false);
                if (result == 0)
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.Message = "user Not register";
                    resposeModel.StatusCode = 501;
                    resposeModel.Results = userDetail;
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user  register Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = userDetail;
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
            }
            return resposeModel;
        }

        public async Task<ResposeModel> DeleteUser(int id)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                var userDetail = await _db.Registration.FindAsync(id).ConfigureAwait(false);
                _db.Registration.Remove(userDetail);
                var result = await _db.SaveChangesAsync().ConfigureAwait(false);
                if (result == 0)
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.Message = "user Not Deleted";
                    resposeModel.StatusCode = 501;
                    resposeModel.Results = userDetail;
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user  Deleted Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = userDetail;
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
            }
            return resposeModel;
        }

        public async Task<ResposeModel> GetAllUser()
        {

            ResposeModel resposeModel = new ResposeModel();
            try
            {
                var result = await _db.Registration.ToListAsync().ConfigureAwait(false);
                if (result.Count == 0)
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "Currently we dont have any user";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user fetch Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
            }
            return resposeModel;
        }

        public async Task<ResposeModel> GetUser(int id)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                var result = await _db.Registration.FindAsync(id);
                if (result?.FirstName == null)
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "Currently we dont have any user";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user fetch Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
            }
            return resposeModel;
        }

        public async Task<ResposeModel> UpdateUser(RegistrationModel registrationModel, int id)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                var userDetail = await _db.Registration.FindAsync(id).ConfigureAwait(false);
                userDetail.FirstName = registrationModel.FirstName;
                userDetail.LastName = registrationModel.LastName;
                userDetail.Email = registrationModel.Email;
                userDetail.PhoneNumber = registrationModel.PhoneNumber;
                userDetail.Password = registrationModel.Password;
                userDetail.LoginUser = registrationModel.LoginUser;
                _db.Registration.Update(userDetail);
                var result = await _db.SaveChangesAsync();
                if (result == 0)
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.Message = "user not Updated";
                    resposeModel.StatusCode = 501;
                    resposeModel.Results = userDetail;
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user  Updated Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = userDetail;
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
            }
            return resposeModel;
        }
    }
}
