using Dapper;
using GenesisMedicalStaffingAgency.IRepositories;
using GenesisMedicalStaffingAgency.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GenesisMedicalStaffingAgency.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnection conn;
        public UserRepository(IDbConnection dbConnection, IConfiguration _configuration)
        {
            this.conn = dbConnection;
            this.configuration = _configuration;
        }
        public int AddCustomer(User user)
        {
            string query = "Insert into [User] (UserName, Email, Age, Grade, ContactNo, Password, Interestofstudy) Values(@UserName, @Email, @Age, @Grade, @ContactNo, @Password, @Interestofstudy) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];";
            var _parameter = new Dictionary<string, object>();
            _parameter.Add("@UserName", user.UserName);
            _parameter.Add("@Email", user.Email);
            _parameter.Add("@Age", user.Age);
            _parameter.Add("@Grade", user.Grade);
            _parameter.Add("@Interestofstudy", user.Interestofstudy);
            _parameter.Add("@ContactNo", user.ContactNo);
            _parameter.Add("@Password", user.Password);
            var result = conn.Query<int>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
            return result;
        }
        public Tokens AuthenticateUserFromDB(string Email, string Password)
        {
            try
            {
                string query = "Select * from [User] where Email = @Email and Password = @Password";
                var _parameter = new Dictionary<string, object>();
                _parameter.Add("Email", Email);
                _parameter.Add("Password", Password);
                User result = conn.Query<User>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
                if (result != null)
                {
                    if (result.Email == Email && result.Password == Password)
                    {
                        string config = configuration.GetSection("JWT").GetSection("Key").Value;
                        //else we generate JWT Token
                        var tokenhandler = new JwtSecurityTokenHandler();
                        var tokenKey = Encoding.UTF8.GetBytes(config);

                        var tokenDescription = new SecurityTokenDescriptor
                        {
                            Subject = new System.Security.Claims.ClaimsIdentity(
                            new Claim[]
                            {
                                new Claim(ClaimTypes.Name, Email)
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(5),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var token = tokenhandler.CreateToken(tokenDescription);

                        return new Tokens { Token = tokenhandler.WriteToken(token)};
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int? ContactUs(Contact contact)
        {
            string query = "Insert into [Contact] Values(@Name, @Email, @Subject, @Message) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];";
            var _parameter = new Dictionary<string, object>();
            _parameter.Add("@Name", contact.Name);
            _parameter.Add("@Email", contact.Email);
            _parameter.Add("@Subject", contact.Subject);
            _parameter.Add("@Message", contact.Message);
            var result = conn.Query<int>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
            return result;
        }
        public int EditCustomer(User user)
        {
            string query = "Update [User] Set UserName = @UserName, Email = @Email, Age = @Age, Grade = @Grade, Interestofstudy = @Interestofstudy, ContactNo = @ContactNo, Password = @Password Where Email = @Email SELECT @Age";
            var _parameter = new Dictionary<string, object>();
            _parameter.Add("@UserName", user.UserName);
            _parameter.Add("@Email", user.Email);
            _parameter.Add("@Age", user.Age);
            _parameter.Add("@Grade", user.Grade);
            _parameter.Add("@Interestofstudy", user.Interestofstudy);
            _parameter.Add("@ContactNo", user.ContactNo);
            _parameter.Add("@Password", user.Password);
            var result = conn.Query<int>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
            return result;
        }
        public User GetCustomerByEmail(string UserEmail)
        {
            string query = "Select * from [User] where Email = @Email";
            var _parameter = new Dictionary<string, object>();
            _parameter.Add("Email", UserEmail);
            User result = conn.Query<User>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
            return result;
        }
        public Admin GetAdminByEmail(string UserEmail)
        {
            string query = "Select * from [Admin] where Email = @Email";
            var _parameter = new Dictionary<string, object>();
            _parameter.Add("Email", UserEmail);
            Admin result = conn.Query<Admin>(query, param: _parameter, commandType: CommandType.Text).FirstOrDefault();
            return result;
        }
        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
