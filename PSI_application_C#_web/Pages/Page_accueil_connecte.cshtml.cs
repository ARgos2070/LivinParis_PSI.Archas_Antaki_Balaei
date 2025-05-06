using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_accueil_connecteModel : PageModel
    {
        [BindProperty]
        public string Id_utilisateur_session { get; set; }

        [BindProperty]
        public bool Est_cuisinier { get; set; }

        [BindProperty]
        public bool Est_client { get; set; }

        [BindProperty]
        public bool Est_livreur { get; set; }

        public void OnGet()
        {
            Id_utilisateur_session = (string)TempData["Id_utilisateur_session"];
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
            TempData["Id_client"] = id_client_connecte;
            //TempData["Id_livreur"] = id_livreur_connecte;
            TempData["Id_commande_memoire"] = 0;
        }

        public IActionResult OnPost()
        {
            
            return Page();
        }
    }
}
