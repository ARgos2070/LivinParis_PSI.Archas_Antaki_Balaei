using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Ocsp;
using System.Net.Sockets;

namespace PSI_application_C__web.Pages
{
    public class Page_ajouter_commentaireModel : PageModel
    {
        private readonly ILogger<Page_ajouter_commentaireModel> _logger;

        [BindProperty]
        public string saisie_note { get; set; }

        [BindProperty]
        public string saisie_commentaire { get; set; }

        [BindProperty]
        public int idPlat { get; set; }

        public Page_ajouter_commentaireModel(ILogger<Page_ajouter_commentaireModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSubmit()
        {
            int id_plat = Convert.ToInt32(TempData.Peek("IDPlat"));
            TempData["IDPlat"] = id_plat;
            int id_client = Convert.ToInt32(TempData.Peek("Id_client"));

            Commentaire_LP commentaire = new Commentaire_LP(
                int.Parse(saisie_note),saisie_commentaire,id_client,id_plat);

            Commentaire_LP.AjoutCommentaireBDD(commentaire);

            return RedirectToPage("./Page_des_commentaires");
        }
    }
}