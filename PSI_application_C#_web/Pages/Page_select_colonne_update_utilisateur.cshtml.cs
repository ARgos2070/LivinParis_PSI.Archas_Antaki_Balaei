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
                    TempData["Colonne_update"] = "Nom_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Prénom_utilisateur":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Type_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Mot_de_passe_utilisateur":
                    TempData["Type_colonne"] = "int";
                    TempData["Colonne_update"] = "Pr_cmb_de_personnes_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Adresse_utilisateur":
                    TempData["Type_colonne"] = "double";
                    TempData["Colonne_update"] = "Prix_par_portion_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Num_tel_utilisateur":
                    TempData["Type_colonne"] = "int";
                    TempData["Colonne_update"] = "Nbre_portion_dispo_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "adresse_mail_utilisateur":
                    TempData["Type_colonne"] = "date";
                    TempData["Colonne_update"] = "Date_fabrication_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Nom_entreprise":
                    TempData["Type_colonne"] = "date";
                    TempData["Colonne_update"] = "Date_péremption_plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                case "Arreter_etre_entreprise":
                    TempData["Type_colonne"] = "string";
                    TempData["Colonne_update"] = "Nationalité_cuisine_Plat";
                    return RedirectToPage("Page_modification_colonne_plat");
                default:
                    ViewData["Erreur_affichage"] = "Veuillez sélectionner une option valide.";
                    return Page();
            }
        }
    }
}
