using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_select_colonne_update_platModel : PageModel
    {
        private readonly ILogger<Page_select_colonne_update_platModel> _logger;

        [BindProperty]
        public string Option_choisie { get; set; }

        public Page_select_colonne_update_platModel(ILogger<Page_select_colonne_update_platModel> logger)
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
                ViewData["Erreur_selection"] = "Vous devez s�lectionner une propri�t�.";
                return Page();
            }

            switch (Option_choisie)
            {
                case "Nom_plat":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Nom_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Type_Plat":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Type_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Pr_cmb_de_personnes_Plat":
                    TempData["Type_colonne"] = "int";
                    TempData["Colonne_update"] = "Pr_cmb_de_personnes_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Prix_par_portion_Plat":
                    TempData["Type_colonne"] = "double";
                    TempData["Colonne_update"] = "Prix_par_portion_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Nbre_portion_dispo_plat":
                    TempData["Type_colonne"] = "int";
                    TempData["Colonne_update"] = "Nbre_portion_dispo_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Date_fabrication_plat":
                    TempData["Type_colonne"] = "date";
                    TempData["Colonne_update"] = "Date_fabrication_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Date_p�remption_plat":
                    TempData["Type_colonne"] = "date";
                    TempData["Colonne_update"] = "Date_p�remption_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Nationalit�_cuisine_Plat":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Nationalit�_cuisine_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "R�gime_alimentaire_Plat":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "R�gime_alimentaire_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Ingr�dients_principaux_Plat":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Ingr�dients_principaux_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                default:
                    ViewData["Erreur_affichage"] = "Veuillez s�lectionner une option valide.";
                    return Page();
            }
        }
    }
}
