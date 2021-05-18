using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryBusinessLogics.Enums;
using FurnitureFactoryEmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FurnitureFactoryEmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
          
            return View(APIUser.GetRequest<List<FurnitureViewModel>>($"api/main/getfurnitures?userId={Program.User.Id}"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.User);
        }

        [HttpPost]
        public void Privacy(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                APIUser.PostRequest("api/user/updatedata", new UserBindingModel
                {
                    Id = Program.User.Id,
                    Email = email,
                    Password = password,
                    Role = UserRole.Сотрудник
                });
                Program.User.Email = email;
                Program.User.Password = password;
                Program.User.Role = UserRole.Сотрудник;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин и пароль");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Program.User = APIUser.GetRequest<UserViewModel>($"api/user/login?email={email}&password={password}");
                if (Program.User == null)
                {
                    throw new Exception("Неверный email/пароль");
                }

                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите email, пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                APIUser.PostRequest("api/user/register", new UserBindingModel
                {
                    Email = email,
                    Password = password,
                    Role = UserRole.Сотрудник
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите email и пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(string furniture, string material, decimal price)
        {
            APIUser.PostRequest("api/main/createfurniture", new FurnitureBindingModel
            {
                UserId = Program.User.Id,
                FurnitureName = furniture,
                Material = material,
                DateOfCreation = DateTime.Now,
                FurniturePrice = price
            });

            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Furniture = APIUser.GetRequest<FurnitureViewModel>($"api/main/getfurniture?furnitureId={id}");
            return View();
        }

        [HttpPost]
        public void Update(int id, string furniture, string material, decimal price, DateTime dateOfCreation)
        {
            var Furniture = APIUser.GetRequest<FurnitureViewModel>($"api/main/getfurniture?furnitureId={id}");
            APIUser.PostRequest("api/main/updatefurniture", new FurnitureBindingModel
            {
                Id = id,
                UserId = Furniture.UserId,
                FurnitureName = furniture,
                Material = material,
                DateOfCreation = Furniture.DateOfCreation,
                FurniturePrice = price
            });

            Response.Redirect("../Index");
        }
        
        public void Delete(int id)
        {
            APIUser.PostRequest("api/main/deletefurniture", new FurnitureBindingModel { Id = id });
            Response.Redirect("../Index");
        }

        [HttpPost]
        public decimal Calc(decimal count, int furniture)
        {
            FurnitureViewModel fur = APIUser.GetRequest<FurnitureViewModel>($"api/main/getfurniture?furnitureId={furniture}");
            return count * fur.FurniturePrice;
        }
    }
}
