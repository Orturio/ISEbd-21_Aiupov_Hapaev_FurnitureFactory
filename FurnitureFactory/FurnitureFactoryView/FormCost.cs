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
        private readonly CostLogic logic;
        private int? id;

        public FormCost(CostLogic logicC)
        {
            InitializeComponent();
            this.logic = logicC;
        }

        CostViewModel view;

        private void FormCost_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    view = logic.Read(new CostBindingModel { Id = id })?[0];

                    if (view != null)
                    {
                        textBoxCost.Text = view.CostName;
                        textBoxPrice.Text = view.Price.ToString();
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
            if (string.IsNullOrEmpty(textBoxCost.Text))
            {
                MessageBox.Show("Заполните затрату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new CostBindingModel
                {
                    Id = id,
                    UserId = Program.User.Id,
                    CostName = textBoxCost.Text,
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
