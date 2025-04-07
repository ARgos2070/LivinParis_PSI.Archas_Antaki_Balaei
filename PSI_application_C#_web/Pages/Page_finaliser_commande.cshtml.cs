using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_finaliser_commandeModel : PageModel
    {
        private readonly ILogger<Page_finaliser_commandeModel> _logger;

        [BindProperty]
        public string Option_choisie { get; set; }

        public Page_finaliser_commandeModel(ILogger<Page_finaliser_commandeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
            TempData["Id_commande_memoire"] = TempData["Id_commande_memoire"];
        }

        public IActionResult OnPost()
        {
            if (Option_choisie == null || Option_choisie.Length == 0)
            {
                ViewData["Erreur_choix_finaliser_commande"] = "Veuillez sélectionner une option.";
                return Page();
            }

            switch (Option_choisie)
            {
                case "Continuer_commande":
                    return RedirectToPage("Page_commander");
                case "Arreter_commande":
                    return RedirectToPage("Page_accueil_connecte");
                case "annuler_commande":
                    int id_commande_passe = (int)TempData["Id_commande_memoire"];
                    Commande.SupprimerCommande(id_commande_passe);
                    return RedirectToPage("Page_accueil_connecte");
                default:
                    ViewData["Erreur_choix_finaliser_commande"] = "Veuillez sélectionner une option valide.";
                    return Page();
            }
        }
    }
}
