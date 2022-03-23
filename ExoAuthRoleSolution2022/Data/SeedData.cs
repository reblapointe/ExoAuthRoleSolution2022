using ExoAuthEtRoleSolution.Authorization;
using ExoAuthRoleSolution2022.Authorizations;
using ExoAuthRoleSolution2022.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExoAuthRoleSolution2022.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string adminPW)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            var adminID = await EnsureUser(serviceProvider, adminPW, "admin@cegepjonquiere.ca");
            await EnsureRole(serviceProvider, adminID, AuthorizationConstants.VetementAdministratorsRole);
            SeedDB(context, adminID);
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }
            if (user == null)
            {
                throw new Exception("Erreur dans la création du compte administrateur. Le mot de passe respecte-t-il les conditions du site?");
            }
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
                throw new Exception("roleManager null");

            if (!await roleManager.RoleExistsAsync(role))
                _ = await roleManager.CreateAsync(new IdentityRole(role));

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
                throw new Exception("Erreur. Le compte administrateur n'existe pas.");

            return await userManager.AddToRoleAsync(user, role);
        }

        public static void SeedDB(ApplicationDbContext context, string adminId)
        {
            if (context.Vetement.Any())
            {
                return;
            }
            context.Vetement.AddRange(
                new Vetement { Nom = "T-shirt bleu", ProprietaireId = adminId },
                new Vetement { Nom = "Camisole verte", ProprietaireId = adminId },
                new Vetement { Nom = "Chienne de travail", ProprietaireId = adminId },
                new Vetement { Nom = "Mom Jeans", ProprietaireId = adminId },
                new Vetement { Nom = "Espadrilles", ProprietaireId = adminId });
            context.SaveChanges();
        }
    }

}
