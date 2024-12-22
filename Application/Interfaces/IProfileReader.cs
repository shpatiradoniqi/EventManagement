using System;
using System.Threading.Tasks;
using Reactivities.Application.Profiles;

namespace Reactivities.Application.Interfaces
{
    public interface IProfileReader
    {
        Task<Profile> ReadProfile(string username); 
    }
}
