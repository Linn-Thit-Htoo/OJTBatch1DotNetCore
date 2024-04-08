namespace RentalApp
{
    partial class DashboardForm
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
            menuStrip1 = new MenuStrip();
            msHome = new ToolStripMenuItem();
            msUserManagement = new ToolStripMenuItem();
            categoryManagementToolStripMenuItem = new ToolStripMenuItem();
            assetManagementToolStripMenuItem = new ToolStripMenuItem();
            borrowManagementToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { msHome, msUserManagement, categoryManagementToolStripMenuItem, assetManagementToolStripMenuItem, borrowManagementToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1094, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // msHome
            // 
            msHome.Name = "msHome";
            msHome.Size = new Size(64, 24);
            msHome.Text = "Home";
            msHome.Click += msHome_Click;
            // 
            // msUserManagement
            // 
            msUserManagement.Name = "msUserManagement";
            msUserManagement.Size = new Size(144, 24);
            msUserManagement.Text = "User Management";
            msUserManagement.Click += msUserManagement_Click;
            // 
            // categoryManagementToolStripMenuItem
            // 
            categoryManagementToolStripMenuItem.Name = "categoryManagementToolStripMenuItem";
            categoryManagementToolStripMenuItem.Size = new Size(175, 24);
            categoryManagementToolStripMenuItem.Text = "Category Management";
            categoryManagementToolStripMenuItem.Click += categoryManagementToolStripMenuItem_Click;
            // 
            // assetManagementToolStripMenuItem
            // 
            assetManagementToolStripMenuItem.Name = "assetManagementToolStripMenuItem";
            assetManagementToolStripMenuItem.Size = new Size(150, 24);
            assetManagementToolStripMenuItem.Text = "Asset Management";
            // 
            // borrowManagementToolStripMenuItem
            // 
            borrowManagementToolStripMenuItem.Name = "borrowManagementToolStripMenuItem";
            borrowManagementToolStripMenuItem.Size = new Size(163, 24);
            borrowManagementToolStripMenuItem.Text = "Borrow Management";
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1094, 631);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "DashboardForm";
            Text = "DashboardForm";
            FormClosed += DashboardForm_FormClosed;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem msHome;
        private ToolStripMenuItem msUserManagement;
        private ToolStripMenuItem categoryManagementToolStripMenuItem;
        private ToolStripMenuItem assetManagementToolStripMenuItem;
        private ToolStripMenuItem borrowManagementToolStripMenuItem;
    }
}