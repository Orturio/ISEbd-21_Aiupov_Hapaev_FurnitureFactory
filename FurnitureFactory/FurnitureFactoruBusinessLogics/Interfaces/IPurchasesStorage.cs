using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.Interfaces
{
    public interface IPurchasesStorage
    {
        List<PurchasesViewModel> GetFullList();

        List<PurchasesViewModel> GetFilteredList(PurchasesBindingModel model);

        PurchasesViewModel GetElement(PurchasesBindingModel model);

        void Insert(PurchasesBindingModel model);

        void Update(PurchasesBindingModel model);

        void Delete(PurchasesBindingModel model);
    }
}
