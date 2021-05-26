using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.HelperModels;
using FurnitureFactoryClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FurnitureFactoryClientApp.Controllers
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
            ViewBag.PurchaseId = new MultiSelectList(APIUser.GetRequest<List<PurchaseViewModel>>
                ($"api/main/GetPurchases?UserId={Program.User.Id}"), "Id", "PurchaseName");
            return View();
        }

        public IActionResult ReportPdf()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateReportPurchaseToWordFile([Bind("PurchaseId")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryClientApp\wwwroot\ReportPurchase\ReportPurchaseDoc.doc";
            APIUser.PostRequest("api/main/CreateReportPurchaseToWordFile", model);

            var fileName = "ReportPurchaseDoc.doc";
            var filePath = _environment.WebRootPath + @"\ReportPurchase\" + fileName;
            return PhysicalFile(filePath, "application/doc", fileName);
        }

        [HttpPost]
        public IActionResult CreateReportPurchaseToExcelFile([Bind("PurchaseId")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryClientApp\wwwroot\ReportPurchase\ReportPurchaseExcel.xls";
            APIUser.PostRequest("api/main/CreateReportPurchaseToExcelFile", model);

            var fileName = "ReportPurchaseExcel.xls";
            var filePath = _environment.WebRootPath + @"\ReportPurchase\" + fileName;
            return PhysicalFile(filePath, "application/xls", fileName);
        }

        [HttpPost]
        public IActionResult CreateReportPurchaseToPdfFile([Bind("DateTo,DateFrom")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryClientApp\wwwroot\ReportPurchase\ReportPurchasePdf.pdf";
            model.UserId = Program.User.Id;
            APIUser.PostRequest("api/main/CreateReportPurchaseToPdfFile", model);

            var fileName = "ReportPurchasePdf.pdf";
            var filePath = _environment.WebRootPath + @"\ReportPurchase\" + fileName;
            ViewBag.CheckingReport = model.FileName;
            return PhysicalFile(filePath, "application/pdf", fileName);
        }

        [HttpPost]
        public IActionResult SendMail([Bind("DateTo,DateFrom")] ReportBindingModel model)
        {
            model.FileName = @"..\FurnitureFactoryClientApp\wwwroot\ReportPurchase\ReportPurchasePdf.pdf";
            model.UserId = Program.User.Id;
            APIUser.PostRequest("api/main/CreateReportPurchaseToPdfFile", model);
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = Program.User.Email,
                Subject = "Отчет",
                Text = "Отчет по покупкам",
                ReportFile = model.FileName
            });
            return RedirectToAction("ReportPdf");
        }
    }
}
