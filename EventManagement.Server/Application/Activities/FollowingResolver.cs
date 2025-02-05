﻿using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EventManagement.Server.Application.Interfaces;
using EventManagement.Server.Domain;
using EventManagement.Server.Persistance;

namespace EventManagement.Server.Application.Activities
{
    public class FollowingResolver : IValueResolver<UserActivity, AttendeeDto, bool >
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public FollowingResolver(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public bool Resolve(UserActivity source, AttendeeDto destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUserName()).Result;

            if (currentUser.Followings.Any(x => x.TargetId == source.AppUserId))
                return true;

            return false;
        }
    }
}
