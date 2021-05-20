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

        public List<ReportPurchaseFurnitureViewModel> GetFurnitures(ReportBindingModel model)
        {
            List<FurnitureViewModel> furnitures = new List<FurnitureViewModel>();
            foreach (var id in model.FurnitureId)
            {
                furnitures.Add(_furnitureStorage.GetElement(new FurnitureBindingModel { Id = id }));
            }
            var purchases = _purchaseStorage.GetFullList();
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

        public void SaveFurnitureToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDocFurniture(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                Furnitures = GetFurnitures(model)
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

        public void SaveFurnitureInfoToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDocFurniture(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                Furnitures = GetFurnitures(model)
            });
        }

        public void SavePurchaseToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDocPurchase(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Purchases = _purchaseStorage.GetFilteredList(new PurchaseBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo, UserId = model.UserId})
            });
        }

        public void SaveFurnitureToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDocFurniture(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Furnitures = _furnitureStorage.GetFilteredList(new FurnitureBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo, UserId = model.UserId })
            });
        }
    }
}
