using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;

namespace PSI_application_C__web.Pages
{
    public class Page_statistiqueModel : PageModel
    {
        //public List<List<string>> CookStats { get; set; }
        
        public void OnGet()
        {
            
        }
    }
}

/*
 
 TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
            this.CookStats = new List<List<string>>();

            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand commander = connection.CreateCommand();
                commander.CommandText = "SELECT * FROM Cuisinier;";
                MySqlDataReader readerer;
                readerer = commander.ExecuteReader();
                string lecture_id = "";
                while (readerer.Read())
                {
                    List<string> stat = new List<string>();
                    //stat.Add(readerer["Nbre_plat_proposé_Cuisinier"].ToString());
                    //stat.Add(readerer["Plat_du_jour_Cuisinier"].ToString());
                    //stat.Add(readerer["Nbre_commandes_cuisinees_cuisinier"].ToString());

                    CookStats.Add(stat);
                }
                connection.Close();

                MySqlConnection connection2 = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand commander2 = connection.CreateCommand();
                commander.CommandText =
                    "SELECT * FROM Utilisateur WHERE ID_utilisateur IN (SELECT ID_utilisateur FROM Cuisinier);";
                MySqlDataReader readerer2 = commander.ExecuteReader();
                lecture_id = "";
                int count = 0;
                while (readerer.Read() && count<CookStats.Count)
                {
                    //CookStats[count].Add(readerer2["Nom_utilisateur"].ToString());
                    //CookStats[count].Add(readerer2["Prenom_utilisateur"].ToString());
                    count++;
                }
                connection2.Close();

                foreach(List<string> stat in CookStats)
                {
                    foreach(string element in stat)
                    {
                        Console.WriteLine(element);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {

            }
 */