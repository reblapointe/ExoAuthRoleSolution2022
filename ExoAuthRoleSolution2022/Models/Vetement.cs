using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExoAuthRoleSolution2022.Models
{
    public class Vetement
    {
        public int VetementId { get; set; }
        public string Nom { get; set; }
        public string ProprietaireId { get; set; }
    }
}
