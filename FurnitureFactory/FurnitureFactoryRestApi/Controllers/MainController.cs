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
        public MainController(PurchaseLogic purchase, FurnitureLogic furniture, PurchaseLogic main, PaymentLogic payment)
        {
            _purchase = purchase;
            _furniture = furniture;
            _main = main;
        }

        [HttpGet]
        public List<FurnitureViewModel> GetFurnitureList() => _furniture.Read(null)?.ToList();

        [HttpGet]
        public FurnitureViewModel GetFurniture(int furnitureId) => _furniture.Read(new FurnitureBindingModel { Id = furnitureId })?[0];

        [HttpGet]
        public List<PurchaseViewModel> GetPurchaseList() => _purchase.Read(null)?.ToList();

        [HttpGet]
        public List<PurchaseViewModel> GetPurchases(int userId) => _purchase.Read(new PurchaseBindingModel { UserId = userId });

        [HttpPost]
        public void CreatePurchase(PurchaseBindingModel model) =>  _main.CreatePurchase(model);
    }
}
