using DatabaseLayer.DatabaseLogic.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;


namespace CRUD.Middlewares
{

    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // Let the request pass to the next middleware in the pipeline.
                await next(context);
            }
            catch (Exception ex)
            {
                //Response
                // Handle the exception and return a custom error response.
                context.Response.Clear();
               /* context.Response.StatusCode = 500; */// Internal Server Error

                // Customize the error response here, e.g., create a JSON response.
                Responses response = new Responses();
                response.Status = StatusCodes.Status500InternalServerError;
                response.StatusMsg = "Error";
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
        }
    }

}
