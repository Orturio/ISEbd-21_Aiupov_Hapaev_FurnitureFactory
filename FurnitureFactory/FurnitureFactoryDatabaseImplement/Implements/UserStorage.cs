using System;
using System.Collections.Generic;
using System.Linq;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryBusinessLogics.Enums;
using FurnitureFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactoryDatabaseImplement.Implements
{
    public class UserStorage : IUserStorage
    {
        public List<UserViewModel> GetFullList()
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Users.Select(rec => new UserViewModel
                {
                    Id = rec.Id,
                    Role = rec.Role,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }

        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Users.Where(rec => rec.Email == model.Email && rec.Password == rec.Password && rec.Role == model.Role)
                .Select(rec => new UserViewModel
                {
                    Id = rec.Id,
                    Role = rec.Role,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }

        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                var user = context.Users.FirstOrDefault(rec => rec.Id == model.Id || rec.Email == model.Email);
                if (user == null || (model.Password != null && user.Password != model.Password))
                {
                    return null;
                }
                return CreateModel(user);
            }
        }

        public void Insert(UserBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                context.Users.Add(CreateModel(model, new User()));
                context.SaveChanges();
            }
        }

        public void Update(UserBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                var element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Клиент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(UserBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                User element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Users.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        private UserViewModel CreateModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Role = user.Role,
                Email = user.Email,
                Password = user.Password
            };
        }

        private User CreateModel(UserBindingModel model, User user)
        {
            user.Role = model.Role;
            user.Email = model.Email;
            user.Password = model.Password;
            return user;
        }
    }
}
