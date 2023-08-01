using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using StageSchoolProjectAuto2.Models;
using SODA;
using System.Globalization;
using System.Net.Http;

namespace StageSchoolProjectAuto2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home  
        [HttpGet]
        public ActionResult EmployeeMaster()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        [HttpPost]
        public ActionResult About(RDWModel TestModel)
        {
            {
                string constr = ConfigurationManager.ConnectionStrings["SimpleDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                //Insert into database
                {
                    string query2 = "INSERT INTO Kentekens(Kenteken) VALUES(@Kenteken)";
                    query2 += " SELECT SCOPE_IDENTITY()";
                    using (SqlCommand cmd = new SqlCommand(query2))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@Kenteken", TestModel.KentekenInput);
                        TestModel.KentekenID = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                }
            }
            var client = new SodaClient("https://opendata.rdw.nl");
            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<RDWModel>("m9d7-ebf2");
            //
            var query = new SoqlQuery().Select("Merk, Kenteken, handelsbenaming, tellerstandoordeel, europese_voertuigcategorie, wielbasis ,aantal_cilinders, datum_eerste_afgifte_nederland, inrichting, vervaldatum_apk")
                                                     //Kenteken input van eerder
                                                     .Where($"kenteken = '{TestModel.KentekenInput}'")
                                                     .Limit(20);
            //Primaire gegevens
            var rows = dataset.Query(query);
            foreach (var keyValue in rows)  

                if (keyValue.Kenteken == null)
                {
                    Console.WriteLine("Fill");
                }
                else
                {
                    if (keyValue.tellerstandoordeel == null)
                    {
                        ViewBag.tellerstandoordeel = ($"{keyValue.tellerstandoordeel}");
                    }
                    else
                    {
                        ViewBag.tellerstandoordeel = ($"{keyValue.tellerstandoordeel}");
                    }
                    //Inrichting / type van de auto ophalen
                    if (keyValue.inrichting == null)
                    {
                        ViewBag.inrichting = ($"Geen handelsbenaming kunnen ophalen");
                    }
                    else
                    {
                        ViewBag.inrichting = ($"{keyValue.inrichting}");
                    }
                    //Het merk van de auto ophalen
                    if (keyValue.Merk == null)
                    {
                        ViewBag.MerkOutput = ("Merk van de auto is niet bekend");
                    }
                    else
                    {
                        ViewBag.MerkOutput = ($"{keyValue.Merk}");
                    }
                    //Het kenteken weer weergeven
                    if (keyValue.Kenteken == null)
                    {
                        ViewBag.KentekenOutput = ("Kenteken is niet bekend}");
                    }
                    else
                    {
                        ViewBag.KentekenOutput = ($" {keyValue.Kenteken}");
                    }
                    //De handelsbenaming van de auto ophalen
                    if (keyValue.handelsbenaming == null)
                    {
                        ViewBag.handelsbenaming = ("Handelsbenaming is niet bekend");
                    }
                    else
                    {
                        ViewBag.handelsbenaming = ($"{keyValue.handelsbenaming}");
                    }
                    //Het europese voertuigs categorie ophalen
                    if (keyValue.europese_voertuigcategorie == null)
                    {
                        ViewBag.europese_voertuigcategorie = ("De europese voertuigs categorie is niet bekend");
                    }
                    else
                    {
                        ViewBag.europese_voertuigcategorie = ($"{keyValue.europese_voertuigcategorie}");
                    }
                    ViewBag.aantal_cilinders = ($"{keyValue.aantal_cilinders}");

                    //Verval datum van de apk
                    if (keyValue.vervaldatum_apk == null)
                    {
                        Console.WriteLine("Apk verval datum kon niet worden gevonden");
                    }
                    else
                    {
                        string[] dateStrings1 = { keyValue.vervaldatum_apk };

                        DateTime parsedDate2;

                        foreach (var dateString2 in dateStrings1)
                        {
                            if (DateTime.TryParseExact(dateString2, "yyyyMMdd", null,
                                                        System.Globalization.DateTimeStyles.AllowWhiteSpaces,
                                                        out parsedDate2))
                            {
                                ViewBag.vervaldatum_apk = (parsedDate2.ToString("dd'/'MM'/'yyyy"));
                            }
                        }
                    }
                    //Datum eerste afgifte Nederland
                    if (keyValue.datum_eerste_afgifte_nederland == null)
                    {
                        ViewBag.datum_eerste_afgifte_nederland = ("Datum eerste afgifte in Nederland kon niet worden gevonden");
                    }
                    else
                    {
                        string[] dateStrings = { keyValue.datum_eerste_afgifte_nederland };

                        DateTime parsedDate;

                        foreach (var dateString in dateStrings)
                        {
                            if (DateTime.TryParseExact(dateString, "yyyyMMdd", null,
                                                        System.Globalization.DateTimeStyles.AllowWhiteSpaces,
                                                        out parsedDate))
                            {
                                ViewBag.datum_eerste_afgifte_nederland = (parsedDate.ToString("dd'/'MM'/'yyyy"));
                            }
                        }
                    }
                    if (keyValue.Kenteken == null)
                    {
                        ViewBag.wielbasis = ("Wielbasis is niet bekend}");
                    }
                    else
                    {
                        ViewBag.wielbasis = ($" {keyValue.wielbasis}mm");
                    }
                    ViewBag.carSearch = ($"{keyValue.Merk}" + $" {keyValue.handelsbenaming}");
                }
            //gegevens met een datum
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Registratie()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpPost]
        //Connect to database
        public ActionResult Index(Registratie Users)
        {
            string constr = ConfigurationManager.ConnectionStrings["SimpleDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            //Insert into database
            {
                string query = "INSERT INTO Users(Gebruikersnaam, Wachtwoord, Email, Age) VALUES(@Gebruikersnaam, @Wachtwoord, @Email, @Age)";
                query += " SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Gebruikersnaam", Users.Gebruikersnaam);
                    cmd.Parameters.AddWithValue("@Wachtwoord", Users.Wachtwoord);
                    cmd.Parameters.AddWithValue("@Email", Users.Email);
                    cmd.Parameters.AddWithValue("@Age", Users.Age);
                    Users.ID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            //after inserting into the database
            if (ModelState.IsValid)
            {
                ViewBag.Message = "User Details Saved";
                return View("Registratie");
            }
            else
            {
                return View("Index");
            }
        }
        public ActionResult Inloggen()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inloggen(Users u)
        {
            // this action is to handle post (login)
            if (ModelState.IsValid)
            {
                using (TEST_StagiairEntities dc = new TEST_StagiairEntities())
                {
                    var v = dc.Users.Where(a => a.Gebruikersnaam.Equals(u.Gebruikersnaam) && a.Wachtwoord.Equals(u.Wachtwoord)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.Gebruikersnaam.ToString();
                        Session["LogedUserFullname"] = v.Age.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(u);
        }
        [HttpGet]
        public ActionResult AfterLogin()
        {
            string connString = @"Data Source=DESKTOP-0K7399A;Initial Catalog=TEST_Stagiair;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            //Insert into database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT TOP 1 PERCENT Kenteken FROM Kentekens ORDER BY newid()";
                //int KentekenID;
                string Kenteken;
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //KentekenID = dr.GetInt32(0);
                            Kenteken = dr.GetString(0);
                            ViewBag.KentekenOut = (Kenteken);

                            var client = new SodaClient("https://opendata.rdw.nl");

                            //"SELECT TOP 1 PERCENT Kenteken FROM Kentekens ORDER BY newid()
                            var dataset = client.GetResource<RDWModel>("m9d7-ebf2");
                            //
                            var query1 = new SoqlQuery().Select("Kenteken, Merk, handelsbenaming, tellerstandoordeel, Inrichting ,europese_voertuigcategorie, wielbasis ,aantal_cilinders")
                                                                     //Kenteken input van eerder
                                                                     .Where($"kenteken = '{Kenteken}'")
                                                                     .Limit(20);
                            //Primaire gegevens
                            var rows = dataset.Query(query1);
                            foreach (var keyValue in rows)
                                if (keyValue.Merk == null)
                                {
                                    Console.WriteLine("Werkt niet");
                                }
                                else
                                {
                                    ViewBag.WilKenteken = Kenteken;
                                    ViewBag.WilMerk = keyValue.Merk;
                                    ViewBag.WilHandelsbenaming = keyValue.handelsbenaming;
                                    ViewBag.WilInrichting = keyValue.inrichting;
                                    ViewBag.WilTellerstandoordeel = keyValue.tellerstandoordeel;
                                    ViewBag.WilEUcat = keyValue.europese_voertuigcategorie;
                                    ViewBag.WilWielbasis = keyValue.wielbasis;
                                    ViewBag.WilaantalCilinder = keyValue.aantal_cilinders;
                                    keyValue.carSearch = ($"{keyValue.Merk}" + $" {keyValue.handelsbenaming}");
                                    ViewBag.carSearch = keyValue.carSearch;
                                    //insert
                                }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    dr.Close();
                    conn.Close();
                }

                return View();
            }
        }
        [HttpPost]
        public ActionResult AfterLogin(RDWModel Carfavs)
        {

            string connString = ConfigurationManager.ConnectionStrings["SimpleDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            //Insert into database
            {
                string query1 = "INSERT INTO Carfavs(Kenteken, Merk, Handelsbenaming, Inrichting, EUcat, AantalCilinders, Wielbasis, carSearch)" +
                "VALUES(@Kenteken, @Merk, @Handelsbenaming, @Inrichting, @EUcat, @AantalCilinders, @Wielbasis, @carSearch)";
                query1 += " SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con;
                    con.Open();
                    cmd1.Parameters.AddWithValue("@Kenteken", Carfavs.Kenteken);
                    cmd1.Parameters.AddWithValue("@Merk", Carfavs.Merk);
                    cmd1.Parameters.AddWithValue("@Handelsbenaming", Carfavs.handelsbenaming);
                    cmd1.Parameters.AddWithValue("@Inrichting", Carfavs.inrichting);
                    cmd1.Parameters.AddWithValue("@EUcat", Carfavs.europese_voertuigcategorie);
                    cmd1.Parameters.AddWithValue("@AantalCilinders", Carfavs.aantal_cilinders);
                    cmd1.Parameters.AddWithValue("@Wielbasis", Carfavs.wielbasis);
                    cmd1.Parameters.AddWithValue("@carSearch", Carfavs.carSearch);

                    Carfavs.ID = Convert.ToInt32(cmd1.ExecuteScalar());

                    con.Close();
                }
            }

            return View();
        }
        public ActionResult Logout(Users log)
        {
            // this action is to handle post (login)
            if (ModelState.IsValid)
            {
                using (TEST_StagiairEntities dc = new TEST_StagiairEntities())
                {
                    var v = dc.Users.Where(a => a.Gebruikersnaam.Equals(log.Gebruikersnaam) && log.Wachtwoord.Equals(log.Wachtwoord)).FirstOrDefault();
                    if (v == null)
                    Session["LogedUserFullname"] = null;
                    Session["LogedUserID"] = null;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Console.WriteLine("Werkt niet");
                return View();
            }
        }
    }
}