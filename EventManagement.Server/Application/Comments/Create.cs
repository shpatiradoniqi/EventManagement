﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EventManagement.Server.Application.Errors;
using EventManagement.Server.Domain;
using EventManagement.Server.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Server.Application.Comments
{
    public class Create
    {
        public class Command : IRequest<CommentDto>
        {
            public string Body { get; set; }
            public Guid ActivityId { get; set; }
            public string Username { get; set; }

        }
        public class Handler : IRequestHandler<Command, CommentDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

             
            }
            public async Task<CommentDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.ActivityId);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Not Found" });


               var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == request.Username);

                var comment = new Comment
                {
                    Author = user,
                    Activity = activity,
                    Body = request.Body,
                    CreatedAt = DateTime.Now
                };

                activity.Comments.Add(comment);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return _mapper.Map<CommentDto>(comment);
                throw new Exception("Problem saving the data");

            }
        }

    }
}
