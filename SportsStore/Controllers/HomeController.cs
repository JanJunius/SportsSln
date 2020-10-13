using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    // Die StartView des Projektes ist Home. Die Konvention von ASP.NET Core besagt, dass der zugehörige Controller HomeController heißen muss
    // Nennt man ihn anders, gibt es einen 404
    public class HomeController : Controller
    {
        private IStoreRepository repository;

        public int PageSize = 4;

        // Dependeny Injection: Wird ein HomeController von ASP.NET Core benötigt, ergibt sich aus dem Code in Startup,
        // welche konkrete Klasse für IStoreRepository instanziiert werden soll
        public HomeController(IStoreRepository repo) => repository = repo;

        // In Startup wurde MapDefaultControllerRoute hinzugefügt: Hierüber wird gesteuert, wie ASP.NET Core URLs zu Controller-Klassen matched
        // Der Default besagt dabei, dass die Index Methode des HomeControllers für alle URLs aufgerufen wird
        // Die Index-Methode ruft dann die von Cotroller geerbte View-Methode auf, welche dafür sorgt, dass eine View mit dem Namen Index im Home-Ordner
        // der Views aufgerufen wird.
        public IActionResult Index(int productPage = 1) => View(new ProductsListViewModel
        {
            Products = repository.Products
            .OrderBy(p => p.ProductID)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo { CurrentPage = productPage, ItemsPerPage = PageSize, TotalItems = repository.Products.Count() }
        });     
    }
}
