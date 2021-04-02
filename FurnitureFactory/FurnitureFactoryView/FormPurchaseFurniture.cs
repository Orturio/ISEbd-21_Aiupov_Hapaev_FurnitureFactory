using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using System.Linq;

namespace FurnitureFactoryView
{
    public partial class FormPurchaseFurniture : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        List<FurnitureViewModel> list;

        public int Id
        {
            get { return Convert.ToInt32(comboBoxFurniture.SelectedValue); }
            set { comboBoxFurniture.SelectedValue = value; }
        }

        public string FurnitureName { get { return comboBoxFurniture.Text; } }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        public decimal Price
        {
            get { return Convert.ToDecimal(textBoxPrice.Text); }

            set {textBoxPrice.Text = value.ToString() ; }
        }

        public FormPurchaseFurniture(FurnitureLogic logic)
        {
            InitializeComponent();

            list = logic.Read(null);

            if (list != null)
            {
                comboBoxFurniture.DisplayMember = "FurnitureName";
                comboBoxFurniture.ValueMember = "Id";
                comboBoxFurniture.DataSource = list;
                comboBoxFurniture.SelectedItem = null;
            }
        }

        private void ComboBoxFurniture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                textBoxPrice.Text = list.FirstOrDefault(x => x.Id == Id).FurniturePrice.ToString();
            }
            Id = Convert.ToInt32(comboBoxFurniture.SelectedValue);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxFurniture.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
