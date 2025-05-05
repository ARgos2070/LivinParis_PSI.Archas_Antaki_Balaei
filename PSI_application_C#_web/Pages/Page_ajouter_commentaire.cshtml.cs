using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_ajouter_commentaireModel : PageModel
    {
        private readonly ILogger<Page_ajouter_commentaireModel> _logger;

        [BindProperty]
        public string saisie_note { get; set; }

        [BindProperty]
        public string saisie_commentaire { get; set; }

        public Page_ajouter_commentaireModel(ILogger<Page_ajouter_commentaireModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSubmit()
        {
            int ID_plat = 502;
            if (TempData.ContainsKey("IdPlat"))
            {
                int.TryParse(TempData["IDPlat"] as string, out ID_plat);
                Console.WriteLine("Id plat transmis");
            }
            else
            {
                Console.WriteLine("Id plat non transmis");
            }
            TempData["IDPlat"] = ID_plat;
            return RedirectToPage("./Page_des_commentaires");
        }
    }
}
