using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace PSI_application_C__web.Pages
{
    public class Page_mon_profileModel : PageModel
    {
        public List<string> StatUtilisateur;
        public bool Est_entreprise = false;
        public void OnGet()
        {
            this.StatUtilisateur = new List<string>();

            try
            {
                string id_utilisateur = "";
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand commander = connection.CreateCommand();
                commander.CommandText = "SELECT * FROM Utilisateur WHERE ID_Utilisateur = " + id_utilisateur + ";";
                MySqlDataReader readerer;
                readerer = commander.ExecuteReader();
                string lecture_id = "";
                Est_entreprise = Convert.ToBoolean(readerer["Utilisateur_est_entreprise"]);
                StatUtilisateur.Add(readerer["Nom_utilisateur"].ToString());
                StatUtilisateur.Add(readerer["Prénom_utilisateur"].ToString());
                StatUtilisateur.Add(readerer["Adresse_utilisateur"].ToString());
                StatUtilisateur.Add(readerer["Num_tel_utilisateur"].ToString());
                StatUtilisateur.Add(readerer["adresse_mail_utilisateur"].ToString());
                if(Est_entreprise)
                {
                    StatUtilisateur.Add(readerer["Nom_entreprise"].ToString());
                }
                StatUtilisateur.Add(readerer["Nbre_signalements_contre_utilisateur"].ToString());
                connection.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
