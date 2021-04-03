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
        private readonly FurnitureLogic logicF;
        private int Id { get; set; }
        public decimal Sum { get; set; }

        FurnitureViewModel furnitureView;
        CostViewModel costView;

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
                    comboBoxFurniture.DisplayMember = "FurnitureName";
                    comboBoxFurniture.ValueMember = "Id";
                    comboBoxFurniture.DataSource = listFurnitures;
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
            try
            {
                Id = Convert.ToInt32(comboBoxCost.SelectedValue);

                costView = logicC.Read(new CostBindingModel { Id = Id })?[0];

                logicC.CreateOrUpdate(new CostBindingModel
                {
                    Id = costView.Id,
                    PurchaseName = costView.PurchaseName,
                    Count = costView.Count,
                    Price = Sum + Convert.ToDecimal(textBoxAdditionalCost.Text)
                });

                Id = Convert.ToInt32(comboBoxFurniture.SelectedValue);
                
                furnitureView = logicF.Read(new FurnitureBindingModel { Id = Id })?[0];
                
                logicF.UpdateFurniture(new FurnitureBindingModel
                {
                    Id = furnitureView.Id,
                    CostsId = costView.Id,
                    FurnitureName = furnitureView.FurnitureName,
                    Material = furnitureView.Material,
                    FurniturePrice = furnitureView.FurniturePrice
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