using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_select_colonne_update_utilisateurModel : PageModel
    {
        private readonly ILogger<Page_select_colonne_update_utilisateurModel> _logger;

        [BindProperty]
        public string Option_choisie { get; set; }

        [BindProperty]
        public string Id_utilisateur_session { get; set; }

        [BindProperty]
        public bool Est_entreprise { get; set; }

        [BindProperty]
        public bool Est_cuisinier { get; set; }

        [BindProperty]
        public bool Est_client { get; set; }

        [BindProperty]
        public bool Est_livreur { get; set; }

        public Page_select_colonne_update_utilisateurModel(ILogger<Page_select_colonne_update_utilisateurModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Id_utilisateur_session = (string)TempData["Id_utilisateur_session"];
            if (Utilisateur.UtilisateurEstEntreprise(Id_utilisateur_session))
            {
                Est_entreprise = true;
            }
            else { Est_entreprise = false; }
            if (Cuisinier.IdCuisinierDunUtilisateur(Id_utilisateur_session) != 0)
            {
                Est_cuisinier = true;
            }
            else { Est_cuisinier = false; }
            if (Client.IdClientDunUtilisateur(Id_utilisateur_session) != 0)
            {
                Est_client = true;
            }
            else { Est_client = false; }
            if (Livreur.IdLivreurDunUtilisateur(Id_utilisateur_session) != 0)
            {
                Est_livreur = true;
            }
            else { Est_livreur = false; }
            int id_cuisinier_connecte = Cuisinier.IdCuisinierDunUtilisateur(Id_utilisateur_session);
            int id_client_connecte = Client.IdClientDunUtilisateur(Id_utilisateur_session);
            int id_livreur_connecte = Livreur.IdLivreurDunUtilisateur(Id_utilisateur_session);
            TempData["Id_utilisateur_session"] = Id_utilisateur_session;
            TempData["Id_cuisinier"] = id_cuisinier_connecte;
            //TempData["Id_client"] = id_client_connecte;
            //TempData["Id_livreur"] = id_livreur_connecte;
        }

        public IActionResult OnPost()
        {
            Id_utilisateur_session = (string)TempData["Id_utilisateur_session"];
            if (Utilisateur.UtilisateurEstEntreprise(Id_utilisateur_session))
            {
                Est_entreprise = true;
            }
            else { Est_entreprise = false; }
            if (Cuisinier.IdCuisinierDunUtilisateur(Id_utilisateur_session) != 0)
            {
                Est_cuisinier = true;
            }
            else { Est_cuisinier = false; }
            if (Client.IdClientDunUtilisateur(Id_utilisateur_session) != 0)
            {
                Est_client = true;
            }
            else { Est_client = false; }
            if (Livreur.IdLivreurDunUtilisateur(Id_utilisateur_session) != 0)
            {
                Est_livreur = true;
            }
            else { Est_livreur = false; }
            int id_cuisinier_connecte = Cuisinier.IdCuisinierDunUtilisateur(Id_utilisateur_session);
            int id_client_connecte = Client.IdClientDunUtilisateur(Id_utilisateur_session);
            int id_livreur_connecte = Livreur.IdLivreurDunUtilisateur(Id_utilisateur_session);
            TempData["Id_utilisateur_session"] = Id_utilisateur_session;
            TempData["Id_cuisinier"] = id_cuisinier_connecte;
            //TempData["Id_client"] = id_client_connecte;
            //TempData["Id_livreur"] = id_livreur_connecte;

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
                    TempData["Type_colonne"] = "adresse";
                    TempData["Colonne_update"] = "Adresse_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Num_tel_utilisateur":
                    TempData["Type_colonne"] = "int";
                    TempData["Colonne_update"] = "Num_tel_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Adresse_mail_utilisateur":
                    TempData["Type_colonne"] = "mail";
                    TempData["Colonne_update"] = "Adresse_mail_utilisateur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Devenir_client":
                    Console.WriteLine("veut devenir client");
                    Client nouveauClient = new Client(Utilisateur.ChargerUtilisateurDepuisBDD(Id_utilisateur_session));
                    Client.AjoutClientBDD(nouveauClient);
                    return RedirectToPage("Page_accueil_connecte");
                case "Devenir_cuisinier":
                    Cuisinier nouveauCuisinier = new Cuisinier(Utilisateur.ChargerUtilisateurDepuisBDD(Id_utilisateur_session));
                    Cuisinier.AjoutCuisinierBDD(nouveauCuisinier);
                    TempData["Id_cuisinier"] = nouveauCuisinier.Id_cuisinier;
                    return RedirectToPage("Page_creation_plat");
                case "Devenir_livreur":
                    Livreur nouveauLivreur = new Livreur(Utilisateur.ChargerUtilisateurDepuisBDD(Id_utilisateur_session));
                    Livreur.AjoutLivreurBDD(nouveauLivreur);
                    return RedirectToPage("Page_accueil_connecte");
                case "Arreter_client":
                    Client.RadierClient(id_client_connecte);
                    return RedirectToPage("Page_accueil_connecte");
                case "Arreter_cuisinier":
                    Console.WriteLine("Radiation cuisinier en cours");
                    Cuisinier.RadierCuisinier(id_cuisinier_connecte);
                    return RedirectToPage("Page_accueil_connecte");
                case "Arreter_livreur":
                    Livreur.RadierLivreur(id_livreur_connecte);
                    return RedirectToPage("Page_accueil_connecte");
                case "Devenir_entreprise":
                    TempData["Type_colonne"] = "bool";
                    TempData["Colonne_update"] = "Continuer_etre_entreprise";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Arreter_entreprise":
                    TempData["Type_colonne"] = "bool";
                    TempData["Colonne_update"] = "Devenir_livreur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                case "Supprimer_profil":
                    TempData["Type_colonne"] = "bool";
                    TempData["Colonne_update"] = "Devenir_livreur";
                    return RedirectToPage("Page_modification_colonne_utilisateur");
                default:
                    ViewData["Erreur_affichage"] = "Veuillez sélectionner une option valide.";
                    return Page();
            }
        }
    }
}
