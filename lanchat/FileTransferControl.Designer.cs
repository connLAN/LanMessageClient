namespace LANChat
{
    partial class FileTransferControl
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
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new LANMedia.CustomControls.TableLayoutPanelEx();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.picBoxTransfer = new System.Windows.Forms.PictureBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTransfer)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel.Controls.Add(this.lblUserName, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblFileInfo, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.progressBar, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.btnCancel, 3, 4);
            this.tableLayoutPanel.Controls.Add(this.btnAccept, 2, 5);
            this.tableLayoutPanel.Controls.Add(this.btnDecline, 3, 5);
            this.tableLayoutPanel.Controls.Add(this.picBoxTransfer, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lblConfirm, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.lblStatus, 1, 3);
            this.tableLayoutPanel.DefaultColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(199)))));
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.32F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.84F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.84F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(237)))), ((int)(((byte)(173)))));
            this.tableLayoutPanel.Size = new System.Drawing.Size(360, 94);
            this.tableLayoutPanel.TabIndex = 0;
            this.tableLayoutPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileTransferControl_MouseDoubleClick);
            this.tableLayoutPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FileTransferControl_MouseDown);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoEllipsis = true;
            this.tableLayoutPanel.SetColumnSpan(this.lblUserName, 3);
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserName.Location = new System.Drawing.Point(39, 2);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(318, 18);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "<UserName>";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileInfo
            // 
            this.tableLayoutPanel.SetColumnSpan(this.lblFileInfo, 3);
            this.lblFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileInfo.Location = new System.Drawing.Point(39, 20);
            this.lblFileInfo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(318, 15);
            this.lblFileInfo.TabIndex = 3;
            this.lblFileInfo.Text = "<FileInfo>";
            // 
            // progressBar
            // 
            this.tableLayoutPanel.SetColumnSpan(this.progressBar, 2);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(42, 54);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(237, 12);
            this.progressBar.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(282, 50);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 20);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAccept.Location = new System.Drawing.Point(204, 70);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 20);
            this.btnAccept.TabIndex = 8;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDecline.Location = new System.Drawing.Point(282, 70);
            this.btnDecline.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 20);
            this.btnDecline.TabIndex = 9;
            this.btnDecline.Text = "Decline";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // picBoxTransfer
            // 
            this.picBoxTransfer.BackColor = System.Drawing.Color.Transparent;
            this.picBoxTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxTransfer.Location = new System.Drawing.Point(3, 2);
            this.picBoxTransfer.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.picBoxTransfer.Name = "picBoxTransfer";
            this.tableLayoutPanel.SetRowSpan(this.picBoxTransfer, 5);
            this.picBoxTransfer.Size = new System.Drawing.Size(36, 88);
            this.picBoxTransfer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBoxTransfer.TabIndex = 12;
            this.picBoxTransfer.TabStop = false;
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConfirm.Location = new System.Drawing.Point(39, 70);
            this.lblConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(165, 20);
            this.lblConfirm.TabIndex = 13;
            this.lblConfirm.Text = "Do you want to accept the file?";
            this.lblConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.tableLayoutPanel.SetColumnSpan(this.lblStatus, 3);
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(39, 35);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(318, 15);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "<Status>";
            // 
            // FileTransferControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "FileTransferControl";
            this.Size = new System.Drawing.Size(360, 94);
            this.Resize += new System.EventHandler(this.FileTransferControl_Resize);
            this.SystemColorsChanged += new System.EventHandler(this.FileTransferControl_SystemColorsChanged);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTransfer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LANMedia.CustomControls.TableLayoutPanelEx tableLayoutPanel;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox picBoxTransfer;
        private System.Windows.Forms.Label lblConfirm;
    }
}
