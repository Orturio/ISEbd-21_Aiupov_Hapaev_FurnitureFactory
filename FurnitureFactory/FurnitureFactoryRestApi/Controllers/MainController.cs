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
        public MainController(PurchaseLogic purchase, FurnitureLogic furniture, PurchaseLogic main, PaymentLogic payment)
        {
            _purchase = purchase;
            _furniture = furniture;
            _main = main;
            _payment = payment;
        }

        [HttpGet]
        public List<FurnitureViewModel> GetFurnitureList() => _furniture.Read(null)?.ToList();

        [HttpGet]
        public FurnitureViewModel GetFurniture(int furnitureId) => _furniture.Read(new FurnitureBindingModel { Id = furnitureId })?[0];

        [HttpGet]
        public List<PurchaseViewModel> GetPurchaseList() => _purchase.Read(null)?.ToList();

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

        public void CreatePayment(PaymentBindingModel model) => _payment.CreateOrUpdate(model);
    }
}
