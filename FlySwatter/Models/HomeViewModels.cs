using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace FlySwatter.Models
{
    public class HomeView
    {
        public List<Ticket> Tickets { get; set; }
    }
}