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
        public int Id { set { id = value; } }
        private readonly CostLogic logicC;
        private readonly FurnitureLogic logicF;
        private int? id;
        public string purchaseName { get; set; }
        public int count { get; set; }
        public decimal sum { get; set; }

        public FormBindingCosts(CostLogic logicC, FurnitureLogic logicF)
        {
            InitializeComponent();
            this.logicC = logicC;
            this.logicF = logicF;
        }

        private void FormCost_Load(object sender, EventArgs e)
        {
            try
            {
                var listCosts = logicC.Read(null);
                foreach (var c in listCosts)
                {
                    comboBoxCost.DisplayMember = "PurchaseName";
                    comboBoxCost.ValueMember = "Id";
                    comboBoxCost.DataSource = listCosts;
                    comboBoxCost.SelectedItem = null;
                }

                var listFurnitures = logicF.Read(null);
                foreach (var f in listFurnitures)
                {
                    comboBoxCost.DisplayMember = "FurnitureName";
                    comboBoxCost.ValueMember = "Id";
                    comboBoxCost.DataSource = listFurnitures;
                    comboBoxCost.SelectedItem = null;
                }
                labelCostSum.Text = $"{sum}";
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
            //if (comboBoxFurniture.SelectedValue == null)
            //{
            //    MessageBox.Show("Выберите мебель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            if (string.IsNullOrEmpty(textBoxAdditionalCost.Text))
            {
                MessageBox.Show("Заполните дополнительные затраты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicC.CreateOrUpdate(new CostBindingModel
                {
                    Id = id,
                    PurchaseName = purchaseName,
                    Count = count,
                    Price = sum + Convert.ToDecimal(textBoxAdditionalCost.Text)
                });
                //logicF.CreateOrUpdate(new FurnitureBindingModel
                //{
                //    Id = id,
                //    PurchaseName = comboBoxPurchase.Text,
                //    Count = Convert.ToInt32(textBoxCount.Text),
                //    Price = Convert.ToDecimal(textBoxPrice.Text)
                //});
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (DbUpdateException exe)
            {
                MessageBox.Show(exe?.InnerException?.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
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