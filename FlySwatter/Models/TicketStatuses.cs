//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TicketStatuses
    {
        public TicketStatuses()
        {
            this.Tickets = new HashSet<Tickets>();
            this.Tickets1 = new HashSet<Tickets>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<Tickets> Tickets1 { get; set; }
    }
}
