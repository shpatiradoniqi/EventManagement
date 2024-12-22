using Reactivities.Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reactivities.Application.Interfaces
{
    public  interface IFacebookAccessor
    {
        Task<FacebookUserInfo> FacebookLogin(string accessToken);
    }
}
