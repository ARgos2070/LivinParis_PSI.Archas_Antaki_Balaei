using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_creation_compte_clientModel : PageModel
    {
        private readonly ILogger<Page_creation_compte_clientModel> _logger;

        [BindProperty]
        public string saisie_prenom { get; set; } // Pour le prénom

        
        public string saisie_nom { get; set; } // Pour le nom

       
        public string saisie_mot_de_passe { get; set; }
        
   
        public string saisie_adresse { get; set; }

     
        public string saisie_num_tel { get; set; } //On déclare le numéro de téléphone sous la forme d'un string
                                                   //car si on le déclare directement en int, la valeur par défaut de num_tel
                                                   //sera 0, et cette valeur par défaut apparaîtra dans le champs (non rempli)
                                                   //de num_tel, ce que l'on veut éviter.
                                                   //Mais dans le code cshtml, on impose que le numéro de téléphone
                                                   //ne peut être soumis que s'il est un ensemble de chiffres
                                                   //donc on pourra directement transformer ce string en int sans avoir d'erreur.

        public string saisie_adresse_mail { get; set; }

     
        public bool saisie_est_entreprise { get; set; }

   
        public string saisie_nom_entreprise { get; set; }

    
        public bool saisie_est_client { get; set; }

        public bool saisie_est_cuisinier { get; set; }


        public bool saisie_est_livreur { get; set; }

        public Page_creation_compte_clientModel(ILogger<Page_creation_compte_clientModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string prenom_utilisateur = saisie_prenom;
            string nom_utilisateur = saisie_nom;
            string mot_de_passe_utilisateur = saisie_mot_de_passe;
            string adresse_utilisateur = saisie_adresse;
            int num_utilisateur;
            if (string.IsNullOrWhiteSpace(saisie_num_tel))
            {
                ViewData["Erreur_num_tel"] = "Un numéro de téléphone valide est requis.";
                return Page();
            }
            else
            {
                num_utilisateur = int.Parse(saisie_num_tel);
            }
            bool utilisateur_est_entreprise = saisie_est_entreprise;
            string nom_entreprise_utilisateur = saisie_nom_entreprise;
            Console.WriteLine(prenom_utilisateur);
            
            //if (!est_entreprise)
            //{
            //    return RedirectToPage("Page_1er_chargement");
            //}
            return Page();
        }
    }
}
