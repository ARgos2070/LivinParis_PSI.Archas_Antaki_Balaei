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

        public static Utilisateur ChargerUtilisateurDepuisBDD(string userId)
        {
            Utilisateur user = null;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Utilisateur WHERE ID_utilisateur = '" + userId + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture1 = "";
                string lecture2 = "";
                string lecture3 = "";
                string lecture4 = "";
                string lecture5 = "";
                string lecture6 = "";
                bool lecture7 = false;
                string lecture8 = "";
                Console.WriteLine("ça a pas planté encore");
                if (reader.Read())
                {
                    lecture1 = reader.GetString("Mot_de_passe_utilisateur");
                    
                    lecture2 = reader.GetString("Nom_utilisateur");
                    
                    lecture3 = reader.GetString("Prénom_utilisateur");
                    
                    lecture4 = reader.GetString("Adresse_utilisateur");
                    
                    lecture5 = reader.GetString("Num_tel_utilisateur");
                    
                    lecture6 = reader.GetString("adresse_mail_utilisateur");
                    
                    lecture7 = reader.GetBoolean("Utilisateur_est_entreprise");
                    
                    lecture8 = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("Nom_entreprise")))
                    {
                        lecture8 = reader.GetString("Nom_entreprise");
                    }

                    Console.WriteLine("-9");
                    //user = new Utilisateur
                    //(
                    //    userId,
                    //    reader.GetString("Mot_de_passe_utilisateur"),
                    //    reader.GetString("Nom_utilisateur"),
                    //    reader.GetString("Prénom_utilisateur"),
                    //    reader.GetString("Adresse_utilisateur"),
                    //    reader.GetString("Num_tel_utilisateur"),
                    //    reader.GetString("adresse_mail_utilisateur"),
                    //    reader.GetBoolean("Utilisateur_est_entreprise"),
                    //    reader.GetString("Nom_entreprise")
                    //);
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                Console.WriteLine("Jusque la tout va bien");
                user = new Utilisateur(userId, lecture1, lecture2, lecture3, lecture4, lecture5, lecture6, lecture7, lecture8 );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return user;
        }


        public static void RadierUtilisateur(string id_utilisateur)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Utilisateur WHERE ID_utilisateur = '" + id_utilisateur + "';";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        static public void UnSignalementRecu(string id_utilisateur)
        {
            int nbre_signalements_contre_utilisateur = -1;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Utilisateur WHERE ID_utilisateur = '" + id_utilisateur + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_mot_de_passe = "";
                while (reader.Read())
                {
                    nbre_signalements_contre_utilisateur = 
                        Convert.ToInt32(reader["Nbre_signalements_contre_utilisateur"].ToString());
                }
                command.Dispose();
                connection.Close();
                if (nbre_signalements_contre_utilisateur < 0)
                {
                    throw new Exception("mauvaise connection");
                }
                if (nbre_signalements_contre_utilisateur >= 3)
                {
                    RadierUtilisateur(id_utilisateur);
                }
                else
                {
                    nbre_signalements_contre_utilisateur++;

                    MySqlConnection renvoyer_connection = new MySqlConnection(ligneConnexion);
                    renvoyer_connection.Open();
                    MySqlCommand renvoyer_command = renvoyer_connection.CreateCommand();
                    renvoyer_command.CommandText = "UPDATE Utilisateur SET Nbre_signalements_contre_utilisateur = "+
                        nbre_signalements_contre_utilisateur + " WHERE ID_utilisateur = '" + id_utilisateur + "';";
                    renvoyer_command.ExecuteNonQuery();
                    renvoyer_command.Dispose();
                    renvoyer_connection.Close();
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void MettreAjourTupleColonneUtilisateur(string id_utilisateur, string nom_colonne, string nouvelle_valeur, string param_optionnel)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Utilisateur SET " + nom_colonne + " = " + nouvelle_valeur + " WHERE ID_utilisateur = '" + id_utilisateur + "' " + param_optionnel + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        /// <summary>
        /// Récupère tous les tuples distincts d'une colonne spécifique dans la table Plat
        /// </summary>
        /// <param name="nom_colonne">
        /// Le nom de la colonne dont on souhaite extraire les valeurs distinctes
        /// </param>
        /// <returns>
        /// Une liste de chaînes contenant toutes les valeurs distinctes non nulles et non vides de la colonne spécifiée
        /// </returns>
        public static List<string> RechercherTousLesTuplesDuneColonneUtilisateur(string nom_colonne, string param_optionnel)
        {
            List<string> liste = new List<string>();
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT " + nom_colonne + " FROM Utilisateur " + param_optionnel + ";";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_tuple = "";
                while (reader.Read())
                {
                    lecture_tuple = reader[nom_colonne].ToString();
                    if (!String.IsNullOrEmpty(lecture_tuple))
                    {
                        liste.Add(lecture_tuple);
                    }
                }
                reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return liste;
        }

        public static string RechercherLeTupleDuneColonneUtilisateur(string id_utilisateur, string nom_colonne)
        {
            string valeur = "";
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT " + nom_colonne + " FROM Utilisateur WHERE ID_utilisateur = '" + id_utilisateur + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                //string lecture_tuple = "";
                while (reader.Read())
                {
                    valeur = reader[nom_colonne].ToString();
                }
                reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return valeur;
        }

        public static bool UtilisateurEstEntreprise(string id_utilisateur)
        {
            bool estEntreprise = false;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Utilisateur_est_entreprise FROM Utilisateur WHERE ID_utilisateur = '" + id_utilisateur + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_tuple = "";
                while (reader.Read())
                {
                    lecture_tuple = reader["Utilisateur_est_entreprise"].ToString();
                    if (!String.IsNullOrEmpty(lecture_tuple))
                    {
                        if (lecture_tuple == "True")
                        {
                            estEntreprise = true;
                        }
                    }
                }
                reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return estEntreprise;
        }

        public static bool UtilisateurSansRole(string id_utilisateur)
        {
            bool estSansRole = true;
            if ((Cuisinier.IdCuisinierDunUtilisateur(id_utilisateur) != 0)
                || (Client.IdClientDunUtilisateur(id_utilisateur) != 0)
                || (Livreur.IdLivreurDunUtilisateur(id_utilisateur) != 0))
            {
                estSansRole = false;
            }
            return estSansRole;
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
