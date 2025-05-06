using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Data;
using System.Text.RegularExpressions;

namespace PSI_application_C__web.Pages
{
    public class Page_chemin_commandeModel : PageModel
    {
        public string AdresseLivraisonClient { get; set; }
        public string AdresseCuisinier { get; set; }

        public List<string> List_des_stations { get; set; }

        public async Task OnGet()
        {
            Graphe les_metro = new Graphe();
            
            int ID_commande = Convert.ToInt32(TempData.Peek("ID_commande"));
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            // 1. Adresse de livraison et cuisinier (nom/prénom) pour la commande
            string query1 = @"
                SELECT
                    u_client.Adresse_utilisateur AS adresse_client,
                    u_cuisinier.Nom_utilisateur,
                    u_cuisinier.Prénom_utilisateur
                FROM Commande c
                JOIN Client cl ON c.ID_Client = cl.ID_Client
                JOIN Utilisateur u_client ON cl.ID_utilisateur = u_client.ID_utilisateur
                JOIN Contient co ON c.ID_Commande = co.ID_Commande
                JOIN Plat p ON co.ID_Plat = p.ID_Plat
                JOIN Cuisinier cu ON p.ID_Cuisinier = cu.ID_Cuisinier
                JOIN Utilisateur u_cuisinier ON cu.ID_utilisateur = u_cuisinier.ID_utilisateur
                WHERE c.ID_Commande = @id_commande
                LIMIT 1;";

            using (var cmd = new MySqlCommand(query1, connection))
            {
                cmd.Parameters.AddWithValue("@id_commande", ID_commande);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    AdresseLivraisonClient = reader.GetString(0);
                }
            }

            // 2. Adresse du cuisinier qui a préparé la commande
            string query2 = @"
                SELECT
                    u.Adresse_utilisateur
                FROM Commande co
                JOIN Contient ct ON co.ID_Commande = ct.ID_Commande
                JOIN Plat p ON ct.ID_Plat = p.ID_Plat
                JOIN Cuisinier cu ON p.ID_Cuisinier = cu.ID_Cuisinier
                JOIN Utilisateur u ON cu.ID_utilisateur = u.ID_utilisateur
                WHERE co.ID_Commande = @id_commande
                LIMIT 1;";

            using (var cmd2 = new MySqlCommand(query2, connection))
            {
                cmd2.Parameters.AddWithValue("@id_commande", ID_commande);
                using var reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    AdresseCuisinier = reader2.GetString(0);
                }
            }

            AdresseDecomposee info_client = DecomposerAdresse(AdresseLivraisonClient);
            AdresseDecomposee info_cusinier = DecomposerAdresse(AdresseCuisinier);

            List_des_stations = await les_metro.Chemin_le_plus_court(
                info_cusinier.Numero, info_cusinier.Rue, info_cusinier.Ville, info_cusinier.CodePostal,
                info_client.Numero, info_client.Rue, info_client.Ville, info_client.CodePostal
            );
        }

        public static AdresseDecomposee DecomposerAdresse(string adresse)
        {
            var regex = new Regex(@"^(\d+)\s+([^\d,]+),\s*(\d{5})\s+(.+)$");
            var match = regex.Match(adresse);
            

            if (!match.Success)
                Console.WriteLine(adresse);

            return new AdresseDecomposee
            {
                Numero = match.Groups[1].Value.Trim(),
                Rue = match.Groups[2].Value.Trim(),
                CodePostal = match.Groups[3].Value.Trim(),
                Ville = match.Groups[4].Value.Trim()
            };
        }

        public struct AdresseDecomposee
        {
            public string Numero;
            public string Rue;
            public string CodePostal;
            public string Ville;
        }
    }
}
