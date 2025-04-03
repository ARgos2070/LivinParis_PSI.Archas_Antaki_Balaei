using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_application_C__web
{
    public class Cuisinier : Utilisateur
    {
        #region Attributs
        private int id_cuisinier;
        private int nbre_plat_propose_cuisinier;
        private string plat_du_jour_cuisinier;
        private int nbre_commandes_cuisinees_cuisinier;
        #endregion

        #region Constructeurs
        public Cuisinier(string mot_de_passe_utilisateur, string nom_utilisateur, string prenom_utilisateur, string adresse_utilisateur, string num_utilisateur, string adresse_mail_utilisateur, bool utilisateur_est_entreprise, string nom_entreprise_utilisateur) :
            base(mot_de_passe_utilisateur, nom_utilisateur, prenom_utilisateur, adresse_utilisateur, num_utilisateur, adresse_mail_utilisateur, utilisateur_est_entreprise, nom_entreprise_utilisateur)
        {
            this.id_cuisinier = Identifiant_cuisinier_determine_depuis_bdd();
            this.nbre_plat_propose_cuisinier = 0;
            this.plat_du_jour_cuisinier = "";
            this.nbre_commandes_cuisinees_cuisinier = 0;
        }

        public Cuisinier(Utilisateur user, int id_client, int Nbre_commandes_passees_client)
       : base(user.Mot_de_passe_utilisateur, user.Nom_utilisateur, user.Prenom_utilisateur,
              user.Adresse_utilisateur, user.Num_utilisateur, user.Adresse_mail_utilisateur,
              user.Utilisateur_est_entreprise, user.Nom_entreprise_utilisateur)
        {
            this.id_cuisinier = Identifiant_cuisinier_determine_depuis_bdd();
            this.nbre_plat_propose_cuisinier = 0;
            this.plat_du_jour_cuisinier = "";
            this.nbre_commandes_cuisinees_cuisinier = 0;
        }
        #endregion

        #region Méthodes
        public static int Identifiant_cuisinier_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_Cuisinier) AS ID_cuisinier_maximum FROM Cuisinier;";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_cuisinier_maximum"].ToString();

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

        public static void RadierCuisinier(Cuisinier cuisinier) //Inutile car les clés étrangères ont été déclarée en cascade, donc il suffit de supprimer l'utilisateur pour supprimer le cuisinier, à voir si on laisse en cascade pour plus tard
        {
            string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
            MySqlConnection connection = new MySqlConnection(ligneConnexion);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Cuisinier WHERE ID_Cuisinier = " + cuisinier.id_cuisinier + ";";
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void UnPlatAjouteeAuMenu()
        {
            this.nbre_plat_propose_cuisinier++;
        }

        public string toString()
        {
            string chaine = "";
            chaine = this.Id_utilisateur.ToString() + this.Mot_de_passe_utilisateur.ToString() + this.Nom_utilisateur.ToString() + this.Prenom_utilisateur.ToString()
                + this.Adresse_utilisateur.ToString() + this.Num_utilisateur.ToString() + this.Adresse_mail_utilisateur.ToString() + this.Utilisateur_est_entreprise.ToString() + this.Nom_entreprise_utilisateur.ToString()
                + this.id_cuisinier.ToString() + this.nbre_plat_propose_cuisinier.ToString() + this.plat_du_jour_cuisinier.ToString() + this.nbre_commandes_cuisinees_cuisinier.ToString();
            return chaine;
        }
        #endregion
    }
}
