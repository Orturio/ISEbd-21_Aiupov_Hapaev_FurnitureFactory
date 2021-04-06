using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryBusinessLogics.Enums;
using FurnitureFactoryClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FurnitureFactoryClientApp.Controllers
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

            return View(APIUser.GetRequest<List<PurchaseViewModel>>($"api/main/getpurchases?userId={Program.User.Id}"));
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
                    Role = UserRole.Клиент
                });
                Program.User.Email = email;
                Program.User.Password = password;
                Program.User.Role = UserRole.Клиент;
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
                RequestId = Activity.Current?.Id ??
            HttpContext.TraceIdentifier
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
                APIUser.PostRequest("api/user/register", new
                UserBindingModel
                {
                    Email = email,
                    Password = password,
                    Role = UserRole.Клиент
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите email и пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Canneds = APIUser.GetRequest<List<PurchaseViewModel>>("api/main/getpurchaselist");
            return View();
        }

        [HttpPost]
        public void Create(string canned, decimal sum)
        {
            if (sum == 0)
            {
                return;
            }

            APIUser.PostRequest("api/main/createpurchase", new PurchaseBindingModel
            {
                UserId = (int)Program.User.Id,
                PurchaseName = canned,
                DateOfCreation = DateTime.Now,
                PurchaseSum = sum
            });
            Response.Redirect("Index");
        }

        [HttpPost]
        public decimal Calc(decimal count, int furniture)
        {
            FurnitureViewModel prod = APIUser.GetRequest<FurnitureViewModel>($"api/main/getpurchases?furnitureId={furniture}");
            return count * prod.FurniturePrice;
        }
    }
}
