using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using System;
using System.Windows.Forms;

namespace FurnitureFactoryView
{
    public partial class FormFurnitureList : Form
    {
        private readonly ReportLogic logic;

        public FormFurnitureList(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormFurnitureList_Load(object sender, EventArgs e)
        {
            try
            {
                //var dict = logic.GetFurniturePurchases(Program.User.Id);

                //if (dict != null)
                //{
                //    dataGridView.Rows.Clear();

                //    foreach (var elem in dict)
                //    {
                //        dataGridView.Rows.Add(new object[] { elem.FurnitureName, "", "" });

                //        foreach (var listElem in elem.Purchases)
                //        {
                //            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                //        }
                //        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                //        dataGridView.Rows.Add(new object[] { });
                //    }
                //}
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //logic.SaveFurnitureInfoToExcelFile(new ReportBindingModel
                        //{
                        //    FileName = dialog.FileName
                        //}, Program.User.Id);
                        //MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonToWord_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                   // logic.SaveFurnitureToWordFile(new ReportBindingModel
                   // {
                   //     FileName = dialog.FileName
                   // }, Program.User.Id);
                   // MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                   //MessageBoxIcon.Information);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
