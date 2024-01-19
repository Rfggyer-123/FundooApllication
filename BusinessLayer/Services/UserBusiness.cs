using BusinessLayer.Interfaces;
using RepositoryLayer.interfaces;
using CommonLayer.model;
using MassTransit;
using RepositoryLayer.Services;
namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        public readonly IUserRegister register;
        public UserBusiness(IUserRegister register)
        {
            this.register = register;
        }


        public bool Register(UserRequest model)
        {
            return register.Register(model);
        }

        public string UserLogin(LoginRequest loginRequest)
        {
            return register.UserLogin(loginRequest);
        }


        public Task<string> ForgotPassword(string EmailId, IBus bus)
        {
            return register.ForgotPassword(EmailId, bus);
        }


        public string ResetPassword(string password, String EmailId)
        {
            return register.ResetPassword(password, EmailId);
        }


        public string DeleteUser(long userId)
        {
            return register.DeleteUser(userId);

        }

        public string UpdateUser(long id)
        {
            return register.UpdateUser(id);
        }

      
    }
}
