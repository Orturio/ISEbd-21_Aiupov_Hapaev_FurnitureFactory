using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryBusinessLogics.Enums;
using FurnitureFactoryEmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FurnitureFactoryEmployeeApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ReportController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            ViewBag.PurchaseId = new MultiSelectList(APIUser.GetRequest<List<PurchaseViewModel>>
                ($"api/main/GetPurchases?UserId={Program.User.Id}"), "Id", "PurchaseName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateReportPurchaseToWordFile([Bind("PurchaseId")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryClientApp\wwwroot\ReportFurniture\ReportFurnitureDoc.doc";
            APIUser.PostRequest("api/main/CreateReportPurchaseToWordFile", model);

            var fileName = "ReportPurhcaseDoc.doc";
            var filePath = _environment.WebRootPath + @"\ReportFurniture\" + fileName;
            return PhysicalFile(filePath, "application/doc", fileName);
        }

        [HttpPost]
        public IActionResult CreateReportPurchaseToExcelFile([Bind("PurchaseId")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryClientApp\wwwroot\ReportFurniture\ReportFurnitureExcel.xls";
            APIUser.PostRequest("api/main/CreateReportPurchaseToExcelFile", model);

            var fileName = "ReportPurhcaseExcel.xls";
            var filePath = _environment.WebRootPath + @"\ReportFurniture\" + fileName;
            return PhysicalFile(filePath, "application/xls", fileName);
        }
    }
}
