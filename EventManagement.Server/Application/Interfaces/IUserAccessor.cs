﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Server.Application.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUserName();

    }
}
