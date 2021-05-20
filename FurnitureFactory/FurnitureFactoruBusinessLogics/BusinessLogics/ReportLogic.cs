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

        public List<ReportPurchaseFurnitureViewModel> GetPurchasesFurniture(int UserId)
        {
            var furnitures = _furnitureStorage.GetFullList();
            var purchases = _purchaseStorage.GetFullList().Where(x => x.UserId == UserId);
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

        public List<ReportPurchaseFurnitureViewModel> GetFurniturePurchases(int UserId)
        {
            var furnitures = _furnitureStorage.GetFullList();
            var purchases = _purchaseStorage.GetFullList();
            var list = new List<ReportPurchaseFurnitureViewModel>();
            foreach (var furniture in furnitures)
            {
                var record = new ReportPurchaseFurnitureViewModel
                {
                    FurnitureName = furniture.FurnitureName,
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
                Furnitures = GetPurchasesFurniture(model.UserId.Value)
                //Furnitures = _furnitureStorage.GetFilteredList(new FurnitureBindingModel {Id = x.Id})//Не Id, надо PurchaseId в Furniture
            }).ToList();
        }

        public List<ReportFurnitureViewModel> GetFurnitures(ReportBindingModel model, int UserId)
        {
            return _furnitureStorage.GetFilteredList(new FurnitureBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Where(x => x.UserId == UserId).Select(x => new ReportFurnitureViewModel
            {
                DateOfCreation = x.DateOfCreation,
                FurnitureName = x.FurnitureName,
                Material = x.Material,
                FurniturePrice =x.FurniturePrice
            }).ToList();
        }

        public void SavePurchaseToWordFile(ReportBindingModel model, int UserId)
        {
            SaveToWord.CreateDocPurchase(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                Purchases = _purchaseStorage.GetFullList().Where(x => x.UserId == UserId).ToList()
            });
        }

        public void SaveFurnitureToWordFile(ReportBindingModel model, int UserId)
        {
            SaveToWord.CreateDocFurniture(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                Furnitures = _furnitureStorage.GetFullList().Where(x => x.UserId == UserId).ToList()
            });
        }

        public void SavePurchaseInfoToExcelFile(ReportBindingModel model, int UserId)
        {
            SaveToExcel.CreateDocPurchase(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                PurchaseFurnitures = GetPurchasesFurniture(UserId)
            });
        }

        public void SaveFurnitureInfoToExcelFile(ReportBindingModel model, int UserId)
        {
            SaveToExcel.CreateDocFurniture(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                PurchaseFurnitures = GetFurniturePurchases(UserId)
            });
        }

        //public void SavePurchasesToPdfFile(ReportBindingModel model, int UserId)
        //{
        //    SaveToPdf.CreateDocPurchase(new PdfInfo
        //    {
        //        FileName = model.FileName,
        //        Title = "Список покупок",
        //        DateFrom = model.DateFrom.Value,
        //        DateTo = model.DateTo.Value,
        //        Purchases = GetPurchases(model, UserId)
        //    });
        //}

        //public void SaveFurnitureToPdfFile(ReportBindingModel model, int UserId)
        //{
        //    SaveToPdf.CreateDocFurniture(new PdfInfo
        //    {
        //        FileName = model.FileName,
        //        Title = "Список мебели",
        //        DateFrom = model.DateFrom.Value,
        //        DateTo = model.DateTo.Value,
        //        Furnitures = GetFurnitures(model, UserId)
        //    });
        //}
    }
}
