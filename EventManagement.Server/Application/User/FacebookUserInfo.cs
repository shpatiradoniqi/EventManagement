﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Server.Application.User
{
    public class FacebookUserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public FacebookPictureData Picture { get; set; }
    }

    public class FacebookPictureData
    {
        public FacebookPicture Data { get; set; }
    }

    public class FacebookPicture
    {
        public string Url { get; set; }
    }
}

