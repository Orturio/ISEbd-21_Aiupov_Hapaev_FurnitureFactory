using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;
using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;

namespace FurnitureFactoryView
{
    public partial class FormReportFurnitures : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportFurnitures(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormReportFurniture_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod", "c " + dateTimePickerFrom.Value.ToShortDateString() + " по " +
                    dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                var dataSource = logic.GetFurnitures(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value
                }, Program.User.Id);
                ReportDataSource source = new ReportDataSource("DataSetFurniture", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonToEmail_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveFurnitureToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
                        }, Program.User.Id);
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
