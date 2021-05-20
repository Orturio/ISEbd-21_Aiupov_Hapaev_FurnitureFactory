using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;

namespace FurnitureFactoryEmployeeApp.Controllers
{
    public class DiagramController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Furnitures = new SelectList(APIUser.GetRequest<List<FurnitureViewModel>> 
                ($"api/main/GetFurnitures?UserId={Program.User.Id}"), "Id", "FurnitureName");
            return View();
        }

        [HttpPost]
        public IActionResult Index(int furnitureId)
        {
            ViewBag.Furnitures = new SelectList(APIUser.GetRequest<List<FurnitureViewModel>>
                ($"api/main/GetFurnitures?UserId={Program.User.Id}"), "Id", "FurnitureName");
            var model = new DiagramViewModel[]{
                APIUser.GetRequest<DiagramViewModel>($"api/main/GetDiagramByPurchaseCount?FurnitureId={furnitureId}"),
                APIUser.GetRequest<DiagramViewModel>($"api/main/GetDiagramByPurchasePrice?FurnitureId={furnitureId}")
            };
            return View(model);
        }
    }
}
