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
        }
    }
}
