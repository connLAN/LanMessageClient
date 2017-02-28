namespace LANChat
{
    partial class BroadcastViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCloseMessage = new System.Windows.Forms.Label();
            this.lblMessageCaption = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel.Controls.Add(this.btnClose, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.lblCloseMessage, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.lblMessageCaption, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtMessage, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(312, 192);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(237, 169);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblCloseMessage
            // 
            this.lblCloseMessage.AutoSize = true;
            this.lblCloseMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCloseMessage.Location = new System.Drawing.Point(0, 166);
            this.lblCloseMessage.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCloseMessage.Name = "lblCloseMessage";
            this.lblCloseMessage.Size = new System.Drawing.Size(231, 26);
            this.lblCloseMessage.TabIndex = 1;
            this.lblCloseMessage.Text = "This window will close in ";
            this.lblCloseMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMessageCaption
            // 
            this.lblMessageCaption.AutoSize = true;
            this.lblMessageCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessageCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageCaption.Location = new System.Drawing.Point(0, 0);
            this.lblMessageCaption.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblMessageCaption.Name = "lblMessageCaption";
            this.lblMessageCaption.Size = new System.Drawing.Size(231, 21);
            this.lblMessageCaption.TabIndex = 2;
            this.lblMessageCaption.Text = "Message Text:";
            this.lblMessageCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMessage
            // 
            this.tableLayoutPanel.SetColumnSpan(this.txtMessage, 2);
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(3, 27);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(306, 136);
            this.txtMessage.TabIndex = 3;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // BroadcastViewer
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(324, 204);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BroadcastViewer";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Broadcast";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.BroadcastDialog_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblCloseMessage;
        private System.Windows.Forms.Label lblMessageCaption;
        private System.Windows.Forms.TextBox txtMessage;
    }
}