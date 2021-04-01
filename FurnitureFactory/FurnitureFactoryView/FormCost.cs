using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactoryView
{
    public partial class FormCost : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly CostLogic logicC;
        private readonly PurchaseLogic logicP;
        private int? id;

        public FormCost(CostLogic logicC, PurchaseLogic logicP)
        {
            InitializeComponent();
            this.logicC = logicC;
            this.logicP = logicP;
        }

        private void FormCost_Load(object sender, EventArgs e)
        {
            try
            {
                var list = logicP.Read(null);
                foreach (var p in list)
                {
                    comboBoxPurchase.DisplayMember = "Name";
                    comboBoxPurchase.ValueMember = "Id";
                    comboBoxPurchase.DataSource = list;
                    comboBoxPurchase.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);             
            }
        }

        private void CalcSum()
        {
            if (comboBoxPurchase.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxPurchase.SelectedValue);
                    PurchaseViewModel purchase = logicP.Read(new PurchaseBindingModel { Id = id })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxPrice.Text = (count * purchase?.Sum ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPurchase.SelectedValue == null)
            {
                MessageBox.Show("Выберите покупку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicC.CreateOrUpdate(new CostBindingModel
                {
                    Id = id,
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Price = Convert.ToDecimal(textBoxPrice.Text)
                });
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
