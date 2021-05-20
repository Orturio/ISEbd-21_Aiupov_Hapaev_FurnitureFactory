using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class MainController : ControllerBase
    {
        private readonly PurchaseLogic _purchase;
        private readonly FurnitureLogic _furniture;
        private readonly PurchaseLogic _main;
        private readonly PaymentLogic _payment;
        private readonly CostLogic _cost;
        private readonly ReportLogic _report;
        public MainController(PurchaseLogic purchase, FurnitureLogic furniture, PurchaseLogic main, PaymentLogic payment, CostLogic cost, ReportLogic report)
        {
            _purchase = purchase;
            _furniture = furniture;
            _main = main;
            _payment = payment;
            _cost = cost;
            _report = report;
        }

        [HttpGet]
        public List<FurnitureViewModel> GetFurnitureList() => _furniture.Read(null)?.ToList();

        [HttpGet]
        public FurnitureViewModel GetFurniture(int furnitureId) => _furniture.Read(new FurnitureBindingModel { Id = furnitureId })?[0];

        [HttpGet]
        public CostViewModel GetCost(int costId) => _cost.Read(new CostBindingModel { Id = costId })?[0];

        [HttpGet]
        public List<FurnitureViewModel> GetFurnitures(int userId) => _furniture.Read(new FurnitureBindingModel { UserId = userId });

        [HttpGet]
        public List<CostViewModel> GetCosts(int userId) => _cost.Read(new CostBindingModel { UserId = userId });

        [HttpGet]
        public List<PurchaseViewModel> GetPurchaseList() => _purchase.Read(null)?.ToList();

        [HttpGet]
        public PurchaseViewModel GetPurchaseNL(int Id) => _purchase.Read(new PurchaseBindingModel { Id = Id })?[0];

        [HttpGet]
        public List<PurchaseViewModel> GetPurchases(int userId) => _purchase.Read(new PurchaseBindingModel { UserId = userId });

        [HttpGet]
        public List<PurchaseViewModel> GetPurchase(int Id) => _purchase.Read(new PurchaseBindingModel { Id = Id });

        [HttpGet]
        public List<PaymentViewModel> GetPayment(int PurchaseId) => _payment.Read(new PaymentBindingModel { PurchaseId = PurchaseId });

        [HttpPost]
        public void CreatePurchase(PurchaseBindingModel model) =>  _main.CreatePurchase(model);

        [HttpPost]
        public void UpdatePurchase(PurchaseBindingModel model) => _main.UpdatePurchase(model);

        [HttpPost]
        public void DeletePurchase(PurchaseBindingModel model) => _main.Delete(model);

        [HttpPost]
        public void CreateFurniture(FurnitureBindingModel model) => _furniture.CreateFurniture(model);

        [HttpPost]
        public void UpdateFurniture(FurnitureBindingModel model) => _furniture.UpdateFurniture(model);

        [HttpPost]
        public void DeleteFurniture(FurnitureBindingModel model) => _furniture.Delete(model);

        [HttpPost]
        public void CreateOrUpdateCost(CostBindingModel model) => _cost.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteCost(CostBindingModel model) => _cost.Delete(model);

        public void CreatePayment(PaymentBindingModel model) => _payment.CreateOrUpdate(model);

        [HttpPost]
        public void CreateReportPurchaseToWordFile(ReportBindingModel model) => _report.SavePurchaseToWordFile(model);

        [HttpPost]
        public void CreateReportPurchaseToExcelFile(ReportBindingModel model) => _report.SavePurchaseInfoToExcelFile(model);

        [HttpPost]
        public void CreateReportFurnitureToWordFile(ReportBindingModel model) => _report.SaveFurnitureToWordFile(model);

        [HttpPost]
        public void CreateReportFurnitureToExcelFile(ReportBindingModel model) => _report.SaveFurnitureInfoToExcelFile(model);

        [HttpGet]
        public ReportBindingModel GetPurchasesForReport(int UserId)
        {
            return new ReportBindingModel
            {
                UserId = UserId,
                PurchaseId = _purchase.Read(new PurchaseBindingModel { UserId = UserId }).Select(x => x.Id).ToList()
            };
        }

        public ReportBindingModel GetFurnituresForReport(int UserId)
        {
            return new ReportBindingModel
            {
                UserId = UserId,
                PurchaseId = _furniture.Read(new FurnitureBindingModel { UserId = UserId }).Select(x => x.Id).ToList()
            };
        }
    }
}
