namespace RentalApp
{
    partial class Form1
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
            panel1 = new Panel();
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtPhoneNumber = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtPhoneNumber);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(5, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(925, 493);
            panel1.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(128, 255, 255);
            btnLogin.Location = new Point(527, 304);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(102, 38);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(443, 232);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(186, 27);
            txtPassword.TabIndex = 4;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(443, 141);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(186, 27);
            txtPhoneNumber.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(274, 144);
            label3.Name = "label3";
            label3.Size = new Size(148, 23);
            label3.TabIndex = 2;
            label3.Text = "Phone Number: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(274, 232);
            label2.Name = "label2";
            label2.Size = new Size(103, 23);
            label2.TabIndex = 1;
            label2.Text = "Password: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 25.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(324, 23);
            label1.Name = "label1";
            label1.Size = new Size(271, 52);
            label1.TabIndex = 0;
            label1.Text = "Admin Login";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 499);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnLogin;
        private TextBox txtPassword;
        private TextBox txtPhoneNumber;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}