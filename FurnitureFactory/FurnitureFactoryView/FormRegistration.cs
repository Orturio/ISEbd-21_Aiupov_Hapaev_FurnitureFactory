﻿using System;
using System.Windows.Forms;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.Enums;
using Unity;

namespace FurnitureFactoryView
{
    public partial class FormRegistration : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly UserLogic logic;

        public FormRegistration(UserLogic logic)
        {
            InitializeComponent();
            comboBoxRole.Items.AddRange(new string[] { "Клиент", "Сотрудник"});
            this.logic = logic;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                MessageBox.Show("Заполните Email", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Заполните пароль", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                UserBindingModel model = new UserBindingModel
                {
                    Role = ((UserRole)Enum.Parse(typeof(UserRole), comboBoxRole.SelectedItem.ToString())),
                    Email = textBoxEmail.Text,
                    Password = textBoxPassword.Text
                };
                logic.CreateOrUpdate(model);
                Program.User = logic.Read(model)?[0];
                MessageBox.Show("Успешная регистрация", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                var form = Container.Resolve<FormAuthorize>();
                form.ShowDialog();
                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAuthorize>();
            form.ShowDialog();
        }
    }
}
