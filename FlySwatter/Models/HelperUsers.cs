using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity; 
using Microsoft.AspNet.Identity.EntityFramework;
using FlySwatter.Models; 

namespace FlySwatter.Models
{
    public static class UsersHelper
    {
        private static UserManager<ApplicationUser> uman = 
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>()); 
        // AppUser <--> BugUser
        // BU.appuser  AU.buguser
        public static ApplicationUser appuser( this BugUser buser)
        {
            var puser = uman.Users.First(u => u.Id == buser.UserId);
            return puser; 
        }
    }
}