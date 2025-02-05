﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Server.Application.Profiles;
using EventManagement.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Server.Controllers
{
    public class ProfilesController : BaseController
    {
        [HttpGet("{username}")]
        public async Task<ActionResult<Profile>> Get(string username)
        {
            return await Mediator.Send(new Details.Query { Username = username });
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Edit(Edit.Command command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet("{username}/activities")]
        public async Task<ActionResult<List<UserActivityDto>>> GetUserActivities(string username, string predicate)
        {
            return await Mediator.Send(new ListaActivities.Query { Username = username, Predicate = predicate });
        }
    }
}
