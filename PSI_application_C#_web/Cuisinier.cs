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
        public Cuisinier(string id_utilisateur, string mot_de_passe_utilisateur, string nom_utilisateur, string prenom_utilisateur, string adresse_utilisateur, string num_utilisateur, string adresse_mail_utilisateur, bool utilisateur_est_entreprise, string nom_entreprise_utilisateur) :
            base(id_utilisateur, mot_de_passe_utilisateur, nom_utilisateur, prenom_utilisateur, adresse_utilisateur, num_utilisateur, adresse_mail_utilisateur, utilisateur_est_entreprise, nom_entreprise_utilisateur)
        {
            this.id_cuisinier = Identifiant_cuisinier_determine_depuis_bdd();
            this.nbre_plat_propose_cuisinier = 0;
            this.plat_du_jour_cuisinier = "";
            this.nbre_commandes_cuisinees_cuisinier = 0;
        }

        public Cuisinier(Utilisateur user)
       : base(user.Id_utilisateur, user.Mot_de_passe_utilisateur, user.Nom_utilisateur, user.Prenom_utilisateur,
              user.Adresse_utilisateur, user.Num_utilisateur, user.Adresse_mail_utilisateur,
              user.Utilisateur_est_entreprise, user.Nom_entreprise_utilisateur)
        {
            this.id_cuisinier = Identifiant_cuisinier_determine_depuis_bdd();
            this.nbre_plat_propose_cuisinier = 0;
            this.plat_du_jour_cuisinier = "";
            this.nbre_commandes_cuisinees_cuisinier = 0;
        }
        #endregion

        #region Propriétés
        public int Id_cuisinier
        {
            get { return id_cuisinier; }
        }

        public int Nbre_plat_propose_cuisinier
        {
            get { return nbre_plat_propose_cuisinier; }
            set { nbre_plat_propose_cuisinier = value; }
        }

        public string Plat_du_jour_cuisinier
        {
            get { return plat_du_jour_cuisinier; }
            set { plat_du_jour_cuisinier = value; }
        }

        public int Nbre_commandes_cuisinees_cuisinier
        {
            get { return nbre_commandes_cuisinees_cuisinier; }
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

        public static void AjoutCuisinierBDD(Cuisinier cuisinier)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Cuisinier (ID_Cuisinier, Nbre_plat_proposé_Cuisinier, Plat_du_jour_Cuisinier, Nbre_commandes_cuisinees_cuisinier, ID_utilisateur) VALUES (" +
                    cuisinier.id_cuisinier + ", " +
                    cuisinier.nbre_plat_propose_cuisinier + ", '" +
                    cuisinier.plat_du_jour_cuisinier + "', " +
                    cuisinier.nbre_commandes_cuisinees_cuisinier + ", '" +
                    cuisinier.Id_utilisateur + "');";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        public static void RadierCuisinier(Cuisinier cuisinier) //Inutile car les clés étrangères ont été déclarée en cascade, donc il suffit de supprimer l'utilisateur pour supprimer le cuisinier, à voir si on laisse en cascade pour plus tard
        {
            try
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
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
            
        }

        public static int IdCuisinierDunUtilisateur(string id_utilisateur)
        {
            int id_cuisinier = 0;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ID_Cuisinier FROM Cuisinier WHERE ID_utilisateur = '" + id_utilisateur + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id = "";
                while (reader.Read())
                {
                    lecture_id = reader["ID_Cuisinier"].ToString();

                }
                if (!String.IsNullOrEmpty(lecture_id))
                {
                    id_cuisinier = int.Parse(lecture_id);
                }
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Il y a une erreur dans la recherche de cuisinier");
            }
            return id_cuisinier;
        }

        /// <summary>
        /// Récupère l'identifiant utilisateur associé à un cuisinier donné à partir de son identifiant cuisinier
        /// </summary>
        /// <param name="id_cuisinier">
        /// L'identifiant unique du cuisinier dans la bdd
        /// </param>
        /// <returns>
        /// Une chaîne représentant l'ID de l'utilisateur associé au cuisinier spécifié
        /// Retourne une chaîne vide si aucun utilisateur n'est trouvé ou erreur
        /// </returns>
        public static string IdUtilisateurDunCuisinier(int id_cuisinier)
        {
            string id_utilisateur = "";
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ID_utilisateur FROM Cuisinier WHERE ID_Cuisinier = " + id_cuisinier + ";";
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    id_utilisateur = reader["ID_utilisateur"].ToString();
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return id_utilisateur;
        }


        /// <summary>
        /// Incrémente de 1 le nombre de plats proposés par un cuisinier dans la bdd
        /// </summary>
        /// <param name="id_cuisinier_parametre">ID unique du cuisinier dans la bdd</param>
        public static void CuisinierAjouteUnPlatAsonMenu(int id_cuisinier_parametre)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Cuisinier SET Nbre_plat_proposé_Cuisinier = Nbre_plat_proposé_Cuisinier + 1 WHERE ID_Cuisinier = " + id_cuisinier_parametre + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        /// <summary>
        /// Décrémente de 1 le nombre de plats proposés par un cuisinier dans la bdd
        /// </summary>
        /// <param name="id_cuisinier_parametre">ID unique du cuisinier dans la bdd</param>
        public static void CuisinierRetireUnPlatAsonMenu(int id_cuisinier_parametre)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Cuisinier SET Nbre_plat_proposé_Cuisinier = Nbre_plat_proposé_Cuisinier - 1 WHERE ID_Cuisinier = " + id_cuisinier_parametre + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        /// <summary>
        /// Vérifie si un cuisinier n’a pas encore défini de plat du jour dans la bdd
        /// </summary>
        /// <param name="id_cuisinier_parametre">Identifiant du cuisinier à vérifier</param>
        /// <returns>Retourne <c>true</c> si le cuisinier n’a pas de plat du jour, sinon <c>false</c></returns>

        public static bool CuisinierSansPlatDuJour(int id_cuisinier_parametre)
        {
            bool sans_plat_du_jour = false;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Plat_du_jour_Cuisinier FROM Cuisinier WHERE ID_Cuisinier = " + id_cuisinier_parametre + ";";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_plat_du_jour = "";
                while (reader.Read())
                {
                    lecture_plat_du_jour = reader["Plat_du_jour_Cuisinier"].ToString();

                }
                if (String.IsNullOrEmpty(lecture_plat_du_jour))
                {
                    sans_plat_du_jour = true;
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return sans_plat_du_jour;
        }

        /// <summary>
        /// Déclare un nouveau plat du jour pour un cuisinier donné en mettant à jour la bdd
        /// </summary>
        /// <param name="id_cuisinier_parametre">Identifiant du cuisinier concerné</param>
        /// <param name="nom_plat_du_jour">Nom du plat du jour à associer au cuisinier</param>
        public static void DeclarerUnNouveauPlatDuJour(int id_cuisinier_parametre, string nom_plat_du_jour)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Cuisinier SET Plat_du_jour_Cuisinier = '" + nom_plat_du_jour + "' WHERE ID_Cuisinier = " + id_cuisinier_parametre + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
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
