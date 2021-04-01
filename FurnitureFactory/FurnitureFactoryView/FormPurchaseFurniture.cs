using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace FurnitureFactoryView
{
    public partial class FormPurchaseFurniture : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

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

        public FormPurchaseFurniture(FurnitureLogic logic)
        {
            InitializeComponent();

            List<FurnitureViewModel> list = logic.Read(null);

            if (list != null)
            {
                comboBoxFurniture.DisplayMember = "Name";
                comboBoxFurniture.ValueMember = "Id";
                comboBoxFurniture.DataSource = list;
                comboBoxFurniture.SelectedItem = null;
            }
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
