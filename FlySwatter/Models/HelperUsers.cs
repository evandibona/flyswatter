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
    }
}