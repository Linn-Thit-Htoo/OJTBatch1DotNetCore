namespace RentalApp
{
    partial class CreateNewUserForm
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
            panel1 = new Panel();
            btnCreate = new Button();
            txtPhoneNumber = new TextBox();
            txtUserName = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnBack = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnBack);
            panel1.Controls.Add(btnCreate);
            panel1.Controls.Add(txtPhoneNumber);
            panel1.Controls.Add(txtUserName);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1061, 575);
            panel1.TabIndex = 0;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.Cyan;
            btnCreate.Location = new Point(629, 301);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(116, 44);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(541, 226);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(204, 27);
            txtPhoneNumber.TabIndex = 6;
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(541, 141);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(204, 27);
            txtUserName.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(346, 230);
            label3.Name = "label3";
            label3.Size = new Size(148, 23);
            label3.TabIndex = 4;
            label3.Text = "Phone Number: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(346, 145);
            label2.Name = "label2";
            label2.Size = new Size(114, 23);
            label2.TabIndex = 3;
            label2.Text = "User Name: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 25.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(404, 9);
            label1.Name = "label1";
            label1.Size = new Size(251, 52);
            label1.TabIndex = 2;
            label1.Text = "Create User";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(67, 22);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(105, 44);
            btnBack.TabIndex = 8;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // CreateNewUserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1061, 576);
            Controls.Add(panel1);
            Name = "CreateNewUserForm";
            Text = "CreateNewUserForm";
            FormClosed += CreateNewUserForm_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TextBox txtPhoneNumber;
        private TextBox txtUserName;
        private Label label3;
        private Label label2;
        private Button btnCreate;
        private Button btnBack;
    }
}