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
                (name: "Invitado_4", email: "InvitedUser4@gmail.com", password: "AppLDni.4", role: "Operario"),
                (name: "Invitado_5", email: "InvitedUser5@gmail.com", password: "AppLDni.5", role: "Operario"),
                (name: "Invitado_6", email: "InvitedUser6@gmail.com", password: "AppLDni.6", role: "Operario"),
                (name: "Invitado_7", email: "InvitedUser7@gmail.com", password: "AppLDni.7", role: "Operario"),
                (name: "Invitado_8", email: "InvitedUser8@gmail.com", password: "AppLDni.8", role: "Operario"),
                (name: "Invitado_9", email: "InvitedUser9@gmail.com", password: "AppLDni.9", role: "Operario"),
                (name: "Invitado_10", email: "InvitedUser10@gmail.com", password: "AppLDni.10", role: "Operario"),
                (name: "Invitado_11", email: "InvitedUser11@gmail.com", password: "AppLDni.11", role: "Operario"),
                (name: "Invitado_12", email: "InvitedUser12@gmail.com", password: "AppLDni.12", role: "Operario"),
                (name: "Invitado_13", email: "InvitedUser13@gmail.com", password: "AppLDni.13", role: "Operario"),
                (name: "Invitado_14", email: "InvitedUser14@gmail.com", password: "AppLDni.14", role: "Operario"),
                (name: "Invitado_15", email: "InvitedUser15@gmail.com", password: "AppLDni.15", role: "Operario"),
                (name: "Invitado_16", email: "InvitedUser16@gmail.com", password: "AppLDni.16", role: "Operario"),
                (name: "Invitado_17", email: "InvitedUser17@gmail.com", password: "AppLDni.17", role: "Operario"),
                (name: "Invitado_18", email: "InvitedUser18@gmail.com", password: "AppLDni.18", role: "Operario"),
                (name: "Invitado_19", email: "InvitedUser19@gmail.com", password: "AppLDni.19", role: "Operario"),
                (name: "Invitado_20", email: "InvitedUser20@gmail.com", password: "AppLDni.20", role: "Operario"),
                (name: "Invitado_21", email: "InvitedUser21@gmail.com", password: "AppLDni.21", role: "Operario"),
                (name: "Invitado_22", email: "InvitedUser22@gmail.com", password: "AppLDni.22", role: "Operario"),
                (name: "Invitado_23", email: "InvitedUser23@gmail.com", password: "AppLDni.23", role: "Operario"),
                (name: "Invitado_24", email: "InvitedUser24@gmail.com", password: "AppLDni.24", role: "Operario"),
                (name: "Invitado_25", email: "InvitedUser25@gmail.com", password: "AppLDni.25", role: "Operario"),
                (name: "Invitado_26", email: "InvitedUser26@gmail.com", password: "AppLDni.26", role: "Operario"),
                (name: "Invitado_27", email: "InvitedUser27@gmail.com", password: "AppLDni.27", role: "Operario"),
                (name: "Invitado_28", email: "InvitedUser28@gmail.com", password: "AppLDni.28", role: "Operario"),
                (name: "Invitado_29", email: "InvitedUser29@gmail.com", password: "AppLDni.29", role: "Operario"),
                (name: "Invitado_30", email: "InvitedUser30@gmail.com", password: "AppLDni.30", role: "Operario")
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
