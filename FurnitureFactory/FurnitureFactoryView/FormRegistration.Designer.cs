namespace FurnitureFactoryView
{
    partial class FormRegistration
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRegister = new System.Windows.Forms.Button();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.comboBoxRole = new System.Windows.Forms.ComboBox();
            this.labelRole = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(23, 186);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(162, 30);
            this.buttonRegister.TabIndex = 3;
            this.buttonRegister.Text = "Зарегистрироваться";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(23, 231);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(162, 42);
            this.buttonEnter.TabIndex = 4;
            this.buttonEnter.Text = "Уже зарегистрирован, войти";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(20, 21);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(38, 13);
            this.labelEmail.TabIndex = 7;
            this.labelEmail.Text = "Email:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(23, 37);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(162, 22);
            this.textBoxEmail.TabIndex = 8;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(21, 71);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(50, 13);
            this.labelPassword.TabIndex = 9;
            this.labelPassword.Text = "Пароль:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(23, 96);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(162, 22);
            this.textBoxPassword.TabIndex = 10;
            // 
            // comboBoxRole
            // 
            this.comboBoxRole.FormattingEnabled = true;
            this.comboBoxRole.Location = new System.Drawing.Point(24, 148);
            this.comboBoxRole.Name = "comboBoxRole";
            this.comboBoxRole.Size = new System.Drawing.Size(161, 21);
            this.comboBoxRole.TabIndex = 11;
            // 
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.Location = new System.Drawing.Point(21, 132);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(35, 13);
            this.labelRole.TabIndex = 12;
            this.labelRole.Text = "Роль:";
            // 
            // FormRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 285);
            this.Controls.Add(this.labelRole);
            this.Controls.Add(this.comboBoxRole);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.buttonRegister);
            this.Name = "FormRegistration";
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.ComboBox comboBoxRole;
        private System.Windows.Forms.Label labelRole;
    }
}