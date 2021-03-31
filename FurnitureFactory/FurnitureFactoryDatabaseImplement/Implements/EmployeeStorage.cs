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
    public class EmployeeStorage : IEmployeeStorage
    {
        public List<EmployeeViewModel> GetFullList()
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Employees.Select(rec => new EmployeeViewModel
                {
                    Id = rec.Id,
                    Login = rec.Login,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }

        public List<EmployeeViewModel> GetFilteredList(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                return context.Employees.Where(rec => rec.Email == model.Email && rec.Password == rec.Password)
                .Select(rec => new EmployeeViewModel
                {
                    Id = rec.Id,
                    Login = rec.Login,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }

        public EmployeeViewModel GetElement(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FurnitureFactoryDatabase())
            {
                var employee = context.Employees.FirstOrDefault(rec => rec.Id == model.Id);
                return employee != null ?
                new EmployeeViewModel
                {
                    Id = employee.Id,
                    Login = employee.Login,
                    Email = employee.Email,
                    Password = employee.Password
                } :
                null;
            }
        }

        public void Insert(EmployeeBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                context.Employees.Add(CreateModel(model, new Employee()));
                context.SaveChanges();
            }
        }

        public void Update(EmployeeBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                var element = context.Employees.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Сотрудник не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(EmployeeBindingModel model)
        {
            using (var context = new FurnitureFactoryDatabase())
            {
                Employee element = context.Employees.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Employees.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Сотрудник не найден");
                }
            }
        }

        private Employee CreateModel(EmployeeBindingModel model, Employee employee)
        {
            employee.Login = model.Login;
            employee.Email = model.Email;
            employee.Password = model.Password;
            return employee;
        }
    }
}
