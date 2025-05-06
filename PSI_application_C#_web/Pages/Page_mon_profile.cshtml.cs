using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

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

        public JsonObject StatUtilisateurToJson(List<string> StatUtilisateur)
        {
            var utilisateur = new JsonObject
            {
                ["ID_utilisateur"] = StatUtilisateur[0],
                ["Mot_de_passe_utilisateur"] = StatUtilisateur[1],
                ["info_personnel"] = new JsonObject
                {
                    ["Nom_utilisateur"] = StatUtilisateur[2],
                    ["Prénom_utilisateur"] = StatUtilisateur[3],
                    ["Adresse_utilisateur"] = StatUtilisateur[4],
                    ["Num_tel_utilisateur"] = StatUtilisateur[5],
                    ["adresse_mail_utilisateur"] = StatUtilisateur[6],
                },
                ["info_entreprise"] = new JsonObject
                {
                    ["Utilisateur_est_entreprise"] = StatUtilisateur[7],
                    ["Nom_entreprise"] = StatUtilisateur[8],
                },
                ["Nbre_signalements_contre_utilisateur"] = StatUtilisateur[9]
            };

            return utilisateur;
        }

        public XElement StatUtilisateurToXml(List<string> StatUtilisateur)
        {
            var utilisateurXml = new XElement("Utilisateur",
                new XElement("ID_utilisateur", StatUtilisateur[0]),
                new XElement("Mot_de_passe_utilisateur", StatUtilisateur[1]),
                new XElement("info_personnel",
                    new XElement("Nom_utilisateur", StatUtilisateur[2]),
                    new XElement("Prénom_utilisateur", StatUtilisateur[3]),
                    new XElement("Adresse_utilisateur", StatUtilisateur[4]),
                    new XElement("Num_tel_utilisateur", StatUtilisateur[5]),
                    new XElement("adresse_mail_utilisateur", StatUtilisateur[6])
                ),
                new XElement("info_entreprise",
                    new XElement("Utilisateur_est_entreprise", StatUtilisateur[7]),
                    new XElement("Nom_entreprise", StatUtilisateur[8])
                ),
                new XElement("Nbre_signalements_contre_utilisateur", StatUtilisateur[9])
            );

            return utilisateurXml;
        }

    }
}

