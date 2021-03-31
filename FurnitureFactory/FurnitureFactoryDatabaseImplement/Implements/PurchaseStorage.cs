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
    public class PurchaseStorage : IPurchaseStorage
    {
        public List<PurchaseViewModel> GetFullList()
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Purchases.Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    Name = rec.Name,
                    Sum = rec.Sum,
                    DateOfCreation = rec.DateOfCreation,
                    DateOfPayment = rec.DateOfPayment
                })
                .ToList();
            }
        }

        public List<PurchaseViewModel> GetFilteredList(PurchaseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Purchases.Where(rec => rec.Id == model.Id && rec.Name == rec.Name)
                .Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    Name = rec.Name,
                    Sum = rec.Sum,
                    DateOfCreation = rec.DateOfCreation,
                    DateOfPayment = rec.DateOfPayment
                })
                .ToList();
            }
        }

        public PurchaseViewModel GetElement(PurchaseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                var purchase = context.Purchases.FirstOrDefault(rec => rec.Id == model.Id);
                return purchase != null ?
                new PurchaseViewModel
                {
                    Id = purchase.Id,
                    UserId = purchase.UserId,
                    Name = purchase.Name,
                    Sum = purchase.Sum,
                    DateOfCreation = purchase.DateOfCreation,
                    DateOfPayment = purchase.DateOfPayment
                } :
                null;
            }
        }

        public void Insert(PurchaseBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                context.Purchases.Add(CreateModel(model, new Purchase()));
                context.SaveChanges();
            }
        }

        public void Update(PurchaseBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                var element = context.Purchases.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Покупка не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(PurchaseBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Purchase element = context.Purchases.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Purchases.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Покупка не найдена");
                }
            }
        }

        private Purchase CreateModel(PurchaseBindingModel model, Purchase purchase)
        {
            purchase.UserId = model.UserId;
            purchase.Name = model.Name;
            purchase.Sum = model.Sum;
            purchase.DateOfCreation = model.DateOfCreation;
            purchase.DateOfPayment = model.DateOfPayment;
            return purchase;
        }
    }
}
