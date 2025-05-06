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
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
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
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
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

        public static List<Commande> RechercherTousLesTuplesCommande(string parametre_optionnel)
        {
            List<Commande> commandes = new List<Commande>();
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Commande " + parametre_optionnel + ";";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Commande commande = new Commande(
                        reader.GetInt32("ID_Commande"),
                        reader.GetDouble("Prix_Commande"),
                        reader.GetString("Date_Commande"),
                        reader.GetInt32("Taille_Commande"),
                        reader.GetInt32("ID_Client")
                    );
                    commandes.Add(commande);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return commandes;
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
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Commande SET Taille_Commande = Taille_commande + " + ajout_nbre_de_portion + " WHERE ID_Commande = " + id_commande + ";";
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
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Commande SET Prix_Commande = Prix_Commande + " + ajout_prix.ToString().Replace(',', '.') + " WHERE ID_Commande = " + id_commande + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public static void SupprimerCommande(int id_commande)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Commande WHERE ID_Commande = " + id_commande + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static List<List<string>> RechercherHistoriqueCommandeClient(int id_client)
        {
            List<List<string>> historique_commandes = new List<List<string>>();
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT c.ID_Commande, c.Prix_Commande, c.Date_Commande, c.Taille_Commande, p.ID_Plat, p.Nom_plat, p.Type_Plat, p.Pr_cmb_de_personnes_Plat, p.Prix_par_portion_Plat, cont.nbre_portion_commendee_contient FROM Commande c JOIN Contient cont ON c.ID_Commande = cont.ID_Commande JOIN Plat p ON cont.ID_Plat = p.ID_Plat WHERE c.ID_Client = " + id_client + ";";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<string> commande = new List<string>
                    {
                            reader["ID_Commande"].ToString(),
                            reader["Prix_Commande"].ToString(),
                            reader["Date_Commande"].ToString(),
                            reader["Taille_Commande"].ToString(),
                            reader["ID_Plat"].ToString(),
                            reader["Nom_plat"].ToString(),
                            reader["Type_Plat"].ToString(),
                            reader["Pr_cmb_de_personnes_Plat"].ToString(),
                            reader["Prix_par_portion_Plat"].ToString(),
                            reader["nbre_portion_commendee_contient"].ToString()
                    };
                    historique_commandes.Add(commande);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return historique_commandes;
        }
        #endregion
    }
}

