using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Mongo;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;

namespace GamerPalsBackend.Policies
{
    public class IsInFriendsListPolicyRequirements : IAuthorizationRequirement
    {
    }
    public class IsInFriendsListPolicyHandler : AuthorizationHandler<IsInFriendsListPolicyRequirements, ObjectId>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsInFriendsListPolicyRequirements requirement, ObjectId res)
        {
            if (context.User.HasClaim(match => match.Type.Equals("UserID")))
            {
                var requesterID = context.User.Claims.Single(match => match.Type.Equals("UserID")).Value;
                if (new MongoHelper<User>(new MongoContext()).Get(res).Result.FriendsList
                    .Contains(new ObjectId(requesterID)))
                {
                    context.Succeed(requirement);
                }
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