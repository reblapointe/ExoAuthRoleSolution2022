using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExoAuthRoleSolution2022.Authorizations
{
    // Contient les noms des opérations possibles dans l'application
    public class AuthorizationConstants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";

        public static readonly string VetementAdministratorsRole = "VetementAdministrators";
    }
}
