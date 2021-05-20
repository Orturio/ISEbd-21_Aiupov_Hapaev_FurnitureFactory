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
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.FurnitureId = new MultiSelectList(APIUser.GetRequest<List<FurnitureViewModel>>
                ($"api/main/GetFurnitures?UserId={Program.User.Id}"), "Id", "FurnitureName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateReportFurnitureToWordFile([Bind("FurnitureId")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryEmployeeApp\wwwroot\ReportFurniture\ReportFurnitureDoc.doc";
            APIUser.PostRequest("api/main/CreateReportFurnitureToWordFile", model);

            var fileName = "ReportFurnitureDoc.doc";
            var filePath = _environment.WebRootPath + @"\ReportFurniture\" + fileName;
            return PhysicalFile(filePath, "application/doc", fileName);
        }

        [HttpPost]
        public IActionResult CreateReportFurnitureToExcelFile([Bind("FurnitureId")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryEmployeeApp\wwwroot\ReportFurniture\ReportFurnitureExcel.xls";
            APIUser.PostRequest("api/main/CreateReportFurnitureToExcelFile", model);

            var fileName = "ReportFurnitureExcel.xls";
            var filePath = _environment.WebRootPath + @"\ReportFurniture\" + fileName;
            return PhysicalFile(filePath, "application/xls", fileName);
        }
    }
}
