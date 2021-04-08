using System;
using System.Collections.Generic;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    public class PaymentLogic
    {
        private readonly IPaymentStorage _paymentStorage;

        public PaymentLogic(IPaymentStorage paymentStorage)
        {
            _paymentStorage = paymentStorage;
        }

        public List<PaymentViewModel> Read(PaymentBindingModel model)
        {
            if (model == null)
            {
                return _paymentStorage.GetFullList();
            }
            if (model.Id.HasValue || model.PurchaseId.HasValue)
            {
                return new List<PaymentViewModel> { _paymentStorage.GetElement(model) };
            }
            if (model.PurchaseId.HasValue)
            {
                return new List<PaymentViewModel> { _paymentStorage.GetElement(model) };
            }
            return _paymentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PaymentBindingModel model)
        {
            var element = _paymentStorage.GetElement(new PaymentBindingModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такие затраты");
            }
            if (model.Id.HasValue)
            {
                _paymentStorage.Update(model);
            }
            else
            {
                _paymentStorage.Insert(model);
            }
        }
        public void Delete(PaymentBindingModel model)
        {
            var element = _paymentStorage.GetElement(new PaymentBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Затраты не найдены");
            }
            _paymentStorage.Delete(model);
        }
    }
}
