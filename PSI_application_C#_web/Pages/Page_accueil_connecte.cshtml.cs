using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_accueil_connecteModel : PageModel
    {
        [BindProperty]
        public bool Est_cuisinier { get; set; }

        [BindProperty]
        public bool Est_client { get; set; }

        [BindProperty]
        public bool Est_livreur { get; set; }

        public void OnGet()
        {
            string id_utilisateur = (string)TempData["Id_utilisateur"];
            Est_cuisinier = Cuisinier.UtilisateurEstCuisinier(id_utilisateur);
            Est_client = Client.UtilisateurEstClient(id_utilisateur);
            Est_livreur = Livreur.UtilisateurEstLivreur(id_utilisateur);
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
