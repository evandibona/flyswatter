using System;
using System.Collections.Generic;

namespace FlySwatter.Models
{
    public class UserViewModel
    {
        public Dictionary<string, bool> Roles { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}