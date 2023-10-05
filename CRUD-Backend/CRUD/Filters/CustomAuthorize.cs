using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq; 
namespace CRUD.Filters
{
  
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredRole;

        public CustomAuthorizeAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            bool role = user.Claims.Any(c => c.Type == "Role" && c.Value == _requiredRole);

            // Check if the user has the required role claim
            if (user != null && user.Identity.IsAuthenticated && role)
            {
                // User is authorized, do nothing
                return;
            }

            // User is not authorized, return a 403 Forbidden response
            context.Result = new StatusCodeResult(403); // You can customize the response as needed
        }
    }

}
