using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventManagement.Server.Application.Errors;
using EventManagement.Server.Application.Interfaces;
using EventManagement.Server.Application.Validators;
using EventManagement.Server.Domain;
using EventManagement.Server.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement.Server.Application.User
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string DisplayName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).Password();
            }
        }
        public class Handler : IRequestHandler<Command,User>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly DataContext _context;
            public Handler(UserManager<AppUser> userManager,  IJwtGenerator jwtGenerator, DataContext context)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }
            public async Task<User> Handle(Command request,CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email aleready Exist" });

                if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "UserName aleready Exist" });

                var user = new AppUser
                {
                    DisplayName=request.DisplayName,
                    Email=request.Email,
                    UserName=request.UserName
                };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded) {
                    return new User
                    {
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user),
                        Username = user.UserName,
                        Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
                    };
                } 
              
                throw new Exception("Problem creating a user");

            }

        }

    }
}
