using SMTP.WebUI.Models;
using System.Web.Mvc;

namespace SMTP.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult NotCorrectLogin(LoginViewModel model)
        {
            ModelState.AddModelError("", "Имя или пароль некорректны"); 
            return View(model);
        }   
    }
}