
namespace FurnitureFactoryView
{
    partial class FormBindingCosts
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
            this.comboBoxCost = new System.Windows.Forms.ComboBox();
            this.comboBoxFurniture = new System.Windows.Forms.ComboBox();
            this.labelCost = new System.Windows.Forms.Label();
            this.labelAdditionalCost = new System.Windows.Forms.Label();
            this.textBoxAdditionalCost = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxCost
            // 
            this.comboBoxCost.FormattingEnabled = true;
            this.comboBoxCost.Location = new System.Drawing.Point(12, 21);
            this.comboBoxCost.Name = "comboBoxCost";
            this.comboBoxCost.Size = new System.Drawing.Size(227, 21);
            this.comboBoxCost.TabIndex = 0;
            this.comboBoxCost.Text = "Выбранная статья затрат";
            // 
            // comboBoxFurniture
            // 
            this.comboBoxFurniture.FormattingEnabled = true;
            this.comboBoxFurniture.Location = new System.Drawing.Point(12, 60);
            this.comboBoxFurniture.Name = "comboBoxFurniture";
            this.comboBoxFurniture.Size = new System.Drawing.Size(227, 21);
            this.comboBoxFurniture.TabIndex = 1;
            this.comboBoxFurniture.Text = "Выбранная мебель";
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(9, 98);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(146, 13);
            this.labelCost.TabIndex = 3;
            this.labelCost.Text = "Имеющаяся сумма затрат:";
            // 
            // labelAdditionalCost
            // 
            this.labelAdditionalCost.AutoSize = true;
            this.labelAdditionalCost.Location = new System.Drawing.Point(9, 131);
            this.labelAdditionalCost.Name = "labelAdditionalCost";
            this.labelAdditionalCost.Size = new System.Drawing.Size(148, 13);
            this.labelAdditionalCost.TabIndex = 4;
            this.labelAdditionalCost.Text = "Дополнительные затраты:";
            // 
            // textBoxAdditionalCost
            // 
            this.textBoxAdditionalCost.Location = new System.Drawing.Point(163, 128);
            this.textBoxAdditionalCost.Name = "textBoxAdditionalCost";
            this.textBoxAdditionalCost.Size = new System.Drawing.Size(76, 22);
            this.textBoxAdditionalCost.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(60, 167);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(151, 167);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(161, 95);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(76, 22);
            this.textBoxCost.TabIndex = 8;
            // 
            // FormBindingCosts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 209);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxAdditionalCost);
            this.Controls.Add(this.labelAdditionalCost);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.comboBoxFurniture);
            this.Controls.Add(this.comboBoxCost);
            this.Name = "FormBindingCosts";
            this.Text = "Привязка затрат";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCost;
        private System.Windows.Forms.ComboBox comboBoxFurniture;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Label labelAdditionalCost;
        private System.Windows.Forms.TextBox textBoxAdditionalCost;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxCost;
    }
}