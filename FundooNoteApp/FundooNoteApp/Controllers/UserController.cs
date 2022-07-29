using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);

                if(result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var result = iuserBL.Login(userLoginModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = iuserBL.ForgetPassword(Email);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Email sent Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Email not send" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
