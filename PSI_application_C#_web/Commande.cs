using MySql.Data.MySqlClient;
using System;

namespace PSI_application_C__web
{
    public class Commande
    {
        #region Attributs
        private int id_commande;
        private double prix_commande;
        private string date_commande;
        private int taille_commande;
        private int id_client;
        #endregion

        #region Constructeur
        public Commande(int id_commande, double prix_commande, string date_commande, int taille_commande, int id_client)
        {
            this.id_commande = id_commande;
            this.prix_commande = prix_commande;
            this.date_commande = date_commande;
            this.taille_commande = taille_commande;
            this.id_client = id_client;
        }
        #endregion

        #region Propriétés
        public int Id_commande
        {
            get { return this.id_commande; }
            set { this.id_commande = value; }
        }

        public double Prix_commande
        {
            get { return this.prix_commande; }
            set { this.prix_commande = value; }
        }

        public string Date_commande
        {
            get { return this.date_commande; }
            set { this.date_commande = value; }
        }

        public int Taille_commande
        {
            get { return this.taille_commande; }
            set { this.taille_commande = value; }
        }

        public int Id_client
        {
            get { return this.id_client; }
            set { this.id_client = value; }
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
                command.CommandText = "SELECT MAX(ID_Commande) FROM Commande;";
                MySqlDataReader reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["MAX(ID_Commande)"].ToString();
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
                    commande.prix_commande.ToString().Replace(',', '.') + ", '" +
                    commande.date_commande + "', " +
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

        /// <summary>
        /// Met à jour le nombre de portions commandées dans une commande existante en bdd
        /// </summary>
        /// <param name="id_commande">L'identifiant de la commande à modifier</param>
        /// <param name="ajout_nbre_de_portion">Le nombre de portions à ajouter à la commande actuelle</param>
        public static void MettreAjourAttributTailleCommande(int id_commande, int ajout_nbre_de_portion)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Commande SET Taille_Commande = Taille_commande + " + ajout_nbre_de_portion + "WHERE ID_Commande =" + id_commande + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        /// <summary>
        /// Met à jour le prix total d'une commande dans la bdd en y ajoutant un montant donné
        /// </summary>
        /// <param name="id_commande">L'identifiant de la commande à maj</param>
        /// <param name="ajout_prix">Le montant à ajouter au prix actuel de la commande</param>
        public static void MettreAjourAttributPrixCommande(int id_commande, double ajout_prix)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Commande SET Prix_Commande = Prix_Commande + " + ajout_prix.ToString().Replace(',', '.') + "WHERE ID_Commande =" + id_commande + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        //public static void SupprimerCommande(Commande commande)
        //{
        //    try
        //    {
        //        string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
        //        MySqlConnection connection = new MySqlConnection(ligneConnexion);
        //        connection.Open();
        //        MySqlCommand command = connection.CreateCommand();
        //        command.CommandText = "DELETE FROM Commande WHERE ID_Commande = " + commande.id_commande + ";";
        //        command.ExecuteNonQuery();
        //        command.Dispose();
        //        connection.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}
        #endregion
    }
}

