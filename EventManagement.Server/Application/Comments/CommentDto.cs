﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Server.Application.Comments
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }

        public string Image { get; set; }

    }
}
