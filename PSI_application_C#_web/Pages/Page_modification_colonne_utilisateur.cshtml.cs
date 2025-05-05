using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_modification_colonne_utilisateurModel : PageModel
    {
        private readonly ILogger<Page_modification_colonne_utilisateurModel> _logger;

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
        public string saisie_adresse_num { get; set; }

        [BindProperty]
        public string saisie_adresse_rue { get; set; }

        [BindProperty]
        public string saisie_adresse_ville { get; set; }

        [BindProperty]
        public string saisie_adresse_code_postal { get; set; }

        public Page_modification_colonne_utilisateurModel(ILogger<Page_modification_colonne_utilisateurModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            //int id_cuisinier = Cuisinier.IdCuisinierDunUtilisateur(id_utilisateur);
            //string transition = TempData["Id_plat_a_modifier"].ToString();
            //int id_plat_a_modifier = int.Parse(transition);
            //TempData["Id_plat_a_modifier"] = TempData["Id_plat_a_modifier"];
            Type_colonne = TempData["Type_colonne"].ToString();
            TempData["Typle_colonne"] = Type_colonne;
            Nom_colonne = TempData["Colonne_update"].ToString();
            TempData["Colonne_update"] = Nom_colonne;
            List<string> list = Plat.RechercherTousLesTuplesDuneColonne(Nom_colonne, "WHERE ID_utilisateur = " + id_utilisateur + "");
            ViewData["Ancienne_valeur"] = list[0];
            Console.WriteLine("Recalcule de l'ancienne variable");
        }

        public IActionResult OnPost()
        {
            //string transition = TempData["Id_plat_a_modifier"].ToString();
            //int id_plat_a_modifier = int.Parse(transition);
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            string type_colonne = TempData["Type_colonne"].ToString();
            TempData["Typle_colonne"] = type_colonne;
            string nom_colonne = TempData["Colonne_update"].ToString();
            TempData["Colonne_update"] = nom_colonne;
            bool saisie_string_valide = saisie_string != null && saisie_string.Length > 0;
            bool saisie_int_valide = saisie_int != null && saisie_int.Length > 0;
            bool saisie_double_valide = saisie_double != null && saisie_double.Length > 0;
            bool saisie_adresse_num = this.saisie_adresse_num != null && this.saisie_adresse_num.Length > 0;
            bool saisie_adresse_rue = this.saisie_adresse_rue != null && this.saisie_adresse_rue.Length > 0;
            bool saisie_adresse_ville = this.saisie_adresse_ville != null && this.saisie_adresse_ville.Length > 0;
            bool saisie_adresse_code_postal = this.saisie_adresse_code_postal != null && this.saisie_adresse_code_postal.Length > 0;
            Console.WriteLine("Test1");
            if (saisie_string_valide)
            {
                Utilisateur.MettreAjourTupleColonneUtilisateur(id_utilisateur, nom_colonne, "'" + saisie_string + "'", "");
                return RedirectToPage("Page_accueil_connecte");
            }
            Console.WriteLine("Test1");
            if (saisie_int_valide)
            {
                Utilisateur.MettreAjourTupleColonneUtilisateur(id_utilisateur, nom_colonne, saisie_int, "");
                return RedirectToPage("Page_accueil_connecte");
            }
            Console.WriteLine("Test1");
            if (saisie_adresse_num || saisie_adresse_rue || saisie_adresse_ville || saisie_adresse_code_postal)
            {

                bool addresse_valide = await Adresse_a_coordonees.GetCoords(saisie_adresse_num + " " + saisie_adresse_rue, saisie_adresse_ville, saisie_adresse_code_postal, "France");
                //if (addresse_valide) { }
                Utilisateur.MettreAjourTupleColonneUtilisateur(id_utilisateur, nom_colonne, "'" + saisie_adresse_num + " " + saisie_adresse_rue + ", " + saisie_adresse_ville + ", " + saisie_adresse_code_postal + "'", "");
                return RedirectToPage("Page_accueil_connecte");
            }
            Console.WriteLine("Test1");
            return Page();
        }
    }
}
