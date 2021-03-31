using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.Interfaces
{
    public interface IPurchaseStorage
    {
        List<PurchaseViewModel> GetFullList();

        List<PurchaseViewModel> GetFilteredList(PurchaseBindingModel model);

        PurchaseViewModel GetElement(PurchaseBindingModel model);

        void Insert(PurchaseBindingModel model);

        void Update(PurchaseBindingModel model);

        void Delete(PurchaseBindingModel model);
    }
}
