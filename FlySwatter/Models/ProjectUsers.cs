namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ProjectUsers
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}