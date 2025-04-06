using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace PSI_application_C__web.Pages
{
    public class Page_prise_livraisonModel : PageModel
    {
        public List<List<string>> CommandesNonLivree { get; set; }

        public void OnGet()
        {
            TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
            this.CommandesNonLivree = new List<List<string>>();

            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand commander = connection.CreateCommand();
                commander.CommandText = 
                    "SELECT * " +
                    "FROM Commande " +
                    "WHERE ID_Commande NOT IN( " +
                    "   SELECT ID_Commande " +
                    "   FROM Livraison" +
                    ");";
                MySqlDataReader readerer;
                readerer = commander.ExecuteReader();
                string lecture_id = "";
                while (readerer.Read())
                {
                    List<string> stat = new List<string>();
                    //stat.Add(readerer["Nom_utilisateur"].ToString());
                    //stat.Add(readerer["Prenom_utilisateur"].ToString());
                    stat.Add(readerer["ID_Commande"].ToString());
                    stat.Add(readerer["Taille_Commande"].ToString());

                    CommandesNonLivree.Add(stat);
                }
                connection.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
