using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace PSI_application_C__web.Pages
{
    public class Page_mon_profileModel : PageModel
    {
        public List<string> StatUtilisateur;
        public bool Est_entreprise = false;

        public void OnGet()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            StatUtilisateur = new List<string>();
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Utilisateur WHERE ID_utilisateur = '" + id_utilisateur + "';";

                MySqlDataReader reader = command.ExecuteReader();
                string lecture = "";
                while (reader.Read())
                {
                    lecture = reader["ID_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Mot_de_passe_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Nom_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Prénom_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Adresse_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Num_tel_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["adresse_mail_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Utilisateur_est_entreprise"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Nom_entreprise"].ToString();
                    StatUtilisateur.Add(lecture);
                    lecture = reader["Nbre_signalements_contre_utilisateur"].ToString();
                    StatUtilisateur.Add(lecture);
                    Console.WriteLine("Chargement réussi");
                }

                reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
