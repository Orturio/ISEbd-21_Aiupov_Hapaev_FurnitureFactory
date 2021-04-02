using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureFactoryView
{
    public partial class FormPayment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly PurchaseLogic _logicP;

        private decimal DifferenceOfNumbers { get; set; }

        private int Id { get; set; }

        private Dictionary<int, (string, int, decimal)> purchaseFurniture;

        List<PurchaseViewModel> listPayment;

        PurchaseViewModel view;

        public FormPayment(PurchaseLogic logicP)
        {
            InitializeComponent();
            _logicP = logicP;
        }

        private void FormPayment_Load(object sender, EventArgs e)
        {
            try
            {
                listPayment = _logicP.Read(null);
                foreach (var item in listPayment)
                {
                    comboBoxPurchase.DisplayMember = "PurchaseName";
                    comboBoxPurchase.ValueMember = "Id";
                    comboBoxPurchase.DataSource = listPayment;
                    comboBoxPurchase.SelectedItem = null;
                }
                textBoxTotalSum.Text = null;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ComboBoxPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(comboBoxPurchase.SelectedValue);
            if (comboBoxPurchase.SelectedValue != null && Id != 0)
            {
                textBoxTotalSum.Text = listPayment.FirstOrDefault(x => x.Id == Id).PurchaseSumToPayment.ToString();
                view = _logicP.Read(new PurchaseBindingModel { Id = Id })?[0];

                if (view != null)
                {
                    purchaseFurniture = view.PurchaseFurniture;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSum.Text))
            {
                MessageBox.Show("Заполните поле Внесённая сумма", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxTotalSum.Text))
            {
                MessageBox.Show("Заполните поле Сумма к оплате", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxPurchase.SelectedValue == null)
            {
                MessageBox.Show("Выберите покупку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (Convert.ToDecimal(textBoxTotalSum.Text) >= Convert.ToDecimal(textBoxSum.Text))
                {
                    DifferenceOfNumbers = Convert.ToDecimal(textBoxTotalSum.Text) - Convert.ToDecimal(textBoxSum.Text);
                    if (listPayment != null)
                    {
                        _logicP.UpdatePurchase(new PurchaseBindingModel
                        {
                            Id = view.Id,
                            PurchaseName = view.PurchaseName,
                            PurchaseSum = view.PurchaseSum,
                            PurchaseSumToPayment = DifferenceOfNumbers,
                            DateOfCreation = view.DateOfCreation,
                            DateOfPayment = DateTime.Now,
                            PurchaseFurnitures = view.PurchaseFurniture
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Внесённая сумма больше необходимой", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (listPayment != null)
                    {
                        _logicP.UpdatePurchase(new PurchaseBindingModel
                        {
                            Id = view.Id,
                            PurchaseName = view.PurchaseName,
                            PurchaseSum = view.PurchaseSum,
                            PurchaseSumToPayment = view.PurchaseSumToPayment,
                            DateOfCreation = view.DateOfCreation,
                            DateOfPayment = DateTime.Now,
                            PurchaseFurnitures = view.PurchaseFurniture
                        });
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
