using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactoryView
{
    public partial class FormBindingCosts : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly CostLogic logicC;
        private readonly PurchaseLogic logicP;
        private int Id { get; set; }
        public decimal Sum { get; set; }

        PurchaseViewModel purchaseView;
        CostViewModel costView;

        public FormBindingCosts(CostLogic logicC, PurchaseLogic logicP)
        {
            InitializeComponent();
            this.logicC = logicC;
            this.logicP = logicP;
        }

        private void FormCost_Load(object sender, EventArgs e)
        {
            try
            {
                var listCosts = logicC.Read(null);
                foreach (var c in listCosts)
                {
                    comboBoxCost.DisplayMember = "CostName";
                    comboBoxCost.ValueMember = "Id";
                    comboBoxCost.DataSource = listCosts;
                    comboBoxCost.SelectedItem = null;
                }

                var listPiurhcases = logicP.Read(null);
                foreach (var f in listPiurhcases)
                {
                    comboBoxFurniture.DisplayMember = "PurchaseName";
                    comboBoxFurniture.ValueMember = "Id";
                    comboBoxFurniture.DataSource = listPiurhcases;
                    comboBoxFurniture.SelectedItem = null;
                }
                labelCostSum.Text = $"{Sum}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCost.SelectedValue == null)
            {
                MessageBox.Show("Выберите затрату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFurniture.SelectedValue == null)
            {
                MessageBox.Show("Выберите мебель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxAdditionalCost.Text))
            {
                MessageBox.Show("Заполните дополнительные затраты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToInt32(textBoxAdditionalCost.Text) < 0)
            {
                MessageBox.Show("Введите неотрицательную сумму дополнительных затрат", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Id = Convert.ToInt32(comboBoxCost.SelectedValue);

                costView = logicC.Read(new CostBindingModel { Id = Id })?[0];

                logicC.CreateOrUpdate(new CostBindingModel
                {
                    Id = costView.Id,
                    UserId = costView.UserId,
                    CostName = costView.CostName,
                    Price = Sum + Convert.ToDecimal(textBoxAdditionalCost.Text)
                });

                Id = Convert.ToInt32(comboBoxFurniture.SelectedValue);

                purchaseView = logicP.Read(new PurchaseBindingModel { Id = Id })?[0];

                logicP.UpdatePurchase(new PurchaseBindingModel
                {
                    Id = purchaseView.Id,
                    UserId = purchaseView.UserId,
                    CostId = costView.Id,
                    PurchaseName = purchaseView.PurchaseName,
                    PurchaseSum = purchaseView.PurchaseSum,
                    DateOfCreation = purchaseView.DateOfCreation,
                    PurchaseFurnitures = purchaseView.PurchaseFurniture
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}