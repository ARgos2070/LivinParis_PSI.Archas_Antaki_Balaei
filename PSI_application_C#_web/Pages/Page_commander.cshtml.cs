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
        public bool saisie_afficher_par_ingredient { get; set; }

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
            Console.WriteLine(saisie_afficher_tout.ToString() + saisie_afficher_par_ingredient.ToString() + saisie_afficher_par_nationalite.ToString()
                + saisie_afficher_par_prix_croissant.ToString() + saisie_afficher_par_prix_decroissant.ToString());
        }

        public IActionResult OnPost()
        {
            TempData["Affiche_tout"] = saisie_afficher_tout;
            TempData["Affiche_nationalite"] = saisie_afficher_par_nationalite;
            TempData["Affiche_ingredient"] = saisie_afficher_par_ingredient;
            TempData["Affiche_prix_croissant"] = saisie_afficher_par_prix_croissant;
            TempData["Affiche_prix_decroissant"] = saisie_afficher_par_prix_decroissant;
            if (saisie_afficher_tout == true && saisie_afficher_par_nationalite == false && saisie_afficher_par_ingredient == false && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == false)
            {
                
                return RedirectToPage("Page_affichage");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == true && saisie_afficher_par_ingredient == false && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == false)
            {
                return RedirectToPage("Page_affichage");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == false && saisie_afficher_par_ingredient == true && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == false)
            {
                return RedirectToPage("Page_affichage");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == false && saisie_afficher_par_ingredient == false && saisie_afficher_par_prix_croissant == true && saisie_afficher_par_prix_decroissant == false)
            {
                return RedirectToPage("Page_affichage");
            }
            if (saisie_afficher_tout == false && saisie_afficher_par_nationalite == false && saisie_afficher_par_ingredient == false && saisie_afficher_par_prix_croissant == false && saisie_afficher_par_prix_decroissant == true)
            {
                return RedirectToPage("Page_affichage");
            }
            ViewData["Erreur_affichage"] = "Veuillez ne selectionner qu'un seul filtre";
            return Page();
        }
    }
}
