using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{

    public class Page_historique_commandes_clientModel : PageModel
    {
        private readonly ILogger<Page_historique_commandes_clientModel> _logger;

        public List<Commande> Commandes { get; set; }

        public List<List<string>> Liste_info_commandes { get; set; }

        public Page_historique_commandes_clientModel(ILogger<Page_historique_commandes_clientModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            int id_client = Client.IdClientDunUtilisateur(id_utilisateur);
            //Commandes = Commande.RechercherTousLesTuplesCommande("WHERE ID_Client = " + id_client + "");
            Liste_info_commandes = Commande.RechercherHistoriqueCommandeClient(id_client);
        }
    }
}
