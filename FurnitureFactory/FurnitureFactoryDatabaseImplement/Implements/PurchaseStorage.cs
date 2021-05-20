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
                .ThenInclude(rec => rec.Furniture).ThenInclude(rec => rec.Payment)
                .Include(rec => rec.PurchaseCost).ThenInclude(rec => rec.Cost)
                .ToList()
                .Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    PurchaseName = rec.PurchaseName,
                    PurchaseSum = rec.PurchaseSum,
                    PurchaseSumToPayment = rec.PurchaseFurniture.FirstOrDefault(x => x.PurchasesId == rec.Id)
                    .Furniture.Payment.FirstOrDefault(x => x.PurchaseId == rec.Id)?.PaymentSum,
                    DateOfCreation = rec.DateOfCreation,
                    DateOfPayment = rec.PurchaseFurniture.FirstOrDefault(x => x.PurchasesId == rec.Id)
                    .Furniture.Payment.FirstOrDefault(x => x.PurchaseId == rec.Id)?.DateOfPayment,
                    PurchaseFurniture = rec.PurchaseFurniture
                .ToDictionary(recPC => recPC.FurnitureId, recPC => (recPC.Furniture?.FurnitureName, recPC.Count, recPC.Furniture.FurniturePrice, recPC.TotalPrice)),
                    PurchaseCosts = rec.PurchaseCost
                .ToDictionary(recPC => recPC.CostId, recPC => (recPC.Cost?.CostName, recPC.Price))
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
                .ThenInclude(rec => rec.Furniture).ThenInclude(rec => rec.Payment)
                .Include(rec => rec.PurchaseCost).ThenInclude(rec => rec.Cost)
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateOfCreation.Date == model.DateOfCreation.Date) ||
(model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateOfCreation.Date >= model.DateFrom.Value.Date && rec.DateOfCreation.Date <= model.DateTo.Value.Date
&& rec.UserId.HasValue && rec.UserId == model.UserId) || (rec.UserId.HasValue && rec.UserId == model.UserId && !model.DateFrom.HasValue && !model.DateTo.HasValue))
                .ToList()
                .Select(rec => new PurchaseViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    PurchaseName = rec.PurchaseName,
                    PurchaseSum = rec.PurchaseSum,
                    PurchaseSumToPayment = rec.PurchaseFurniture.FirstOrDefault(x => x.PurchasesId == rec.Id)
                    .Furniture.Payment.FirstOrDefault(x => x.PurchaseId == rec.Id)?.PaymentSum,
                    DateOfCreation = rec.DateOfCreation,
                    DateOfPayment = rec.PurchaseFurniture.FirstOrDefault(x => x.PurchasesId == rec.Id)
                    .Furniture.Payment.FirstOrDefault(x => x.PurchaseId == rec.Id)?.DateOfPayment,
                    PurchaseFurniture = rec.PurchaseFurniture
                .ToDictionary(recPC => recPC.FurnitureId, recPC => (recPC.Furniture?.FurnitureName, recPC.Count, recPC.Furniture.FurniturePrice, recPC.TotalPrice)),
                    PurchaseCosts = rec.PurchaseCost
                .ToDictionary(recPC => recPC.CostId, recPC => (recPC.Cost?.CostName, recPC.Price))
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
                .ThenInclude(rec => rec.Furniture).ThenInclude(rec => rec.Payment)
                .Include(rec => rec.PurchaseCost).ThenInclude(rec => rec.Cost)
                .FirstOrDefault(rec => rec.PurchaseName == model.PurchaseName || rec.Id == model.Id);
                var payment = purchase.PurchaseFurniture.FirstOrDefault(x => x.PurchasesId == model.Id)
                    .Furniture;
                return purchase != null ?
                new PurchaseViewModel
                {
                    Id = purchase.Id,
                    UserId = purchase.UserId,
                    PurchaseName = purchase.PurchaseName,
                    PurchaseSum = purchase.PurchaseSum,
                    PurchaseSumToPayment = purchase.PurchaseFurniture.FirstOrDefault(x => x.PurchasesId == purchase.Id)
                    .Furniture.Payment.FirstOrDefault(x => x.PurchaseId == purchase.Id)?.PaymentSum,
                    DateOfCreation = purchase.DateOfCreation,
                    DateOfPayment = purchase.PurchaseFurniture.FirstOrDefault(x => x.PurchasesId == purchase.Id)
                    .Furniture.Payment.FirstOrDefault(x => x.PurchaseId == purchase.Id)?.DateOfPayment,
                    PurchaseFurniture = purchase.PurchaseFurniture
                .ToDictionary(recPC => recPC.FurnitureId, recPC => (recPC.Furniture?.FurnitureName, recPC.Count, recPC.Furniture.FurniturePrice, recPC.TotalPrice)),
                    PurchaseCosts = purchase.PurchaseCost
                .ToDictionary(recPC => recPC.CostId, recPC => (recPC.Cost?.CostName, recPC.Price))
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
                        var element = context.Purchases.FirstOrDefault(rec => rec.Id == model.Id);
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
            purchase.PurchaseName = model.PurchaseName;
            purchase.PurchaseSum = model.PurchaseSum;
            purchase.DateOfCreation = model.DateOfCreation;
            return purchase;
        }

        private Purchase CreateModel(PurchaseBindingModel model, Purchase purchase, FurnitureFactoryDatabase context)
        {
            purchase.UserId = model.UserId;
            purchase.PurchaseName = model.PurchaseName;
            purchase.PurchaseSum = model.PurchaseSum;
            purchase.DateOfCreation = model.DateOfCreation;

            if (model.Id.HasValue)
            {
                var purchaseFurniture = context.PurchaseFurnitures.Where(rec => rec.PurchasesId == model.Id.Value).ToList();
                var purchaseCost = context.PurchaseCosts.Where(rec => rec.PurchasesId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.PurchaseFurnitures.RemoveRange(purchaseFurniture.Where(rec => !model.PurchaseFurnitures.ContainsKey(rec.FurnitureId)).ToList());
                context.PurchaseCosts.RemoveRange(purchaseCost.Where(rec => !model.PurchaseCosts.ContainsKey(rec.CostId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateFurniture in purchaseFurniture)
                {
                    updateFurniture.Count = model.PurchaseFurnitures[updateFurniture.FurnitureId].Item2;
                    updateFurniture.TotalPrice = model.PurchaseFurnitures[updateFurniture.FurnitureId].Item4;
                    model.PurchaseFurnitures.Remove(updateFurniture.FurnitureId);
                }
                foreach (var updateCost in purchaseCost)
                {
                    updateCost.Price = model.PurchaseCosts[updateCost.CostId].Item2;
                    model.PurchaseCosts.Remove(updateCost.CostId);
                }
                context.SaveChanges();
            }
            //добавили новые
            foreach (var pc in model.PurchaseFurnitures)
            {
                context.PurchaseFurnitures.Add(new PurchaseFurniture
                {
                    PurchasesId = purchase.Id,
                    FurnitureId = pc.Key,
                    Count = pc.Value.Item2,
                    TotalPrice = pc.Value.Item4
                });
                context.SaveChanges();
            }
            if (model.PurchaseCosts != null)
            {
                foreach (var pc in model.PurchaseCosts)
                {
                    context.PurchaseCosts.Add(new PurchaseCost
                    {
                        PurchasesId = purchase.Id,
                        CostId = pc.Key,
                        Price = pc.Value.Item2
                    });
                    context.SaveChanges();
                }
            }
            return purchase;
        }
    }
}
