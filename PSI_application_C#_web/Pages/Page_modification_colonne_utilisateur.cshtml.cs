using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_modification_colonne_utilisateurModel : PageModel
    {
        private readonly ILogger<Page_modification_colonne_utilisateurModel> _logger;

        [BindProperty]
        public string Type_colonne { get; set; }

        [BindProperty]
        public string Nom_colonne { get; set; }

        [BindProperty]
        public string saisie_string { get; set; }

        [BindProperty]
        public string saisie_num_tel { get; set; }

        [BindProperty]
        public string saisie_adresse_mail { get; set; }

        [BindProperty]
        public string saisie_adresse_num { get; set; }

        [BindProperty]
        public string saisie_adresse_rue { get; set; }

        [BindProperty]
        public string saisie_adresse_ville { get; set; }

        [BindProperty]
        public string saisie_adresse_code_postal { get; set; }

        public Page_modification_colonne_utilisateurModel(ILogger<Page_modification_colonne_utilisateurModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            Type_colonne = TempData["Type_colonne"].ToString();
            TempData["Type_colonne"] = Type_colonne;
            Nom_colonne = TempData["Colonne_update"].ToString();
            TempData["Colonne_update"] = Nom_colonne;
            Console.WriteLine("nom_colonne" + Nom_colonne);
            Console.WriteLine(id_utilisateur);
            List<string> list = Utilisateur.RechercherTousLesTuplesDuneColonneUtilisateur(Nom_colonne, "WHERE ID_utilisateur = '" + id_utilisateur + "'");
            ViewData["Ancienne_valeur"] = list[0];
            Console.WriteLine("Recalcule de l'ancienne variable");
        }

        public static bool EstNumeroTelCorrect(string num_tel_saisie)
        {
            bool est_correct = false;
            if (num_tel_saisie != null && num_tel_saisie.Length > 0)
            {
                if (num_tel_saisie[0] == '0')
                {
                    est_correct = true;
                    for (int i = 1; i < num_tel_saisie.Length; i++)
                    {
                        switch (num_tel_saisie[i])
                        {
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                break;
                            default:
                                est_correct = false;
                                break;
                        }
                    }
                }
            }
            return est_correct;
        }

        public static bool EstAdresseMailCorrect(string adresse_mail_saisie)
        {
            bool est_correct = false;
            int nbre_arobase = 0;
            if (adresse_mail_saisie != null && adresse_mail_saisie.Length > 0)
            {
                for (int i = 0; i < adresse_mail_saisie.Length; i++)
                {
                    if (adresse_mail_saisie[i] == '@')
                    {
                        nbre_arobase++;
                    }
                }
            }
            if (nbre_arobase == 1)
            {
                est_correct = true;
            }
            return est_correct;
        }

        public static bool EstPasUtilisateurSansRole(bool saisie_client, bool saisie_cuisinier, bool saisie_livreur)
        {
            bool est_correct = false;
            if (saisie_client || saisie_cuisinier || saisie_livreur)
            {
                est_correct = true;
            }
            return est_correct;
        }

        public async Task<IActionResult> OnPost()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            string type_colonne = (string)TempData["Type_colonne"];
            TempData["Type_colonne"] = type_colonne;
            string nom_colonne = (string)TempData["Colonne_update"];
            TempData["Colonne_update"] = nom_colonne;
            Console.WriteLine("nom_colonne" + Nom_colonne);
            Console.WriteLine(id_utilisateur);
            List<string> list = Utilisateur.RechercherTousLesTuplesDuneColonneUtilisateur(nom_colonne, "WHERE ID_utilisateur = '" + id_utilisateur + "'");
            ViewData["Ancienne_valeur"] = list[0];
            Console.WriteLine("Recalcule de l'ancienne variable");

            bool saisie_string_valide = !string.IsNullOrEmpty(saisie_string);
            bool saisie_num_tel_valide = !string.IsNullOrEmpty(saisie_num_tel) && EstNumeroTelCorrect(saisie_num_tel);
            bool saisie_adresse_mail_valide = EstAdresseMailCorrect(saisie_adresse_mail);
            bool saisie_adresse_num_valide = !string.IsNullOrEmpty(saisie_adresse_num);
            bool saisie_adresse_rue_valide = !string.IsNullOrEmpty(saisie_adresse_rue);
            bool saisie_adresse_ville_valide = !string.IsNullOrEmpty(saisie_adresse_ville);
            bool saisie_adresse_code_postal_valide = !string.IsNullOrEmpty(saisie_adresse_code_postal);

            //if (saisie_string_valide)
            //{
            //    Utilisateur.MettreAjourTupleColonneUtilisateur("'" + id_utilisateur + "'", nom_colonne, "'" + saisie_string + "'", "");
            //    return RedirectToPage("Page_accueil_connecte");
            //}
            //else if (!saisie_string_valide)
            //{
            //    TempData["Erreur_saisie"] = "Vous n'avez pas saisi ce qu'il faut, refaites s'il-vous-plaît";
            //    Type_colonne = type_colonne;
            //    Nom_colonne = nom_colonne;
            //    return Page();
            //}

            if (type_colonne == "adresse")
            {
                if (saisie_adresse_num_valide && saisie_adresse_rue_valide && saisie_adresse_ville_valide && saisie_adresse_code_postal_valide)
                {
                    bool adresse_valide = await Adresse_a_coordonees.GetCoords(saisie_adresse_num + " " + saisie_adresse_rue, saisie_adresse_ville, saisie_adresse_code_postal, "France");
                    Console.WriteLine(saisie_adresse_num + " " + saisie_adresse_rue, saisie_adresse_ville, saisie_adresse_code_postal, "France");
                    if (adresse_valide)
                    {
                        Utilisateur.MettreAjourTupleColonneUtilisateur("'" + id_utilisateur + "'", nom_colonne, "'" + saisie_adresse_num + " " + saisie_adresse_rue + ", " + saisie_adresse_ville + ", " + saisie_adresse_code_postal + "'", "");
                        return RedirectToPage("Page_accueil_connecte");
                    }
                    else
                    {
                        TempData["Erreur_saisie"] = "Vous n'avez pas saisi correctement l'adresse, elle doit être constituée d'un numéro de rue, de la rue où vous habitez, de votre ville et de votre code postal";
                    }
                }
                else
                {
                    TempData["Erreur_saisie"] = "Vous n'avez pas saisi correctement l'adresse, elle doit être constituée d'un numéro de rue, de la rue où vous habitez, de votre ville et de votre code postal";
                }
            }
            
            if (type_colonne == "int")
            {
                if (saisie_num_tel_valide)
                {
                    Utilisateur.MettreAjourTupleColonneUtilisateur(id_utilisateur, nom_colonne, "'" + saisie_num_tel + "'", "");
                    return RedirectToPage("Page_accueil_connecte");
                }
                else if (!saisie_num_tel_valide)
                {
                    TempData["Erreur_saisie"] = "Le numéro n'est pas correct, il doit commencer par un 0";
                }
            }

            if (type_colonne == "mail")
            {
                if (saisie_adresse_mail_valide)
                {
                    Utilisateur.MettreAjourTupleColonneUtilisateur(id_utilisateur, nom_colonne, "'" + saisie_adresse_mail + "'", "");
                    return RedirectToPage("Page_accueil_connecte");
                }
                else if (!saisie_string_valide)
                {
                    TempData["Erreur_saisie"] = "Vous n'avez pas saisi ce qu'il faut, refaites s'il-vous-plaît";
                }
            }
            
            if (type_colonne == "string")
            {
                if (saisie_string_valide)
                {
                    Console.WriteLine("id de l'utilisateur : " +  id_utilisateur);
                    Utilisateur.MettreAjourTupleColonneUtilisateur(id_utilisateur, nom_colonne, "'" + saisie_string + "'", "");
                    return RedirectToPage("Page_accueil_connecte");
                }
                else if (!saisie_string_valide)
                {
                    TempData["Erreur_saisie"] = "Vous n'avez pas saisi ce qu'il faut, refaites s'il-vous-plaît";
                }
            }
            
            Type_colonne = type_colonne;
            Nom_colonne = nom_colonne;
            return Page();
        }
    }
}