using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Context;
using RepositoryLayer.interfaces;
using RepositoryLayer.Services;
using System.Text;

namespace Fundoo_Application_6vi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swiagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            //swagger authorization code
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }
});
            });


            //layes abstaction code for User
            builder.Services.AddTransient<IUserBusiness, UserBusiness>();
            builder.Services.AddTransient<IUserRegister, UserRegister>();
          /*  builder.Services.AddTransient<IUserBusinessLogin,UserBusenessLogin>();
            builder.Services.AddTransient<IuserRepoLogin, UserRepoLogin>();
            builder.Services.AddTransient<IuserBusinessDelete,UserBusinessDelete>();
            builder.Services.AddTransient<IUserRepoDelete,UserRepoDelete>();
            builder.Services.AddTransient<IUserForgotPassword, UserForgotPassword>();
            builder.Services.AddTransient<IUserRepoForgetPassword, UserRepoForgotPassword>();
            builder.Services.AddTransient<IuserBusinessResetPassword, UserBusinessResetPassword>(); 
            builder.Services.AddTransient<IUserRepoResetPassword,UserRepoREsetPassword>();*/
            //Note  Entity 
            builder.Services.AddTransient<INoteEntityBusiness,NoteEntityBusinessService>();
            builder.Services.AddTransient<INoteEntityRepo,NoteEntityRepoServicecs>();
            
            //REview Entity
            builder.Services.AddTransient<IReviewBusiness,ReviewBusinussService>();
            builder.Services.AddTransient<IReviewRepo, ReviewRepoService>();

            builder.Services.AddDbContext<FundooContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString:FundooDb"]));

           


            //jwt token generatator registraion code step-1
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            //Rabit mq registration code 
            builder.Services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });
            builder.Services.AddMassTransitHostedService();




            //radis cache xode
          builder.Services.AddDistributedRedisCache(
          options =>
          {
            options.Configuration = "localhost:6379";
          }
         );


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
