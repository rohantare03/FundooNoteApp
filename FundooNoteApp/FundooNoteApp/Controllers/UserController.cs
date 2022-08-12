using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Context;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBL iuserBL, ILogger<UserController> logger)
        {
            this.iuserBL = iuserBL;
            this.logger = logger;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userRegistrationModel">The user registration model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);

                if (result != null)
                {
                    logger.LogInformation("Registeration Sucessfull");
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    logger.LogError("Registeration Unsuccessfull");
                    return BadRequest(new { success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }
        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="userLoginModel">The user login model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var result = iuserBL.Login(userLoginModel);

                if (result != null)
                {
                    logger.LogInformation("Login Sucessfull");
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    logger.LogError("Registeration Unsuccessfull");
                    return BadRequest(new { success = false, message = "Login Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = iuserBL.ForgetPassword(Email);

                if (result != null)
                {
                    logger.LogInformation("Email sent Successful");
                    return Ok(new { success = true, message = "Email sent Successful" });
                }
                else
                {
                    logger.LogError("Reset Email not send");
                    return BadRequest(new { success = false, message = "Reset Email not send" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Resets the link.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ResetLink")]
        public IActionResult ResetLink(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserBL.ResetLink(Email, password, confirmPassword);

                if (result != null)
                {
                    logger.LogInformation("Password Reset Successful");
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
                    logger.LogError("Password Reset Unsuccessful");
                    return BadRequest(new { success = false, message = "Password Reset Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }
    }
}
