using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.HelperModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Collections.Generic;
using FurnitureFactoryBusinessLogics.Enums;
using System.Linq;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IFurnitureStorage _furnitureStorage;
        private readonly IPurchaseStorage _purchaseStorage;

        public ReportLogic(IPurchaseStorage purchaseStorage, IFurnitureStorage furnitureStorage)
        {
            _purchaseStorage = purchaseStorage;
            _furnitureStorage = furnitureStorage;
        }

        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>

        public List<ReportPurchaseFurnitureViewModel> GetFurniturePurchases()
        {
            var furnitures = _furnitureStorage.GetFullList();
            var purchases = _purchaseStorage.GetFullList();
            var list = new List<ReportPurchaseFurnitureViewModel>();
            foreach (var purchase in purchases)
            {
                var record = new ReportPurchaseFurnitureViewModel
                {
                    PurchaseName = purchase.PurchaseName,
                    Furnitures = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var furniture in furnitures)
                {
                    if (purchase.PurchaseFurniture.ContainsKey(furniture.Id))
                    {
                        record.Furnitures.Add(new Tuple<string, int>(furniture.FurnitureName,
                        purchase.PurchaseFurniture[furniture.Id].Item2));
                        record.TotalCount += purchase.PurchaseFurniture[furniture.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportPurchaseViewModel> GetPurchases(ReportBindingModel model)
        {
            return _purchaseStorage.GetFilteredList(new PurchaseBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Select(x => new ReportPurchaseViewModel
            {
                DateOfCreation = x.DateOfCreation,
                PurchaseName = x.PurchaseName,
                PurchaseSum = x.PurchaseSum,
                PurchaseSumToPayment = x.PurchaseSumToPayment,
            }).ToList();
        }

        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>

        public void SavePurchaseToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDocPurchase(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                Purchases = _purchaseStorage.GetFullList()
            });
        }

        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>

        public void SavePurchaseInfoToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDocPurchase(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                PurchaseFurnitures = GetFurniturePurchases()
            });
        }
    }
}
