using Microsoft.AspNetCore.Mvc;

namespace HMS_Team1_PRN211_SE1608.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
