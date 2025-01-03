﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Server.Domain
{
    public class Comment
    {
     public Guid Id { get; set; }

     public string Body { get; set; }
     
     public virtual AppUser Author { get; set; }

     public virtual Activity Activity { get; set; }
     
     public DateTime CreatedAt { get; set; }
    }
}
