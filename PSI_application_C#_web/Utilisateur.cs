using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maquette_interface_C_
{
    internal class Utilisateur
    {
        #region Attributs
        private int id_utilisateur;
        private string mot_de_passe_utilisateur;
        private string nom_utilisateur;
        private string prenom_utilisateur;
        private string adresse_utilisateur;
        private int num_utilisateur;
        private bool utilisateur_est_entreprise;
        private string nom_entreprise_utilisateur;
        #endregion

        #region Constructeurs
        public Utilisateur(string mot_de_passe_utilisateur, string nom_utilisateur, string prenom_utilisateur, string adresse_utilisateur, int num_utilisateur, bool utilisateur_est_entreprise, string nom_entreprise_utilisateur)
        {
            this.id_utilisateur = identifiant_determine_depuis_bdd();
            this.mot_de_passe_utilisateur = mot_de_passe_utilisateur;
            this.nom_utilisateur= nom_utilisateur;
            this.prenom_utilisateur = prenom_utilisateur;
            this.adresse_utilisateur = adresse_utilisateur;
            this.num_utilisateur = num_utilisateur;
            this.utilisateur_est_entreprise = utilisateur_est_entreprise;
            this.nom_entreprise_utilisateur = nom_entreprise_utilisateur;
        }
        #endregion

        #region Méthodes
        public static int identifiant_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_utilisateur) AS ID_utilisateur_maximum FROM Utilisateur;";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_utilisateur_maximum"].ToString();

                }
                if ( String.IsNullOrEmpty(lecture_id_max))
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





        public string toString()
        {
            string chaine = "";
            chaine = this.id_utilisateur.ToString() + this.mot_de_passe_utilisateur.ToString() + this.nom_utilisateur.ToString() + this.prenom_utilisateur.ToString() 
                + this.adresse_utilisateur.ToString() + this.num_utilisateur.ToString() + this.utilisateur_est_entreprise.ToString() + this.nom_entreprise_utilisateur.ToString();
            return chaine;
        }
        #endregion
    }
}
