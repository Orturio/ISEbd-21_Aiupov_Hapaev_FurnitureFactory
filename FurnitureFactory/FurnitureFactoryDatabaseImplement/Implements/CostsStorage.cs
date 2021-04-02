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
    public class CostsStorage : ICostStorage
    {
        public List<CostViewModel> GetFullList()
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Costs.Select(rec => new CostViewModel
                {
                    Id = rec.Id,
                    PurchaseName = context.Purchases.Include(x => x.PurchaseFurniture).FirstOrDefault(x => x.PurchaseName == rec.PurchaseName).PurchaseName,
                    Count = rec.Count,
                    Price = rec.CostPrice
                })
                .ToList();
            }
        }

        public List<CostViewModel> GetFilteredList(CostBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Costs.Include(rec => rec.Furniture).ThenInclude(rec => rec.PurchaseFurniture)
                    .ThenInclude(rec => rec.Purchases).Where(rec => rec.Id == model.Id)
                .Select(rec => new CostViewModel
                {
                    Id = rec.Id,
                    PurchaseName = context.Purchases.Include(x => x.PurchaseFurniture).FirstOrDefault(x => x.PurchaseName == rec.PurchaseName).PurchaseName,
                    Count = rec.Count,
                    Price = rec.CostPrice
                })
                .ToList();
            }
        }

        public CostViewModel GetElement(CostBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                var cost = context.Costs.Include(rec => rec.Furniture).ThenInclude(rec => rec.PurchaseFurniture)
                    .ThenInclude(rec => rec.Purchases).FirstOrDefault(rec => rec.Id == model.Id);
                return cost != null ?
                new CostViewModel
                {
                    Id = cost.Id,
                    PurchaseName = context.Purchases.Include(x => x.PurchaseFurniture).FirstOrDefault(x => x.PurchaseName == cost.PurchaseName).PurchaseName,
                    Count = cost.Count,
                    Price = cost.CostPrice
                } :
                null;
            }
        }

        public void Insert(CostBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                context.Costs.Add(CreateModel(model, new Cost()));
                context.SaveChanges();
            }
        }

        public void Update(CostBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                var element = context.Costs.Include(rec => rec.Furniture).ThenInclude(rec => rec.PurchaseFurniture)
                    .ThenInclude(rec => rec.Purchases).FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Затрата не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(CostBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Cost element = context.Costs.Include(rec => rec.Furniture).ThenInclude(rec => rec.PurchaseFurniture)
                    .ThenInclude(rec => rec.Purchases).FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Costs.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Затрата не найдена");
                }
            }
        }

        private Cost CreateModel(CostBindingModel model, Cost cost)
        {
            cost.PurchaseName = model.PurchaseName;
            cost.Count = model.Count;
            cost.CostPrice = model.Price;
            return cost;
        }
    }
}
