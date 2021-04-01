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
                return context.Purchases
                .Include(rec => rec.PurchaseFurniture)
                .ThenInclude(rec => rec.Furniture)
                .ToList()
                .Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    Name = rec.Name,
                    Sum = rec.Sum,
                    DateOfCreation = rec.DateOfCreation,
                    DateOfPayment = rec.DateOfPayment,
                    PurchaseFurniture = rec.PurchaseFurniture
                .ToDictionary(recPC => recPC.FurnitureId, recPC => (recPC.Furniture?.Name, recPC.Count))
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
                return context.Purchases
                .Include(rec => rec.PurchaseFurniture)
                .ThenInclude(rec => rec.Furniture)
                .Where(rec => rec.Name.Contains(model.Name))
                .ToList()
                .Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    Name = rec.Name,
                    Sum = rec.Sum,
                    DateOfCreation = rec.DateOfCreation,
                    DateOfPayment = rec.DateOfPayment,
                    PurchaseFurniture = rec.PurchaseFurniture
                .ToDictionary(recPC => recPC.FurnitureId, recPC => (recPC.Furniture?.Name, recPC.Count))
                }).ToList();
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
                var purchase = context.Purchases
                .Include(rec => rec.PurchaseFurniture)
                .ThenInclude(rec => rec.Furniture)
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
                return purchase != null ?
                new PurchaseViewModel
                {
                    Id = purchase.Id,
                    UserId = purchase.UserId,
                    Name = purchase.Name,
                    Sum = purchase.Sum,
                    DateOfCreation = purchase.DateOfCreation,
                    DateOfPayment = purchase.DateOfPayment,
                    PurchaseFurniture = purchase.PurchaseFurniture
                .ToDictionary(recPC => recPC.FurnitureId, recPC => (recPC.Furniture?.Name, recPC.Count))
                } :
                null;
            }
        }

        public void Insert(PurchaseBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Purchase purchase = CreateModel(model, new Purchase());
                        context.Purchases.Add(purchase);
                        context.SaveChanges();
                        CreateModel(model, purchase, context);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(PurchaseBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Purchases.Include(rec => rec.PurchaseFurniture)
                            .ThenInclude(rec => rec.Furniture).FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(PurchaseBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Purchase element = context.Purchases.Include(rec => rec.PurchaseFurniture)
                .ThenInclude(rec => rec.Furniture).FirstOrDefault(rec => rec.Id == model.Id);
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

        private Purchase CreateModel(PurchaseBindingModel model, Purchase purchase, FurnitureFactoryDatabase context)
        {
            purchase.UserId = model.UserId;
            purchase.Name = model.Name;
            purchase.Sum = model.Sum;
            purchase.DateOfCreation = model.DateOfCreation;
            purchase.DateOfPayment = model.DateOfPayment;

            if (model.Id.HasValue)
            {
                var productComponents = context.PurchaseFurnitures.Where(rec => rec.PurchasesId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.PurchaseFurnitures.RemoveRange(productComponents.Where(rec => !model.PurchaseFurnitures.ContainsKey(rec.FurnitureId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in productComponents)
                {
                    updateComponent.Count = model.PurchaseFurnitures[updateComponent.FurnitureId].Item2;
                    model.PurchaseFurnitures.Remove(updateComponent.FurnitureId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.PurchaseFurnitures)
            {
                context.PurchaseFurnitures.Add(new PurchaseFurniture
                {
                    PurchasesId = purchase.Id,
                    FurnitureId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }

            return purchase;
        }
    }
}
