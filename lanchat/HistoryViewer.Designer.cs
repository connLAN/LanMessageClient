namespace LANChat
{
    partial class HistoryViewer
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
            if (instanceCount == 0)
                Instance = null;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lvMessages = new LANMedia.CustomControls.ListViewEx();
            this.colHdrName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHdrDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanelHistory = new System.Windows.Forms.TableLayoutPanel();
            this.rtbMessageHistory = new System.Windows.Forms.RichTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTip = new LANMedia.CustomControls.LabelEx();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanelHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.splitContainer, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lblTip, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(584, 414);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.btnClose);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 382);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(578, 29);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(500, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 33);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lvMessages);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanelHistory);
            this.splitContainer.Size = new System.Drawing.Size(578, 343);
            this.splitContainer.SplitterDistance = 187;
            this.splitContainer.TabIndex = 0;
            // 
            // lvMessages
            // 
            this.lvMessages.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvMessages.AllowColumnResize = false;
            this.lvMessages.AlwaysShowHeader = true;
            this.lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHdrName,
            this.colHdrDate});
            this.lvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMessages.EmptyText = "There are no items to display.";
            this.lvMessages.HeaderColor = System.Drawing.Color.Lavender;
            this.lvMessages.HighLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvMessages.Location = new System.Drawing.Point(0, 0);
            this.lvMessages.MultiSelect = false;
            this.lvMessages.Name = "lvMessages";
            this.lvMessages.OwnerDraw = true;
            this.lvMessages.SelectedColor = System.Drawing.Color.Khaki;
            this.lvMessages.Size = new System.Drawing.Size(187, 343);
            this.lvMessages.TabIndex = 0;
            this.lvMessages.UseCompatibleStateImageBehavior = false;
            this.lvMessages.View = System.Windows.Forms.View.Tile;
            this.lvMessages.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMessages_ColumnClick);
            this.lvMessages.SelectedIndexChanged += new System.EventHandler(this.lvMessages_SelectedIndexChanged);
            this.lvMessages.Resize += new System.EventHandler(this.lvMessages_Resize);
            // 
            // colHdrName
            // 
            this.colHdrName.Text = "Name";
            // 
            // colHdrDate
            // 
            this.colHdrDate.Text = "Date";
            // 
            // tableLayoutPanelHistory
            // 
            this.tableLayoutPanelHistory.ColumnCount = 1;
            this.tableLayoutPanelHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHistory.Controls.Add(this.rtbMessageHistory, 0, 2);
            this.tableLayoutPanelHistory.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanelHistory.Controls.Add(this.lblDate, 0, 1);
            this.tableLayoutPanelHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelHistory.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelHistory.Name = "tableLayoutPanelHistory";
            this.tableLayoutPanelHistory.RowCount = 3;
            this.tableLayoutPanelHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanelHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanelHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHistory.Size = new System.Drawing.Size(387, 343);
            this.tableLayoutPanelHistory.TabIndex = 0;
            // 
            // rtbMessageHistory
            // 
            this.rtbMessageHistory.BackColor = System.Drawing.SystemColors.Window;
            this.rtbMessageHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessageHistory.Location = new System.Drawing.Point(0, 30);
            this.rtbMessageHistory.Margin = new System.Windows.Forms.Padding(0);
            this.rtbMessageHistory.Name = "rtbMessageHistory";
            this.rtbMessageHistory.ReadOnly = true;
            this.rtbMessageHistory.Size = new System.Drawing.Size(387, 313);
            this.rtbMessageHistory.TabIndex = 0;
            this.rtbMessageHistory.TabStop = false;
            this.rtbMessageHistory.Text = "";
            this.rtbMessageHistory.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbMessageHistory_KeyUp);
            this.rtbMessageHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtbMessageHistory_MouseDown);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(387, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "No message selected";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDate.Location = new System.Drawing.Point(0, 15);
            this.lblDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(387, 15);
            this.lblDate.TabIndex = 3;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.BackColor = System.Drawing.SystemColors.Info;
            this.lblTip.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.lblTip.BorderStyle = LANMedia.CustomControls.LabelStyle.Standard;
            this.lblTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTip.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblTip.Image = null;
            this.lblTip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTip.Location = new System.Drawing.Point(3, 3);
            this.lblTip.Margin = new System.Windows.Forms.Padding(3);
            this.lblTip.Name = "lblTip";
            this.lblTip.SelectedColor = System.Drawing.SystemColors.Highlight;
            this.lblTip.Size = new System.Drawing.Size(578, 24);
            this.lblTip.TabIndex = 1;
            this.lblTip.Text = "To change history settings go to the Messages tab in Options.";
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HistoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(584, 414);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "HistoryViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History Viewer";
            this.Load += new System.EventHandler(this.HistoryViewer_Load);
            this.Resize += new System.EventHandler(this.HistoryViewer_Resize);
            this.tableLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanelHistory.ResumeLayout(false);
            this.tableLayoutPanelHistory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer;
        private LANMedia.CustomControls.ListViewEx lvMessages;
        private System.Windows.Forms.ColumnHeader colHdrName;
        private System.Windows.Forms.ColumnHeader colHdrDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHistory;
        private System.Windows.Forms.RichTextBox rtbMessageHistory;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDate;
        private LANMedia.CustomControls.LabelEx lblTip;
    }
}