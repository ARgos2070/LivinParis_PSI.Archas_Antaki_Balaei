using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_affichage_platModel : PageModel
    {
        private readonly ILogger<Page_affichage_platModel> _logger;

        [BindProperty]
        public int taille_max_id_plat { get; set; }

        [BindProperty]
        public string saisie_id_plat_voulu { get; set; }

        public List<Plat> Plats { get; set; }

        public Page_affichage_platModel(ILogger<Page_affichage_platModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
            taille_max_id_plat = (Plat.Identifiant_plat_determine_depuis_bdd().ToString()).Length;
            bool afficher_tout = (bool)TempData["Affiche_tout"];
            bool afficher_par_nationalite = (bool)TempData["Affiche_nationalite"];
            bool afficher_par_regime_alimentaire = (bool)TempData["Affiche_regime_alimentaire"];
            bool afficher_par_prix_croissant = (bool)TempData["Affiche_prix_croissant"];
            bool afficher_par_prix_decroissant = (bool)TempData["Affiche_prix_decroissant"];
            if (afficher_tout)
            {
                Plats = Plat.RechercherTousLesTuplesPlats();
                Console.WriteLine($"Nombre de plats récupérés : {Plats.Count}");
            }
            if (afficher_par_nationalite)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre);
            }
            if (afficher_par_regime_alimentaire)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre);
            }
            if (afficher_par_prix_croissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("ASC");
            }
            if (afficher_par_prix_decroissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("DESC");
            }
        }

        public IActionResult OnPost()
        {
            if (saisie_id_plat_voulu == null || saisie_id_plat_voulu.Length == 0)
            {
                ViewData["Erreur_saisie_id_vide"] = "Il faut saisir l'id du plat que vous voulez";
                return Page();
            }
            return RedirectToPage();
        }
    }
}
