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
            int ID_plat_temp = 0;
            if (TempData.ContainsKey("IdPlat"))
            {
                int.TryParse(TempData["IDPlat"] as string, out ID_plat_temp);
                Console.WriteLine(ID_plat_temp);
                TempData["IDPlat"] = ID_plat_temp;
                idPlat = ID_plat_temp;
                Console.WriteLine(idPlat);
                Console.WriteLine("Id plat transmis");
            }
            else
            {
                Console.WriteLine("Id plat non transmis");
            }
            TempData["IDPlat"] = ID_plat_temp;
        }

        public IActionResult OnPostSubmit()
        {
            Console.WriteLine("Retour : " + TempData["IDPlat"]);
            Console.WriteLine("Notre veleur : " + idPlat);
            return RedirectToPage("./Page_des_commentaires");
        }
    }
}
