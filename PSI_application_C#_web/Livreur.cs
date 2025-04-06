using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_application_C__web
{
    public class Livreur : Utilisateur
    {
        #region Attributs
        private int id_livreur;
        private int gain_livreur;
        private int nbre_commandes_livrees_livreur;
        #endregion

        #region Constructeurs
        public Livreur(string id_utilisateur, string mot_de_passe_utilisateur, string nom_utilisateur, string prenom_utilisateur, string adresse_utilisateur, string num_utilisateur, string adresse_mail_utilisateur, bool utilisateur_est_entreprise, string nom_entreprise_utilisateur) :
            base(id_utilisateur, mot_de_passe_utilisateur, nom_utilisateur, prenom_utilisateur, adresse_utilisateur, num_utilisateur, adresse_mail_utilisateur, utilisateur_est_entreprise, nom_entreprise_utilisateur)
        {
            this.id_livreur = Identifiant_livreur_determine_depuis_bdd();
            this.gain_livreur = 0;
            this.nbre_commandes_livrees_livreur = 0;
        }

        public Livreur(Utilisateur user)
       : base(user.Id_utilisateur, user.Mot_de_passe_utilisateur, user.Nom_utilisateur, user.Prenom_utilisateur,
              user.Adresse_utilisateur, user.Num_utilisateur, user.Adresse_mail_utilisateur,
              user.Utilisateur_est_entreprise, user.Nom_entreprise_utilisateur)
        {
            this.id_livreur = Identifiant_livreur_determine_depuis_bdd();
            this.gain_livreur = 0;
            this.nbre_commandes_livrees_livreur = 0;
        }
        #endregion

        #region Méthodes
        public static int Identifiant_livreur_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_Livreur) AS ID_livreur_maximum FROM Livreur;";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_livreur_maximum"].ToString();

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

        public static void AjoutLivreurBDD(Livreur livreur)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Livreur (ID_Livreur, Gain_Livreur, Nbre_commandes_livrees_livreur, ID_utilisateur) VALUES (" +
                    livreur.id_livreur + ", " +
                    livreur.gain_livreur + ", " +
                    livreur.nbre_commandes_livrees_livreur + ", '" +
                    livreur.Id_utilisateur + "');";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        public static void RadierLivreur(Livreur livreur) //Inutile car les clés étrangères ont été déclarée en cascade, donc il suffit de supprimer l'utilisateur pour supprimer le livreur, à voir si on laisse en cascade pour plus tard
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Livreur WHERE ID_Livreur = " + livreur.id_livreur + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        /// <summary>
        /// Récupère l'identifiant du livreur associé à un utilisateur donné à partir de son identifiant utilisateur
        /// </summary>
        /// <param name="id_utilisateur">L'identifiant de l'utilisateur dont on souhaite retrouver l'identifiant livreur</param>
        /// <returns>
        /// Un entier représentant l'identifiant du livreur si trouvé, sinon 0
        /// </returns>
        public static int IdLivreurDunUtilisateur(string id_utilisateur)
        {
            int id_livreur = 0;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ID_Livreur FROM Livreur WHERE ID_utilisateur = '" + id_utilisateur + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id = "";
                while (reader.Read())
                {
                    lecture_id = reader["ID_Livreur"].ToString();

                }
                if (!String.IsNullOrEmpty(lecture_id))
                {
                    id_livreur = int.Parse(lecture_id);
                }
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Il y a une erreur dans la recherche de cuisinier");
            }
            return id_livreur;
        }

        public void GainGagne(int gain_livraison)
        {
            this.gain_livreur += gain_livraison;
        }

        public string toString()
        {
            string chaine = "";
            chaine = this.Id_utilisateur.ToString() + this.Mot_de_passe_utilisateur.ToString() + this.Nom_utilisateur.ToString() + this.Prenom_utilisateur.ToString()
                + this.Adresse_utilisateur.ToString() + this.Num_utilisateur.ToString() + this.Adresse_mail_utilisateur.ToString() + this.Utilisateur_est_entreprise.ToString() + this.Nom_entreprise_utilisateur.ToString()
                + this.id_livreur.ToString() + this.gain_livreur.ToString();
            return chaine;
        }
        #endregion
    }
}
