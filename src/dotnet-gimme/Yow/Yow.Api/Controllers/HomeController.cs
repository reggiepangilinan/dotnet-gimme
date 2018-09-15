using Microsoft.AspNetCore.Mvc;

namespace Yow.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        public RedirectResult Index()
        {
            return this.Redirect("/swagger");
        }
    }
}