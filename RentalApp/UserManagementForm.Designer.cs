namespace RentalApp
{
    partial class UserManagementForm
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
            btnEdit = new Button();
            btnCreate = new Button();
            dgv1 = new DataGridView();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnCreate);
            panel1.Controls.Add(dgv1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(53, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(1077, 606);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(192, 255, 255);
            btnEdit.Location = new Point(13, 66);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(131, 41);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Edit User";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(192, 192, 255);
            btnCreate.Location = new Point(932, 66);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(131, 41);
            btnCreate.TabIndex = 3;
            btnCreate.Text = "Create New User";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // dgv1
            // 
            dgv1.AllowUserToAddRows = false;
            dgv1.AllowUserToDeleteRows = false;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv1.Location = new Point(13, 126);
            dgv1.Name = "dgv1";
            dgv1.ReadOnly = true;
            dgv1.RowHeadersWidth = 51;
            dgv1.RowTemplate.Height = 29;
            dgv1.Size = new Size(1050, 467);
            dgv1.TabIndex = 2;
            dgv1.CellContentClick += dgv1_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 25.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(360, 7);
            label1.Name = "label1";
            label1.Size = new Size(383, 52);
            label1.TabIndex = 1;
            label1.Text = "User Management";
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 653);
            Controls.Add(panel1);
            MinimumSize = new Size(1200, 700);
            Name = "UserManagementForm";
            Text = "UserManagementForm";
            FormClosed += UserManagementForm_FormClosed;
            Load += UserManagementForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dgv1;
        private Label label1;
        private Button btnCreate;
        private Button btnEdit;
    }
}