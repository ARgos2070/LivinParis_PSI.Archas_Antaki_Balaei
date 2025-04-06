using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_commanderModel : PageModel
    {
        private readonly ILogger<Page_commanderModel> _logger;

        [BindProperty]
        public bool saisie_afficher_tout { get; set; }

        [BindProperty]
        public bool saisie_afficher_par_nationalite { get; set; }

        [BindProperty]
        public bool saisie_afficher_par_regime_alimentaire { get; set; }

        [BindProperty]
        public bool saisie_afficher_par_prix_croissant { get; set; }

        [BindProperty]
        public bool saisie_afficher_par_prix_decroissant { get; set; }

        public Page_commanderModel(ILogger<Page_commanderModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
        }

        public IActionResult OnPost()
        {
            TempData["Affiche_tout"] = saisie_afficher_tout;
            TempData["Affiche_nationalite"] = saisie_afficher_par_nationalite;
            TempData["Affiche_regime_alimentaire"] = saisie_afficher_par_regime_alimentaire;
            TempData["Affiche_prix_croissant"] = saisie_afficher_par_prix_croissant;
            TempData["Affiche_prix_decroissant"] = saisie_afficher_par_prix_decroissant;
            if (saisie_afficher_tout == true && saisie_afficher_par_nationalite == false && saisie_afficher_par_regime_alimentaire == false && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == false)
            {
                return RedirectToPage("Page_affichage_plat");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == true && saisie_afficher_par_regime_alimentaire == false && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == false)
            {
                TempData["Filtre"] = "Nationalité_cuisine_Plat";
                Console.WriteLine("Je suis rentré dans la matrice");
                return RedirectToPage("Page_ecriture_filtre_commande");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == false && saisie_afficher_par_regime_alimentaire == true && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == false)
            {
                TempData["Filtre"] = "Régime_alimentaire_Plat";
                return RedirectToPage("Page_ecriture_filtre_commande");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == false && saisie_afficher_par_regime_alimentaire == false && saisie_afficher_par_prix_croissant == true && saisie_afficher_par_prix_decroissant == false)
            {
                return RedirectToPage("Page_affichage_plat");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == false && saisie_afficher_par_regime_alimentaire == false && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == true)
            {
                return RedirectToPage("Page_affichage_plat");
            }
            ViewData["Erreur_affichage"] = "Veuillez ne selectionner qu'un seul filtre";
            return Page();
        }
    }
}
