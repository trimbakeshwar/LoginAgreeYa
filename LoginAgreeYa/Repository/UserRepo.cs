using LoginAgreeYa.AppDbContext;
using LoginAgreeYa.Controllers;
using LoginAgreeYa.Model;
using Microsoft.EntityFrameworkCore;

namespace LoginAgreeYa.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContexts _db;
        private readonly ILogger<UserRepo> _logger;
        public UserRepo(AppDbContexts db,ILogger<UserRepo> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<ResposeModel> AddUser(RegistrationModel registrationModel)
        {
            _logger.LogInformation("*******call AddUser Repo Method******");
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
                    _logger.LogInformation("get Response from AddUser method : {resposeModel} ", resposeModel);
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user  register Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = userDetail;
                    _logger.LogInformation("get Response from AddUser method : {resposeModel} ", resposeModel);
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
                _logger.LogError("error in AddUser method : {resposeModel} ", resposeModel);

            }
            return resposeModel;
        }

        public async Task<ResposeModel> DeleteUser(int id)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                _logger.LogInformation("*******call DeleteUser Repo Method******");
                var userDetail = await _db.Registration.FindAsync(id).ConfigureAwait(false);
                _logger.LogInformation("find user from FindAsync method : {userDetail} ", userDetail);
                _db.Registration.Remove(userDetail);
                var result = await _db.SaveChangesAsync().ConfigureAwait(false);
                if (result == 0)
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.Message = "user Not Deleted";
                    resposeModel.StatusCode = 501;
                    resposeModel.Results = userDetail;
                    _logger.LogInformation("get Response from DeleteUser method : {resposeModel} ", resposeModel);
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user  Deleted Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = userDetail;
                    _logger.LogInformation("get Response from DeleteUser method : {resposeModel} ", resposeModel);
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
                _logger.LogError("error in DeleteUser method : {resposeModel} ", resposeModel);
            }
            return resposeModel;
        }

        public async Task<ResposeModel> GetAllUser()
        {
            _logger.LogInformation("*******call GetAllUser Repo Method******");
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
                    _logger.LogInformation("get Response from GetAllUser method : {resposeModel} ", resposeModel);

                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user fetch Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                    _logger.LogInformation("get Response from GetAllUser method : {resposeModel} ", resposeModel);
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
                _logger.LogError("error in GetAllUser method : {resposeModel} ", resposeModel);
            }
            return resposeModel;
        }

        public async Task<ResposeModel> GetUser(int id)
        {
            _logger.LogInformation("*******call GetUser Repo Method******");
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
                    _logger.LogInformation("get Response from GetUser method : {resposeModel} ", resposeModel);
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user fetch Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                    _logger.LogInformation("get Response from GetUser method : {resposeModel} ", resposeModel);
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
                _logger.LogError("error in GetUser method : {resposeModel} ", resposeModel);
            }
            return resposeModel;
        }

        public async Task<ResposeModel> UpdateUser(RegistrationModel registrationModel, int id)
        {
            _logger.LogInformation("*******call UpdateUser Repo Method******");
            ResposeModel resposeModel = new ResposeModel();
            try
            {

                var userDetail = await _db.Registration.FindAsync(id).ConfigureAwait(false);
                _logger.LogInformation("find user by id from FindAsync : {userDetail} ", userDetail);
                userDetail.FirstName = registrationModel.FirstName;
                userDetail.LastName = registrationModel.LastName;
                userDetail.Email = registrationModel.Email;
                userDetail.PhoneNumber = registrationModel.PhoneNumber;
                userDetail.Password = registrationModel.Password;
                userDetail.LoginUser = registrationModel.LoginUser;
                _logger.LogInformation("map user model with db model : {userDetail} ", userDetail);
                _db.Registration.Update(userDetail);
                var result = await _db.SaveChangesAsync();
                if (result == 0)
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.Message = "user not Updated";
                    resposeModel.StatusCode = 501;
                    resposeModel.Results = userDetail;
                    _logger.LogInformation("get Response from UpdateUser method : {resposeModel} ", resposeModel);
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user  Updated Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = userDetail;
                    _logger.LogInformation("get Response from UpdateUser method : {resposeModel} ", resposeModel);
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.Message = ex.Message;
                resposeModel.StatusCode = 501;
                resposeModel.Results = null;
                _logger.LogError("error in UpdateUser method : {resposeModel} ", resposeModel);
            }
            return resposeModel;
        }
    }
}
