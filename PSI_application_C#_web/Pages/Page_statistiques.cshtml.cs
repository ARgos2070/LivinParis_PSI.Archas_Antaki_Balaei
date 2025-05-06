using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PSI_application_C__web.Pages
{
    public class Page_statistiqueModel : PageModel
    {
        public List<CookStat> CookStats { get; set; } = new();

        // Nouvelles propriétés pour les statistiques
        public decimal MoyenneDepensesClients { get; set; }
        public decimal MoyennePlatParCuisinier { get; set; }
        public CookierMaxPlatResult CuisinierMaxPlat { get; set; }
        public PlatNoteResult PlatMieuxNote { get; set; }
        public ClientDepenseResult ClientMaxDepense { get; set; }
        public PlatVenduResult PlatPlusVendu { get; set; }

        public void OnGet()
        {
            const string connectionString = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            // Code original
            string query =
                "SELECT c.Nbre_plat_propose_Cuisinier, " +
                       "c.Plat_du_jour_Cuisinier, " +
                       "c.Nbre_commandes_cuisinees_cuisinier, " +
                       "u.Nom_utilisateur, " +
                       "u.Prénom_utilisateur " +
                "FROM Cuisinier c " +
                "INNER JOIN Utilisateur u ON c.ID_utilisateur = u.ID_utilisateur;";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                CookStats.Add(new CookStat(
                    reader.GetInt32(0),      // Nbre_plat_proposé
                    reader.GetString(1),     // Plat_du_jour
                    reader.GetInt32(2),      // Nbre_commandes
                    reader.GetString(3),     // Nom
                    reader.GetString(4)      // Prénom
                ));
            }
            reader.Close();

            // Nouvelles statistiques
            MoyenneDepensesClients = GetMoyenneDepensesClients(connection);
            MoyennePlatParCuisinier = GetMoyennePlatParCuisinier(connection);
            CuisinierMaxPlat = GetCuisinierMaxPlat(connection);
            PlatMieuxNote = GetPlatMieuxNote(connection);
            ClientMaxDepense = GetClientMaxDepense(connection);
            PlatPlusVendu = GetPlatPlusVendu(connection);

            connection.Close();
        }

        // Moyenne de dépenses des clients
        private decimal GetMoyenneDepensesClients(MySqlConnection connection)
        {
            string query = @"
                SELECT AVG(depenses) AS moyenne_depenses
                FROM (
                    SELECT SUM(Prix_Commande) AS depenses
                    FROM Commande
                    GROUP BY ID_Client
                ) AS total_par_client;
            ";
            using var cmd = new MySqlCommand(query, connection);
            var result = cmd.ExecuteScalar();
            return result != DBNull.Value ? System.Convert.ToDecimal(result) : 0;
        }

        // Moyenne du nombre de plats proposés par cuisinier
        private decimal GetMoyennePlatParCuisinier(MySqlConnection connection)
        {
            string query = "SELECT AVG(Nbre_plat_propose_Cuisinier) FROM Cuisinier;";
            using var cmd = new MySqlCommand(query, connection);
            var result = cmd.ExecuteScalar();
            return result != DBNull.Value ? System.Convert.ToDecimal(result) : 0;
        }

        // Le cuisinier qui propose le plus de plats
        public class CookierMaxPlatResult
        {
            public int ID_Cuisinier { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public int NbrePlat { get; set; }
        }
        private CookierMaxPlatResult GetCuisinierMaxPlat(MySqlConnection connection)
        {
            string query = @"
                SELECT c.ID_Cuisinier, u.Nom_utilisateur, u.Prénom_utilisateur, c.Nbre_plat_propose_Cuisinier
                FROM Cuisinier c
                JOIN Utilisateur u ON c.ID_utilisateur = u.ID_utilisateur
                ORDER BY c.Nbre_plat_propose_Cuisinier DESC
                LIMIT 1;";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var res = new CookierMaxPlatResult
                {
                    ID_Cuisinier = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    NbrePlat = reader.GetInt32(3)
                };
                reader.Close();
                return res;
            }
            reader.Close();
            return null;
        }

        // Le plat le mieux noté
        public class PlatNoteResult
        {
            public int ID_Plat { get; set; }
            public string NomPlat { get; set; }
            public decimal MoyenneNote { get; set; }
        }
        private PlatNoteResult GetPlatMieuxNote(MySqlConnection connection)
        {
            string query = @"
                SELECT p.ID_Plat, p.Nom_plat, AVG(c.Note_Commentaire) AS moyenne_note
                FROM Plat p
                JOIN Commentaire c ON p.ID_Plat = c.ID_Plat
                GROUP BY p.ID_Plat
                ORDER BY moyenne_note DESC
                LIMIT 1;";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var res = new PlatNoteResult
                {
                    ID_Plat = reader.GetInt32(0),
                    NomPlat = reader.GetString(1),
                    MoyenneNote = reader.GetDecimal(2)
                };
                reader.Close();
                return res;
            }
            reader.Close();
            return null;
        }

        // Le client qui a dépensé le plus
        public class ClientDepenseResult
        {
            public int ID_Client { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public decimal TotalDepense { get; set; }
        }
        private ClientDepenseResult GetClientMaxDepense(MySqlConnection connection)
        {
            string query = @"
                SELECT cl.ID_Client, u.Nom_utilisateur, u.Prénom_utilisateur, SUM(co.Prix_Commande) AS total_depense
                FROM Commande co
                JOIN Client cl ON co.ID_Client = cl.ID_Client
                JOIN Utilisateur u ON cl.ID_utilisateur = u.ID_utilisateur
                GROUP BY cl.ID_Client
                ORDER BY total_depense DESC
                LIMIT 1;";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var res = new ClientDepenseResult
                {
                    ID_Client = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    TotalDepense = reader.GetDecimal(3)
                };
                reader.Close();
                return res;
            }
            reader.Close();
            return null;
        }

        // Le plat le plus vendu
        public class PlatVenduResult
        {
            public int ID_Plat { get; set; }
            public string NomPlat { get; set; }
            public int TotalVendu { get; set; }
        }
        private PlatVenduResult GetPlatPlusVendu(MySqlConnection connection)
        {
            string query = @"
                SELECT p.ID_Plat, p.Nom_plat, SUM(c.nbre_portion_commendee_contient) AS total_vendu
                FROM Plat p
                JOIN Contient c ON p.ID_Plat = c.ID_Plat
                GROUP BY p.ID_Plat
                ORDER BY total_vendu DESC
                LIMIT 1;";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var res = new PlatVenduResult
                {
                    ID_Plat = reader.GetInt32(0),
                    NomPlat = reader.GetString(1),
                    TotalVendu = reader.GetInt32(2)
                };
                reader.Close();
                return res;
            }
            reader.Close();
            return null;
        }
    }

    public struct CookStat
    {
        public int Nbre_plat_proposé { get; }
        public string Plat_du_jour { get; }
        public int Nbre_commandes { get; }
        public string Nom { get; }
        public string Prenom { get; }

        public CookStat(int nbre_plat_proposé, string plat_du_jour, int nbre_commandes,
                       string nom, string prenom)
        {
            Nbre_plat_proposé = nbre_plat_proposé;
            Plat_du_jour = plat_du_jour;
            Nbre_commandes = nbre_commandes;
            Nom = nom;
            Prenom = prenom;
        }
    }
}






