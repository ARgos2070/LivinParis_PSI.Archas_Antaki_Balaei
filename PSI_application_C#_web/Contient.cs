using MySql.Data.MySqlClient;
using System;

namespace PSI_application_C__web
{
    public class Contient
    {
        #region Attributs
        private int id_plat;
        private int id_commande;
        private int nbre_portion_commendee_contient;
        #endregion

        #region Constructeur
        public Contient(int id_plat, int id_commande, int nbre_portion_commendee_contient)
        {
            this.id_plat = id_plat;
            this.id_commande = id_commande;
            this.nbre_portion_commendee_contient = nbre_portion_commendee_contient;
        }
        #endregion

        #region Méthodes
        public static void AjoutContientBDD(Contient contient)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Contient (ID_Plat, ID_Commande, nbre_portion_commendee_contient) VALUES (" +
                    contient.id_plat + ", " +
                    contient.id_commande + ", " +
                    contient.nbre_portion_commendee_contient + ");";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SupprimerContient(Contient contient)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Contient WHERE ID_Plat = " + contient.id_plat + " AND ID_Commande = " + contient.id_commande + ";";
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

