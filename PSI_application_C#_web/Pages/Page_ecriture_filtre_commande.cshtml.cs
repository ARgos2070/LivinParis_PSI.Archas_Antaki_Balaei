using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_ecriture_filtre_commandeModel : PageModel
    {
        private readonly ILogger<Page_ecriture_filtre_commandeModel> _logger;

        [BindProperty]
        public string saisie_filtre { get; set; }

        [BindProperty]
        public List<string> Liste_tuple { get; set; }

        public Page_ecriture_filtre_commandeModel(ILogger<Page_ecriture_filtre_commandeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
            TempData["Affiche_nationalite"] = TempData["Affiche_nationalite"];
            TempData["Affiche_regime_alimentaire"] = TempData["Affiche_regime_alimentaire"];

            string filtre = TempData["Filtre"].ToString();
            Console.WriteLine("C'est l'histoire d'un projet qui chute de 50 étages, jusque là tout va bien");
            Liste_tuple = Plat.RechercherTousLesTuplesDuneColonne(filtre);
            Console.WriteLine("liste de tuple count : " + Liste_tuple.Count);
            TempData["Liste_tuple_filtre"] = TempData["Liste_tuple_filtre"] as List<string>;
            if (Liste_tuple == null)
            {
                Console.WriteLine("liste tuple est null");
            }
            TempData["Filtre"] = filtre;
        }

        public static bool SaisieEstTuple(List<string> liste_tuple,string saisie)
        {
            bool est_tuple = false;
            for (int i = 0; i < liste_tuple.Count; i++)
            {
                if (liste_tuple[i].ToLower().Trim() == saisie.ToLower().Trim())
                {
                    est_tuple = true;
                }
            }
            return est_tuple;
        }

        public IActionResult OnPost()
        {
            string filtre = TempData["Filtre"].ToString();
            if (saisie_filtre == null || saisie_filtre.Length == 0)
            {
                ViewData["Erreur_saisie_filtre_vide"] = "Il faut saisir quelque chose pour le filtre.";
                TempData["Filtre"] = filtre;
                return Page();
            }
            
            Console.WriteLine("C'est l'histoire d'un tyrrty" + filtre);
            Liste_tuple = Plat.RechercherTousLesTuplesDuneColonne(filtre);

            if (SaisieEstTuple(Liste_tuple, saisie_filtre) == false)
            {
                ViewData["Erreur_saisie_filtre_inconnue"] = "Il faut saisir un des choix proposé ci-dessous pour le filtre.";
                TempData["Filtre"] = filtre;
                return Page();
            }
            TempData["Filtre"] = filtre;
            TempData["Liste_tuple_filtre"] = Liste_tuple;
            TempData["Valeur_filtre"] = saisie_filtre.ToLower().Trim();
            return RedirectToPage("Page_affichage_plat");
        }
    }
}
