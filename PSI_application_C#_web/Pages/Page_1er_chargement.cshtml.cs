using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PSI_application_C__web.Pages
{
    public class Page_1er_chargementModel : PageModel
    {
        private readonly ILogger<Page_1er_chargementModel> _logger;

        public Page_1er_chargementModel(ILogger<Page_1er_chargementModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
