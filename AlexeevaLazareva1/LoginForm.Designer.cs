namespace AlexeevaLazareva1
{
    partial class LoginForm
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
            logiblabel = new Label();
            passwordlabel = new Label();
            txtPassword = new TextBox();
            txtLogin = new TextBox();
            loginButton = new Button();
            registerButton = new Button();
            SuspendLayout();
            // 
            // logiblabel
            // 
            logiblabel.AutoSize = true;
            logiblabel.Location = new Point(12, 15);
            logiblabel.Name = "logiblabel";
            logiblabel.Size = new Size(44, 15);
            logiblabel.TabIndex = 0;
            logiblabel.Text = "Логин:";
            // 
            // passwordlabel
            // 
            passwordlabel.AutoSize = true;
            passwordlabel.Location = new Point(12, 44);
            passwordlabel.Name = "passwordlabel";
            passwordlabel.Size = new Size(52, 15);
            passwordlabel.TabIndex = 1;
            passwordlabel.Text = "Пароль:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(70, 41);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(253, 23);
            txtPassword.TabIndex = 2;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(70, 12);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(253, 23);
            txtLogin.TabIndex = 3;
            // 
            // loginButton
            // 
            loginButton.Location = new Point(12, 70);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(75, 23);
            loginButton.TabIndex = 4;
            loginButton.Text = "Войти";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // registerButton
            // 
            registerButton.Location = new Point(233, 70);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(90, 23);
            registerButton.TabIndex = 5;
            registerButton.Text = "Регистрация";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(339, 106);
            Controls.Add(registerButton);
            Controls.Add(loginButton);
            Controls.Add(txtLogin);
            Controls.Add(txtPassword);
            Controls.Add(passwordlabel);
            Controls.Add(logiblabel);
            Name = "LoginForm";
            Text = "Вход";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label logiblabel;
        private Label passwordlabel;
        private TextBox txtPassword;
        private TextBox txtLogin;
        private Button loginButton;
        private Button registerButton;
    }
}