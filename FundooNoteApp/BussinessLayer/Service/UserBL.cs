using BussinessLayer.Interface;
using CommonLayer.Modal;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
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
                return iuserRL.Login(userLoginModel);

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public string ForgetPassword(string Email)
        {
            try
            {
                return iuserRL.ForgetPassword(Email);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
