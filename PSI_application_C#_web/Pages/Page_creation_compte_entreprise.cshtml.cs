using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlX.XDevAPI;

namespace PSI_application_C__web.Pages
{
    public class Page_creation_compte_entrepriseModel : PageModel
    {
        private readonly ILogger<Page_creation_compte_entrepriseModel> _logger;

        [BindProperty]
        public string saisie_id_utilisateur { get; set; }

        [BindProperty]
        public string saisie_prenom { get; set; } // Pour le prénom

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
        public string saisie_num_tel { get; set; } //On déclare le numéro de téléphone sous la forme d'un string
                                                   //car si on le déclare directement en int, la valeur par défaut de num_tel
                                                   //sera 0, et cette valeur par défaut apparaîtra dans le champs (non rempli)
                                                   //de num_tel, ce que l'on veut éviter.
                                                   //Mais dans le code cshtml, on impose que le numéro de téléphone
                                                   //ne peut être soumis que s'il est un ensemble de chiffres
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

        public Page_creation_compte_entrepriseModel(ILogger<Page_creation_compte_entrepriseModel> logger)
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

        public IActionResult OnPost()
        {
            bool id_utilisateur_valide = Utilisateur.Identifiant_utilisateur_nouveau_dans_bdd(saisie_id_utilisateur);
            bool prenom_valide = saisie_prenom != null && saisie_prenom.Length > 0;
            bool nom_valide = saisie_nom != null && saisie_nom.Length > 0;
            bool mot_de_passe_valide = saisie_mot_de_passe != null && saisie_mot_de_passe.Length > 0;
            bool adresse_num_rue_valide = saisie_adresse_num_rue != null && saisie_adresse_num_rue.Length > 0;
            bool adresse_nom_rue_valide = saisie_adresse_nom_rue != null && saisie_adresse_nom_rue.Length > 0;
            bool adresse_ville_valide = saisie_adresse_ville != null && saisie_adresse_ville.Length > 0;
            bool num_tel_valide = EstNumeroTelCorrect(saisie_num_tel);
            bool adresse_mail_valide = EstAdresseMailCorrect(saisie_adresse_mail);
            bool nom_entreprise_valide = saisie_nom_entreprise != null && saisie_nom_entreprise.Length > 0;
            if (!id_utilisateur_valide)
            {
                ViewData["Erreur_id_utilisateur"] = "Ce pseudonyme (id utilisateur) a déjà été pris. Veuillez en choisir un autre.";
            }
            if (!prenom_valide)
            {
                ViewData["Erreur_prenom"] = "Un prénom est requis.";
            }
            if (!nom_valide)
            {
                ViewData["Erreur_nom"] = "Un nom est requis.";
            }
            if (!mot_de_passe_valide)
            {
                ViewData["Erreur_mot_de_passe"] = "Un mot de passe est requis.";
            }
            if (!adresse_num_rue_valide)
            {
                ViewData["Erreur_adresse_num_rue"] = "Un numéro de rue est requis.";
            }
            if (!adresse_nom_rue_valide)
            {
                ViewData["Erreur_adresse_nom_rue"] = "Un nom de rue est requis.";
            }
            if (!adresse_ville_valide)
            {
                ViewData["Erreur_adresse_ville"] = "Une ville est requise.";
            }
            if (!num_tel_valide)
            {
                ViewData["Erreur_num_tel"] = "Il faut que votre numéro de téléphone commence par un 0.";
            }
            if (!adresse_mail_valide)
            {
                ViewData["Erreur_adresse_mail"] = "Une adresse mail correcte est requise.";
            }
            if (!nom_entreprise_valide)
            {
                ViewData["Erreur_nom_entreprise"] = "Un nom d'entreprise est requis.";
            }
            if (!EstPasUtilisateurSansRole(saisie_est_client, saisie_est_cuisinier, saisie_est_livreur))
            {
                ViewData["Erreur_client"] = "Il faut que vous séléctionnez au moins un rôle.";
            }
            if (!id_utilisateur_valide || !prenom_valide || !nom_valide || !mot_de_passe_valide || !adresse_num_rue_valide || !adresse_nom_rue_valide || !adresse_ville_valide || !adresse_mail_valide || !num_tel_valide || !nom_entreprise_valide
                || !EstPasUtilisateurSansRole(saisie_est_client, saisie_est_cuisinier, saisie_est_livreur))
            {
                return Page();
            }
            string id_utilisateur = saisie_id_utilisateur;
            string prenom_utilisateur = saisie_prenom;
            string nom_utilisateur = saisie_nom;
            string mot_de_passe_utilisateur = saisie_mot_de_passe;
            string adresse_utilisateur = saisie_adresse_num_rue + " " + saisie_adresse_nom_rue + ", " + saisie_adresse_ville;
            string adresse_mail_utilisateur = saisie_adresse_mail;
            string num_utilisateur = saisie_num_tel;
            //Console.WriteLine("Numéro en entier : " + num_utilisateur);
            bool utilisateur_est_entreprise = true;
            string nom_entreprise_utilisateur = saisie_nom_entreprise;
            bool est_client = saisie_est_client;
            bool est_cuisinier = saisie_est_cuisinier;
            bool est_livreur = saisie_est_livreur;
            Utilisateur entreprise_creee = new Utilisateur(id_utilisateur, mot_de_passe_utilisateur, nom_utilisateur, prenom_utilisateur, adresse_utilisateur, num_utilisateur, adresse_mail_utilisateur, true, nom_entreprise_utilisateur);
            Utilisateur.AjoutUtilisateurBDD(entreprise_creee);
            if (est_client == true && est_cuisinier == true && est_livreur == true)
            {
                Client client_cree = new Client(entreprise_creee);
                Client.AjoutClientBDD(client_cree);
                Cuisinier cuisinier_cree = new Cuisinier(entreprise_creee);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                Livreur livreur_cree = new Livreur(entreprise_creee);
                Livreur.AjoutLivreurBDD(livreur_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == false && est_cuisinier == true && est_livreur == true)
            {
                Cuisinier cuisinier_cree = new Cuisinier(entreprise_creee);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                Livreur livreur_cree = new Livreur(entreprise_creee);
                Livreur.AjoutLivreurBDD(livreur_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == true && est_cuisinier == false && est_livreur == true)
            {
                Client client_cree = new Client(entreprise_creee);
                Client.AjoutClientBDD(client_cree);
                Livreur livreur_cree = new Livreur(entreprise_creee);
                Livreur.AjoutLivreurBDD(livreur_cree);
            }
            if (est_client == true && est_cuisinier == true && est_livreur == false)
            {
                Client client_cree = new Client(entreprise_creee);
                Client.AjoutClientBDD(client_cree);
                Cuisinier cuisinier_cree = new Cuisinier(entreprise_creee);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == true && est_cuisinier == false && est_livreur == false)
            {
                Client client_cree = new Client(entreprise_creee);
                Client.AjoutClientBDD(client_cree);
            }
            if (est_client == false && est_cuisinier == true && est_livreur == false)
            {
                Cuisinier cuisinier_cree = new Cuisinier(entreprise_creee);
                Cuisinier.AjoutCuisinierBDD(cuisinier_cree);
                TempData["Id_cuisinier"] = cuisinier_cree.Id_cuisinier;
                return RedirectToPage("Page_creation_plat");
            }
            if (est_client == false && est_cuisinier == false && est_livreur == true)
            {
                Livreur livreur_cree = new Livreur(entreprise_creee);
                Livreur.AjoutLivreurBDD(livreur_cree);
            }

            return RedirectToPage("Page_creation_plat");
        }
    }
}
