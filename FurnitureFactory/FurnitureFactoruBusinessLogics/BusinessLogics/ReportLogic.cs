using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.HelperModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Collections.Generic;
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
       
        public List<ReportPurchaseViewModel> GetPurchases(ReportBindingModel model)
        {
            return _purchaseStorage.GetFilteredList(new PurchaseBindingModel
            {
                UserId = model.UserId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Select(x => new ReportPurchaseViewModel
            {
                DateOfCreation = x.DateOfCreation,
                PurchaseName = x.PurchaseName,
                PurchaseSum = x.PurchaseSum,
                PurchaseSumToPayment = x.PurchaseSumToPayment,
                Furnitures = _furnitureStorage.GetFilteredList(new FurnitureBindingModel {PurchaseId = x.Id})
            }).ToList();
        }

        public List<ReportPurchaseFurnitureViewModel> GetFurnitures(int UserId)
        {
            var furnitures = _furnitureStorage.GetFullList().Where(x => UserId == x.UserId);
            var purchases = _purchaseStorage.GetFullList().Where(x => UserId == x.UserId);
            var list = new List<ReportPurchaseFurnitureViewModel>();
            foreach (var furniture in furnitures)
            {
                var record = new ReportPurchaseFurnitureViewModel
                {
                    FurnitureName = furniture.FurnitureName,
                    Material = furniture.Material,
                    FurniturePrice = furniture.FurniturePrice,
                    Purchases = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var purchase in purchases)
                {
                    if (purchase.PurchaseFurniture.ContainsKey(furniture.Id))
                    {
                        record.Purchases.Add(new Tuple<string, int>(purchase.PurchaseName,
                        purchase.PurchaseFurniture[furniture.Id].Item2));
                        record.TotalCount += purchase.PurchaseFurniture[furniture.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }       

        public void SavePurchaseToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDocPurchase(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                Purchases = model.PurchaseId.Select(x => _purchaseStorage.GetElement(new PurchaseBindingModel { Id = x })).ToList()
            });
        }

        public void SaveFurnitureToWordFile(ReportBindingModel model, int UserId)
        {
            SaveToWord.CreateDocFurniture(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                Furnitures = GetFurnitures(UserId)
            });
        }

        public void SavePurchaseInfoToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDocPurchase(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                Purchases = model.PurchaseId.Select(x => _purchaseStorage.GetElement(new PurchaseBindingModel { Id = x })).ToList()
            });
        }

        public void SaveFurnitureInfoToExcelFile(ReportBindingModel model, int UserId)
        {
            SaveToExcel.CreateDocFurniture(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                Furnitures = GetFurnitures(UserId)
            });
        }       
    }
}
