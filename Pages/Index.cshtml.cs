using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace billyOrBob.Pages
{
    public class IndexModel : PageModel
    {

        // TODO Reference actual files
        public string[] ShakespeareFiles { get; private set; } = {
            "much_afoo_abar_nothing",
            "foolius_ceabar",
            "midwinter_nights_code"
        };
        public string[] BurnsFiles { get; private set; } = {
            "much_afoo_abar_nothing",
            "foolius_ceabar",
            "midwinter_nights_code"
        };
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {


        }
    }
}
