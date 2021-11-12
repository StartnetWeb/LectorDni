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
            (string name, string email, string password, string role)[] demoUsers = new[]
            {
                (name: "Administrador", email: "admin@gmail.com", password: "Admin.20", role: "Administrador"),
                (name: "Invitado_1", email: "InvitedUser1@gmail.com", password: "AppLDni.1", role: "Operario"),
                (name: "Invitado_2", email: "InvitedUser2@gmail.com", password: "AppLDni.2", role: "Operario"),
                (name: "Invitado_3", email: "InvitedUser3@gmail.com", password: "AppLDni.3", role: "Operario"),
                (name: "Invitado_4", email: "InvitedUser4@gmail.com", password: "AppLDni.4", role: "Operario")
            };

            foreach ((string name, string email, string password, string role) user in demoUsers)
            {
                AddUserIfNotExists(userManager, user);
            }

        }

        private static void AddUserIfNotExists(UserManager<ApplicationUser> userManager, (string name, string email, string password, string role) demoUser)
        {
            ApplicationUser user = userManager.FindByNameAsync(demoUser.name).Result;
            if (user == default)
            {
                var newAppUser = new ApplicationUser
                {
                    UserName = demoUser.name,
                    Email = demoUser.email,
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(newAppUser, demoUser.password).Result;
                if (!string.IsNullOrWhiteSpace(demoUser.role))
                {
                    var roles = demoUser.role.Split(',').Select(a => a.Trim()).ToArray();
                    Console.WriteLine($"{roles.Length}");
                    foreach (var role in roles)
                    {
                        result = userManager.AddToRoleAsync(newAppUser, role).Result;
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
