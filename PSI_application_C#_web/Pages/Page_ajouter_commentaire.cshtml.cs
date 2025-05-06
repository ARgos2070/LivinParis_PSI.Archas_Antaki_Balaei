using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Ocsp;

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
            int id_plat = Convert.ToInt32(TempData.Peek("IDPlat"));
            TempData["IDPlat"] = id_plat;
        }

        public IActionResult OnPostSubmit()
        {
            return RedirectToPage("./Page_des_commentaires");
        }
    }
}
