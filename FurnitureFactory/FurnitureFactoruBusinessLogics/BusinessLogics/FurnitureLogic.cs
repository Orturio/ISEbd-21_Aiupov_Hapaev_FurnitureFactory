using System;
using System.Collections.Generic;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    public class FurnitureLogic
    {
        private readonly IFurnitureStorage _furnitureStorage;

        public FurnitureLogic(IFurnitureStorage furnitureStorage)
        {
            _furnitureStorage = furnitureStorage;
        }

        public List<FurnitureViewModel> Read(FurnitureBindingModel model)
        {
            if (model == null)
            {
                return _furnitureStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<FurnitureViewModel> { _furnitureStorage.GetElement(model) };
            }
            return _furnitureStorage.GetFilteredList(model);
        }

        public void CreateFurniture(FurnitureBindingModel model)
        {
            var element = _furnitureStorage.GetElement(new FurnitureBindingModel
            {
                FurnitureName = model.FurnitureName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть мебель с таким названием");
            }
            _furnitureStorage.Insert(model);
        }

        public void UpdateFurniture(FurnitureBindingModel model)
        {

            _furnitureStorage.Update(model);

        }

        public void Delete(FurnitureBindingModel model)
        {
            var element = _furnitureStorage.GetElement(new FurnitureBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Мебель не найдена");
            }
            _furnitureStorage.Delete(model);
        }
    }
}
