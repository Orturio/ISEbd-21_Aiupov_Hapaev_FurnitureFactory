using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.Interfaces
{
    public interface ICostStorage
    {
        List<CostViewModel> GetFullList();

        List<CostViewModel> GetFilteredList(CostBindingModel model);

        CostViewModel GetElement(CostBindingModel model);

        void Insert(CostBindingModel model);

        void Update(CostBindingModel model);

        void Delete(CostBindingModel model);
    }
}
