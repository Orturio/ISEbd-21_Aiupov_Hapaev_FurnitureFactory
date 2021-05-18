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

        public IActionResult Costs()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            var a = APIUser.GetRequest<List<CostViewModel>>($"api/main/getcosts?userId={Program.User.Id}");
            return View(APIUser.GetRequest<List<CostViewModel>>($"api/main/getcosts?userId={Program.User.Id}"));
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
        public IActionResult CreateFurniture()
        {
            return View();
        }

        [HttpPost]
        public void CreateFurniture(string furniture, string material, decimal price)
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
        public IActionResult UpdateFurniture(int id)
        {
            ViewBag.Furniture = APIUser.GetRequest<FurnitureViewModel>($"api/main/getfurniture?furnitureId={id}");
            return View();
        }

        [HttpPost]
        public void UpdateFurniture(int id, string furniture, string material, decimal price)
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
        
        public void DeleteFurniture(int id)
        {
            APIUser.PostRequest("api/main/deletefurniture", new FurnitureBindingModel { Id = id });
            Response.Redirect("../Index");
        }

        [HttpGet]
        public IActionResult CreateCost()
        {
            return View();
        }

        [HttpPost]
        public void CreateCost(string cost, decimal costprice)
        {
            APIUser.PostRequest("api/main/createorupdatecost", new CostBindingModel
            {
                UserId = Program.User.Id,
                CostName = cost,
                Price = costprice
            });

            Response.Redirect("Costs");
        }

        [HttpGet]
        public IActionResult UpdateCost(int id)
        {
            ViewBag.Cost = APIUser.GetRequest<CostViewModel>($"api/main/getcost?costId={id}");
            return View();
        }

        [HttpPost]
        public void UpdateCost(int id, string cost, decimal costprice)
        {
            var Cost = APIUser.GetRequest<CostViewModel>($"api/main/getcost?costId={id}");
            APIUser.PostRequest("api/main/createorupdatecost", new CostBindingModel
            {
                Id = id,
                UserId = Cost.UserId,
                CostName = cost,
                Price = costprice
            });

            Response.Redirect("../Costs");
        }

        [HttpGet]
        public IActionResult BindCost(int id)
        {
            ViewBag.Cost = APIUser.GetRequest<CostViewModel>($"api/main/getcost?costId={id}");            
            ViewBag.Purchases = APIUser.GetRequest<List<PurchaseViewModel>>($"api/main/GetPurchaseList");
            return View();
        }

        [HttpPost]
        public void BindCost(int id, string cost, decimal costprice)
        {
            var Cost = APIUser.GetRequest<CostViewModel>($"api/main/getcost?costId={id}");
            

            Response.Redirect("../Costs");
        }

        public void DeleteCost(int id)
        {
            APIUser.PostRequest("api/main/deletecost", new CostBindingModel { Id = id });
            Response.Redirect("../Costs");
        }

        [HttpPost]
        public decimal Bind(int id, string purchase)
        {
            PurchaseViewModel Purchase = APIUser.GetRequest<PurchaseViewModel>($"api/main/getpurchasenl?Id={id}");
            return Purchase.PurchaseSum;
        }
    }
}
