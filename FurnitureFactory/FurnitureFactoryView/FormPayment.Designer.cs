namespace FurnitureFactoryView
{
    partial class FormPayment
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
            this.comboBoxPurchase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTotalSum = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.comboBoxFurniture = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxPurchase
            // 
            this.comboBoxPurchase.FormattingEnabled = true;
            this.comboBoxPurchase.Location = new System.Drawing.Point(25, 12);
            this.comboBoxPurchase.Name = "comboBoxPurchase";
            this.comboBoxPurchase.Size = new System.Drawing.Size(232, 21);
            this.comboBoxPurchase.TabIndex = 0;
            this.comboBoxPurchase.Text = "Выбранная покупка";
            this.comboBoxPurchase.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPurchase_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сумма к оплате";
            // 
            // textBoxTotalSum
            // 
            this.textBoxTotalSum.Location = new System.Drawing.Point(116, 153);
            this.textBoxTotalSum.Name = "textBoxTotalSum";
            this.textBoxTotalSum.Size = new System.Drawing.Size(141, 20);
            this.textBoxTotalSum.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(25, 179);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(108, 24);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(139, 179);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(118, 24);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Внесённая сумма:";
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(121, 109);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.Size = new System.Drawing.Size(136, 20);
            this.textBoxSum.TabIndex = 6;
            // 
            // comboBoxFurniture
            // 
            this.comboBoxFurniture.FormattingEnabled = true;
            this.comboBoxFurniture.Location = new System.Drawing.Point(25, 60);
            this.comboBoxFurniture.Name = "comboBoxFurniture";
            this.comboBoxFurniture.Size = new System.Drawing.Size(232, 21);
            this.comboBoxFurniture.TabIndex = 7;
            this.comboBoxFurniture.Text = "Выбранная мебель";
            this.comboBoxFurniture.SelectedIndexChanged += new System.EventHandler(this.comboBoxFurniture_SelectedIndexChanged);
            // 
            // FormPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 215);
            this.Controls.Add(this.comboBoxFurniture);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxTotalSum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPurchase);
            this.Name = "FormPayment";
            this.Text = "Оплата покупки";
            this.Load += new System.EventHandler(this.FormPayment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPurchase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTotalSum;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.ComboBox comboBoxFurniture;
    }
}