using System;
using System.Windows.Forms;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.Enums;
using Unity;

namespace FurnitureFactoryView
{
    public partial class FormAuthorize : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly UserLogic logic;

        public FormAuthorize(UserLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                MessageBox.Show("Заполните Email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var user = logic.Read(new UserBindingModel { Role = UserRole.Сотрудник, Email = textBoxEmail.Text, Password = textBoxPassword.Text })?[0];
            if (user == null)
            {
                MessageBox.Show("Неверный Email или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Program.User = user;
            
            DialogResult = DialogResult.OK;
            Hide();
            var form = Container.Resolve<FormMain>();
            form.ShowDialog();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
