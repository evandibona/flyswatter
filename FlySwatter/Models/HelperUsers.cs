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

    public class RoleHelper
    {
        private UserManager<ApplicationUser> uman =
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public bool IsUserInRole(string userId, string role)
        {
            return uman.IsInRole(userId, role);
        }
    }
}