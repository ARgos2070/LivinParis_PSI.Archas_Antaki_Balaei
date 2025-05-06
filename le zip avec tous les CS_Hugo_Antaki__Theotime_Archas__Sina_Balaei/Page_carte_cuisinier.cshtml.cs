using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_carte_cuisinierModel : PageModel
    {
        [BindProperty]
        public string saisie_id_plat_voulu { get; set; }

        public List<Plat> Plats { get; set; }

        public void OnGet()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            int id_cuisinier = Cuisinier.IdCuisinierDunUtilisateur(id_utilisateur);
            Plats = Plat.RechercherTousLesTuplesPlat("WHERE ID_Cuisinier = " + id_cuisinier + "");

            
        }

        public IActionResult OnPost()
        {
            string id_utilisateur = TempData["Id_utilisateur_session"].ToString();
            TempData["Id_utilisateur_session"] = id_utilisateur;
            int id_cuisinier = Cuisinier.IdCuisinierDunUtilisateur(id_utilisateur);
            Plats = Plat.RechercherTousLesTuplesPlat("WHERE ID_Cuisinier = " + id_cuisinier + "");
            if (saisie_id_plat_voulu == null || saisie_id_plat_voulu.Length == 0)
            {
                ViewData["Erreur_saisie_id_vide"] = "Il faut saisir l'id du plat que vous voulez";
                return Page();
            }
            if (Plat.IdPlatExiste(int.Parse(saisie_id_plat_voulu), "", "", "AND ID_Cuisinier = " + id_cuisinier + "") == false)
            {
                ViewData["Erreur_saisie_id_vide"] = "Il faut saisir une id proposé parmis celle ci-dessous";
                return Page();
            }
            TempData["Id_plat_a_modifier"] = saisie_id_plat_voulu;
            return RedirectToPage("Page_select_colonne_update_plat");
        }
    }
}