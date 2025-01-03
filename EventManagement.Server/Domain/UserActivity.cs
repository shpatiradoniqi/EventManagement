﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Server.Domain
{
    public class UserActivity
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}
