using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_select_colonne_update_utilisateurModel : PageModel
    {
        private readonly ILogger<Page_select_colonne_update_utilisateurModel> _logger;

        [BindProperty]
        public string Option_choisie { get; set; }

        public Page_select_colonne_update_utilisateurModel(ILogger<Page_select_colonne_update_utilisateurModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
            TempData["Id_plat_a_modifier"] = TempData["Id_plat_a_modifier"];
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Option_choisie))
            {
                ViewData["Erreur_selection"] = "Vous devez sélectionner une propriété.";
                return Page();
            }

            switch (Option_choisie)
            {
                case "Nom_utilisateur":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Nom_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Prénom_utilisateur":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Prénom_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Mot_de_passe_utilisateur":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Mot_de_passe_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Adresse_utilisateur":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Adresse_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Num_tel_utilisateur":
                    TempData["Type_colonne"] = "int";
                    TempData["Colonne_update"] = "Num_tel_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "adresse_mail_utilisateur":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "adresse_mail_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Nom_entreprise":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Date_péremption_plat";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Continuer_etre_entreprise":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Continuer_etre_entreprise";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                default:
                    ViewData["Erreur_affichage"] = "Veuillez sélectionner une option valide.";
                    return Page();
            }
        }
    }
}
