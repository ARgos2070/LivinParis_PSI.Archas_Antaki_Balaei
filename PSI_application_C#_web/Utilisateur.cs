using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_application_C__web
{
    public class Utilisateur
    {
        #region Attributs
        private string id_utilisateur;
        private string mot_de_passe_utilisateur;
        private string nom_utilisateur;
        private string prenom_utilisateur;
        private string adresse_utilisateur;
        private string num_utilisateur;
        private string adresse_mail_utilisateur;
        private bool utilisateur_est_entreprise;
        private string nom_entreprise_utilisateur;
        private int nbre_signalements_contre_utilisateur;
        #endregion

        #region Constructeur
        public Utilisateur(string id_utilisateur, string mot_de_passe_utilisateur, string nom_utilisateur, string prenom_utilisateur, string adresse_utilisateur, string num_utilisateur, string adresse_mail_utilisateur, bool utilisateur_est_entreprise, string nom_entreprise_utilisateur)
        {
            this.id_utilisateur = id_utilisateur.Trim();
            this.mot_de_passe_utilisateur = mot_de_passe_utilisateur.Trim();
            this.nom_utilisateur= nom_utilisateur.Trim();
            this.prenom_utilisateur = prenom_utilisateur.Trim();
            this.adresse_utilisateur = adresse_utilisateur.Trim();
            this.num_utilisateur = num_utilisateur;
            this.adresse_mail_utilisateur = adresse_mail_utilisateur.ToLower().Trim();
            this.utilisateur_est_entreprise = utilisateur_est_entreprise;
            this.nom_entreprise_utilisateur = nom_entreprise_utilisateur;
            this.nbre_signalements_contre_utilisateur = 0;
        }
        #endregion

        #region Propriétés
        public string Id_utilisateur
        {
            get { return this.id_utilisateur; }
        }

        public string Mot_de_passe_utilisateur
        {
            get { return this.mot_de_passe_utilisateur; }
        }

        public string Nom_utilisateur 
        { 
            get { return this.nom_utilisateur; } 
        }

        public string Prenom_utilisateur 
        { 
            get { return this.prenom_utilisateur; } 
        }

        public string Adresse_utilisateur 
        { 
            get { return this.adresse_utilisateur; } 
        }

        public string Num_utilisateur 
        { 
            get { return this.num_utilisateur; } 
        }

        public string Adresse_mail_utilisateur 
        {
            get { return this.adresse_mail_utilisateur; } 
        }

        public bool Utilisateur_est_entreprise 
        {
            get { return this.utilisateur_est_entreprise; } 
        }

        public string Nom_entreprise_utilisateur 
        { 
            get { return this.nom_entreprise_utilisateur; } 
        }

        public int Nbre_signalements_contre_utilisateur
        {
            get { return this.nbre_signalements_contre_utilisateur; }
        }
        #endregion

        #region Méthodes
        public static bool Identifiant_utilisateur_nouveau_dans_bdd(string id_utilisateur_test)
        {
            try
            {
                bool est_unique = false;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(ID_utilisateur) AS ID_utilisateur_similaire FROM Utilisateur WHERE ID_utilisateur = '" + id_utilisateur_test + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture = "";
                while (reader.Read())
                {
                    lecture = reader["ID_utilisateur_similaire"].ToString();

                }
                if (int.TryParse(lecture, out int resultat))
                {
                    if (resultat == 0)
                    {
                        est_unique = true;
                    }
                }
                reader.Close();
                connection.Close();
                return est_unique;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Il y a eu une erreur ici");
                return false;
            }
        }


        public static void AjoutUtilisateurBDD(Utilisateur user)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Utilisateur (ID_utilisateur, Mot_de_passe_utilisateur, Nom_utilisateur, Prénom_utilisateur, Adresse_utilisateur, Num_tel_utilisateur, adresse_mail_utilisateur, Utilisateur_est_entreprise, Nom_entreprise, Nbre_signalements_contre_utilisateur) VALUES ('" +
                    user.id_utilisateur + "', '" +
                    user.mot_de_passe_utilisateur + "', '" +
                    user.nom_utilisateur + "', '" +
                    user.prenom_utilisateur + "', '" +
                    user.adresse_utilisateur + "', '" +
                    user.num_utilisateur + "', '" +
                    user.adresse_mail_utilisateur + "', " +
                    user.utilisateur_est_entreprise + ", '" +
                    user.nom_entreprise_utilisateur + "', " +
                    user.nbre_signalements_contre_utilisateur + ");";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Console.WriteLine("Message juste après l'ajout à la base de donnée, user.num_utilisateur est : " + user.num_utilisateur);
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        public static void RadierUtilisateur(Utilisateur user)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Utilisateur WHERE ID_utilisateur = '" + user.id_utilisateur + "';";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        public void UnSignalementRecu()
        {
            this.nbre_signalements_contre_utilisateur++;
            if (this.nbre_signalements_contre_utilisateur >= 3)
            {
                RadierUtilisateur(this);
            }
        }

        public string toString()
        {
            string chaine = "";
            chaine = this.id_utilisateur.ToString() + this.mot_de_passe_utilisateur.ToString() + this.nom_utilisateur.ToString() + this.prenom_utilisateur.ToString() 
                + this.adresse_utilisateur.ToString() + this.num_utilisateur.ToString() + this.adresse_mail_utilisateur.ToString() + this.utilisateur_est_entreprise.ToString() + this.nom_entreprise_utilisateur.ToString();
            return chaine;
        }
        #endregion
    }
}
