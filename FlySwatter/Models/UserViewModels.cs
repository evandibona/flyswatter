namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;

    public partial class UserDetailsView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int TicketCount { get; set; } 

        public ICollection<Ticket> AssignedTickets { get; set; }
        public ICollection<Ticket> OwnedTickets { get; set; }
    }
}