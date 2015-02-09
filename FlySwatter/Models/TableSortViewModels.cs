using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace FlySwatter.Models
{
    public class TicketTableViewModel
    {
        public Dictionary<string, char> Sorters { get; set; }
        public Dictionary<string, string> Search { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}