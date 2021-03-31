using System;
using System.Collections.Generic;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    public class CostLogic
    {
        private readonly ICostStorage _costsStorage;

        public CostLogic(ICostStorage costsStorage)
        {
            _costsStorage = costsStorage;
        }

        public List<CostViewModel> Read(CostBindingModel model)
        {
            if (model == null)
            {
                return _costsStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CostViewModel> { _costsStorage.GetElement(model) };
            }
            return _costsStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CostBindingModel model)
        {
            var element = _costsStorage.GetElement(new CostBindingModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такие затраты");
            }
            if (model.Id.HasValue)
            {
                _costsStorage.Update(model);
            }
            else
            {
                _costsStorage.Insert(model);
            }
        }
        public void Delete(CostBindingModel model)
        {
            var element = _costsStorage.GetElement(new CostBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Затраты не найдены");
            }
            _costsStorage.Delete(model);
        }
    }
}
