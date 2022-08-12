using CommonLayer.Modal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration Config;
        public UserRL(FundooContext fundooContext, IConfiguration Config)
        {
            this.fundooContext = fundooContext;
            this.Config = Config;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="userRegistrationModel">The user registration model.</param>
        /// <returns></returns>
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = ConvertToEncrypt(userRegistrationModel.Password);

                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();

                if(result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }          
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="userLoginModel">The user login model.</param>
        /// <returns></returns>
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var LoginResult = fundooContext.UserTable.Where(r => r.Email == userLoginModel.Email && r.Password == ConvertToDecrypt(userLoginModel.Password)).FirstOrDefault();

                if (LoginResult != null)
                {

                    var Token = GenerateSecurityToken(LoginResult.Email, LoginResult.UserId);
                    return Token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Generates the security token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        public string GenerateSecurityToken(string email, long userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns></returns>
        public string ForgetPassword(string Email)
        {
            try
            {
                var emailCheck = fundooContext.UserTable.FirstOrDefault(r => r.Email == Email);


                if (emailCheck != null)
                {
                    var Token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSMQmodel mSMQmodel = new MSMQmodel();
                    mSMQmodel.sendData2Queue(Token);
                    return Token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Resets the Password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
        public bool ResetLink(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var emailCheck = fundooContext.UserTable.FirstOrDefault(r => r.Email == email);
                    emailCheck.Password = password;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        string Key = "abc@xyz@123@";

        /// <summary>
        /// Encrypt the Password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string ConvertToEncrypt(string password)
        {

            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        /// <summary>
        /// Decrypt the Password.
        /// </summary>
        /// <param name="base64EncodeData">The base64 encode data.</param>
        /// <returns></returns>
        public string ConvertToDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}
