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
                    EmployeeId = rec.UserId,
                    CostsId = rec.CostsId,
                    Name = rec.Name,
                    Material = rec.Material,
                    Price = rec.Price
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
                .ThenInclude(rec => rec.Purchases).Where(rec => rec.Id == model.Id && rec.Name == rec.Name)
                .Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    EmployeeId = rec.UserId,
                    CostsId = rec.CostsId,
                    Name = rec.Name,
                    Material = rec.Material,
                    Price = rec.Price
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
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id); 
                return furniture != null ?
                new FurnitureViewModel
                {
                    Id = furniture.Id,
                    EmployeeId = furniture.UserId,
                    CostsId = furniture.CostsId,
                    Name = furniture.Name,
                    Material = furniture.Material,
                    Price = furniture.Price
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
            furniture.UserId = model.EmployeeId;
            furniture.CostsId = model.CostsId;
            furniture.Name = model.Name;
            furniture.Material = model.Material;
            furniture.Price = model.Price;           
            return furniture;
        }
    }
}
