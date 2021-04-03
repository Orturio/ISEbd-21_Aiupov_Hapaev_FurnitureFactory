using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactoryView
{
    public partial class FormFurniture : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly FurnitureLogic logic;
        private int? id;

        public FormFurniture(FurnitureLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        FurnitureViewModel view;

        private void FormFurniture_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    view = logic.Read(new FurnitureBindingModel { Id = id})?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.FurnitureName;
                        textBoxMaterial.Text = view.Material;
                        textBoxPrice.Text = view.FurniturePrice.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMaterial.Text))
            {
                MessageBox.Show("Заполните материал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (view != null)
                {
                    logic.UpdateFurniture(new FurnitureBindingModel
                    {
                        Id = id,
                        FurnitureName = textBoxName.Text,
                        Material = textBoxMaterial.Text,
                        FurniturePrice = Convert.ToDecimal(textBoxPrice.Text),
                        DateOfCreation = view.DateOfCreation
                    });
                }
                else
                {
                    logic.CreateFurniture(new FurnitureBindingModel
                    {
                        Id = id,
                        FurnitureName = textBoxName.Text,
                        Material = textBoxMaterial.Text,
                        FurniturePrice = Convert.ToDecimal(textBoxPrice.Text),
                        DateOfCreation = DateTime.Now,
                    });
                }
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
