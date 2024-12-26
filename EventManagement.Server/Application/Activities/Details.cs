﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EventManagement.Server.Application.Errors;
using EventManagement.Server.Domain;
using EventManagement.Server.Persistance;
using System;   
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Server.Application.Activities
{
    public class Details
    {

        public class Query : IRequest<ActivityDto>
        {

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {

                var activity = await _context.Activities
                    .FindAsync(request.Id);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Not Found" });

                var activityToReturn = _mapper.Map<Activity, ActivityDto>(activity);

                return activityToReturn;
               
            }
        }


    }
}
