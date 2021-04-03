using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using System;
using FurnitureFactoryBusinessLogics.ViewModels;
using System.Windows.Forms;
using Unity;
using System.Collections.Generic;

namespace FurnitureFactoryView
{
    public partial class FormMain : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void покупкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPurchases>();
            form.ShowDialog();
        }

        private void мебельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormFurnitures>();
            form.ShowDialog();
        }

        private void оплатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPayment>();
            form.ShowDialog();
        }

        private void затратыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCosts>();
            form.ShowDialog();
        }

        private void отчетПоПокупкамToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportPurchases>();
            form.ShowDialog();
        }

        private void отчетПоМебелиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportFurnitures>();
            form.ShowDialog();
        }

        private void покупочныйОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPurchasesList>();
            form.ShowDialog();
        }

        private void мебельныйОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormFurnitureList>();
            form.ShowDialog();
        }
    }
}
