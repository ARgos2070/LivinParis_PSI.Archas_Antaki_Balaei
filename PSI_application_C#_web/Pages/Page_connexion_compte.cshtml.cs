using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;

namespace PSI_application_C__web.Pages
{
    public class Page_connexion_compteModel : PageModel
    {
        private readonly ILogger<Page_connexion_compteModel> _logger;

        [BindProperty]
        public string saisie_id_connexion { get; set; }

        [BindProperty]
        public string saisie_mot_de_passe_connexion { get; set; }

        public Page_connexion_compteModel(ILogger<Page_connexion_compteModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public static bool IdentifiantExiste(string identifiant_saisi)
        {
            bool existe = false;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(ID_utilisateur) FROM Utilisateur WHERE ID_utilisateur = '" + identifiant_saisi +"';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id = "";
                while (reader.Read())
                {
                    lecture_id = reader["COUNT(ID_utilisateur)"].ToString();
                }
                if (int.TryParse(lecture_id, out int resultat))
                {
                    if (resultat == 1)
                    {
                        existe = true;
                    }
                }
                Console.WriteLine(lecture_id + " : " + resultat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Il y a eu une erreur ici");
            }
            return existe;
        }

        public static bool MotDePasseCorrect(string id_saisi, string mot_de_passe_saisi)
        {
            bool est_correct = false;
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=utilisateur_site;PASSWORD=mot_de_passe";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Mot_de_passe_utilisateur FROM Utilisateur WHERE ID_utilisateur = '" + id_saisi + "';";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_mot_de_passe = "";
                while (reader.Read())
                {
                    lecture_mot_de_passe = reader["Mot_de_passe_utilisateur"].ToString();
                }
                if (lecture_mot_de_passe == mot_de_passe_saisi)
                {
                    est_correct = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Il y a eu une erreur ici");
            }
            return est_correct;
        }

        public IActionResult OnPost()
        {
            bool id_valide = saisie_id_connexion != null && saisie_id_connexion.Length > 0;
            bool mot_de_passe_valide = saisie_mot_de_passe_connexion != null && saisie_mot_de_passe_connexion.Length > 0;
            if (id_valide == false)
            {
                ViewData["Erreur_id_utilisateur_aucune_saisie"] = "Veuillez renseigner votre pseudonyme (id utilisateur).";
            }
            if (mot_de_passe_valide == false)
            {
                ViewData["Erreur_mot_de_passe_aucune_saisie"] = "Votre mot de passe est requis.";
            }
            if (id_valide == false || mot_de_passe_valide == false)
            {
                return Page();
            }
            else
            {
                if (IdentifiantExiste(saisie_id_connexion) == false)
                {
                    ViewData["Erreur_id_utilisateur_incorrect"] = "Ce pseudonyme (id utilisateur) n'est pas correct. Veuillez réessayer.";
                }
                else if (MotDePasseCorrect(saisie_id_connexion, saisie_mot_de_passe_connexion) == false)
                {
                    ViewData["Erreur_mot_de_passe_incorrect"] = "Ce mot de passe n'est pas valide.";
                }
                if (IdentifiantExiste(saisie_id_connexion) == false || MotDePasseCorrect(saisie_id_connexion, saisie_mot_de_passe_connexion) == false)
                {
                    return Page();
                }
                else
                {
                    string nbre_signalement_profil = Utilisateur.RechercherLeTupleDuneColonneUtilisateur(saisie_id_connexion, "Nbre_signalements_contre_utilisateur");
                    int nbre_signalement = Convert.ToInt32(nbre_signalement_profil);
                    if (nbre_signalement >= 3)
                    {
                        Utilisateur.RadierUtilisateur(saisie_id_connexion);
                        ViewData["Erreur_mot_de_passe_incorrect"] = "Utilisateur radié";
                        TempData["Id_utilisateur_session"] = saisie_id_connexion;
                        return Page();
                    }
                    else
                    {
                        TempData["Id_utilisateur_session"] = saisie_id_connexion;
                        return RedirectToPage("Page_accueil_connecte");
                    }
                }
            }
        }
    }
}
