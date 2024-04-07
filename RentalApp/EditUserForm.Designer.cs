namespace RentalApp
{
    partial class EditUserForm
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
            btnUpdate = new Button();
            cbo1 = new ComboBox();
            txtUserName = new TextBox();
            txtPhoneNumber = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            btnBack = new Button();
            btnSearch = new Button();
            txtSearchPhoneNumber = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnUpdate);
            panel1.Controls.Add(cbo1);
            panel1.Controls.Add(txtUserName);
            panel1.Controls.Add(txtPhoneNumber);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnBack);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(txtSearchPhoneNumber);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(2, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1004, 600);
            panel1.TabIndex = 0;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.Cyan;
            btnUpdate.Location = new Point(585, 444);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(116, 44);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // cbo1
            // 
            cbo1.FormattingEnabled = true;
            cbo1.Items.AddRange(new object[] { "user", "admin" });
            cbo1.Location = new Point(510, 361);
            cbo1.Name = "cbo1";
            cbo1.Size = new Size(191, 28);
            cbo1.TabIndex = 15;
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(510, 214);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(191, 27);
            txtUserName.TabIndex = 14;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(510, 283);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(191, 27);
            txtPhoneNumber.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(255, 361);
            label5.Name = "label5";
            label5.Size = new Size(101, 23);
            label5.TabIndex = 12;
            label5.Text = "User Role: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(255, 287);
            label4.Name = "label4";
            label4.Size = new Size(142, 23);
            label4.TabIndex = 11;
            label4.Text = "Phone Nunber: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(255, 218);
            label3.Name = "label3";
            label3.Size = new Size(114, 23);
            label3.TabIndex = 10;
            label3.Text = "User Name: ";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(72, 18);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(105, 44);
            btnBack.TabIndex = 9;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(255, 255, 128);
            btnSearch.Location = new Point(759, 137);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(102, 29);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearchPhoneNumber
            // 
            txtSearchPhoneNumber.Location = new Point(510, 139);
            txtSearchPhoneNumber.Name = "txtSearchPhoneNumber";
            txtSearchPhoneNumber.Size = new Size(191, 27);
            txtSearchPhoneNumber.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(255, 143);
            label2.Name = "label2";
            label2.Size = new Size(244, 23);
            label2.TabIndex = 4;
            label2.Text = "Search By Phone Number :  ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 25.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(406, 18);
            label1.Name = "label1";
            label1.Size = new Size(200, 52);
            label1.TabIndex = 3;
            label1.Text = "Edit User";
            // 
            // EditUserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1007, 607);
            Controls.Add(panel1);
            Name = "EditUserForm";
            Text = "EditUserForm";
            FormClosed += EditUserForm_FormClosed;
            Load += EditUserForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtSearchPhoneNumber;
        private Label label2;
        private Label label1;
        private Button btnSearch;
        private Button btnBack;
        private Label label4;
        private Label label3;
        private TextBox txtUserName;
        private TextBox txtPhoneNumber;
        private Label label5;
        private ComboBox cbo1;
        private Button btnUpdate;
    }
}