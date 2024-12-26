using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EventManagement.Server.Application.Interfaces;
using EventManagement.Server.Application.Profiles;
using EventManagement.Server.Domain;
using EventManagement.Server.Persistance;

namespace EventManagement.Server.Application.Followers
{
    public class List
    {
        public class Query : IRequest<List<Profile>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Profile>>
        {
            private readonly DataContext _contex;
            private readonly IProfileReader _profileReader;

            public Handler(DataContext dataContext, IProfileReader profileReader)
            {
                _contex = dataContext;
                _profileReader = profileReader;
            }
            public async Task<List<Profile>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _contex.Followings.AsQueryable();

                var userFollowing = new List<UserFollowing>();
                var profiles = new List<Profile>();

                switch (request.Predicate)
                {
                    case "followers":
                    {
                        userFollowing = await queryable.Where(x => x.Target.UserName ==
                            request.Username).ToListAsync();

                        foreach(var follower in userFollowing)
                        {
                            profiles.Add(await _profileReader.ReadProfile
                                (follower.Observer.UserName));
                        }
                        break;
                    }

                    case "following":
                    {
                        userFollowing = await queryable.Where(x => x.Observer.UserName ==
                            request.Username).ToListAsync();

                        foreach (var follower in userFollowing)
                        {
                            profiles.Add(await _profileReader.ReadProfile
                                (follower.Target.UserName));
                        }
                        break;
                    }

                }

                return profiles;
            }
        }
    }
}