/*
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PSI_application_C__web.Pages
{
    public class Page_statistiqueModel : PageModel
    {
        public List<CookStat> CookStats { get; set; } = new();

        public void OnGet()
        {
            const string connectionString = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query =
                "SELECT c.Nbre_plat_propose_Cuisinier, " +
                       "c.Plat_du_jour_Cuisinier, " +
                       "c.Nbre_commandes_cuisinees_cuisinier, " +
                       "u.Nom_utilisateur, " +
                       "u.Prénom_utilisateur " +
                "FROM Cuisinier c " +
                "INNER JOIN Utilisateur u ON c.ID_utilisateur = u.ID_utilisateur;";


            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                CookStats.Add(new CookStat(
                    reader.GetInt32(0),      // Nbre_plat_proposé
                    reader.GetString(1),     // Plat_du_jour
                    reader.GetInt32(2),      // Nbre_commandes
                    reader.GetString(3),     // Nom
                    reader.GetString(4)      // Prénom
                ));
            }
        }
    }

    public struct CookStat
    {
        public int Nbre_plat_proposé { get; }
        public string Plat_du_jour { get; }
        public int Nbre_commandes { get; }
        public string Nom { get; }
        public string Prenom { get; }

        public CookStat(int nbre_plat_proposé, string plat_du_jour, int nbre_commandes,
                       string nom, string prenom)
        {
            Nbre_plat_proposé = nbre_plat_proposé;
            Plat_du_jour = plat_du_jour;
            Nbre_commandes = nbre_commandes;
            Nom = nom;
            Prenom = prenom;
        }
    }
}
*/