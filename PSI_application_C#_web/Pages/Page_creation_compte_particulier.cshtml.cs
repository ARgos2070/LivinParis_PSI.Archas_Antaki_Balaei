using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace PSI_application_C__web.Pages
{
    public class Page_creation_compte_particulierModel : PageModel
    {
        private readonly ILogger<Page_creation_compte_particulierModel> _logger;

        [BindProperty]
        public string saisie_id_utilisateur { get; set; }

        [BindProperty]
        public string saisie_prenom { get; set; } // Pour le pr�nom

        [BindProperty]
        public string saisie_nom { get; set; } // Pour le nom

        [BindProperty]
        public string saisie_mot_de_passe { get; set; }

        //[BindProperty]
        //public string saisie_adresse { get; set; }

        [BindProperty]
        public string saisie_adresse_num_rue { get; set; }

        [BindProperty]
        public string saisie_adresse_nom_rue { get; set; }

        [BindProperty]
        public string saisie_adresse_ville { get; set; }

        [BindProperty]
        public string saisie_adresse_code_postal { get; set; }

        [BindProperty]
        public string saisie_num_tel { get; set; } //On d�clare le num�ro de t�l�phone sous la forme d'un string
                                                   //car si on le d�clare directement en int, la valeur par d�faut de num_tel
                                                   //sera 0, et cette valeur par d�faut appara�tra dans le champs (non rempli)
                                                   //de num_tel, ce que l'on veut �viter.
                                                   //Mais dans le code cshtml, on impose que le num�ro de t�l�phone
                                                   //ne peut �tre soumis que s'il est un ensemble de chiffres
                                                   //donc on pourra directement transformer ce string en int sans avoir d'erreur.
        [BindProperty]
        public string saisie_adresse_mail { get; set; }

        [BindProperty]
        public bool saisie_est_entreprise { get; set; }

        [BindProperty]
        public string saisie_nom_entreprise { get; set; }

        [BindProperty]
        public bool saisie_est_client { get; set; }

        [BindProperty]
        public bool saisie_est_cuisinier { get; set; }

        [BindProperty]
        public bool saisie_est_livreur { get; set; }

        public Page_creation_compte_particulierModel(ILogger<Page_creation_compte_particulierModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
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
            bool id_utilisateur_valide = Utilisateur.Identifiant_utilisateur_nouveau_dans_bdd(saisie_id_utilisateur);
            bool prenom_valide = saisie_prenom != null && saisie_prenom.Length > 0;
            bool nom_valide = saisie_nom != null && saisie_nom.Length > 0;
            bool mot_de_passe_valide = saisie_mot_de_passe != null && saisie_mot_de_passe.Length > 0;
            bool adresse_num_rue_valide = saisie_adresse_num_rue != null && saisie_adresse_num_rue.Length > 0;
            bool adresse_nom_rue_valide = saisie_adresse_nom_rue != null && saisie_adresse_nom_rue.Length > 0;
            bool adresse_ville_valide = saisie_adresse_ville != null && saisie_adresse_ville.Length > 0;
            bool adresse_code_postal_valide = saisie_adresse_code_postal != null && saisie_adresse_code_postal.Length > 0;
            bool num_tel_valide = EstNumeroTelCorrect(saisie_num_tel);
            bool addresse_valide = await Adresse_a_coordonees.GetCoords(saisie_adresse_num_rue + " " + saisie_adresse_nom_rue,
                saisie_adresse_ville, saisie_adresse_code_postal, "France");
            bool adresse_mail_valide = EstAdresseMailCorrect(saisie_adresse_mail);
            if (id_utilisateur_valide == false)
            {
                ViewData["Erreur_id_utilisateur"] = "Ce pseudonyme (id utilisateur) a d�j� �t� pris. Veuillez en choisir un autre.";
            }
            if (prenom_valide == false)
            {
                ViewData["Erreur_prenom"] = "Un pr�nom est requis.";
            }
            if (nom_valide == false)
            {
                ViewData["Erreur_nom"] = "Un nom est requis.";
            }
            if (mot_de_passe_valide == false)
            {
                ViewData["Erreur_mot_de_passe"] = "Un mot de passe est requis.";
            }
            if (adresse_num_rue_valide == false)
            {
                ViewData["Erreur_adresse_num_rue"] = "Un num�ro de rue est requis.";
            }
            if (adresse_nom_rue_valide == false)
            {
                ViewData["Erreur_adresse_nom_rue"] = "Un nom de rue est requis.";
            }
            if (adresse_ville_valide == false)
            {
                ViewData["Erreur_adresse_ville"] = "Une ville est requise.";
            }
            if (adresse_code_postal_valide == false)
            {
                ViewData["Erreur_adresse_code_postal"] = "Un code postal est requis.";
            }
            if (addresse_valide == false)
            {
                ViewData["Erreur_adresse_reseau"] = "Notre service ne dessert pas cette adresse.";
            }
            if (num_tel_valide == false)
            {
                ViewData["Erreur_num_tel"] = "Il faut que votre num�ro de t�l�phone commence par un 0.";
            }
            if (adresse_mail_valide == false)
            {
                ViewData["Erreur_adresse_mail"] = "Une adresse mail correcte est requise.";
            }
            if (EstPasUtilisateurSansRole(saisie_est_client, saisie_est_cuisinier, saisie_est_livreur) == false)
            {
                ViewData["Erreur_client"] = "Il faut que vous s�l�ctionnez au moins un r�le.";
            }
            if (id_utilisateur_valide == false || prenom_valide == false || nom_valide == false || mot_de_passe_valide == false || adresse_num_rue_valide == false || adresse_nom_rue_valide == false || adresse_ville_valide == false || adresse_mail_valide == false || num_tel_valide == false
                ||adresse_code_postal_valide == false || !EstPasUtilisateurSansRole(saisie_est_client, saisie_est_cuisinier, saisie_est_livreur)
                || await Adresse_a_coordonees.GetCoords(saisie_adresse_num_rue + " " + saisie_adresse_nom_rue, saisie_adresse_ville, saisie_adresse_code_postal, "France") == false)
            {
                return Page();
            }
            string id_utilisateur = saisie_id_utilisateur;
            string prenom_utilisateur = saisie_prenom;
            string nom_utilisateur = saisie_nom;
            string mot_de_passe_utilisateur = saisie_mot_de_passe;
            string adresse_utilisateur = saisie_adresse_num_rue + " " + saisie_adresse_nom_rue + ", " + saisie_adresse_ville + ", " + saisie_adresse_code_postal;
            string adresse_mail_utilisateur = saisie_adresse_mail;
            string num_utilisateur = saisie_num_tel;
            bool utilisateur_est_entreprise = true;
            string nom_entreprise_utilisateur = saisie_nom_entreprise;
            bool est_client = saisie_est_client;
            bool est_cuisinier = saisie_est_cuisinier;
            bool est_livreur = saisie_est_livreur;
            Utilisateur particulier_cree = new Utilisateur(id_utilisateur, mot_de_passe_utilisateur, nom_utilisateur, prenom_utilisateur, adresse_utilisateur, num_utilisateur, adresse_mail_utilisateur, false, null);
            Utilisateur.AjoutUtilisateurBDD(particulier_cree);
            TempData["Id_utilisateur_session"] = id_utilisateur;
            if (est_client == true && est_cuisinier == true && est_livreur == true)
            {
                Client client_cree = new Client(particulier_cree);
                Client.AjoutClientBDD(client_cree);
                Cuisinier cuisinier_cree = new Cuisinier(particulier_cree);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                Livreur livreur_cree = new Livreur(particulier_cree);
                Livreur.AjoutLivreurBDD(livreur_cree);
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == false && est_cuisinier == true && est_livreur == true)
            {
                Cuisinier cuisinier_cree = new Cuisinier(particulier_cree);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                Livreur livreur_cree = new Livreur(particulier_cree);
                Livreur.AjoutLivreurBDD(livreur_cree);
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == true && est_cuisinier == false && est_livreur == true)
            {
                Client client_cree = new Client(particulier_cree);
                Client.AjoutClientBDD(client_cree);
                Livreur livreur_cree = new Livreur(particulier_cree);
                Livreur.AjoutLivreurBDD(livreur_cree);
            }
            if (est_client == true && est_cuisinier == true && est_livreur == false)
            {
                Client client_cree = new Client(particulier_cree);
                Client.AjoutClientBDD(client_cree);
                Cuisinier cuisinier_cree = new Cuisinier(particulier_cree);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == true && est_cuisinier == false && est_livreur == false)
            {
                Client client_cree = new Client(particulier_cree);
                Client.AjoutClientBDD(client_cree);
            }
            if (est_client == false && est_cuisinier == true && est_livreur == false)
            {
                Cuisinier cuisinier_cree = new Cuisinier(particulier_cree);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == false && est_cuisinier == false && est_livreur == true)
            {
                Livreur livreur_cree = new Livreur(particulier_cree);
                Livreur.AjoutLivreurBDD(livreur_cree);
            }
            return RedirectToPage("Page_accueil_connecte");
        }
    }
}
