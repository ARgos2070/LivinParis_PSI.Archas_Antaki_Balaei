using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace PSI_application_C__web.Pages
{
    public class Page_historique_livraisonsModel : PageModel
    {
        [BindProperty]
        public string Id_utilisateur_session { get; set; }

        [BindProperty]
        public List<Dictionary<string, string>> HistoriqueLivraisons { get; set; }

        public void OnGet()
        {
            Id_utilisateur_session = (string)TempData["Id_utilisateur_session"];
            TempData["Id_utilisateur_session"] = Id_utilisateur_session;
            int id_livreur = Livreur.IdLivreurDunUtilisateur(Id_utilisateur_session);
            HistoriqueLivraisons = Livreur.RechercherHistoriqueLivraisonsLivreur(id_livreur);
        }
    }
}
