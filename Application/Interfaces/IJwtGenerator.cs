using Reactivities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reactivities.Application.Interfaces
{
  public  interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
