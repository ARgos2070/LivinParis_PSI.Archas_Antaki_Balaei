using MySql.Data.MySqlClient;
using System;

namespace PSI_application_C__web
{
    public class Commentaire_LP
    {
        #region Attributs
        private int id_commentaire;
        private int note_commentaire;
        private string texte_commentaire;
        private int id_client;
        private int id_plat;
        #endregion

        #region Constructeur
        public Commentaire_LP(int note_commentaire, string texte_commentaire, int id_client, int id_plat)
        {
            this.id_commentaire = Identifiant_commentaire_determine_depuis_bdd();
            this.note_commentaire = note_commentaire;
            this.texte_commentaire = texte_commentaire;
            this.id_client = id_client;
            this.id_plat = id_plat;
        }
        #endregion

        #region Méthodes
        public static int Identifiant_commentaire_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_Commentaire) AS ID_commentaire_maximum FROM Commentaire;";
                MySqlDataReader reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_commentaire_maximum"].ToString();
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

        public static void AjoutCommentaireBDD(Commentaire_LP commentaire)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Commentaire (ID_Commentaire, Note_Commentaire, Texte_Commentaire, ID_Client, ID_Plat) VALUES (" +
                    commentaire.id_commentaire + ", " +
                    commentaire.note_commentaire + ", '" +
                    commentaire.texte_commentaire + "', " +
                    commentaire.id_client + ", " +
                    commentaire.id_plat + ");";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SupprimerCommentaire(Commentaire_LP commentaire)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Commentaire WHERE ID_Commentaire = " + commentaire.id_commentaire + ";";
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
