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
            response.StatusMsg = "Success";

            if(!string.IsNullOrEmpty(errorMsg))
            {
                response.Data = data; 
                response.ResponseMessage = errorMsg;
                return response;
            }
            else
            {
                response.Status = StatusCodes.Status500InternalServerError;
                response.StatusMsg = "Error";
                response.ResponseMessage = "Records not Found";
                return response;
            }

            
        }
    }
}
