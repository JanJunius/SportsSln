using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        // In Startup wurde MapDefaultControllerRoute hinzugefügt: Hierüber wird gesteuert, wie ASP.NET Core URLs zu Controller-Klassen matched
        // Der Default besagt dabei, dass die Index Methode des HomeControllers für alle URLs aufgerufen wird
        // Die Index-Methode ruft dann die von Cotroller geerbte View-Methode auf, welche dafür sorgt, dass eine View mit dem Namen Index im Home-Ordner
        // der Views aufgerufen wird.
        public IActionResult Index() => View();
    }
}
