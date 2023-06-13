using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace interview.Helper;

    public class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Return a custom response body for unauthorized requests
                var response = new
                {
                    StatusCode = 401,
                    Message = "Unauthorized access"
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = 401
                };
            }
        }
    }

