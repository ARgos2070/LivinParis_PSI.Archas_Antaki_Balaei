using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_historique_livraisonsModel : PageModel
    {
        [BindProperty]
        public string Id_utilisateur_session { get; set; }


        [BindProperty]
        public double GainTotalLivreur { get; set; }

        [BindProperty]
        public List<List<string>> Historique_livraisons { get; set; }

        public void OnGet()
        {
            Id_utilisateur_session = (string)TempData["Id_utilisateur_session"];
            GainTotalLivreur = 0;
            TempData["Id_utilisateur_session"] = Id_utilisateur_session;
            int id_livreur = Livreur.IdLivreurDunUtilisateur(Id_utilisateur_session);
            //Historique_livraisons = Livreur.RechercherHistoriqueLivraisonsLivreur(id_livreur);

        }
    }
}
