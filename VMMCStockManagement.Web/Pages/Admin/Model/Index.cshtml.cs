using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VMMCStockManagement.Web.Pages.Admin.Model
{
	[Authorize]
	public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
