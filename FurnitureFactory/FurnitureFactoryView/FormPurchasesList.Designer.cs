
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonToWord = new System.Windows.Forms.Button();
            this.buttonToExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 129);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбранная мебель";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(213, 153);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonToWord
            // 
            this.buttonToWord.Location = new System.Drawing.Point(12, 153);
            this.buttonToWord.Name = "buttonToWord";
            this.buttonToWord.Size = new System.Drawing.Size(75, 23);
            this.buttonToWord.TabIndex = 8;
            this.buttonToWord.Text = "Word";
            this.buttonToWord.UseVisualStyleBackColor = true;
            // 
            // buttonToExcel
            // 
            this.buttonToExcel.Location = new System.Drawing.Point(116, 153);
            this.buttonToExcel.Name = "buttonToExcel";
            this.buttonToExcel.Size = new System.Drawing.Size(75, 23);
            this.buttonToExcel.TabIndex = 9;
            this.buttonToExcel.Text = "Excel";
            this.buttonToExcel.UseVisualStyleBackColor = true;
            // 
            // FormPurchasesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 187);
            this.Controls.Add(this.buttonToExcel);
            this.Controls.Add(this.buttonToWord);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label1);
            this.Name = "FormPurchasesList";
            this.Text = "Список покупок";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonToWord;
        private System.Windows.Forms.Button buttonToExcel;
    }
}