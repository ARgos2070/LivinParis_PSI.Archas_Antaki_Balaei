using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_application_C__web
{
    public class Client : Utilisateur
    {
        #region Attributs
        private int id_client;
        private int nbre_commandes_passees_client;
        #endregion

        #region Constructeurs
        public Client(string id_utilisateur, string mot_de_passe_utilisateur, string nom_utilisateur, string prenom_utilisateur, string adresse_utilisateur, string num_utilisateur, string adresse_mail_utilisateur, bool utilisateur_est_entreprise, string nom_entreprise_utilisateur) : 
            base(id_utilisateur, mot_de_passe_utilisateur, nom_utilisateur, prenom_utilisateur, adresse_utilisateur, num_utilisateur, adresse_mail_utilisateur, utilisateur_est_entreprise, nom_entreprise_utilisateur)
        {
            this.id_client = Identifiant_client_determine_depuis_bdd();
            this.nbre_commandes_passees_client = 0;
        }

        public Client(Utilisateur user)
       : base(user.Id_utilisateur, user.Mot_de_passe_utilisateur, user.Nom_utilisateur, user.Prenom_utilisateur,
              user.Adresse_utilisateur, user.Num_utilisateur, user.Adresse_mail_utilisateur,
              user.Utilisateur_est_entreprise, user.Nom_entreprise_utilisateur)
        {
            this.id_client = Identifiant_client_determine_depuis_bdd();
            this.nbre_commandes_passees_client = 0;
        }
        #endregion

        #region Propriétés
        public int Id_client
        {
            get { return this.id_client; }
        }

        public int Nbre_commandes_passees_client
        {
            get { return this.nbre_commandes_passees_client; }
        }

        #endregion

        #region Méthodes
        public static int Identifiant_client_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_Client) AS ID_client_maximum FROM Client;";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_client_maximum"].ToString();

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

        public static void AjoutClientBDD(Client client)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Client (ID_Client, ID_utilisateur, Nbre_commandes_passees_client) VALUES (" +
                    client.id_client + ", '" +
                    client.Id_utilisateur + "', " +
                    client.nbre_commandes_passees_client + ");";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        public static void RadierClient(Client client) //Inutile car les clés étrangères ont été déclarée en cascade, donc il suffit de supprimer l'utilisateur pour supprimer le client, à voir si on laisse en cascade pour plus tard
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Client WHERE ID_Client = " + client.id_client + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        /// <summary>
        /// Récupère l'identifiant du client associé à un utilisateur donné à partir de son identifiant utilisateur
        /// </summary>
        /// <param name="id_utilisateur">L'identifiant de l'utilisateur dont on souhaite retrouver l'identifiant client</param>
        /// <returns>
        /// Un entier représentant l'identifiant du client si trouvé, sinon 0
        /// </returns>
        public static int IdClientDunUtilisateur(string id_utilisateur)
        {
            int id_client = 0;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ID_Client FROM Client WHERE ID_utilisateur = '" + id_utilisateur + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id = "";
                while (reader.Read())
                {
                    lecture_id = reader["ID_Client"].ToString();

                }
                if (!String.IsNullOrEmpty(lecture_id))
                {
                    id_client = int.Parse(lecture_id);
                }
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Il y a une erreur dans la recherche de cuisinier");
            }
            return id_client;
        }

        public void UneCommandePassee()
        {
            this.nbre_commandes_passees_client++;
        }

        public string toString()
        {
            string chaine = "";
            chaine = this.Id_utilisateur.ToString() + this.Mot_de_passe_utilisateur.ToString() + this.Nom_utilisateur.ToString() + this.Prenom_utilisateur.ToString()
                + this.Adresse_utilisateur.ToString() + this.Num_utilisateur.ToString() + this.Adresse_mail_utilisateur.ToString() + this.Utilisateur_est_entreprise.ToString() + this.Nom_entreprise_utilisateur.ToString()
                + this.id_client.ToString() + this.nbre_commandes_passees_client.ToString();
            return chaine;
        }
        #endregion
    }
}
