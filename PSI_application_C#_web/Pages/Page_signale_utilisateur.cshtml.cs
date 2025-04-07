using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_signale_utilisateurModel : PageModel
    {
        private readonly ILogger<Page_connexion_compteModel> _logger;

        [BindProperty]
        public string nom_utilisateur_signale { get; set; }

        [BindProperty]
        public string pleinte { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            bool id_valide = nom_utilisateur_signale != null && nom_utilisateur_signale.Length > 0;
            bool pleinte_valide = pleinte != null && pleinte.Length > 0;
            if (id_valide == false)
            {
                ViewData["Erreur_id_utilisateur_aucune_saisie"] = "Veuillez renseigner votre pseudonyme (id utilisateur).";
            }
            if (pleinte_valide == false)
            {
                ViewData["Erreur_pleinte_saisie"] = "votre pleinte est requis.";
            }
            if (id_valide == false || pleinte_valide == false)
            {
                return Page();
            }
            if (Utilisateur.Identifiant_utilisateur_nouveau_dans_bdd(nom_utilisateur_signale) == false)
            {
                ViewData["Erreur_id_utilisateur_incorrect"] = "Ce pseudonyme (id utilisateur) n'est pas correct. Veuillez réessayer.";
            }
            Utilisateur.UnSignalementRecu(id_valide.ToString());
            //pleinte stocker nul part
            return Page();
        }
    }
}
