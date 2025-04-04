using MySql.Data.MySqlClient;
using System;

namespace PSI_application_C__web
{
    public class Commande
    {
        #region Attributs
        private int id_commande;
        private double prix_commande;
        private DateTime date_commande;
        private int taille_commande;
        private int id_client;
        #endregion

        #region Constructeur
        public Commande(int id_commande, double prix_commande, DateTime date_commande, int taille_commande, int id_client)
        {
            this.id_commande = id_commande;
            this.prix_commande = prix_commande;
            this.date_commande = date_commande;
            this.taille_commande = taille_commande;
            this.id_client = id_client;
        }
        #endregion

        #region Méthodes
        public static int Identifiant_commande_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_Commande) AS ID_commande_maximum FROM Commande;";
                MySqlDataReader reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_commande_maximum"].ToString();
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

        public static void AjoutCommandeBDD(Commande commande)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Commande (ID_Commande, Prix_Commande, Date_Commande, Taille_Commande, ID_Client) VALUES (" +
                    commande.id_commande + ", " +
                    commande.prix_commande + ", '" +
                    commande.date_commande.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    commande.taille_commande + ", " +
                    commande.id_client + ");";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SupprimerCommande(Commande commande)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Commande WHERE ID_Commande = " + commande.id_commande + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
    }
}

