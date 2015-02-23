﻿namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;

    public partial class TicketHistory
    {
        public TicketHistory()
        {
        }

        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTimeOffset Changed { get; set; }
        public string UserId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}