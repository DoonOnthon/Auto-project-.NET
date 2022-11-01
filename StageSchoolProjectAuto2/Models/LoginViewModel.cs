using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StageSchoolProjectAuto2.Models
{
    public class LoginViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Gebruikersnaam { get; set; }
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Wachtwoord { get; set; }
    }
}