namespace RentalApp
{
    partial class EditCategoryForm
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
            btnBack = new Button();
            txtCategoryName = new TextBox();
            label2 = new Label();
            panel1 = new Panel();
            btnSave = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.BackColor = SystemColors.ActiveBorder;
            btnBack.Location = new Point(57, 29);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(97, 38);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(470, 165);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(218, 27);
            txtCategoryName.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(263, 165);
            label2.Name = "label2";
            label2.Size = new Size(151, 23);
            label2.TabIndex = 8;
            label2.Text = "Category Name: ";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnBack);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(txtCategoryName);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(-31, -17);
            panel1.Name = "panel1";
            panel1.Size = new Size(945, 540);
            panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(192, 255, 255);
            btnSave.Location = new Point(572, 244);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(116, 34);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 25.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(356, 29);
            label1.Name = "label1";
            label1.Size = new Size(288, 52);
            label1.TabIndex = 7;
            label1.Text = "Edit Category";
            // 
            // EditCategoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 522);
            Controls.Add(panel1);
            Name = "EditCategoryForm";
            Text = "EditCategoryForm";
            FormClosed += EditCategoryForm_FormClosed;
            Load += EditCategoryForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnBack;
        private TextBox txtCategoryName;
        private Label label2;
        private Panel panel1;
        private Button btnSave;
        private Label label1;
    }
}