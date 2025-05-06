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

        [BindProperty]
        public string saisie_nbre_plat_voulu { get; set; }

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
                Plats = Plat.RechercherTousLesTuplesPlat("WHERE nbre_portion_dispo_plat >=1");
            }
            if (afficher_par_nationalite)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre, "AND nbre_portion_dispo_plat >=1");
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
            }
            if (afficher_par_regime_alimentaire)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre, "AND nbre_portion_dispo_plat >=1");
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
            }
            if (afficher_par_prix_croissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("ASC", "WHERE nbre_portion_dispo_plat >=1");
            }
            if (afficher_par_prix_decroissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("DESC", "WHERE nbre_portion_dispo_plat >=1");
            }
            TempData["Affiche_tout"] = afficher_tout;
            TempData["Affiche_nationalite"] = afficher_par_nationalite;
            TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
            TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
            TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
        }

        public IActionResult OnPostComentaire()
        {
            TempData["IDPlat"] = saisie_id_plat_voulu;
            return RedirectToPage("./Page_des_commentaires");
        }

        public IActionResult OnPostSubmitChoice()
        {
            bool afficher_tout = (bool)TempData["Affiche_tout"];
            bool afficher_par_nationalite = (bool)TempData["Affiche_nationalite"];
            bool afficher_par_regime_alimentaire = (bool)TempData["Affiche_regime_alimentaire"];
            bool afficher_par_prix_croissant = (bool)TempData["Affiche_prix_croissant"];
            bool afficher_par_prix_decroissant = (bool)TempData["Affiche_prix_decroissant"];
            if (afficher_tout)
            {
                Plats = Plat.RechercherTousLesTuplesPlat("WHERE nbre_portion_dispo_plat >=1");
            }
            if (afficher_par_nationalite)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre, "AND nbre_portion_dispo_plat >=1");
            }
            if (afficher_par_regime_alimentaire)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                Plats = Plat.RechercherTousLesTuplesPlatPourUnFiltreFixe(filtre, valeur_filtre, "AND nbre_portion_dispo_plat >=1");
            }
            if (afficher_par_prix_croissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("ASC", "WHERE nbre_portion_dispo_plat >=1");
            }
            if (afficher_par_prix_decroissant)
            {
                Plats = Plat.RechercherTousLesTuplesPlatOrdonnePrix("DESC", "WHERE nbre_portion_dispo_plat >=1");
            }
            if (saisie_id_plat_voulu == null || saisie_id_plat_voulu.Length == 0)
            {
                ViewData["Erreur_saisie_id_vide"] = "Il faut saisir l'id du plat que vous voulez";
                TempData["Affiche_tout"] = afficher_tout;
                TempData["Affiche_nationalite"] = afficher_par_nationalite;
                TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
                TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
                TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
                return Page();
            }
            if (afficher_par_nationalite)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
                if (Plat.IdPlatExiste(int.Parse(saisie_id_plat_voulu), filtre, valeur_filtre, "AND nbre_portion_dispo_plat >=1") == false)
                {
                    ViewData["Erreur_saisie_id_vide"] = "Il faut saisir une id proposé parmis celle ci-dessous";
                    TempData["Affiche_tout"] = afficher_tout;
                    TempData["Affiche_nationalite"] = afficher_par_nationalite;
                    TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
                    TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
                    TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
                    return Page();
                }
            }
            if (afficher_par_regime_alimentaire)
            {
                string filtre = TempData["Filtre"].ToString();
                string valeur_filtre = TempData["Valeur_filtre"].ToString();
                TempData["Filtre"] = filtre;
                TempData["Valeur_filtre"] = valeur_filtre;
                if (Plat.IdPlatExiste(int.Parse(saisie_id_plat_voulu), filtre, valeur_filtre, "AND nbre_portion_dispo_plat >=1") == false)
                {
                    ViewData["Erreur_saisie_id_vide"] = "Il faut saisir une id proposé parmis celle ci-dessous";
                    TempData["Affiche_tout"] = afficher_tout;
                    TempData["Affiche_nationalite"] = afficher_par_nationalite;
                    TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
                    TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
                    TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
                    return Page();
                }
            }
            if (Plat.IdPlatExiste(int.Parse(saisie_id_plat_voulu), "", "", "AND nbre_portion_dispo_plat >=1") == false)
            {
                ViewData["Erreur_saisie_id_vide"] = "Il faut saisir une id proposé parmis celle ci-dessous";
                TempData["Affiche_tout"] = afficher_tout;
                TempData["Affiche_nationalite"] = afficher_par_nationalite;
                TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
                TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
                TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
                return Page();
            }

            if (saisie_nbre_plat_voulu == null || saisie_nbre_plat_voulu.Length == 0)
            {
                ViewData["Erreur_saisie_nbre_plat_voulu"] = "Il faut saisir un nombre de portion voulu";
                TempData["Affiche_tout"] = afficher_tout;
                TempData["Affiche_nationalite"] = afficher_par_nationalite;
                TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
                TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
                TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
                return Page();
            }
            
            if (int.Parse(saisie_nbre_plat_voulu) > Plat.ConnaitreNbrePortionDispo(int.Parse(saisie_id_plat_voulu)))
            {
                ViewData["Erreur_saisie_nbre_plat_voulu"] = "Il faut saisir le nombre de portions voulues dans la limite des stocks disponibles";
                TempData["Affiche_tout"] = afficher_tout;
                TempData["Affiche_nationalite"] = afficher_par_nationalite;
                TempData["Affiche_regime_alimentaire"] = afficher_par_regime_alimentaire;
                TempData["Affiche_prix_croissant"] = afficher_par_prix_croissant;
                TempData["Affiche_prix_decroissant"] = afficher_par_prix_decroissant;
                return Page();
            }
            int id_plat = int.Parse(saisie_id_plat_voulu);
            int nbre_portion_voulue = int.Parse(saisie_nbre_plat_voulu);
            string lecture_id_commande_anterieure = TempData["Id_commande_memoire"].ToString();
            int id_commande_anterieure = int.Parse(lecture_id_commande_anterieure);
            if (id_commande_anterieure != 0)
            {
                Contient contient = new Contient(id_plat, id_commande_anterieure, nbre_portion_voulue);
                Contient.AjoutContientBDD(contient);
                int nouveau_nbre_portion_disponible = Plat.ConnaitreNbrePortionDispo(id_plat) - nbre_portion_voulue;
                Plat.MettreAjourTupleColonne(id_plat, "nbre_portion_dispo_plat", nouveau_nbre_portion_disponible.ToString(), "AND nbre_portion_dispo_plat >=1");
                Commande.MettreAjourAttributTailleCommande(id_commande_anterieure, nbre_portion_voulue);
                double prix_maj_commande = nbre_portion_voulue * Plat.ConnaitrePrixPortion(id_plat);
                Commande.MettreAjourAttributPrixCommande(id_commande_anterieure, prix_maj_commande);
                TempData["Id_commande_memoire"] = id_commande_anterieure;
                return RedirectToPage("Page_finaliser_commande");
            }
            
            int id_commande = Commande.Identifiant_commande_determine_depuis_bdd();
            double prix_commande = nbre_portion_voulue * Plat.ConnaitrePrixPortion(id_plat);
            string date_actuelle = ObtenirDateActuelle();
            int taille_commande = nbre_portion_voulue;
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            int id_client = Client.IdClientDunUtilisateur(id_utilisateur);
            
            Commande commande_cree = new Commande(id_commande, prix_commande, date_actuelle, taille_commande, id_client);
            Commande.AjoutCommandeBDD(commande_cree);
            Contient contient_cree = new Contient(id_plat, id_commande, nbre_portion_voulue);
            Contient.AjoutContientBDD(contient_cree);
            
            int nouveau_nbre_portion_dispo = Plat.ConnaitreNbrePortionDispo(id_plat) - nbre_portion_voulue;
            
            Plat.MettreAjourTupleColonne(id_plat, "nbre_portion_dispo_plat", nouveau_nbre_portion_dispo.ToString(), "AND nbre_portion_dispo_plat >=1");
            TempData["Id_commande_memoire"] = id_commande;
            return RedirectToPage("Page_finaliser_commande");
        }
    }
}
