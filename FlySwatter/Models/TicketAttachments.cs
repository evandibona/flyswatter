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
    
    public partial class TicketAttachments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public int UserId { get; set; }
        public string FileUrl { get; set; }
    
        public virtual BugUsers BugUsers { get; set; }
        public virtual Tickets Tickets { get; set; }
    }
}
