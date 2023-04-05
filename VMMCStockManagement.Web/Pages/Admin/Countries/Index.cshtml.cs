using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VMMCStockManagement.Web.Pages.Admin.Country
{
	[Authorize]
	public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
