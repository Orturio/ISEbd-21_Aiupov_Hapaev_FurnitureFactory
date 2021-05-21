using System;
using System.Collections.Generic;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    public class PurchaseLogic
    {
        private readonly IPurchaseStorage _purchasesStorage;

        public PurchaseLogic(IPurchaseStorage purchasesStorage)
        {
            _purchasesStorage = purchasesStorage;
        }

        public List<PurchaseViewModel> Read(PurchaseBindingModel model)
        {
            if (model == null)
            {
                return _purchasesStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PurchaseViewModel> { _purchasesStorage.GetElement(model) };
            }
            return _purchasesStorage.GetFilteredList(model);
        }

        public void CreatePurchase(PurchaseBindingModel model)
        {
            var element = _purchasesStorage.GetElement(new PurchaseBindingModel
            {
                PurchaseName = model.PurchaseName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть покупка с таким названием");
            }
            _purchasesStorage.Insert(model); 
        }

        public void UpdatePurchase(PurchaseBindingModel model)
        {
            
            _purchasesStorage.Update(model);
            
        }

        public void Delete(PurchaseBindingModel model)
        {
            var element = _purchasesStorage.GetElement(new PurchaseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Покупка не найдена");
            }
            _purchasesStorage.Delete(model);
        }
    }
}
