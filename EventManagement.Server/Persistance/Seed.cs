using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using EventManagement.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Server.Persistance
{
    public class Seed
    {
        public static async Task SeedData(IApplicationBuilder applicationBuilder)
        {


            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();


                string Email = "shpatiradoniqi@gmail.com";

                var user = await userManager.FindByEmailAsync(Email);
                if (user == null)
                {
                    var newAppUser = new AppUser()
                    {
                        DisplayName = "Jetmir",
                        UserName = "jetmir",
                        Email= "jetmir@gmail.com"
                };
                    await userManager.CreateAsync(newAppUser, "Pa$$w0rd");
                }

            }
        }
    }
}




