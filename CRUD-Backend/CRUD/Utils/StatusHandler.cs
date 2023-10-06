using DatabaseLayer.DatabaseLogic.Models;
using Microsoft.AspNetCore.Mvc;


namespace CRUD.Utils
{
    public static class StatusHandler
    {
        //proceess Responses
        public static Responses ProcessHttpStatusCode(object data, string errorMsg,int statusCode)
        {
            Responses response = new Responses();
            if(statusCode == 200)
            {
                response.Status = StatusCodes.Status200OK;
                response.Data = data;
                response.StatusMsg = "Success";
                response.ResponseMessage = errorMsg;
                
            }else if(statusCode == 404)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Data = data;
                response.StatusMsg = "Failure";
                response.ResponseMessage = errorMsg;
            }else if(statusCode == 401)
            {
                response.Status = StatusCodes.Status401Unauthorized;
                response.Data = data;
                response.StatusMsg = "Failure";
                response.ResponseMessage = errorMsg;
            }else if(statusCode == 403)
            {
                response.Status = StatusCodes.Status403Forbidden;
                response.Data = data;
                response.StatusMsg = "Failure";
                response.ResponseMessage = errorMsg;
            }else if (statusCode == 204)
            {
                response.Status = StatusCodes.Status204NoContent;
                response.Data = data;
                response.StatusMsg = "Failure";
                response.ResponseMessage = errorMsg;
            }
            else if (statusCode == 400)
            {
                response.Status = StatusCodes.Status400BadRequest;
                response.Data = data;
                response.StatusMsg = "Failure";
                response.ResponseMessage = errorMsg;
            }
            else
            {
                response.Status = StatusCodes.Status500InternalServerError;
                response.Data = data;
                response.StatusMsg = "Failure";
                response.ResponseMessage = errorMsg;
            }

            return response;
        }
    }
}
