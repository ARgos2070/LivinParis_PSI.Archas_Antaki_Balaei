using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace PSI_application_C__web.Pages
{
    public class Page_des_commentairesModel : PageModel
    {
        public List<Commentaire> Commentaires { get; set; } = new List<Commentaire>();

        public void OnGet()
        {
            int ID_plat = 0;
            Console.WriteLine("hey");
            if (TempData.ContainsKey("IdPlat"))
            {
                Console.WriteLine(TempData["IDPlat"] as string);
                int.TryParse(TempData["IDPlat"] as string,out ID_plat);
            }

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
                "WHERE co.ID_Plat = " + ID_plat + ";";


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

    public struct Commentaire
    {
        public int Note_Commentaire { get; }
        public string Texte_Commentaire { get; }
        public string Nom { get; }
        public string Prenom { get; }

        public Commentaire(int note_Commentaire, string texte_Commentaire,
                       string nom, string prenom)
        {
            Note_Commentaire = note_Commentaire;
            Texte_Commentaire = texte_Commentaire;
            Nom = nom;
            Prenom = prenom;
        }
    }
}
