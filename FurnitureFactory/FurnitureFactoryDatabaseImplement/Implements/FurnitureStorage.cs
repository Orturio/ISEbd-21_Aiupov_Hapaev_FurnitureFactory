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
    public class FurnitureStorage : IFurnitureStorage
    {
        public List<FurnitureViewModel> GetFullList()
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Furnitures.Include(rec => rec.PurchaseFurniture)
                .ThenInclude(rec => rec.Purchases).Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    CostId = rec.CostsId,
                    FurnitureName = rec.FurnitureName,
                    Material = rec.Material,
                    FurniturePrice = rec.FurniturePrice
                })
                .ToList();
            }
        }

        public List<FurnitureViewModel> GetFilteredList(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Furnitures.Include(rec => rec.PurchaseFurniture)
                .ThenInclude(rec => rec.Purchases).Where(rec => rec.Id == model.Id && rec.FurnitureName == rec.FurnitureName)
                .Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    CostId = rec.CostsId,
                    FurnitureName = rec.FurnitureName,
                    Material = rec.Material,
                    FurniturePrice = rec.FurniturePrice
                })
                .ToList();
            }
        }

        public FurnitureViewModel GetElement(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                var furniture = context.Furnitures.Include(rec => rec.PurchaseFurniture)
                .ThenInclude(rec => rec.Purchases)
                .FirstOrDefault(rec => rec.FurnitureName == model.FurnitureName || rec.Id == model.Id); 
                return furniture != null ?
                new FurnitureViewModel
                {
                    Id = furniture.Id,
                    UserId = furniture.UserId,
                    CostId = furniture.CostsId,
                    FurnitureName = furniture.FurnitureName,
                    Material = furniture.Material,
                    FurniturePrice = furniture.FurniturePrice
                } :
                null;
            }
        }

        public void Insert(FurnitureBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                context.Furnitures.Add(CreateModel(model, new Furniture()));
                context.SaveChanges();
            }
        }

        public void Update(FurnitureBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                var element = context.Furnitures.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Мебель не найдена");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(FurnitureBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Furniture element = context.Furnitures.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Furnitures.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Мебель не найдена");
                }
            }
        }

        private Furniture CreateModel(FurnitureBindingModel model, Furniture furniture)
        {
            furniture.UserId = model.UserId;
            furniture.CostsId = model.CostsId;
            furniture.FurnitureName = model.FurnitureName;
            furniture.Material = model.Material;
            furniture.FurniturePrice = model.FurniturePrice;           
            return furniture;
        }
    }
}
