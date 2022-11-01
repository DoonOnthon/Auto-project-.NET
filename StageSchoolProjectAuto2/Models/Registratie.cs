using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StageSchoolProjectAuto2.Models
{
    public class Registratie
    {
        [Required(ErrorMessage = "Vul een gebruikersnaam in")]
        public string Gebruikersnaam { get; set; }
        [Required(ErrorMessage = "Vul een wachtwoord in")]
        public string Wachtwoord { get; set; }
        [Required(ErrorMessage = "Vul een email in")]
        public string Email { get; set; }
        public int Age { get; set; }
        public int ID { get; set; }

    }
}