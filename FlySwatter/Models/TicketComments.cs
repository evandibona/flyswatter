namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;

    public partial class TicketComment
    {
        public TicketComment()
        {
        }
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset Created { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}