using LoginAgreeYa.AppDbContext;
using LoginAgreeYa.Controllers;
using LoginAgreeYa.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace LoginAgreeYa.Repository
{
    public class UserRepo : IUserRepo
    {
        public static readonly SymmetricSecurityKey SIGNINGKEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(UserRepo.SECRETKEY));

        /// <summary>
        /// The secret key
        /// </summary>
        private const string SECRETKEY = "SuperSecretKey@345fghhhhhhhhhhhhhhhhhhhhhhhhhhhhhfggggggg";
        private readonly AppDbContexts _db;
        private readonly ILogger<UserRepo> _logger;
        public UserRepo(AppDbContexts db,ILogger<UserRepo> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<ResposeModel> AddUser(CustomerModel CustomerModel)
        {
            _logger.LogInformation("*******call AddUser Repo Method******");
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                await _db.Customers.AddAsync(CustomerModel).ConfigureAwait(false);
                var result = await _db.SaveChangesAsync().ConfigureAwait(false);
                var userDetail = await _db.Customers.FindAsync(CustomerModel.Id).ConfigureAwait(false);
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
                var userDetail = await _db.Customers.FindAsync(id).ConfigureAwait(false);
                _logger.LogInformation("find user from FindAsync method : {userDetail} ", userDetail);
                _db.Customers.Remove(userDetail);
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
                var result = await _db.Customers.ToListAsync().ConfigureAwait(false);
               
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
                var result = await _db.Customers.FindAsync(id);
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

        public async Task<ResposeModel> UpdateUser(CustomerModel CustomerModel, int id)
        {
            _logger.LogInformation("*******call UpdateUser Repo Method******");
            ResposeModel resposeModel = new ResposeModel();
            try
            {

                var userDetail = await _db.Customers.FindAsync(id).ConfigureAwait(false);
                _logger.LogInformation("find user by id from FindAsync : {userDetail} ", userDetail);
                userDetail.FirstName = CustomerModel.FirstName;
                userDetail.LastName = CustomerModel.LastName;
                userDetail.Email = CustomerModel.Email;
                userDetail.PhoneNumber = CustomerModel.PhoneNumber;
                userDetail.Password = CustomerModel.Password;
                userDetail.LoginUser = CustomerModel.LoginUser;
                _logger.LogInformation("map user model with db model : {userDetail} ", userDetail);
                _db.Customers.Update(userDetail);
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
        public ResposeModel GenerateToken(string userEmail)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, userEmail)
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNINGKEY, SecurityAlgorithms.HmacSha256));

                var result =  new JwtSecurityTokenHandler().WriteToken(token);
                if (result != null)
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.StatusCode = 200;
                    resposeModel.Message = "Token genrated Successfully";
                    resposeModel.Results = result;
                }
                else
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.StatusCode = 200;
                    resposeModel.Message = "Token genration fail ";
                    resposeModel.Results = result;
                }
            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.StatusCode = 200;
                resposeModel.Message = ex.Message;
                resposeModel.Results = null;
            }
            return resposeModel;
        }
        public ResposeModel Validating(string token)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Encoding.UTF8.GetBytes(SECRETKEY);

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = SIGNINGKEY
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                if (principal != null)
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.StatusCode = 200;
                    resposeModel.Message = "Token genrated Successfully";
                    resposeModel.Results = principal;
                }
                else
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.StatusCode = 200;
                    resposeModel.Message = "Token genration fail ";
                    resposeModel.Results = principal;
                }

            }
            catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.StatusCode = 501;
                resposeModel.Message = ex.Message;
                resposeModel.Results = null;
               
            }
            return resposeModel;
        }
        public async Task<ResposeModel> Login(string username, string password)
        {
            ResposeModel resposeModel = new ResposeModel();
            try
            {
                var result = await _db.Customers.Where(x => x.LoginUser == username && x.Password == password).FirstOrDefaultAsync().ConfigureAwait(false);
                _logger.LogInformation("find user by id from FindAsync : {result} ", result);
                if (result?.FirstName == null)
                {
                    resposeModel.IsSuccess = false;
                    resposeModel.Message = "please check Username and Password";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                    _logger.LogInformation("get Response from GetUser method : {resposeModel} ", resposeModel);
                }
                else
                {
                    resposeModel.IsSuccess = true;
                    resposeModel.Message = "user Login Successfully";
                    resposeModel.StatusCode = 200;
                    resposeModel.Results = result;
                    _logger.LogInformation("get Response from GetUser method : {resposeModel} ", resposeModel);
                }
            } catch (Exception ex)
            {
                resposeModel.IsSuccess = false;
                resposeModel.StatusCode = 501;
                resposeModel.Message = ex.Message;
                resposeModel.Results = null;


            }
            return resposeModel;
        }
    }
}
