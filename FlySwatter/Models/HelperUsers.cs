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

    public static class RoleHelper
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
}