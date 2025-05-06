using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PSI_application_C__web.Pages
{
    public class Page_commentairesModel : PageModel
    {
        /* [BindProperty(SupportsGet = true)]
        public int? IdPlat { get; set; }  // Nullable int to handle cases where it's missing
        */

        public List<Commentaire> Commentaires { get; set; } = new();

        public void OnGet()
        {
            int ID_plat = 10;
            //TempData["Id_Plat"] = TempData["Id_Plat"];

            const string connectionString = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query =
                "SELECT co.Note_Commentaire, " +
                       "co.Texte_Commentaire, " +
                       "u.Nom_utilisateur, " +
                       "u.Prénom_utilisateur " +
                "FROM Commentaire co " +
                "INNER JOIN Client cl ON co.ID_Client = cl.ID_Client " +
                "INNER JOIN Utilisateur u ON cl.ID_Utilisateur = u.ID_Utilisateur " +
                "WHERE co.ID_Plat = " + ID_plat +";";


            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Commentaires.Add(new Commentaire(
                    reader.GetInt32(0),      // Note_Commentaire
                    reader.GetString(1),     // Texte_Commentaire
                    reader.GetString(2),     // Nom
                    reader.GetString(3)      // Prénom
                ));
            }
        }
    }
}
