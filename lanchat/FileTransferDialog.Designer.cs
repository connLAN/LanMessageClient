namespace LANChat
{
    partial class FileTransferDialog
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
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
            instanceCount--;
            if (instanceCount == 0) {
                if (tcpListener != null)
                    tcpListener.Stop();
                Instance = null;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuFile = new System.Windows.Forms.ContextMenu();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.mnuFileCancel = new System.Windows.Forms.MenuItem();
            this.mnuFileOpenFolder = new System.Windows.Forms.MenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
            this.mnuFileRemove = new System.Windows.Forms.MenuItem();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel = new System.Windows.Forms.Panel();
            this.lblLine = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuFile
            // 
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileOpen,
            this.mnuFileCancel,
            this.mnuFileOpenFolder,
            this.mnuFileSep1,
            this.mnuFileRemove});
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.DefaultItem = true;
            this.mnuFileOpen.Index = 0;
            this.mnuFileOpen.Text = "Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileCancel
            // 
            this.mnuFileCancel.Index = 1;
            this.mnuFileCancel.Text = "Cancel";
            this.mnuFileCancel.Click += new System.EventHandler(this.mnuFileCancel_Click);
            // 
            // mnuFileOpenFolder
            // 
            this.mnuFileOpenFolder.Index = 2;
            this.mnuFileOpenFolder.Text = "Open Containing Folder";
            this.mnuFileOpenFolder.Click += new System.EventHandler(this.mnuFileOpenFolder_Click);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Index = 3;
            this.mnuFileSep1.Text = "-";
            // 
            // mnuFileRemove
            // 
            this.mnuFileRemove.Index = 4;
            this.mnuFileRemove.Text = "Remove From List";
            this.mnuFileRemove.Click += new System.EventHandler(this.mnuFileRemove_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.panel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lblLine, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnClear, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(404, 264);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel.SetColumnSpan(this.panel, 2);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(404, 234);
            this.panel.TabIndex = 1;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel.SetColumnSpan(this.lblLine, 2);
            this.lblLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLine.Location = new System.Drawing.Point(0, 234);
            this.lblLine.Margin = new System.Windows.Forms.Padding(0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(404, 1);
            this.lblLine.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(3, 238);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear List";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FileTransferDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 264);
            this.Controls.Add(this.tableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(385, 275);
            this.Name = "FileTransferDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Transfers";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileTransferDialog_FormClosing);
            this.Load += new System.EventHandler(this.FileTransferDialog_Load);
            this.Resize += new System.EventHandler(this.FileTransferDialog_Resize);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.ContextMenu mnuFile;
        private System.Windows.Forms.MenuItem mnuFileOpen;
        private System.Windows.Forms.MenuItem mnuFileOpenFolder;
        private System.Windows.Forms.MenuItem mnuFileCancel;
        private System.Windows.Forms.MenuItem mnuFileSep1;
        private System.Windows.Forms.MenuItem mnuFileRemove;


    }
}