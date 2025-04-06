using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_finaliser_commandeModel : PageModel
    {
        private readonly ILogger<Page_finaliser_commandeModel> _logger;

        [BindProperty]
        public bool saisie_arreter_commande { get; set; }

        [BindProperty]
        public bool saisie_continuer_commande { get; set; }

        [BindProperty]
        public bool saisie_annuler_commande { get; set; }

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
            if (saisie_continuer_commande == true && saisie_arreter_commande == false && saisie_annuler_commande == false)
            {
                return RedirectToPage("Page_commander");
            }
            if (saisie_continuer_commande == false && saisie_arreter_commande == true && saisie_annuler_commande == false)
            {
                return RedirectToPage("Page_accueil_connecte");
            }
            if (saisie_continuer_commande == false && saisie_arreter_commande == false && saisie_annuler_commande == true)
            {
                int id_commande_passe = (int)TempData["Id_commande_memoire"];
                Commande.SupprimerCommande(id_commande_passe);
                return RedirectToPage("Page_accueil_connecte");
            }
            ViewData["Erreur_saisir_un_choix"] = "Il ne faut saisir qu'une seule option merci";
            return Page();
        }
    }
}
