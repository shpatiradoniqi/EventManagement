using System;
using Microsoft.AspNetCore.Http;
using EventManagement.Server.Application.Photos;

namespace EventManagement.Server.Application.Interfaces
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file);
        string DeletePhoto(string publicId);
    }
}
