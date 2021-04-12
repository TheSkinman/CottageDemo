using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CottageApp.Pages
{
    public class CottagesModel : PageModel
    {
        private readonly ILogger<CottagesModel> _logger;

        public CottagesModel(ILogger<CottagesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
