namespace LANChat
{
    partial class BroadcastSender
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblMessageCaption = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.tvUsers = new LANMedia.CustomControls.TreeViewEx();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRecipients = new System.Windows.Forms.Label();
            this.cboRecipients = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 5;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel.Controls.Add(this.lblMessageCaption, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtMessage, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tvUsers, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.btnSend, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.btnCancel, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.lblRecipients, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.cboRecipients, 4, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(482, 192);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // lblMessageCaption
            // 
            this.lblMessageCaption.AutoSize = true;
            this.lblMessageCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessageCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageCaption.Location = new System.Drawing.Point(0, 0);
            this.lblMessageCaption.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblMessageCaption.Name = "lblMessageCaption";
            this.lblMessageCaption.Size = new System.Drawing.Size(152, 21);
            this.lblMessageCaption.TabIndex = 0;
            this.lblMessageCaption.Text = "Message Text:";
            this.lblMessageCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMessage
            // 
            this.tableLayoutPanel.SetColumnSpan(this.txtMessage, 3);
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(3, 27);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(308, 136);
            this.txtMessage.TabIndex = 1;
            // 
            // tvUsers
            // 
            this.tvUsers.CheckBoxes = true;
            this.tableLayoutPanel.SetColumnSpan(this.tvUsers, 2);
            this.tvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUsers.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvUsers.HeaderColor = System.Drawing.Color.Lavender;
            this.tvUsers.HeaderLevel = 1;
            this.tvUsers.HighlightColor = System.Drawing.Color.LemonChiffon;
            this.tvUsers.Indent = 8;
            this.tvUsers.Location = new System.Drawing.Point(317, 27);
            this.tvUsers.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.tvUsers.Name = "tvUsers";
            this.tableLayoutPanel.SetRowSpan(this.tvUsers, 2);
            this.tvUsers.SelectedColor = System.Drawing.Color.PaleGoldenrod;
            this.tvUsers.Size = new System.Drawing.Size(165, 165);
            this.tvUsers.TabIndex = 4;
            this.tvUsers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvUsers_AfterCheck);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(158, 169);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(236, 169);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRecipients
            // 
            this.lblRecipients.AutoSize = true;
            this.lblRecipients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipients.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecipients.Location = new System.Drawing.Point(317, 0);
            this.lblRecipients.Margin = new System.Windows.Forms.Padding(0);
            this.lblRecipients.Name = "lblRecipients";
            this.lblRecipients.Size = new System.Drawing.Size(75, 21);
            this.lblRecipients.TabIndex = 2;
            this.lblRecipients.Text = "Recipients:";
            this.lblRecipients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboRecipients
            // 
            this.cboRecipients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRecipients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRecipients.FormattingEnabled = true;
            this.cboRecipients.Items.AddRange(new object[] {
            "Select All",
            "Select None"});
            this.cboRecipients.Location = new System.Drawing.Point(392, 0);
            this.cboRecipients.Margin = new System.Windows.Forms.Padding(0);
            this.cboRecipients.Name = "cboRecipients";
            this.cboRecipients.Size = new System.Drawing.Size(90, 21);
            this.cboRecipients.TabIndex = 3;
            this.cboRecipients.SelectedIndexChanged += new System.EventHandler(this.cboRecipients_SelectedIndexChanged);
            // 
            // BroadcastSender
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 204);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BroadcastSender";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Broadcast Message";
            this.Load += new System.EventHandler(this.BroadcastSender_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblMessageCaption;
        private System.Windows.Forms.TextBox txtMessage;
        private LANMedia.CustomControls.TreeViewEx tvUsers;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRecipients;
        private System.Windows.Forms.ComboBox cboRecipients;
    }
}