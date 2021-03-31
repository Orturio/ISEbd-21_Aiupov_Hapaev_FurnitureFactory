using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.Interfaces
{
    public interface ICostsStorage
    {
        List<CostsViewModel> GetFullList();

        List<CostsViewModel> GetFilteredList(CostsBindingModel model);

        CostsViewModel GetElement(CostsBindingModel model);

        void Insert(CostsBindingModel model);

        void Update(CostsBindingModel model);

        void Delete(CostsBindingModel model);
    }
}
