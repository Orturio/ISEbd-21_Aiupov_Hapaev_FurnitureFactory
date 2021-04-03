using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.Interfaces;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryDatabaseImplement.Implements;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace FurnitureFactoryView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ICostStorage, CostsStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFurnitureStorage, FurnitureStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPaymentStorage, PaymentStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPurchaseStorage, PurchaseStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IUserStorage, UserStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<CostLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<FurnitureLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PaymentLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PurchaseLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<UserLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
