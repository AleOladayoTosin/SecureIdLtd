﻿using Microsoft.AspNetCore.Identity;
using SecureId.AccessControl.Domain;
using System.Data;

namespace SecureId.AccessControl.Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>() {
                 new IdentityRole() { Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                 new IdentityRole() { Name = "Client-User", NormalizedName = "CLIENTUSER" },
                 new IdentityRole() { Name = "Viewer", NormalizedName = "VIEWER" }
                };

                 foreach (var role in roles)
                {
                    var roleExists = await roleManager.RoleExistsAsync(role.Name);

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
            };
            
           
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>{
                    new AppUser{DisplayName = "Oladayo Ale", UserName = "oladayo", Email = "oladayo.ale@secureidltd.com"},
                    new AppUser{DisplayName = "Zoe Ale", UserName = "Zoe", Email = "zoe.ale@secureidltd.com"}
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Day@1993");
                    await userManager.AddToRoleAsync(user, "SuperAdmin");
                }
            }
            if (context.Activities.Any()) return;

            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue = "Pub",
                },
                new Activity
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Paris",
                    Venue = "Louvre",
                },
                new Activity
                {
                    Title = "Future Activity 1",
                    Date = DateTime.UtcNow.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Category = "culture",
                    City = "London",
                    Venue = "Natural History Museum",
                },
                new Activity
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "O2 Arena",
                },
                new Activity
                {
                    Title = "Future Activity 3",
                    Date = DateTime.UtcNow.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Another pub",
                },
                new Activity
                {
                    Title = "Future Activity 4",
                    Date = DateTime.UtcNow.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Yet another pub",
                },
                new Activity
                {
                    Title = "Future Activity 5",
                    Date = DateTime.UtcNow.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Just another pub",
                },
                new Activity
                {
                    Title = "Future Activity 6",
                    Date = DateTime.UtcNow.AddMonths(6),
                    Description = "Activity 6 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "Roundhouse Camden",
                },
                new Activity
                {
                    Title = "Future Activity 7",
                    Date = DateTime.UtcNow.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new Activity
                {
                    Title = "Future Activity 8",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Cinema",
                }
            };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}