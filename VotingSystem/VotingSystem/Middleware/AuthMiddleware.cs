using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Middleware
{
    public class AuthMiddleware : IMiddleware
    {
        private readonly IVotingSystemDb _db;

        public AuthMiddleware(IVotingSystemDb db)
        {
            _db = db;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claimedUsername = context.User?.Identity?.Name;
            if (claimedUsername == null)
            {
                await next(context);
                return;
            }

            var user = _db.AuthRepo.FindUserByUsername(claimedUsername);
            if (user.Locked)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;       
            }
            else
            {
                await next(context);
            }
        }
    }
}
