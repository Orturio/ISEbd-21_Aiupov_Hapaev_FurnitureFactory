﻿using FurnitureFactoryBusinessLogics.BindingModels;
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

        public List<ReportPurchaseFurnitureViewModel> GetPurchasesFurniture()
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

        public List<ReportPurchaseFurnitureViewModel> GetFurniturePurchases()
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

        public List<ReportFurnitureViewModel> GetFurnitures(ReportBindingModel model)
        {
            return _furnitureStorage.GetFilteredList(new FurnitureBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Select(x => new ReportFurnitureViewModel
            {
                DateOfCreation = x.DateOfCreation,
                FurnitureName = x.FurnitureName,
                Material = x.Material,
                FurniturePrice =x.FurniturePrice
            }).ToList();
        }

        public void SavePurchaseToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDocPurchase(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                Purchases = _purchaseStorage.GetFullList()
            });
        }

        public void SaveFurnitureToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDocFurniture(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                Furnitures = _furnitureStorage.GetFullList()
            });
        }

        public void SavePurchaseInfoToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDocPurchase(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок",
                PurchaseFurnitures = GetPurchasesFurniture()
            });
        }

        public void SaveFurnitureInfoToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDocFurniture(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список мебели",
                PurchaseFurnitures = GetFurniturePurchases()
            });
        }
    }
}