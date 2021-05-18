using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryBusinessLogics.Enums;
using FurnitureFactoryClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FurnitureFactoryClientApp.Controllers
{
    public class HomeController : Controller
    {
        private Dictionary<int, (string, int, decimal, decimal)> purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();

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
            ViewBag.Furnitures = APIUser.GetRequest<List<FurnitureViewModel>>("api/main/getfurniturelist");
            return View();
        }

        [HttpPost]
        public void Create(string purchase, string furniture, int count, decimal sum)
        {

            purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();
            purchaseFurniture.Add(Convert.ToInt32(furniture), (furniture, count, sum / count, sum));

            APIUser.PostRequest("api/main/createpurchase", new PurchaseBindingModel
            {
                UserId = Program.User.Id,
                PurchaseName = purchase,
                DateOfCreation = DateTime.Now,
                PurchaseSum = sum,
                PurchaseFurnitures = purchaseFurniture
            });

            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Update(string purchasename)
        {
            ViewBag.PurchaseName = purchasename;
            ViewBag.Furnitures = APIUser.GetRequest<List<FurnitureViewModel>>("api/main/getfurniturelist");
            return View();
        }

        [HttpPost]
        public void Update(int id, string purchase, string furniture, int count, decimal sum, DateTime datecreation)
        {
            purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();
            purchaseFurniture.Add(Convert.ToInt32(furniture), (furniture, count, sum / count, count));

            APIUser.PostRequest("api/main/updatepurchase", new PurchaseBindingModel
            {
                Id = id,
                UserId = Program.User.Id,
                PurchaseName = purchase,
                DateOfCreation = datecreation,
                PurchaseSum = sum,
                PurchaseFurnitures = purchaseFurniture
            });

            Response.Redirect("../Index");
        }

        [HttpGet]
        public IActionResult AddFurnitureToPurchase(string purchasename)
        {
            ViewBag.PurchaseName = purchasename;
            ViewBag.Furnitures = APIUser.GetRequest<List<FurnitureViewModel>>("api/main/getfurniturelist");
            return View();
        }

        [HttpPost]
        public void AddFurnitureToPurchase(int id, string furniture, int count, decimal sum)
        {
            List<PurchaseViewModel> listPurchase = APIUser.GetRequest<List<PurchaseViewModel>>($"api/main/getpurchase?Id={id}");
            List<PaymentViewModel> listPayment = APIUser.GetRequest<List<PaymentViewModel>>($"api/main/getpayment?PurchaseId={id}");

            if (listPayment[0] != null)
            {
                decimal sumToPaymentPurchase = listPayment.FirstOrDefault(x => x.PurchaseId == id).PaymentSum + sum;
                APIUser.PostRequest("api/main/createpayment", new PaymentBindingModel
                {
                    Id = listPayment.FirstOrDefault(x => x.PurchaseId == id).Id,
                    FurnitureId = listPayment.FirstOrDefault(x => x.PurchaseId == id).FurnitureId,
                    PurchaseId = listPayment.FirstOrDefault(x => x.PurchaseId == id).PurchaseId,
                    PaymentSum = sumToPaymentPurchase,
                });
            }

            purchaseFurniture = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseFurniture;
            if (purchaseFurniture.ContainsKey(Convert.ToInt32(furniture)))
            {
                purchaseFurniture[Convert.ToInt32(furniture)] = (furniture, count + purchaseFurniture[Convert.ToInt32(furniture)].Item2, sum / count, sum + purchaseFurniture[Convert.ToInt32(furniture)].Item4);
            }

            else
            {
                purchaseFurniture.Add(Convert.ToInt32(furniture), (furniture, count, sum / count, sum));
            }

            decimal PurchaseSum = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseSum + sum;
            APIUser.PostRequest("api/main/updatepurchase", new PurchaseBindingModel
            {
                Id = listPurchase.FirstOrDefault(x => x.Id == id).Id,
                UserId = Program.User.Id,
                PurchaseName = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseName,
                DateOfCreation = listPurchase.FirstOrDefault(x => x.Id == id).DateOfCreation,
                PurchaseSum = PurchaseSum,
                PurchaseFurnitures = purchaseFurniture
            });

            Response.Redirect("../Index");
        }

        [HttpGet]
        public IActionResult Pay(int id)
        {
            List<PaymentViewModel> listPayment = APIUser.GetRequest<List<PaymentViewModel>>($"api/main/getpayment?PurchaseId={id}");
            List<PurchaseViewModel> listPurchase = APIUser.GetRequest<List<PurchaseViewModel>>($"api/main/getpurchase?Id={id}");
            List<string> listFurniture = new List<string>();
            purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();
            purchaseFurniture = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseFurniture;

            foreach (var furniture in purchaseFurniture)
            {
                listFurniture.Add($"{furniture.Value.Item1}");
            }
            ViewBag.Purchase = APIUser.GetRequest<PurchaseViewModel>($"api/main/GetPurchaseNL?Id={id}");
            ViewBag.Furniture = listFurniture;
            return View();
        }

        [HttpPost]
        public void Pay(int id, string furniture, decimal sumToPayment, decimal sum)
        {
            List<PurchaseViewModel> listPurchase = APIUser.GetRequest<List<PurchaseViewModel>>($"api/main/getpurchase?Id={id}");
            List<PaymentViewModel> listPayment = APIUser.GetRequest<List<PaymentViewModel>>($"api/main/getpayment?PurchaseId={id}");
            purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();
            purchaseFurniture = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseFurniture;
            int FurnitureId = purchaseFurniture.ElementAt(0).Key;
            if (listPayment[0] == null)
            {
                if (sum >= sumToPayment)
                {
                    decimal sumToPaymentPurchase = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseSum - sumToPayment;
                    APIUser.PostRequest("api/main/createpayment", new PaymentBindingModel
                    {
                        PurchaseId = id,
                        FurnitureId = FurnitureId,
                        PaymentSum = sumToPaymentPurchase,
                        DateOfPayment = DateTime.Now,
                    });
                    Response.Redirect("../Index");
                    return;
                }
                else
                {
                    throw new Exception("Внесённая сумма больше суммы к оплате");
                }
            }

            else
            {
                if (listPayment.FirstOrDefault(x => x.PurchaseId == id).PaymentSum >= sumToPayment)
                {
                    decimal sumToPaymentPurchase = listPayment.FirstOrDefault(x => x.PurchaseId == id).PaymentSum - sumToPayment;
                    APIUser.PostRequest("api/main/createpayment", new PaymentBindingModel
                    {
                        Id = listPayment.FirstOrDefault(x => x.PurchaseId == id).Id,
                        FurnitureId = FurnitureId,
                        PurchaseId = listPayment.FirstOrDefault(x => x.PurchaseId == id).PurchaseId,
                        PaymentSum = sumToPaymentPurchase,
                        DateOfPayment = DateTime.Now,
                    });
                    Response.Redirect("../Index");
                    return;
                }


                else
                {
                    throw new Exception("Внесённая сумма больше суммы к оплате");
                }
            }
        }

        public void Delete(int id)
        {
            APIUser.PostRequest("api/main/deletepurchase", new PurchaseBindingModel { Id = id });
            Response.Redirect("../Index");
        }

        [HttpPost]
        public decimal Calc(decimal count, int furniture)
        {
            FurnitureViewModel fur = APIUser.GetRequest<FurnitureViewModel>($"api/main/getfurniture?furnitureId={furniture}");
            return count * fur.FurniturePrice;
        }

        public decimal CalcTotalSum(string furniture, int id)
        {
            List<PurchaseViewModel> listPurchase = APIUser.GetRequest<List<PurchaseViewModel>>($"api/main/getpurchase?Id={id}");
            purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();
            purchaseFurniture = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseFurniture;
            decimal sumToPayment = new decimal();
            foreach (var furnitures in purchaseFurniture)
            {
                if (furnitures.Value.Item1 != furniture)
                {
                    continue;
                }
                else
                {
                    sumToPayment = furnitures.Value.Item4;
                    break;
                }
            }
            return sumToPayment;
        }

        public decimal ChangeTotalSum(string furniture, int id, decimal sumToPayment)
        {
            List<PurchaseViewModel> listPurchase = APIUser.GetRequest<List<PurchaseViewModel>>($"api/main/getpurchase?Id={id}");
            purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();
            purchaseFurniture = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseFurniture;
            decimal totalSum = new decimal();
            foreach (var furnitures in purchaseFurniture)
            {
                if (furnitures.Value.Item1 == furniture && sumToPayment <= furnitures.Value.Item4)
                {
                    purchaseFurniture[furnitures.Key] = (furnitures.Value.Item1, furnitures.Value.Item2, furnitures.Value.Item3, furnitures.Value.Item4 - sumToPayment);
                    totalSum = furnitures.Value.Item4 - sumToPayment;
                    APIUser.PostRequest("api/main/updatepurchase", new PurchaseBindingModel
                    {
                        Id = listPurchase.FirstOrDefault(x => x.Id == id).Id,
                        UserId = Program.User.Id,
                        PurchaseName = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseName,
                        DateOfCreation = listPurchase.FirstOrDefault(x => x.Id == id).DateOfCreation,
                        PurchaseSum = listPurchase.FirstOrDefault(x => x.Id == id).PurchaseSum,
                        PurchaseFurnitures = purchaseFurniture
                    });
                    break;
                }
                else if (furnitures.Value.Item1 == furniture && sumToPayment > furnitures.Value.Item4)
                {
                    throw new Exception("Внесённая сумма больше суммы к оплате");
                }
                else
                {
                    continue;
                }
            }

            return totalSum;
        }
    }
}
