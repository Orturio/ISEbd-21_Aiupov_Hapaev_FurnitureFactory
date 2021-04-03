
namespace FurnitureFactoryView
{
    partial class FormPurchasesList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonToWord = new System.Windows.Forms.Button();
            this.buttonToExcel = new System.Windows.Forms.Button();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFurnitureName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPurchaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(525, 339);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 35);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonToWord
            // 
            this.buttonToWord.Location = new System.Drawing.Point(12, 339);
            this.buttonToWord.Name = "buttonToWord";
            this.buttonToWord.Size = new System.Drawing.Size(102, 35);
            this.buttonToWord.TabIndex = 8;
            this.buttonToWord.Text = "Word";
            this.buttonToWord.UseVisualStyleBackColor = true;
            this.buttonToWord.Click += new System.EventHandler(this.buttonToWord_Click);
            // 
            // buttonToExcel
            // 
            this.buttonToExcel.Location = new System.Drawing.Point(120, 339);
            this.buttonToExcel.Name = "buttonToExcel";
            this.buttonToExcel.Size = new System.Drawing.Size(102, 35);
            this.buttonToExcel.TabIndex = 9;
            this.buttonToExcel.Text = "Excel";
            this.buttonToExcel.UseVisualStyleBackColor = true;
            this.buttonToExcel.Click += new System.EventHandler(this.buttonToExcel_Click);
            // 
            // ColumnCount
            // 
            this.ColumnCount.HeaderText = "Количество";
            this.ColumnCount.Name = "ColumnCount";
            this.ColumnCount.Width = 140;
            // 
            // ColumnFurnitureName
            // 
            this.ColumnFurnitureName.HeaderText = "Мебель";
            this.ColumnFurnitureName.Name = "ColumnFurnitureName";
            this.ColumnFurnitureName.Width = 206;
            // 
            // ColumnPurchaseName
            // 
            this.ColumnPurchaseName.HeaderText = "Покупка";
            this.ColumnPurchaseName.Name = "ColumnPurchaseName";
            this.ColumnPurchaseName.Width = 250;
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPurchaseName,
            this.ColumnFurnitureName,
            this.ColumnCount});
            this.dataGridView.Location = new System.Drawing.Point(1, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(639, 333);
            this.dataGridView.TabIndex = 10;
            // 
            // FormPurchasesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 386);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonToExcel);
            this.Controls.Add(this.buttonToWord);
            this.Controls.Add(this.buttonCancel);
            this.Name = "FormPurchasesList";
            this.Text = "Список покупок";
            this.Load += new System.EventHandler(this.FormPurchasesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonToWord;
        private System.Windows.Forms.Button buttonToExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFurnitureName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPurchaseName;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}