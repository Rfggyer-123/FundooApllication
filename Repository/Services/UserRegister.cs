using CommonLayer.model;
using MassTransit;
using MassTransit.Initializers.Variables;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Logging.LogCategoryName;

namespace RepositoryLayer.Services
{
    public class UserRegister : IUserRegister
    {
        public readonly FundooContext Context;
        
        public readonly IConfiguration configuration;
        // public readonly IBus bus;

        public UserRegister (FundooContext context, IConfiguration configuration)
        {
            this.Context = context;
            this.configuration = configuration;
            //this.bus = bus;
        }


       

        public bool Register(UserRequest model)
        {
            UserEntity user = new UserEntity();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.EmailId;
            user.Password = EncryptPasswordClass.EcryptPassword(model.Password);
            user.CreatedAt  = DateTime.Now;
            if(user == null)
            {
                throw new Exception("User ran away");
            }
            Context.Users.Add(user);
            Context.SaveChanges();
            return true;
        }


        public string UserLogin(LoginRequest login)
        {
            if (string.IsNullOrEmpty(login.EmailId) || string.IsNullOrEmpty(login.Password))
            {
                return null; ;
            }

            var User = Context.Users.FirstOrDefault(u => u.Email == login.EmailId);
            if (User == null)
            {
                return null;
            }
            if (User.Password == login.Password)

                if (!BCrypt.Net.BCrypt.Verify(login.Password, User.Password))
                {
                    return null;
                }
            String token = new GenerateTokenCLass().GenerateSecurityToken(login.EmailId, User.id, configuration);

            return token;
        }


        //public async Task<string> ForgotPasswordAsync(string EmailId, IBus bus)
        //{
        //    if (String.IsNullOrEmpty(EmailId))
        //    {
        //        return null;
        //    }
        //    var user = Context.Users.FirstOrDefault(u => u.Email == EmailId);

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var token = new GenerateTokenCLass().GenerateSecurityToken(EmailId, user.id, configuration);

        //    Sent send = new Sent();

        //    send.SendingMail(EmailId, token);

        //    Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
        //    var endPoint = await bus.GetSendEndpoint(uri);

        //    return token;
        //}
        public string ResetPassword(string Password, string EmailId)
        {
            var user = Context.Users.FirstOrDefault(u => u.Email == EmailId);

            if (user == null)
            {
                return null;
            }

            user.Password = EncryptPasswordClass.EcryptPassword(Password);
            Context.SaveChanges();
            return "Password Chenged successfully";

        }

        public string DeleteUser(long userId)
        {
            // Find the user to delete based on UserId
            var userToDelete = Context.Users.FirstOrDefault(u => u.id == userId);

            // Check if the user exists
            if (userToDelete == null)
            {
                return "User Not Found";
            }
            // Remove the user from the database and save changes
            Context.Users.Remove(userToDelete);
            Context.SaveChanges();
            return "User Deleted Successfully";
        }

        public string UpdateUser(long id)
        {
            throw new Exception("u");
        }

        public async Task<string> ForgotPassword(string EmailId, IBus bus)
        {
            if (String.IsNullOrEmpty(EmailId))
            {
                return null;
            }
            var user = Context.Users.FirstOrDefault(u => u.Email == EmailId);

            if (user == null)
            {
                return null;
            }

            var token = new GenerateTokenCLass().GenerateSecurityToken(EmailId, user.id, configuration);

            Sent send = new Sent();

            send.SendingMail(EmailId, token);

            Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
            var endPoint = await bus.GetSendEndpoint(uri);

            return token;

        }
    }
}
