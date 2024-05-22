namespace RentalApp
{
    partial class CreateCategoryForm
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
            btnBack = new Button();
            btnSave = new Button();
            txtCategoryName = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnBack);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(txtCategoryName);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(-1, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(976, 557);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnBack
            // 
            btnBack.BackColor = SystemColors.ActiveBorder;
            btnBack.Location = new Point(45, 34);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(97, 38);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(192, 255, 255);
            btnSave.Location = new Point(593, 267);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(116, 34);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(491, 173);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(218, 27);
            txtCategoryName.TabIndex = 9;
            txtCategoryName.TextChanged += txtCategoryName_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(284, 173);
            label2.Name = "label2";
            label2.Size = new Size(151, 23);
            label2.TabIndex = 8;
            label2.Text = "Category Name: ";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 25.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(333, 18);
            label1.Name = "label1";
            label1.Size = new Size(339, 52);
            label1.TabIndex = 7;
            label1.Text = "Create Category";
            label1.Click += label1_Click;
            // 
            // CreateCategoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 561);
            Controls.Add(panel1);
            Name = "CreateCategoryForm";
            Text = "CreateCategoryForm";
            FormClosed += CreateCategoryForm_FormClosed;
            Load += CreateCategoryForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnSave;
        private TextBox txtCategoryName;
        private Label label2;
        private Label label1;
        private Button btnBack;
    }
}