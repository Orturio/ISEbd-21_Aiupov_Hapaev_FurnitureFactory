using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactoryView
{
    public partial class FormPurchase : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly PurchaseLogic logicPurchase;

        private readonly PaymentLogic logicPayment;

        private int? id;

        private decimal Price { get; set; }

        private Dictionary<int, (string, int, decimal, decimal)> purchaseFurniture;

        public FormPurchase(PurchaseLogic logicPurchase, PaymentLogic logicPayment)
        {
            InitializeComponent();
            this.logicPurchase = logicPurchase;
            this.logicPayment = logicPayment;
        }

        PurchaseViewModel viewPurchase;

        PaymentViewModel viewPayment;

        private void FormPurchase_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    viewPurchase = logicPurchase.Read(new PurchaseBindingModel { Id = id.Value })?[0];
                    viewPayment = logicPayment.Read(new PaymentBindingModel { PurchaseId = id.Value })?[0];

                    if (viewPurchase != null)
                    {
                        textBoxName.Text = viewPurchase.PurchaseName;
                        textBoxPrice.Text = viewPurchase.PurchaseSum.ToString();
                        purchaseFurniture = viewPurchase.PurchaseFurniture;
                        LoadData();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                purchaseFurniture = new Dictionary<int, (string, int, decimal, decimal)>();
            }
        }



        private void LoadData()
        {
            try
            {
                if (purchaseFurniture != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in purchaseFurniture)
                    {
                        Price += pc.Value.Item3 * pc.Value.Item2;
                        textBoxPrice.Text = Price.ToString();
                        dataGridView.Rows.Add(new object[] {pc.Key, pc.Value.Item1, pc.Value.Item2, pc.Value.Item3});
                    }
                    Price = 0;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPurchaseFurniture>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (purchaseFurniture.ContainsKey(form.Id))
                {
                    purchaseFurniture[form.Id] = (form.FurnitureName, form.Count, form.Price, form.Price * form.Count);
                }

                else
                {
                    purchaseFurniture.Add(form.Id, (form.FurnitureName, form.Count, form.Price, form.Price * form.Count));
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormPurchaseFurniture>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = purchaseFurniture[id].Item2;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    purchaseFurniture[form.Id] = (form.FurnitureName, form.Count, form.Price, form.Price * form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        purchaseFurniture.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (purchaseFurniture == null || purchaseFurniture.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {               
                if (viewPurchase != null)
                {
                    logicPurchase.UpdatePurchase(new PurchaseBindingModel
                    {
                        Id = viewPurchase.Id,
                        UserId = Program.User.Id,
                        PurchaseName = textBoxName.Text,
                        PurchaseSum = Convert.ToDecimal(textBoxPrice.Text),
                        DateOfCreation = viewPurchase.DateOfCreation,
                        PurchaseFurnitures = purchaseFurniture,
                    });
                }
                else
                {                    
                    logicPurchase.CreatePurchase(new PurchaseBindingModel
                    {
                        Id = id,
                        UserId = Program.User.Id,
                        PurchaseName = textBoxName.Text,
                        PurchaseSum = Convert.ToDecimal(textBoxPrice.Text),
                        DateOfCreation = DateTime.Now,
                        PurchaseFurnitures = purchaseFurniture,
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
