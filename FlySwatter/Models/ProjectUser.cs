﻿namespace FlySwatter.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ProjectUser
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Project Project { get; set; }
    }
}