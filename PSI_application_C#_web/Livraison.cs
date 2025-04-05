using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;

namespace PSI_application_C__web
{
    public class Livraison
    {
        #region Attributs
        private int id_livraison;
        private string adresse_initiale_livraison;
        private string adresse_finale_livraison;
        private double prix_livraison;
        private DateTime date_livraison;
        private int id_commande;
        private int id_livreur;
        #endregion

        #region Constructeur
        public Livraison(int id_livraison, string adresse_initiale_livraison, string adresse_finale_livraison, double prix_livraison, DateTime date_livraison, int id_commande, int id_livreur)
        {
            this.id_livraison = id_livraison;
            this.adresse_initiale_livraison = adresse_initiale_livraison;
            this.adresse_finale_livraison = adresse_finale_livraison;
            this.prix_livraison = prix_livraison;
            this.date_livraison = date_livraison;
            this.id_commande = id_commande;
            this.id_livreur = id_livreur;
        }
        #endregion

        #region Méthodes
        public static int Identifiant_livraison_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_Livraison) AS ID_livraison_maximum FROM Livraison;";
                MySqlDataReader reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_livraison_maximum"].ToString();
                }
                if (String.IsNullOrEmpty(lecture_id_max))
                {
                    identifiant = 1;
                }
                else
                {
                    identifiant = int.Parse(lecture_id_max);
                    identifiant++;
                }
                reader.Close();
                connection.Close();
                return identifiant;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }

        public static void AjoutLivraisonBDD(Livraison livraison)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Livraison (ID_Livraison, Adresse_initiale_Livraison, Adresse_finale_Livraison, Prix_Livraison, Date_Livraison, ID_Commande, ID_Livreur) VALUES (" +
                    livraison.id_livraison + ", '" +
                    livraison.adresse_initiale_livraison + "', '" +
                    livraison.adresse_finale_livraison + "', " +
                    livraison.prix_livraison + ", '" +
                    livraison.date_livraison.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    livraison.id_commande + ", " +
                    livraison.id_livreur + ");";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SupprimerLivraison(Livraison livraison)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Livraison WHERE ID_Livraison = " + livraison.id_livraison + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        /// <summary>
        /// La méthode calcule le chemin de livraison le plus court entre 2 adresses
        /// </summary>
        /// <param name="adresse_depart">Adresse de départ format: numéro et rue, ville, code postal</param>
        /// <param name="adresse_arrivee">Adresse d’arrivée format : numéro et rue, ville, code postal</param>
        /// <returns>
        /// retourne une liste des noms des stations pour le chemin le plus court entre les 2 adresses
        /// </returns>
        public async static Task<List<string>> CheminDeLivraison(string adresse_depart, string adresse_arrivee)
        {
            string num_et_rue_depart = "";
            string ville_depart = "";
            string code_postal_depart = "";
            string[] composant_adresse_depart = adresse_depart.Split(',');
            for (int i=0; i<composant_adresse_depart.Length; i++)
            {
                composant_adresse_depart[i] = composant_adresse_depart[i].Trim();
                switch(i)
                {
                    case 0: num_et_rue_depart = composant_adresse_depart[i]; break;
                    case 1: ville_depart = composant_adresse_depart[i]; break;
                    default: code_postal_depart = composant_adresse_depart[i]; break;
                }
            }
            string num_et_rue_arrivee = "";
            string ville_arrivee = "";
            string code_postal_arrivee = "";
            string[] composant_adresse_arrivee = adresse_arrivee.Split(',');
            for (int i = 0; i < composant_adresse_arrivee.Length; i++)
            {
                composant_adresse_arrivee[i] = composant_adresse_arrivee[i].Trim();
                switch (i)
                {
                    case 0: num_et_rue_arrivee = composant_adresse_arrivee[i]; break;
                    case 1: ville_arrivee = composant_adresse_arrivee[i]; break;
                    default: code_postal_arrivee = composant_adresse_arrivee[i]; break;
                }
            }
            Graphe lesmetros = new Graphe();
            List<string> resultat = await lesmetros.Chemin_le_plus_court(num_et_rue_depart, ville_depart, code_postal_depart, "France",
                num_et_rue_arrivee, ville_arrivee, code_postal_arrivee, "France");
            return resultat;
        }
        #endregion
    }
}
