using MySql.Data.MySqlClient;
using System;

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

        //public static void CheminDeLivraison(string adresse_depart, string adresse_fin)
        //{
        //    string num_et_rue_depart = "";
        //    string ville_depart = "";
        //    string code_postal_depart = "";
            
        //    Graphe lesmetros = new Graphes();
        //    List<string> resultat = await lesmetros.Chemin_le_plus_court("parametrre");
        //}
        #endregion
    }
}
