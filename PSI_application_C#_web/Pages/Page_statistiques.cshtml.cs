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
        public List<List<string>> CookStats { get; set; }
        
        public void OnGet()
        {
            this.CookStats = new List<List<string>>();

            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
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
                    stat.Add(readerer["Nom_utilisateur"].ToString());
                    stat.Add(readerer["Prenom_utilisateur"].ToString());
                    stat.Add(readerer["nbre_plat_propose_cuisinier"].ToString());
                    stat.Add(readerer["plat_du_jour_cuisinier"].ToString());
                    stat.Add(readerer["nbre_commandes_cuisinees_cuisinier"].ToString());

                    CookStats.Add(stat);
                }
                connection.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
