﻿using CommonLayer.Modal;
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
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;

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
            catch (Exception ex)
            {
                throw;
            }          
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var LoginResult = fundooContext.UserTable.Where(r => r.Email == userLoginModel.Email && r.Password == userLoginModel.Password).FirstOrDefault();

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
            catch (Exception ex)
            {

                throw;
            }
        }
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

    }
}
