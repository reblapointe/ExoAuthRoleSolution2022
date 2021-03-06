using System.Threading.Tasks;
using ExoAuthRoleSolution2022.Authorizations;
using ExoAuthRoleSolution2022.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ExoAuthEtRoleSolution.Authorization
{
    // Gestionnaire de l'autorisation de l'administrateur :
    // Détermine si un utilisateur a les droit d'administrateur
    public class VetementAdminAuthorizationHandler
                  : AuthorizationHandler<OperationAuthorizationRequirement, Vetement>
    {
        protected override Task HandleRequirementAsync(
                                    AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                    Vetement resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(AuthorizationConstants.VetementAdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
