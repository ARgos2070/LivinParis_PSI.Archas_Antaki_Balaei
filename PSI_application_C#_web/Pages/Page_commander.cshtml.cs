using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_commanderModel : PageModel
    {
        private readonly ILogger<Page_commanderModel> _logger;

        [BindProperty]
        public string Option_choisie { get; set; }

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
            if (Option_choisie == null || Option_choisie.Length == 0)
            {
                ViewData["Erreur_affichage"] = "Veuillez sélectionner une option";
                return Page();
            }

            switch (Option_choisie)
            {
                case "Afficher_tout":
                    TempData["Affiche_tout"] = true;
                    TempData["Affiche_nationalite"] = false;
                    TempData["Affiche_regime_alimentaire"] = false;
                    TempData["Affiche_prix_croissant"] = false;
                    TempData["Affiche_prix_decroissant"] = false;
                    return RedirectToPage("Page_affichage_plat");
                case "afficher_par_nationalite":
                    TempData["Affiche_tout"] = false;
                    TempData["Affiche_nationalite"] = true;
                    TempData["Affiche_regime_alimentaire"] = false;
                    TempData["Affiche_prix_croissant"] = false;
                    TempData["Affiche_prix_decroissant"] = false;
                    TempData["Filtre"] = "Nationalité_cuisine_Plat";
                    return RedirectToPage("Page_ecriture_filtre_commande");
                case "afficher_par_regime_alimentaire":
                    TempData["Affiche_tout"] = false;
                    TempData["Affiche_nationalite"] = false;
                    TempData["Affiche_regime_alimentaire"] = true;
                    TempData["Affiche_prix_croissant"] = false;
                    TempData["Affiche_prix_decroissant"] = false;
                    TempData["Filtre"] = "Régime_alimentaire_Plat";
                    return RedirectToPage("Page_ecriture_filtre_commande");
                case "afficher_par_prix_croissant":
                    TempData["Affiche_tout"] = false;
                    TempData["Affiche_nationalite"] = false;
                    TempData["Affiche_regime_alimentaire"] = false;
                    TempData["Affiche_prix_croissant"] = true;
                    TempData["Affiche_prix_decroissant"] = false;
                    return RedirectToPage("Page_affichage_plat");
                case "afficher_par_prix_decroissant":
                    TempData["Affiche_tout"] = false;
                    TempData["Affiche_nationalite"] = false;
                    TempData["Affiche_regime_alimentaire"] = false;
                    TempData["Affiche_prix_croissant"] = false;
                    TempData["Affiche_prix_decroissant"] = true;
                    return RedirectToPage("Page_affichage_plat");
                default:
                    ViewData["Erreur_affichage"] = "Veuillez sélectionner une option valide.";
                    return Page();
            }
        }
    }
}
