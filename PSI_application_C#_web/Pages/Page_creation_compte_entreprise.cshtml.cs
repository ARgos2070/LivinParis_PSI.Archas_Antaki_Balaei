using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_creation_compte_entrepriseModel : PageModel
    {
        private readonly ILogger<Page_creation_compte_entrepriseModel> _logger;

        [BindProperty]
        public string saisie_prenom { get; set; } // Pour le prénom

        [BindProperty]
        public string saisie_nom { get; set; } // Pour le nom

        [BindProperty]
        public string saisie_mot_de_passe { get; set; }

        [BindProperty]
        public string saisie_adresse { get; set; }

        [BindProperty]
        public string saisie_num_tel { get; set; } //On déclare le numéro de téléphone sous la forme d'un string
                                                   //car si on le déclare directement en int, la valeur par défaut de num_tel
                                                   //sera 0, et cette valeur par défaut apparaîtra dans le champs (non rempli)
                                                   //de num_tel, ce que l'on veut éviter.
                                                   //Mais dans le code cshtml, on impose que le numéro de téléphone
                                                   //ne peut être soumis que s'il est un ensemble de chiffres
                                                   //donc on pourra directement transformer ce string en int sans avoir d'erreur.

        public string saisie_adresse_mail { get; set; }

        [BindProperty]
        public bool saisie_est_entreprise { get; set; }

        [BindProperty]
        public string saisie_nom_entreprise { get; set; }

        [BindProperty]
        public bool saisie_est_client { get; set; }

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

        public IActionResult OnPost()
        {
            Console.WriteLine(string.IsNullOrWhiteSpace(saisie_adresse_mail));
            if (string.IsNullOrWhiteSpace(saisie_prenom))
            {
                ViewData["Erreur_prenom"] = "Un prénom est requis.";
            }

            if (string.IsNullOrWhiteSpace(saisie_nom))
            {
                ViewData["Erreur_nom"] = "Un nom est requis.";
            }

            if (string.IsNullOrWhiteSpace(saisie_mot_de_passe))
            {
                ViewData["Erreur_mot_de_passe"] = "Un mot de passe est requis.";
            }

            if (string.IsNullOrWhiteSpace(saisie_adresse))
            {
                ViewData["Erreur_adresse"] = "Une adresse est requise.";
            }

            //if (string.IsNullOrWhiteSpace(saisie_num_tel))
            //{
            //    ViewData["Erreur_num_tel"] = "Un numéro de téléphone est requis.";
            //}

            //if (string.IsNullOrWhiteSpace(saisie_adresse_mail))
            //{
            //    ViewData["Erreur_adresse_mail"] = "Une adresse mail au format valide est requise.";
            //}
            if (string.IsNullOrWhiteSpace(saisie_prenom) ||
                string.IsNullOrWhiteSpace(saisie_nom) ||
                string.IsNullOrWhiteSpace(saisie_mot_de_passe) ||
                string.IsNullOrWhiteSpace(saisie_adresse))
            {
                return Page();
            }
            Console.WriteLine("Numéro en saisie : " + saisie_num_tel);
            string prenom_utilisateur = saisie_prenom;
            string nom_utilisateur = saisie_nom;
            string mot_de_passe_utilisateur = saisie_mot_de_passe;
            string adresse_utilisateur = saisie_adresse;
            //string adresse_mail_utilisateur = saisie_adresse_mail;
            float num_utilisateur = 0;
            if (!string.IsNullOrWhiteSpace(saisie_num_tel))
                if (!string.IsNullOrWhiteSpace(saisie_num_tel))
                {
                    if (float.TryParse(saisie_num_tel, out num_utilisateur))
                    {
                        // La conversion a réussi, vous pouvez utiliser num_utilisateur
                        Console.WriteLine($"Numéro de téléphone converti : {num_utilisateur}");
                    }
                    else
                    {
                        // La conversion a échoué
                        ViewData["Erreur_num_tel"] = "Le numéro de téléphone doit être un nombre valide.";
                        return Page();
                    }
                }
                else
                {
                    // Le champ est vide ou nul
                    ViewData["Erreur_num_tel"] = "Un numéro de téléphone est requis.";
                    return Page();
                }

            Console.WriteLine("Numéro en entier : " + num_utilisateur);
            bool utilisateur_est_entreprise = saisie_est_entreprise;
            string nom_entreprise_utilisateur = saisie_nom_entreprise;

            //if (!est_entreprise)
            //{
            //    return RedirectToPage("Page_1er_chargement");
            //}
            return RedirectToPage("Ajout_plat");
        }
    }
}
