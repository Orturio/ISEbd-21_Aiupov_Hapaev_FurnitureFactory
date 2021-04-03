namespace FurnitureFactoryView
{
    partial class FormMain
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
            this.labelLogo = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.покупкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мебельToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оплатаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.затратыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетПоПокупкамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетПоМебелиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.покупочныйОтчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мебельныйОтчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLogo
            // 
            this.labelLogo.AutoSize = true;
            this.labelLogo.Location = new System.Drawing.Point(249, 132);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(47, 13);
            this.labelLogo.TabIndex = 5;
            this.labelLogo.Text = "логотип";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.покупкиToolStripMenuItem,
            this.мебельToolStripMenuItem,
            this.оплатаToolStripMenuItem,
            this.затратыToolStripMenuItem,
            this.отчетыToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(697, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // покупкиToolStripMenuItem
            // 
            this.покупкиToolStripMenuItem.Name = "покупкиToolStripMenuItem";
            this.покупкиToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.покупкиToolStripMenuItem.Text = "Покупки";
            this.покупкиToolStripMenuItem.Click += new System.EventHandler(this.покупкиToolStripMenuItem_Click);
            // 
            // мебельToolStripMenuItem
            // 
            this.мебельToolStripMenuItem.Name = "мебельToolStripMenuItem";
            this.мебельToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.мебельToolStripMenuItem.Text = "Мебель";
            this.мебельToolStripMenuItem.Click += new System.EventHandler(this.мебельToolStripMenuItem_Click);
            // 
            // оплатаToolStripMenuItem
            // 
            this.оплатаToolStripMenuItem.Name = "оплатаToolStripMenuItem";
            this.оплатаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.оплатаToolStripMenuItem.Text = "Оплата";
            this.оплатаToolStripMenuItem.Click += new System.EventHandler(this.оплатаToolStripMenuItem_Click);
            // 
            // затратыToolStripMenuItem
            // 
            this.затратыToolStripMenuItem.Name = "затратыToolStripMenuItem";
            this.затратыToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.затратыToolStripMenuItem.Text = "Затраты";
            this.затратыToolStripMenuItem.Click += new System.EventHandler(this.затратыToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отчетПоПокупкамToolStripMenuItem,
            this.отчетПоМебелиToolStripMenuItem,
            this.покупочныйОтчетToolStripMenuItem,
            this.мебельныйОтчетToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // отчетПоПокупкамToolStripMenuItem
            // 
            this.отчетПоПокупкамToolStripMenuItem.Name = "отчетПоПокупкамToolStripMenuItem";
            this.отчетПоПокупкамToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.отчетПоПокупкамToolStripMenuItem.Text = "Отчет по покупкам";
            this.отчетПоПокупкамToolStripMenuItem.Click += new System.EventHandler(this.отчетПоПокупкамToolStripMenuItem_Click_1);
            // 
            // отчетПоМебелиToolStripMenuItem
            // 
            this.отчетПоМебелиToolStripMenuItem.Name = "отчетПоМебелиToolStripMenuItem";
            this.отчетПоМебелиToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.отчетПоМебелиToolStripMenuItem.Text = "Отчет по мебели";
            this.отчетПоМебелиToolStripMenuItem.Click += new System.EventHandler(this.отчетПоМебелиToolStripMenuItem_Click);
            // 
            // покупочныйОтчетToolStripMenuItem
            // 
            this.покупочныйОтчетToolStripMenuItem.Name = "покупочныйОтчетToolStripMenuItem";
            this.покупочныйОтчетToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.покупочныйОтчетToolStripMenuItem.Text = "Покупочный отчет";
            this.покупочныйОтчетToolStripMenuItem.Click += new System.EventHandler(this.покупочныйОтчетToolStripMenuItem_Click);
            // 
            // мебельныйОтчетToolStripMenuItem
            // 
            this.мебельныйОтчетToolStripMenuItem.Name = "мебельныйОтчетToolStripMenuItem";
            this.мебельныйОтчетToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.мебельныйОтчетToolStripMenuItem.Text = "Мебельный отчет";
            this.мебельныйОтчетToolStripMenuItem.Click += new System.EventHandler(this.мебельныйОтчетToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 303);
            this.Controls.Add(this.labelLogo);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "Главная форма";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLogo;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem покупкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мебельToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оплатаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem затратыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетПоПокупкамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетПоМебелиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem покупочныйОтчетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мебельныйОтчетToolStripMenuItem;
    }
}

