using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;

namespace FurnitureFactoryClientApp.Controllers
{
    public class DiagramController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Purchases = new SelectList(APIUser.GetRequest<List<PurchaseViewModel>> 
                ($"api/main/GetPurchases?UserId={Program.User.Id}"), "Id", "PurchaseName");
            return View();
        }

        [HttpPost]
        public IActionResult Index(int purchaseId)
        {
            ViewBag.Purchases = new SelectList(APIUser.GetRequest<List<PurchaseViewModel>>
                ($"api/main/GetPurchases?UserId={Program.User.Id}"), "Id", "PurchaseName");
            var model = new DiagramViewModel[]{
                APIUser.GetRequest<DiagramViewModel>($"api/main/GetDiagramByFurnitureCount?PurchaseId={purchaseId}"),
                APIUser.GetRequest<DiagramViewModel>($"api/main/GetDiagramByFurniturePrice?PurchaseId={purchaseId}")
            };
            return View(model);
        }
    }
}
