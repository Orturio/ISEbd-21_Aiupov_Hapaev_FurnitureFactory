using System;
using System.Linq;
using System.Collections.Generic;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    public class DiagramLogic
    {
        private readonly PurchaseLogic _purchaseLogic;
        private readonly FurnitureLogic _furnitureLogic;

        private readonly IFurnitureStorage _furnitureStorage;
        private readonly IPurchaseStorage _purchaseStorage;

        public DiagramLogic(PurchaseLogic purchaseLogic, FurnitureLogic furnitureLogic, IPurchaseStorage purchaseStorage, IFurnitureStorage furnitureStorage)
        {
            _purchaseLogic = purchaseLogic;
            _furnitureLogic = furnitureLogic;
            _purchaseStorage = purchaseStorage;
            _furnitureStorage = furnitureStorage;
        }

        public DiagramViewModel GetDiagramByFurnitureCount(int purchaseId)
        {
            return new DiagramViewModel
            {
                Title = "Диаграмма количества мебели в покупках",
                ColumnName = "Покупка",
                ValueName = "Количество покупок",
                Data = _purchaseLogic.Read(new PurchaseBindingModel
                {
                    Id = purchaseId
                }).FirstOrDefault().PurchaseFurniture.Select(x => Tuple.Create( x.Value.Item1, x.Value.Item2)).ToList()
            };
        }

        public DiagramViewModel GetDiagramByFurniturePrice(int purchaseId)
        {
            return new DiagramViewModel
            {
                Title = "Диаграмма стоимости мебели в покупке",
                ColumnName = "Покупка",
                ValueName = "Стоимость покупок",
                Data = _purchaseLogic.Read(new PurchaseBindingModel
                {
                    Id = purchaseId
                }).FirstOrDefault().PurchaseFurniture.Select(x => Tuple.Create(x.Value.Item1, Convert.ToInt32(x.Value.Item4))).ToList()
            };
        }

        public DiagramViewModel GetDiagramByPurchaseCount(int furnitureId)
        {
            var furniture = GetFurnitures(furnitureId);
            return new DiagramViewModel
            {
                Title = "Диаграмма количества покупок в мебели",
                ColumnName = "Мебель",
                ValueName = "Количество мебели",
                Data = furniture.FirstOrDefault().Purchases.Select(x => Tuple.Create(x.Item1, x.Item2)).ToList()
            };
        }

        public DiagramViewModel GetDiagramByPurchasePrice(int furnitureId)
        {
            var furniture = GetFurnitures(furnitureId);
            return new DiagramViewModel
            {
                Title = "Диаграмма стоимости покупок в мебели",
                ColumnName = "Мебель",
                ValueName = "Количество мебели",
                Data = furniture.Select(x => Tuple.Create(x.PurchaseName, Convert.ToInt32(x.FurniturePrice))).ToList()
            };
        }

        private List<ReportPurchaseFurnitureViewModel> GetFurnitures(int furnitureId)
        {
            List<FurnitureViewModel> furnitures = new List<FurnitureViewModel>();
            furnitures.Add(_furnitureStorage.GetElement(new FurnitureBindingModel { Id = furnitureId }));
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
                        record.Purchases.Add(new Tuple<string, int>(purchase.PurchaseName, Convert.ToInt32(purchase.PurchaseSum)));
                        record.TotalCount += purchase.PurchaseFurniture[furniture.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
    }
}
