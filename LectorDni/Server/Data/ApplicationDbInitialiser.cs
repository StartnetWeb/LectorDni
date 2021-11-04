using LectorDni.Server.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectorDni.Server.Data
{
    public class ApplicationDbInitialiser
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            AddRoleIfNotExists(roleManager, "Administrador");
            AddRoleIfNotExists(roleManager, "Operario");
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            (string name, string password, string role)[] demoUsers = new[]
            {
                (name: "admin@gmail.com", password: "Admin.20", role: "Administrador")
            };

            foreach ((string name, string password, string role) user in demoUsers)
            {
                AddUserIfNotExists(userManager, user);
            }

        }

        private static void AddUserIfNotExists(UserManager<ApplicationUser> userManager, (string name, string password, string role) demoUser)
        {
            ApplicationUser user = userManager.FindByNameAsync(demoUser.name).Result;
            if (user == default)
            {
                var newAppUser = new ApplicationUser
                {
                    UserName = demoUser.name,
                    Email = demoUser.name,
                    EmailConfirmed = true
                };
                _ = userManager.CreateAsync(newAppUser, demoUser.password).Result;
                if (!string.IsNullOrWhiteSpace(demoUser.role))
                {
                    var roles = demoUser.role.Split(',').Select(a => a.Trim()).ToArray();
                    Console.WriteLine($"{roles.Length}");
                    foreach (var role in roles)
                    {
                        _ = userManager.AddToRoleAsync(newAppUser, role).Result;

                    }
                }

            }
        }

        private static void AddRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (roleManager.FindByNameAsync(roleName).Result == default)
            {
                roleManager.CreateAsync(new IdentityRole { Name = roleName }).Wait();
            }
        }
    }
}
