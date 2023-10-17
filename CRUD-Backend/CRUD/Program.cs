using BusinessLayer;
using BusinessLayer.BL_layer;
using BusinessLayer.RespositoryLayer;
using CRUD.BL_layer;
using CRUD.Middlewares;
using CRUD.RespositoryLayer;
using CrudValidation;
using CrudValidation.LoginValidation;
using DatabaseLayer;
using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.DatabseAbstraction;
using DatabaseLayer.DTO;
using DatabaseLayer.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using System.Text;

namespace CRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            QuestPDF.Settings.License = LicenseType.Community;


            //Registering DI db Context
            builder.Services.AddTransient<CrudContext>();

            //Registering DL layer
            builder.Services.RegisterDlServices();

            //Register BL layer
            builder.Services.RegisterBlServices();

            //Register Validation Services
            builder.Services.RegisterValidationServices();

            builder.Services.AddSingleton<ErrorHandlerMiddleware>();

          
            //Adding the Authentication Services 
            //it should emphasis that it should be default JwtBearer is Default Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                //configuting that Authentication
                .AddJwtBearer(options =>
                {
                    //how the JWT incoming Token Should Be Validated
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //It creates a SymmetricSecurityKey using the secret key obtained 
                        //from the application's configuration settings (typically from AppSettings:Token).
                        //The application will use this key to verify the authenticity
                        //and integrity of incoming JWTs.
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("TokenName").Value)),
                        ValidateIssuer = false, // Issuer validation is disabled
                        ValidateAudience = false, // Audience validation is disabled
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });



            var app = builder.Build();

            

            //Configure the HTTP request pipeline.        
            app.UseHttpsRedirection();

            //Enable CROS
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthentication();

            app.UseAuthorization();



            //Handling Error
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();

            

            app.Run();
        }
    }
}