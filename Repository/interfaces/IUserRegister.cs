

using CommonLayer.model;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer.interfaces
{
    public interface IUserRegister
    {
        public bool Register(UserRequest model);

        public string UserLogin(LoginRequest loginRequest);

        public String ResetPassword(String password, String EmailId);

        public  Task<string> ForgotPassword(string EmailId, IBus bus);

        public String DeleteUser(long userId);

        public string UpdateUser(long id);

    }
}
