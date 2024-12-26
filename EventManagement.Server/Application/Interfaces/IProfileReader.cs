using System;
using System.Threading.Tasks;
using EventManagement.Server.Application.Profiles;

namespace EventManagement.Server.Application.Interfaces
{
    public interface IProfileReader
    {
        Task<Profile> ReadProfile(string username); 
    }
}
