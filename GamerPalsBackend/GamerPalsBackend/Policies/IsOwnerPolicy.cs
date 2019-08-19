using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;

namespace GamerPalsBackend.Policies
{
    public class IsOwnerPolicyRequirements : IAuthorizationRequirement
    {
    }
    public class IsOwnerPolicyHandler : AuthorizationHandler<IsOwnerPolicyRequirements, ObjectId>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsOwnerPolicyRequirements requirement, ObjectId res)
        {
            if (context.User.HasClaim(match => match.Type.Equals("UserID")) && context.User.FindFirst(c => c.Type.Equals("UserID")).Value.Equals(res.ToString()))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            //TODO: Use the following if targeting a version of
            //.NET Framework older than 4.6:
            //      return Task.FromResult(0);
            
            return Task.CompletedTask;
        }
    }
}
