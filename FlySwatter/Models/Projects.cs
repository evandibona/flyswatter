namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Project
    {
        public Project()
        {
            this.BugUsers = new HashSet<BugUser>(); 
            this.Tickets = new HashSet<Ticket>(); 
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BugUser> BugUsers { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}