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
                return context.Furnitures.Include(rec => rec.User).Include(rec => rec.Payment).Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    UserEmail = rec.User.Email,
                    FurnitureName = rec.FurnitureName,
                    FurniturePayment = rec.FurniturePayment,
                    Material = rec.Material,
                    FurniturePrice = rec.FurniturePrice,
                    DateOfCreation = rec.DateOfCreation
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
                return context.Furnitures.Include(rec => rec.User).Include(rec => rec.Payment)
                    .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateOfCreation.Date == model.DateOfCreation.Date) ||
                    (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateOfCreation.Date >= model.DateFrom.Value.Date && rec.DateOfCreation.Date <= model.DateTo.Value.Date && rec.UserId == model.UserId)
                     || (rec.UserId == model.UserId && !model.DateFrom.HasValue && !model.DateTo.HasValue) 
                     || rec.PurchaseFurniture.Select(x => x.FurnitureId).Contains(model.PurchaseId))
                .Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    UserEmail = rec.User.Email,
                    FurnitureName = rec.FurnitureName,
                    FurniturePayment = rec.FurniturePayment,
                    Material = rec.Material,
                    FurniturePrice = rec.FurniturePrice,
                    DateOfCreation = rec.DateOfCreation
                })
                .ToList();
            }
        }

        public List<FurnitureViewModel> GetFilteredPickList(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Furnitures.Include(rec => rec.User).Include(rec => rec.Payment)
.Where(rec => (rec.UserId == model.UserId) || rec.PurchaseFurniture.Select(x => x.FurnitureId).Contains(model.PurchaseId))
                .Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    UserId = rec.UserId,
                    UserEmail = rec.User.Email,
                    FurnitureName = rec.FurnitureName,
                    FurniturePayment = rec.FurniturePayment,
                    Material = rec.Material,
                    FurniturePrice = rec.FurniturePrice,
                    DateOfCreation = rec.DateOfCreation
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
                var furniture = context.Furnitures.Include(rec => rec.User).Include(rec => rec.Payment)
                .FirstOrDefault(rec => rec.FurnitureName == model.FurnitureName || rec.Id == model.Id); 
                return furniture != null ?
                new FurnitureViewModel
                {
                    Id = furniture.Id,
                    UserId = furniture.UserId,
                    UserEmail = furniture.User.Email,
                    FurnitureName = furniture.FurnitureName,
                    FurniturePayment = furniture?.FurniturePayment,
                    Material = furniture.Material,
                    FurniturePrice = furniture.FurniturePrice,
                    DateOfCreation = furniture.DateOfCreation
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
            furniture.FurniturePayment = model?.FurniturePayment;
            furniture.FurnitureName = model.FurnitureName;
            furniture.Material = model.Material;
            furniture.FurniturePrice = model.FurniturePrice;
            furniture.DateOfCreation = model.DateOfCreation;
            return furniture;
        }
    }
}
