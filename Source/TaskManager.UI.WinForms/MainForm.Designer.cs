namespace TaskManager.UI.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox TxtTitle;
        private System.Windows.Forms.Button CmdAdd, CmdDelete, CmdToggle;
        private System.Windows.Forms.ListView LstTasks;
        private System.Windows.Forms.Label LblCounter;
        private System.Windows.Forms.Panel PnlStatus;
        private System.Windows.Forms.DateTimePicker DateDue;
        private System.Windows.Forms.CheckBox ChkDue;

        /// <summary>
        ///     Gibt die von der Form verwendeten Ressourcen frei.
        /// </summary>
        /// <param name="d">True, um verwaltete Ressourcen zu entsorgen; andernfalls false.</param>
        protected override void Dispose(bool d)
        {
            if (d && components != null) components.Dispose();
            base.Dispose(d);
        }

        private void InitializeComponent()
        {
            TxtTitle = new System.Windows.Forms.TextBox();
            CmdAdd = new System.Windows.Forms.Button();
            CmdDelete = new System.Windows.Forms.Button();
            CmdToggle = new System.Windows.Forms.Button();
            LstTasks = new System.Windows.Forms.ListView();
            LblCounter = new System.Windows.Forms.Label();
            PnlStatus = new System.Windows.Forms.Panel();
            DateDue = new System.Windows.Forms.DateTimePicker();
            ChkDue = new System.Windows.Forms.CheckBox();
            TxtTitle.Location = new System.Drawing.Point(12, 12);
            TxtTitle.Size = new System.Drawing.Size(348, 23);
            DateDue.Location = new System.Drawing.Point(12, 41);
            DateDue.Size = new System.Drawing.Size(227, 23);
            ChkDue.AutoSize = true;
            ChkDue.Location = new System.Drawing.Point(245, 45);
            ChkDue.Text = "Due setzen";
            CmdAdd.Location = new System.Drawing.Point(366, 12);
            CmdAdd.Size = new System.Drawing.Size(94, 23);
            CmdAdd.Text = "Add";
            CmdAdd.Click += new System.EventHandler(this.btnAdd_Click);
            CmdDelete.Location = new System.Drawing.Point(366, 70);
            CmdDelete.Size = new System.Drawing.Size(94, 23);
            CmdDelete.Text = "Delete";
            CmdDelete.Click += new System.EventHandler(this.btnDelete_Click);
            CmdToggle.Location = new System.Drawing.Point(366, 99);
            CmdToggle.Size = new System.Drawing.Size(94, 23);
            CmdToggle.Text = "Toggle Done";
            CmdToggle.Click += new System.EventHandler(this.btnToggle_Click);
            LstTasks.CheckBoxes = true;
            LstTasks.View = System.Windows.Forms.View.List;
            LstTasks.Location = new System.Drawing.Point(12, 70);
            LstTasks.Size = new System.Drawing.Size(348, 259);
            LstTasks.DoubleClick += new System.EventHandler(this.listTasks_DoubleClick);
            LstTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listTasks_ItemCheck);
            LblCounter.AutoSize = true;
            LblCounter.Location = new System.Drawing.Point(12, 345);
            LblCounter.Text = "Open: 0";
            PnlStatus.Location = new System.Drawing.Point(366, 345);
            PnlStatus.Size = new System.Drawing.Size(94, 15);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 372);
            this.Controls.Add(PnlStatus);
            this.Controls.Add(LblCounter);
            this.Controls.Add(LstTasks);
            this.Controls.Add(CmdToggle);
            this.Controls.Add(CmdDelete);
            this.Controls.Add(CmdAdd);
            this.Controls.Add(ChkDue);
            this.Controls.Add(DateDue);
            this.Controls.Add(TxtTitle);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Manager (ZbW)";
        }
    }
}