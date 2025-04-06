using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace PSI_application_C__web.Pages
{
    public class Page_creation_platModel : PageModel
    {
        private readonly ILogger<Page_creation_platModel> _logger;

        [BindProperty]
        public string saisie_nom_plat { get; set; }

        [BindProperty]
        public string saisie_type_plat { get; set; }

        [BindProperty]
        public string saisie_pr_cmb_de_personnes_plat { get; set; }

        [BindProperty]
        public string saisie_prix_par_portion_plat { get; set; }

        [BindProperty]
        public string saisie_nbre_portion_dispo_plat { get; set; }

        [BindProperty]
        public string saisie_date_fabrication_plat_jour { get; set; }

        [BindProperty]
        public string saisie_date_fabrication_plat_mois { get; set; }

        [BindProperty]
        public string saisie_date_fabrication_plat_annee { get; set; }

        [BindProperty]
        public string saisie_date_peremption_plat_jour { get; set; }

        [BindProperty]
        public string saisie_date_peremption_plat_mois { get; set; }

        [BindProperty]
        public string saisie_date_peremption_plat_annee { get; set; }

        [BindProperty]
        public string saisie_nationalite_plat { get; set; }

        [BindProperty]
        public string saisie_regime_alimentaire_plat { get; set; }

        [BindProperty]
        public string saisie_ingredients_principaux_plat { get; set; }

        [BindProperty]
        public bool saisie_choix_plat_du_jour { get; set; }

        public Page_creation_platModel(ILogger<Page_creation_platModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TempData["Id_utilisateur_session"] = TempData["Id_utilisateur_session"];
        }

        public static bool DateCorrecte(string jour, string mois, string annee, bool peremption)
        {
            bool est_correct = false;
            if ((jour != null && jour.Length > 0) && (mois != null && mois.Length > 0) && (annee != null && annee.Length > 0))
            {
                if (int.TryParse(jour, out int jourInt) == true && int.TryParse(mois, out int moisInt) == true && int.TryParse(annee, out int anneeInt) == true)
                {
                    DateTime dateEntree;
                    try
                    {
                        dateEntree = new DateTime(anneeInt, moisInt, jourInt);
                        DateTime dateActuelle = DateTime.Now;
                        if (peremption == false)
                        {
                            est_correct = dateEntree < dateActuelle;
                        }
                        else
                        {
                            est_correct = true;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        est_correct = false;
                    }
                }
            }
            return est_correct;
        }

        public static bool DateFabInferieureDatePeremp(string jour_fab, string mois_fab, string annee_fab, string jour_peremp, string mois_peremp, string annee_peremp)
        {
            bool est_correct = false;
            if (DateCorrecte(jour_fab, mois_fab, annee_fab, false) && DateCorrecte(jour_peremp, mois_peremp, annee_peremp, true))
            {
                DateTime dateFab = new DateTime(int.Parse(annee_fab), int.Parse(mois_fab), int.Parse(jour_fab));
                DateTime datePeremp = new DateTime(int.Parse(annee_peremp), int.Parse(mois_peremp), int.Parse(jour_peremp));
                Console.WriteLine(dateFab);
                Console.WriteLine(datePeremp);
                est_correct = dateFab < datePeremp;
            }
            return est_correct;
        }

        public IActionResult OnPost()
        {
            bool nom_plat_valide = saisie_nom_plat != null && saisie_nom_plat.Length > 0;
            bool type_plat_valide = saisie_type_plat != null && saisie_type_plat.Length > 0;
            bool pr_cmb_de_personnes_plat_valide = saisie_pr_cmb_de_personnes_plat != null && saisie_pr_cmb_de_personnes_plat.Length > 0;
            bool prix_par_portion_plat_valide = saisie_prix_par_portion_plat != null && saisie_prix_par_portion_plat.Length > 0;
            bool nbre_portion_dispo_plat_valide = saisie_nbre_portion_dispo_plat != null && saisie_nbre_portion_dispo_plat.Length > 0;
            bool date_fabrication_valide = DateCorrecte(saisie_date_fabrication_plat_jour, saisie_date_fabrication_plat_mois, saisie_date_fabrication_plat_annee, false);
            bool date_peremption_valide = DateCorrecte(saisie_date_peremption_plat_jour, saisie_date_peremption_plat_mois, saisie_date_peremption_plat_annee, true);
            bool ordre_date_fab_peremp_valide = DateFabInferieureDatePeremp(saisie_date_fabrication_plat_jour, saisie_date_fabrication_plat_mois, saisie_date_fabrication_plat_annee,
                saisie_date_peremption_plat_jour, saisie_date_peremption_plat_mois, saisie_date_peremption_plat_annee);
            bool nationalite_plat_valide = saisie_nationalite_plat != null && saisie_nationalite_plat.Length > 0;
            bool regime_alimentaire_plat_valide = saisie_regime_alimentaire_plat != null && saisie_regime_alimentaire_plat.Length > 0;
            bool ingredients_principaux_plat_valide = saisie_ingredients_principaux_plat != null && saisie_ingredients_principaux_plat.Length > 0;

            if (!nom_plat_valide)
            {
                ViewData["Erreur_nom_plat"] = "Un nom de plat est requis.";
            }
            if (!type_plat_valide)
            {
                ViewData["Erreur_type_plat"] = "Un type de plat est requis.";
            }
            if (!pr_cmb_de_personnes_plat_valide)
            {
                ViewData["Erreur_pr_cmb_de_personnes_plat"] = "Le nombre de personnes est requis.";
            }
            if (!prix_par_portion_plat_valide)
            {
                ViewData["Erreur_prix_par_perso_plat"] = "Un prix par personne est requis.";
            }
            if (nbre_portion_dispo_plat_valide == false)
            {
                ViewData["Erreur_nbre_portion_dispo_plat"] = "Le nombre de portions disponible de ce plat est requis.";
            }
            if (date_fabrication_valide == false)
            {
                ViewData["Erreur_date_fabrication_plat"] = "Il faut une adresse correcte.";
            }
            if (date_peremption_valide == false)
            {
                ViewData["Erreur_date_fabrication_plat_mois"] = "Il faut une adresse de peremption correct.";
            }
            if (ordre_date_fab_peremp_valide == false)
            {
                ViewData["Erreur_ordre_date"] = "Vos dates ne collent pas.";
            }
            if (!nationalite_plat_valide)
            {
                ViewData["Erreur_nationalite_plat"] = "La nationalité du plat est requise.";
            }
            if (!regime_alimentaire_plat_valide)
            {
                ViewData["Erreur_regime_alimentaire_plat"] = "Le régime alimentaire est requis.";
            }
            if (!ingredients_principaux_plat_valide)
            {
                ViewData["Erreur_ingredients_principaux_plat"] = "Les ingrédients principaux sont requis.";
            }

            if (!nom_plat_valide || !type_plat_valide || !pr_cmb_de_personnes_plat_valide || !prix_par_portion_plat_valide ||
                !nationalite_plat_valide || !regime_alimentaire_plat_valide || !ingredients_principaux_plat_valide || nbre_portion_dispo_plat_valide == false ||
                date_fabrication_valide == false || date_peremption_valide == false ||ordre_date_fab_peremp_valide == false)
            {
                return Page();
            }
            int id_plat = Plat.Identifiant_plat_determine_depuis_bdd();
            string nom_plat = saisie_nom_plat;
            string type_plat = saisie_type_plat;
            int pr_cmb_de_personnes_plat = int.Parse(saisie_pr_cmb_de_personnes_plat);
            if (pr_cmb_de_personnes_plat < 1)
            {
                ViewData["Erreur_pr_cmb_de_personnes_plat"] = "Le nombre de personnes doit être au moins de 1.";
                return Page();
            }
            string transition = saisie_prix_par_portion_plat.Replace('.', ',');
            double prix_par_portion_plat = double.Parse(transition);
            int nbre_portion_dispo_plat = int.Parse(saisie_nbre_portion_dispo_plat);
            if (nbre_portion_dispo_plat < 1)
            {
                ViewData["Erreur_nbre_portion_dispo_plat"] = "Le nombre de portions doit être au moins de 1.";
                return Page();
            }
            string date_fabrication_plat = saisie_date_fabrication_plat_annee + "-" + saisie_date_fabrication_plat_mois + "-" + saisie_date_fabrication_plat_jour;
            string date_peremption_plat = saisie_date_peremption_plat_annee + "-" + saisie_date_peremption_plat_mois + "-" + saisie_date_peremption_plat_jour;
            string nationalite_plat = saisie_nationalite_plat;
            string regime_alimentaire_plat = saisie_regime_alimentaire_plat;
            string ingredients_principaux_plat = saisie_ingredients_principaux_plat;
            bool choix_plat_du_jour = saisie_choix_plat_du_jour;
            int id_cuisinier_plat = (int)TempData["Id_cuisinier"];
            Plat plat_cree = new Plat(id_plat, nom_plat, type_plat, pr_cmb_de_personnes_plat, prix_par_portion_plat, nbre_portion_dispo_plat, date_fabrication_plat, date_peremption_plat, nationalite_plat, regime_alimentaire_plat, ingredients_principaux_plat, id_cuisinier_plat);
            Plat.AjoutPlatBDD(plat_cree);
            Cuisinier.CuisinierAjouteUnPlatAsonMenu(id_cuisinier_plat);
            if (Cuisinier.CuisinierSansPlatDuJour(id_cuisinier_plat) || choix_plat_du_jour)
            {
                Cuisinier.DeclarerUnNouveauPlatDuJour(id_cuisinier_plat, nom_plat);
            }
            //string perpetuation = (string)TempData["Id_utilisateur"];
            //Console.WriteLine("id_utilisateur_transitoire : " + perpetuation);
            //TempData["Id_utilisateur"] = perpetuation;
            return RedirectToPage("Page_accueil_connecte");
        }

    }
}
