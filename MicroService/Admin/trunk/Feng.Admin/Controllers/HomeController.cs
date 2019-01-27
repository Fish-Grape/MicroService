
using Microsoft.AspNetCore.Mvc;

namespace Feng.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger/");
        }
    }
}