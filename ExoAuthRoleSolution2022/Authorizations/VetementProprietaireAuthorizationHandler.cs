using ExoAuthRoleSolution2022.Authorizations;
using ExoAuthRoleSolution2022.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExoAuthEtRoleSolution.Authorization
{
    public class VetementProprietaireAuthorizationHandler
                 : AuthorizationHandler<OperationAuthorizationRequirement, Vetement>
    {
        readonly UserManager<IdentityUser> _userManager;

        public VetementProprietaireAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Vetement resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != AuthorizationConstants.CreateOperationName &&
                requirement.Name != AuthorizationConstants.ReadOperationName &&
                requirement.Name != AuthorizationConstants.UpdateOperationName &&
                requirement.Name != AuthorizationConstants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.ProprietaireId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
