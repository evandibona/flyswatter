using System;
using System.Collections.Generic;

namespace FlySwatter.Models
{
    public class UserViewModel
    {
        public Dictionary<string, bool> Roles { get; set; }
    }
    public class EveryoneViewModel
    {
        public IList<UserViewModel> Users { get; set; }
    }
}