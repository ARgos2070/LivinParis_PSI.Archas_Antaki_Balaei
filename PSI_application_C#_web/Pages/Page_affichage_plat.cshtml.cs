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

        public static string ObtenirDateActuelle()
        {
            DateTime dateActuelle = DateTime.Now;
            return dateActuelle.ToString("yyyy-MM-dd");
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
                Plats = Plat.RechercherTousLesTuplesPlat();
                Console.WriteLine($"Nombre de plats récupérés : {Plats.Count}");
            }
            if (afficher_par_nationalite)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre);
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
            }
            if (afficher_par_regime_alimentaire)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre);
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
            }
            if (afficher_par_prix_croissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("ASC");
            }
            if (afficher_par_prix_decroissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("DESC");
            }
            TempData["Affiche_tout"] = afficher_tout;
            TempData["Affiche_nationalite"] = afficher_par_nationalite;
            TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
            TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
            TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
        }

        public IActionResult OnPost()
        {
            bool afficher_tout = (bool)TempData["Affiche_tout"];
            bool afficher_par_nationalite = (bool)TempData["Affiche_nationalite"];
            bool afficher_par_regime_alimentaire = (bool)TempData["Affiche_regime_alimentaire"];
            bool afficher_par_prix_croissant = (bool)TempData["Affiche_prix_croissant"];
            bool afficher_par_prix_decroissant = (bool)TempData["Affiche_prix_decroissant"];
            if (afficher_tout)
            {
                Plats = Plat.RechercherTousLesTuplesPlat();
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
            if (saisie_id_plat_voulu == null || saisie_id_plat_voulu.Length == 0)
            {
                ViewData["Erreur_saisie_id_vide"] = "Il faut saisir l'id du plat que vous voulez";
                TempData["Affiche_tout"] = TempData["Affiche_tout"];
                TempData["Affiche_nationalite"] = TempData["Affiche_nationalite"];
                TempData["Affiche_regime_alimentaire"] = TempData["Affiche_regime_alimentaire"];
                TempData["Affiche_prix_croissant"] = TempData["Affiche_prix_croissant"];
                TempData["Affiche_prix_decroissant"] = TempData["Affiche_prix_decroissant"];
                Console.WriteLine("Recharge réussie de la page");
                return Page();
            }
            if (afficher_par_nationalite)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
                if (Plat.IdPlatExiste(int.Parse(saisie_id_plat_voulu), filtre, valeur_filtre) == false)
                {
                    ViewData["Erreur_saisie_id_vide"] = "Il faut saisir une id proposé parmis celle ci-dessous";
                    TempData["Affiche_tout"] = TempData["Affiche_tout"];
                    TempData["Affiche_nationalite"] = TempData["Affiche_nationalite"];
                    TempData["Affiche_regime_alimentaire"] = TempData["Affiche_regime_alimentaire"];
                    TempData["Affiche_prix_croissant"] = TempData["Affiche_prix_croissant"];
                    TempData["Affiche_prix_decroissant"] = TempData["Affiche_prix_decroissant"];
                    Console.WriteLine("Recharge réussie de la page");
                    return Page();
                }
            }
            if (afficher_par_regime_alimentaire)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
                if (Plat.IdPlatExiste(int.Parse(saisie_id_plat_voulu), filtre, valeur_filtre) == false)
                {
                    ViewData["Erreur_saisie_id_vide"] = "Il faut saisir une id proposé parmis celle ci-dessous";
                    TempData["Affiche_tout"] = TempData["Affiche_tout"];
                    TempData["Affiche_nationalite"] = TempData["Affiche_nationalite"];
                    TempData["Affiche_regime_alimentaire"] = TempData["Affiche_regime_alimentaire"];
                    TempData["Affiche_prix_croissant"] = TempData["Affiche_prix_croissant"];
                    TempData["Affiche_prix_decroissant"] = TempData["Affiche_prix_decroissant"];
                    Console.WriteLine("Recharge réussie de la page");
                    return Page();
                }
            }
            if (Plat.IdPlatExiste(int.Parse(saisie_id_plat_voulu), "", "") == false)
            {
                ViewData["Erreur_saisie_id_vide"] = "Il faut saisir une id proposé parmis celle ci-dessous";
                TempData["Affiche_tout"] = TempData["Affiche_tout"];
                TempData["Affiche_nationalite"] = TempData["Affiche_nationalite"];
                TempData["Affiche_regime_alimentaire"] = TempData["Affiche_regime_alimentaire"];
                TempData["Affiche_prix_croissant"] = TempData["Affiche_prix_croissant"];
                TempData["Affiche_prix_decroissant"] = TempData["Affiche_prix_decroissant"];
                Console.WriteLine("Recharge réussie de la page");
                return Page();
            }
            bool continuer_commande = (bool)TempData["Continuer_commande"];
            if (continuer_commande)
            {
                return RedirectToPage("Page_1er_chargement");
            }
            int id_commande = Commande.Identifiant_commande_determine_depuis_bdd();
            double prix_commande = 0;
            string date_actuelle = ObtenirDateActuelle();
            int taille_commande = 1;
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            int id_client = Client.IdClientDunUtilisateur(id_utilisateur);
            Commande commande_cree = new Commande(id_commande, prix_commande, date_actuelle, taille_commande, id_client);
            //Initialiser le contient
            //Update le prix avec celui du plat
            return RedirectToPage("Page_1er_chargement");
        }
    }
}
