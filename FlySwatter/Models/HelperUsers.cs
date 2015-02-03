namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Web.Security; 
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using FlySwatter.Models;

    public static class MethodHelpers
    {
        private static UserManager<ApplicationUser> uman =
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public static bool IsInRole(this ApplicationUser user, string role)
        {
            var userId = user.Id; 
            return uman.IsInRole(userId, role);
        }
    }
    public class ManageHelpers
    {
        private UserManager<ApplicationUser> uman =
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext())); 

        public bool UserIsInRole(string id, string role)
        {
            return uman.IsInRole(id, role); 
        }

        public string[] UserHasRoles(string id)
        {
            IList<string> a = uman.GetRoles(id); 
            return a.ToArray(); 
        }
        
        public void AddToRole(string id, string role)
        {
            uman.AddToRole(id, role); 
        }
        
        public void RemoveFromRole(string id, string role)
        {
            uman.RemoveFromRole(id, role); 
        }
    }
}