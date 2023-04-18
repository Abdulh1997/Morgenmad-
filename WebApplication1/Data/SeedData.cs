﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApplication1.Data
{
    public class SeedData
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            // User information
            const string receptionistEmail = "Receptionist@localhost";
            const string receptionistPassword = "Secret7$";
            const string waiterEmail = "Waiter@localhost";
            const string waiterPassword = "Secret7$";
            const string customerEmail = "Employee@localhost";
            const string customerPassword = "Secret7$";

            // Overordnet tjek af userManager
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            // Receptionist seed
            if (userManager.FindByNameAsync(receptionistEmail).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = receptionistEmail,
                    Email = receptionistEmail,
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, receptionistPassword).Result;

                if (result.Succeeded)
                {
                    var receptionistUser = userManager.FindByNameAsync(receptionistEmail).Result;
                    var claims = new List<Claim>()
                    {
                        new("IsReceptionist", "true"),
                        new("IsWorker", "true"),
                    };
                    var claimAdded = userManager.AddClaimsAsync(receptionistUser, claims).Result;
                }
            }

            // Waiter seed
            if (userManager.FindByNameAsync(waiterEmail).Result == null)
            {
                var user = new IdentityUser();
                user.UserName = waiterEmail;
                user.Email = waiterEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, waiterPassword).Result;

                if (result.Succeeded)
                {
                    var waiterUser = userManager.FindByNameAsync(waiterEmail).Result;
                    var claims = new List<Claim>()
                    {
                        new Claim("IsWaiter", "true"),
                        new Claim("IsWorker", "true"),
                    };

                    var claimAdded = userManager.AddClaimsAsync(waiterUser, claims).Result;
                }
            }

            // Customer seed
            if (userManager.FindByNameAsync(customerEmail).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = customerEmail,
                    Email = customerEmail,
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, customerPassword).Result;
            }
        }
    }
}
