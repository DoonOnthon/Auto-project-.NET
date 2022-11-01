using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StageSchoolProjectAuto2.Models
{
    public class RDWModel
    {
        public string tellerstandoordeel { get; set; }
        public string Kenteken { get; set; }
        public string KentekenRandom { get; set; }
        public int KentekenID { get; set; }
        public string Merk { get; set; }
        public string KentekenInput { get; set; }
        public string handelsbenaming { get; set; }
        public int aantal_cilinders { get; set; }
        public string europese_voertuigcategorie { get; set; }
        public string inrichting { get; set; }
        public string datum_eerste_afgifte_nederland { get; set; }
        public string vervaldatum_apk { get; set; }
        public string wielbasis { get; set; }
        public string carSearch { get; set; }
        public int ID { get; set; }
    }
}