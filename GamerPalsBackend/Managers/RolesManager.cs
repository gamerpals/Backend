﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using GamerpalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GamerPalsBackend.Managers
{
    public static class RolesManager
    {
        
        static RolesManager()
        {

        }
        public static ClaimsIdentity GetClaimsForUser(this MongoContext context, ObjectId userID)
        {
            var user = context.Users.Find(u => u._id == userID).SingleAsync().Result;
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Role, Role.VerifiedBlank));
            
            if (user != null)
            {
                try
                {
                    var role = context.Roles.Find(r => r._id == user.Role).Single();
                    Claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                Claims.Add(new Claim("UserID", userID.ToString()));
            }

            return new ClaimsIdentity(Claims.ToArray<Claim>());
        }
        public static void GrantRoleToUser(this MongoContext context, ObjectId userId, string rolename)
        {
            var user = context.Users.Single(u => u._id == userId);
            var role = context.Roles.Single(r => r.RoleName == rolename);
            user.Role = role._id;
            context.Users.ReplaceOne(u => u._id == user._id, user);
        }
        public static void RemoveRoleFromUser(this MongoContext context, ObjectId userId, string rolename)
        {
            var user = context.Users.Single(u => u._id == userId);
            var role = context.Roles.Single(r => r.RoleName == rolename);

            user.Role = new ObjectId((string)null);
            context.Users.ReplaceOne(u => u._id == user._id, user);
        }
    }
}
