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
    class FurnitureStorage
    {
        public List<FurnitureViewModel> GetFullList()
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Furnitures.Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    EmployeeId = rec.EmployeeId,
                    CostsId = rec.CostsId,
                    Price = rec.Price,
                    Material = rec.Material
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
                return context.Furnitures.Where(rec => rec.Id == model.Id && rec.Name == rec.Name)
                .Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    EmployeeId = rec.EmployeeId,
                    CostsId = rec.CostsId,
                    Price = rec.Price,
                    Material = rec.Material
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
                var furniture = context.Furnitures.FirstOrDefault(rec => rec.Id == model.Id);
                return furniture != null ?
                new FurnitureViewModel
                {
                    Id = furniture.Id,
                    EmployeeId = furniture.EmployeeId,
                    CostsId = furniture.CostsId,
                    Price = furniture.Price,
                    Material = furniture.Material
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
            furniture.EmployeeId = model.EmployeeId;
            furniture.CostsId = model.CostsId;
            furniture.Price = model.Price;
            furniture.Material = model.Material;
            return furniture;
        }
    }
}
