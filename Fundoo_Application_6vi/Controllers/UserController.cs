using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Fundoo_Application_6vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserBusiness userBusiness;
        //public readonly IUserBusinessLogin UserBussinessLogin;
        //public readonly IuserBusinessDelete userBusinessDelete;
        //public readonly IUserForgotPassword userForgotPassword;
        //public readonly IuserBusinessResetPassword userBusinessReset;
        public readonly IBus bus;

        public UserController(IUserBusiness userBusiness, IBus bus)
        {
            this.userBusiness = userBusiness;
            //this.UserBussinessLogin = userBussinessLogin;
            //this.userBusinessDelete = userBusinessDelete;
            //this.userForgotPassword = userForgotPassword;
            //this.userBusinessReset=userBusinessReset;
            this.bus = bus;
        }
   

        [HttpPost("Register")]
        public IActionResult Register([FromBody]UserRequest user)
        {
            try
            {
                userBusiness.Register(user);
                return Ok(new {success = true,message = "Successful"});
            }
            catch (Exception e)
            {

                return BadRequest(new { success = true, message = e.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult UserLogin(LoginRequest model)
        {

            try
            {
                var entity= userBusiness.UserLogin(model);
                if (entity != null)
                {
                    return Ok(new { success = true, message = "Login Successfull ", data = entity });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failure ", data = model.EmailId});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = userBusiness.DeleteUser(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Delete Operation Successfull ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, messgae = "Delete operation is UnSuccessfull" ,data=result});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ForgetPassword")]
        public IActionResult ForgetPassword(string EmailId)
        {
            try
            { 
                var result= userBusiness.ForgotPassword(EmailId ,bus);
                if (result != null)
                {
                    return Ok(new { success = true, message = "token  generated successfullly and Authenticatoion Notification send To "+EmailId, Emailid = EmailId, data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Token Not Generated", data = result });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.InnerException.Message);
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string Password)
        {
            try
            {
                String EmailId = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                var result= userBusiness.ResetPassword(Password, EmailId);

                if (result != null)
                {
                    return Ok(new { success = true, message = result, data = "Password chenged To "+Password });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Ubable To Chenge Password", data = result });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


    }
}
