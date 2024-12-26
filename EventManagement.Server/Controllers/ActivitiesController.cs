﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Server.Application.Activities;
using System.Threading.Tasks;
using System;

namespace EventManagement.Server.Controllers
{
    public class ActivitiesController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<List.ActivitiesEnvelope>> List(int? limit,
            int? offset, bool isGoing, bool isHost, DateTime? startDate)
        {
            return await Mediator.Send(new List.Query(limit, offset, isGoing, isHost, startDate));
        }

        [HttpGet("{id}")]
        //[Authorize]       
        [AllowAnonymous]
        public async Task<ActionResult<ActivityDto>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });

        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut("{id}")]
        //[Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {

            command.Id = id;
            return await Mediator.Send(command);

        }
        [HttpDelete("{id}")]
        //[Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }

        [HttpPost("{id}/attend")]

        public async Task<ActionResult<Unit>> Attend(Guid id)
        {
            return await Mediator.Send(new Attend.Command { Id = id });
        }
        [HttpDelete("{id}/attend")]

        public async Task<ActionResult<Unit>> Unattend(Guid id)
        {
            return await Mediator.Send(new Unattend.Command { Id = id });
        }
    }
}