using EventManagement.Server.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;

namespace EventManagement.Server.Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {

    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly IHttpContextAccessor __httpContextAccessor;
        private readonly DataContext _context;

        public IsHostRequirementHandler(IHttpContextAccessor httpContextAccessor,
            DataContext context)
        {
            _context = context;
            __httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsHostRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext authContext)
            {
                var currentUserName = __httpContextAccessor.HttpContext.User?.Claims?
                    .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                var activityId = Guid.Parse(authContext.RouteData.Values["id"].ToString());

                var activity = _context.Activities.FindAsync(activityId).Result;

                var host = activity.UserActivities.FirstOrDefault(x => x.IsHost);

                if (host?.AppUser?.UserName == currentUserName)
                    context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
