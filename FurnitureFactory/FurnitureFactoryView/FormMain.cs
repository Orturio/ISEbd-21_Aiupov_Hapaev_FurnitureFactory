using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using System;
using FurnitureFactoryBusinessLogics.ViewModels;
using FurnitureFactoryBusinessLogics.Enums;
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
            if (Program.User.Role == (UserRole)0)
            {
                var form = Container.Resolve<FormPurchases>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Совершать покупки может только клиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void мебельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.User.Role == (UserRole)1)
            {
                var form = Container.Resolve<FormFurnitures>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Работать с мебелью может только сотрудник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void оплатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.User.Role == (UserRole)0)
            {
                var form = Container.Resolve<FormPayment>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Совершать оплату может только клиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void затратыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.User.Role == (UserRole)1)
            {
                var form = Container.Resolve<FormCosts>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Формировать затраты может только сотрудник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void отчетПоПокупкамToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Program.User.Role == (UserRole)0)
            {
                var form = Container.Resolve<FormReportPurchases>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Получать отчет по покупкам может только клиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void отчетПоМебелиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.User.Role == (UserRole)1)
            {
                var form = Container.Resolve<FormReportFurnitures>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Получать отчет по мебели может только сотрудник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void отчетПоПокупкамЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.User.Role == (UserRole)0)
            {
                var form = Container.Resolve<FormPurchasesList>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Получать отчет по покупкам за период может только клиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void отчетПоМебелиЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.User.Role == (UserRole)1)
            {
                var form = Container.Resolve<FormFurnitureList>();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Получать отчет по мебели за период может только сотрудник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
