using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.model;
using MassTransit;
namespace BusinessLayer.Interfaces
{
     public interface IUserBusiness
    {
        public bool Register(UserRequest model);

        public string UserLogin(LoginRequest loginRequest);

        public String ResetPassword(String password, String EmailId);

        public Task<string> ForgotPassword(string EmailId, IBus bus);

        public String DeleteUser(long userId);

        public string UpdateUser(long id);



    }
}
