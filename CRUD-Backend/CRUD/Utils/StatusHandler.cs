using DatabaseLayer.DatabaseLogic.Models;
using Microsoft.AspNetCore.Mvc;


namespace CRUD.Utils
{
    public static class StatusHandler
    {
        //proceess Responses
        public static Responses ProcessHttpStatusCode(object data, string errorMsg)
        {
            Responses response = new Responses();
            response.Status = StatusCodes.Status200OK;

            if(!string.IsNullOrEmpty(errorMsg))
            {
                response.Data = data;
                response.StatusMsg = "Success";
                response.ResponseMessage = errorMsg;
                return response;
            }
            else
            {
                response.Status = StatusCodes.Status500InternalServerError;
                response.StatusMsg = "Failed";
                response.ResponseMessage = errorMsg;
                return response;
            }

            
        }
    }
}
