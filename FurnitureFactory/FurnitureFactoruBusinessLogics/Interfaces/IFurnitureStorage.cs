using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.Interfaces
{
    public interface IFurnitureStorage
    {
        List<FurnitureViewModel> GetFullList();

        List<FurnitureViewModel> GetFilteredList(FurnitureBindingModel model);

        FurnitureViewModel GetElement(FurnitureBindingModel model);

        void Insert(FurnitureBindingModel model);

        void Update(FurnitureBindingModel model);

        void Delete(FurnitureBindingModel model);
    }
}
