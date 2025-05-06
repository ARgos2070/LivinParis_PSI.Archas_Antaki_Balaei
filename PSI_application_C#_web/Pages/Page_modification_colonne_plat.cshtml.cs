using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_modification_colonne_platModel : PageModel
    {
        private readonly ILogger<Page_modification_colonne_platModel> _logger;

        [BindProperty]
        public string Type_colonne { get; set; }

        [BindProperty]
        public string Nom_colonne { get; set; }

        [BindProperty]
        public string saisie_string { get; set; }

        [BindProperty]
        public string saisie_int { get; set; }

        [BindProperty]
        public string saisie_double { get; set; }

        [BindProperty]
        public string saisie_date_jour { get; set; }

        [BindProperty]
        public string saisie_date_mois { get; set; }

        [BindProperty]
        public string saisie_date_annee { get; set; }

        public Page_modification_colonne_platModel(ILogger<Page_modification_colonne_platModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            //int id_cuisinier = Cuisinier.IdCuisinierDunUtilisateur(id_utilisateur);
            string transition = TempData["Id_plat_a_modifier"].ToString();
            int id_plat_a_modifier = int.Parse(transition);
            TempData["Id_plat_a_modifier"] = TempData["Id_plat_a_modifier"];
            Type_colonne = TempData["Type_colonne"].ToString();
            TempData["Type_colonne"] = Type_colonne;
            Nom_colonne = TempData["Colonne_update"].ToString();
            TempData["Colonne_update"] = Nom_colonne;
            List<string> list = Plat.RechercherTousLesTuplesDuneColonne(Nom_colonne, "WHERE ID_Plat = " + id_plat_a_modifier + "");
            ViewData["Ancienne_valeur"] = list[0];
            Console.WriteLine("Recalcule de l'ancienne variable");
        }

        public IActionResult OnPost()
        {
            string transition = TempData["Id_plat_a_modifier"].ToString();
            int id_plat_a_modifier = int.Parse(transition);
            TempData["Id_plat_a_modifier"] = TempData["Id_plat_a_modifier"];
            string nom_colonne = TempData["Colonne_update"].ToString();
            TempData["Colonne_update"] = nom_colonne;
            bool saisie_string_valide = saisie_string != null && saisie_string.Length > 0;
            bool saisie_int_valide = saisie_int != null && saisie_int.Length > 0;
            bool saisie_double_valide = saisie_double != null && saisie_double.Length > 0;
            bool saisie_date_jour_valide = saisie_date_jour != null && saisie_date_jour.Length > 0;
            bool saisie_date_mois_valide = saisie_date_mois != null && saisie_date_mois.Length > 0;
            bool saisie_date_annee_valide = saisie_date_annee != null && saisie_date_annee.Length > 0;
            if (saisie_string_valide)
            {
                Plat.MettreAjourTupleColonne(id_plat_a_modifier, nom_colonne, "'" + saisie_string + "'", "");
                return RedirectToPage("Page_accueil_connecte");
            }
            if (saisie_int_valide)
            {
                Plat.MettreAjourTupleColonne(id_plat_a_modifier, nom_colonne, saisie_int, "");
                return RedirectToPage("Page_accueil_connecte");
            }
            if (saisie_double_valide)
            {
                Plat.MettreAjourTupleColonne(id_plat_a_modifier, nom_colonne, saisie_double, "");
                return RedirectToPage("Page_accueil_connecte");
            }
            if (saisie_date_jour_valide|| saisie_date_mois_valide|| saisie_date_annee_valide || Plat.VerifierCoherenceDate(id_plat_a_modifier, nom_colonne, saisie_date_annee + "-" + saisie_date_mois + "-" + saisie_date_jour))
            {
                Plat.MettreAjourTupleColonne(id_plat_a_modifier, nom_colonne, "'" + saisie_date_annee + "-" + saisie_date_mois + "-" + saisie_date_jour + "'", "");
                return RedirectToPage("Page_accueil_connecte");
            }
            return Page();
        }
    }
}
