using BusinessLayer.BL_layer;
using BusinessLayer.RespositoryLayer;
using CRUD.BL_layer;
using CRUD.Middlewares;
using CRUD.RespositoryLayer;
using DatabaseLayer.DatabaseAbstraction;
using DatabaseLayer.DatabaseLogic;
using DatabaseLayer.DatabaseLogic.Models;
using DatabaseLayer.DatabseAbstraction;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            //Adding DI Db context
            builder.Services.AddTransient<CrudContext>();

            //Adding DI for DL layer
            builder.Services.AddTransient<IDLSlqLogic, DLSlqLogic>();

            //Adding DI for BL layer
            builder.Services.AddTransient<IBlSqlLogic, BlSqlLogic>();

            //Adding DI for DL layer --pagination
            builder.Services.AddTransient<IDLPagination, DLPagination>();

            //Adding DI for BL layer  --pagination
            builder.Services.AddTransient<IBLPagination, BLPagination>();

            //injecting Error Middle ware
            builder.Services.AddTransient<ErrorHandlerMiddleware>();

            var app = builder.Build();

            //Configure the HTTP request pipeline.        
            app.UseHttpsRedirection();

            //Enable CROS
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            //Handling Error
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}