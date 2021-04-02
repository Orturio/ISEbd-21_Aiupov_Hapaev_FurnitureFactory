using System;
using System.Collections.Generic;
using System.Linq;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactoryDatabaseImplement.Implements
{
    public class PaymentStorage : IPaymentStorage
    {
        public List<PaymentViewModel> GetFullList()
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Payments.Select(rec => new PaymentViewModel
                {
                    Id = rec.Id,
                    PurchaseId = rec.PurchaseId,
                    Sum = rec.PaymentSum,
                    DateOfPayment = rec.DateOfPayment
                })
                .ToList();
            }
        }

        public List<PaymentViewModel> GetFilteredList(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Payments.Where(rec => rec.Id == model.Id)
                .Select(rec => new PaymentViewModel
                {
                    Id = rec.Id,
                    PurchaseId = rec.PurchaseId,
                    Sum = rec.PaymentSum,
                    DateOfPayment = rec.DateOfPayment
                })
                .ToList();
            }
        }

        public PaymentViewModel GetElement(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                var payment = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
                return payment != null ?
                new PaymentViewModel
                {
                    Id = payment.Id,
                    PurchaseId = payment.PurchaseId,
                    Sum = payment.PaymentSum,
                    DateOfPayment = payment.DateOfPayment
                } :
                null;
            }
        }

        public void Insert(PaymentBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                context.Payments.Add(CreateModel(model, new Payment()));
                context.SaveChanges();
            }
        }

        public void Update(PaymentBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                var element = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Оплата не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(PaymentBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Payment element = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Payments.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Оплата не найдена");
                }
            }
        }

        private Payment CreateModel(PaymentBindingModel model, Payment payment)
        {
            payment.PurchaseId = model.PurchaseId;
            payment.PaymentSum = model.Sum;
            payment.DateOfPayment = model.DateOfPayment;
            return payment;
        }
    }
}
