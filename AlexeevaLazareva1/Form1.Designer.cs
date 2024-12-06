namespace AlexeevaLazareva1
{
    partial class MySign
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            search_file_button = new Button();
            file_path = new TextBox();
            label1 = new Label();
            label2 = new Label();
            sign_path = new TextBox();
            search_sign_button = new Button();
            label3 = new Label();
            openkey_path = new TextBox();
            search_openkey_button = new Button();
            sign_button = new Button();
            check_button = new Button();
            log = new TextBox();
            label4 = new Label();
            secretkey_path = new TextBox();
            search_secretkey_button = new Button();
            keysgen_button = new Button();
            lblStatus = new Label();
            openLoginFormButton = new Button();
            SuspendLayout();
            // 
            // search_file_button
            // 
            search_file_button.Location = new Point(266, 75);
            search_file_button.Name = "search_file_button";
            search_file_button.Size = new Size(75, 23);
            search_file_button.TabIndex = 0;
            search_file_button.Text = "Обзор...";
            search_file_button.UseVisualStyleBackColor = true;
            search_file_button.Click += search_file_button_Click;
            // 
            // file_path
            // 
            file_path.Location = new Point(12, 75);
            file_path.Name = "file_path";
            file_path.Size = new Size(248, 23);
            file_path.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 57);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 4;
            label1.Text = "Путь к файлу:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 105);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 7;
            label2.Text = "Путь к подписи:";
            // 
            // sign_path
            // 
            sign_path.Location = new Point(12, 123);
            sign_path.Name = "sign_path";
            sign_path.Size = new Size(248, 23);
            sign_path.TabIndex = 6;
            // 
            // search_sign_button
            // 
            search_sign_button.Location = new Point(266, 123);
            search_sign_button.Name = "search_sign_button";
            search_sign_button.Size = new Size(75, 23);
            search_sign_button.TabIndex = 5;
            search_sign_button.Text = "Обзор...";
            search_sign_button.UseVisualStyleBackColor = true;
            search_sign_button.Click += search_sign_button_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 152);
            label3.Name = "label3";
            label3.Size = new Size(148, 15);
            label3.TabIndex = 10;
            label3.Text = "Путь к открытому ключу:";
            // 
            // openkey_path
            // 
            openkey_path.Location = new Point(12, 170);
            openkey_path.Name = "openkey_path";
            openkey_path.Size = new Size(248, 23);
            openkey_path.TabIndex = 9;
            // 
            // search_openkey_button
            // 
            search_openkey_button.Location = new Point(266, 170);
            search_openkey_button.Name = "search_openkey_button";
            search_openkey_button.Size = new Size(75, 23);
            search_openkey_button.TabIndex = 8;
            search_openkey_button.Text = "Обзор...";
            search_openkey_button.UseVisualStyleBackColor = true;
            search_openkey_button.Click += search_key_button_Click;
            // 
            // sign_button
            // 
            sign_button.Location = new Point(12, 243);
            sign_button.Name = "sign_button";
            sign_button.Size = new Size(75, 23);
            sign_button.TabIndex = 11;
            sign_button.Text = "Подписать";
            sign_button.UseVisualStyleBackColor = true;
            sign_button.Click += sign_button_Click;
            // 
            // check_button
            // 
            check_button.Location = new Point(93, 243);
            check_button.Name = "check_button";
            check_button.Size = new Size(75, 23);
            check_button.TabIndex = 12;
            check_button.Text = "Проверить";
            check_button.UseVisualStyleBackColor = true;
            check_button.Click += check_button_Click;
            // 
            // log
            // 
            log.Location = new Point(12, 288);
            log.Multiline = true;
            log.Name = "log";
            log.ReadOnly = true;
            log.ScrollBars = ScrollBars.Vertical;
            log.Size = new Size(329, 265);
            log.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 196);
            label4.Name = "label4";
            label4.Size = new Size(147, 15);
            label4.TabIndex = 16;
            label4.Text = "Путь к закрытому ключу:";
            // 
            // secretkey_path
            // 
            secretkey_path.Location = new Point(12, 214);
            secretkey_path.Name = "secretkey_path";
            secretkey_path.Size = new Size(248, 23);
            secretkey_path.TabIndex = 15;
            // 
            // search_secretkey_button
            // 
            search_secretkey_button.Location = new Point(266, 214);
            search_secretkey_button.Name = "search_secretkey_button";
            search_secretkey_button.Size = new Size(75, 23);
            search_secretkey_button.TabIndex = 14;
            search_secretkey_button.Text = "Обзор...";
            search_secretkey_button.UseVisualStyleBackColor = true;
            search_secretkey_button.Click += search_secretkey_button_Click;
            // 
            // keysgen_button
            // 
            keysgen_button.Location = new Point(266, 243);
            keysgen_button.Name = "keysgen_button";
            keysgen_button.Size = new Size(75, 39);
            keysgen_button.TabIndex = 17;
            keysgen_button.Text = "Генерация ключей";
            keysgen_button.UseVisualStyleBackColor = true;
            keysgen_button.Click += keysgen_button_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 18;
            lblStatus.Text = "label5";
            // 
            // openLoginFormButton
            // 
            openLoginFormButton.Location = new Point(12, 27);
            openLoginFormButton.Name = "openLoginFormButton";
            openLoginFormButton.Size = new Size(75, 23);
            openLoginFormButton.TabIndex = 19;
            openLoginFormButton.Text = "Войти";
            openLoginFormButton.UseVisualStyleBackColor = true;
            openLoginFormButton.Click += openLoginFormButton_Click;
            // 
            // MySign
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 568);
            Controls.Add(openLoginFormButton);
            Controls.Add(lblStatus);
            Controls.Add(keysgen_button);
            Controls.Add(label4);
            Controls.Add(secretkey_path);
            Controls.Add(search_secretkey_button);
            Controls.Add(log);
            Controls.Add(check_button);
            Controls.Add(sign_button);
            Controls.Add(label3);
            Controls.Add(openkey_path);
            Controls.Add(search_openkey_button);
            Controls.Add(label2);
            Controls.Add(sign_path);
            Controls.Add(search_sign_button);
            Controls.Add(label1);
            Controls.Add(file_path);
            Controls.Add(search_file_button);
            Name = "MySign";
            Text = "Цифровая подпись";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button search_file_button;
        private TextBox file_path;
        private Label label1;
        private Label label2;
        private TextBox sign_path;
        private Button search_sign_button;
        private Label label3;
        private TextBox openkey_path;
        private Button search_openkey_button;
        private Button sign_button;
        private Button check_button;
        private TextBox log;
        private Label label4;
        private TextBox secretkey_path;
        private Button search_secretkey_button;
        private Button keysgen_button;
        private Label lblStatus;
        private Button openLoginFormButton;
    }
}