namespace NetVolveGUI
{
    partial class mainFrm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExRed = new System.Windows.Forms.Button();
            this.btnExBin = new System.Windows.Forms.Button();
            this.lstCode = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnSelectedWarrior = new System.Windows.Forms.Panel();
            this.lblWarriorWins = new System.Windows.Forms.Label();
            this.lblWarriorName = new System.Windows.Forms.Label();
            this.lblWarriorLose = new System.Windows.Forms.Label();
            this.lblWarriorFields = new System.Windows.Forms.Label();
            this.txtWarrior = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTotalWarriors = new System.Windows.Forms.ToolStripLabel();
            this.pnGrid = new NetVolveGUI.DoubleBufferedPanel();
            this.lstRank = new NetVolveGUI.DoubleBufferedListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1019, 533);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.pnGrid);
            this.tabPage1.Controls.Add(this.lstRank);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1011, 507);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Overview";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(504, 391);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(504, 112);
            this.panel3.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.lstCode);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.txtWarrior);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1011, 507);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Warrior";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnExRed);
            this.panel2.Controls.Add(this.btnExBin);
            this.panel2.Location = new System.Drawing.Point(815, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(127, 485);
            this.panel2.TabIndex = 10;
            // 
            // btnExRed
            // 
            this.btnExRed.Location = new System.Drawing.Point(10, 450);
            this.btnExRed.Name = "btnExRed";
            this.btnExRed.Size = new System.Drawing.Size(104, 23);
            this.btnExRed.TabIndex = 1;
            this.btnExRed.Text = "Export .red";
            this.btnExRed.UseVisualStyleBackColor = true;
            this.btnExRed.Click += new System.EventHandler(this.btnExRed_Click);
            // 
            // btnExBin
            // 
            this.btnExBin.Location = new System.Drawing.Point(10, 421);
            this.btnExBin.Name = "btnExBin";
            this.btnExBin.Size = new System.Drawing.Size(104, 23);
            this.btnExBin.TabIndex = 0;
            this.btnExBin.Text = "Export .bin";
            this.btnExBin.UseVisualStyleBackColor = true;
            this.btnExBin.Click += new System.EventHandler(this.btnExBin_Click);
            // 
            // lstCode
            // 
            this.lstCode.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lstCode.FullRowSelect = true;
            this.lstCode.GridLines = true;
            this.lstCode.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstCode.Location = new System.Drawing.Point(385, 11);
            this.lstCode.Name = "lstCode";
            this.lstCode.Size = new System.Drawing.Size(424, 485);
            this.lstCode.TabIndex = 9;
            this.lstCode.UseCompatibleStateImageBehavior = false;
            this.lstCode.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Instructor";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Modifier";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Addrs Mode";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Number";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Addrs Mode";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 80;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Number";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnSelectedWarrior);
            this.panel1.Controls.Add(this.lblWarriorWins);
            this.panel1.Controls.Add(this.lblWarriorName);
            this.panel1.Controls.Add(this.lblWarriorLose);
            this.panel1.Controls.Add(this.lblWarriorFields);
            this.panel1.Location = new System.Drawing.Point(69, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 55);
            this.panel1.TabIndex = 8;
            // 
            // pnSelectedWarrior
            // 
            this.pnSelectedWarrior.BackColor = System.Drawing.Color.White;
            this.pnSelectedWarrior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSelectedWarrior.Location = new System.Drawing.Point(-3, -3);
            this.pnSelectedWarrior.Name = "pnSelectedWarrior";
            this.pnSelectedWarrior.Size = new System.Drawing.Size(56, 58);
            this.pnSelectedWarrior.TabIndex = 0;
            // 
            // lblWarriorWins
            // 
            this.lblWarriorWins.AutoSize = true;
            this.lblWarriorWins.Location = new System.Drawing.Point(61, 31);
            this.lblWarriorWins.Name = "lblWarriorWins";
            this.lblWarriorWins.Size = new System.Drawing.Size(45, 13);
            this.lblWarriorWins.TabIndex = 1;
            this.lblWarriorWins.Text = "Wins: 0";
            // 
            // lblWarriorName
            // 
            this.lblWarriorName.AutoSize = true;
            this.lblWarriorName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarriorName.Location = new System.Drawing.Point(61, 9);
            this.lblWarriorName.Name = "lblWarriorName";
            this.lblWarriorName.Size = new System.Drawing.Size(13, 14);
            this.lblWarriorName.TabIndex = 4;
            this.lblWarriorName.Text = "-";
            // 
            // lblWarriorLose
            // 
            this.lblWarriorLose.AutoSize = true;
            this.lblWarriorLose.Location = new System.Drawing.Point(127, 31);
            this.lblWarriorLose.Name = "lblWarriorLose";
            this.lblWarriorLose.Size = new System.Drawing.Size(42, 13);
            this.lblWarriorLose.TabIndex = 2;
            this.lblWarriorLose.Text = "Lose: 0";
            // 
            // lblWarriorFields
            // 
            this.lblWarriorFields.AutoSize = true;
            this.lblWarriorFields.Location = new System.Drawing.Point(194, 31);
            this.lblWarriorFields.Name = "lblWarriorFields";
            this.lblWarriorFields.Size = new System.Drawing.Size(49, 13);
            this.lblWarriorFields.TabIndex = 3;
            this.lblWarriorFields.Text = "Fields: 0";
            // 
            // txtWarrior
            // 
            this.txtWarrior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWarrior.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWarrior.Location = new System.Drawing.Point(69, 73);
            this.txtWarrior.Multiline = true;
            this.txtWarrior.Name = "txtWarrior";
            this.txtWarrior.Size = new System.Drawing.Size(310, 423);
            this.txtWarrior.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.lblTotalWarriors});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1017, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.loadFromToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // loadFromToolStripMenuItem
            // 
            this.loadFromToolStripMenuItem.Name = "loadFromToolStripMenuItem";
            this.loadFromToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadFromToolStripMenuItem.Text = "Load From...";
            this.loadFromToolStripMenuItem.Click += new System.EventHandler(this.loadFromToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // lblTotalWarriors
            // 
            this.lblTotalWarriors.Name = "lblTotalWarriors";
            this.lblTotalWarriors.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTotalWarriors.Size = new System.Drawing.Size(64, 22);
            this.lblTotalWarriors.Text = "Warriors:";
            // 
            // pnGrid
            // 
            this.pnGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnGrid.Location = new System.Drawing.Point(3, 3);
            this.pnGrid.Name = "pnGrid";
            this.pnGrid.Size = new System.Drawing.Size(500, 500);
            this.pnGrid.TabIndex = 0;
            this.pnGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lstRank
            // 
            this.lstRank.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader13,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstRank.FullRowSelect = true;
            this.lstRank.GridLines = true;
            this.lstRank.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstRank.Location = new System.Drawing.Point(504, 3);
            this.lstRank.Name = "lstRank";
            this.lstRank.Size = new System.Drawing.Size(504, 389);
            this.lstRank.TabIndex = 1;
            this.lstRank.UseCompatibleStateImageBehavior = false;
            this.lstRank.View = System.Windows.Forms.View.Details;
            this.lstRank.DoubleClick += new System.EventHandler(this.lstRank_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "#";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 140;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Author";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader13.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Win";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Lose";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Fields";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1017, 558);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainFrm";
            this.Text = "NetVolve";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainFrm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedPanel pnGrid;
        private DoubleBufferedListView lstRank;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lstCode;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnSelectedWarrior;
        private System.Windows.Forms.Label lblWarriorWins;
        private System.Windows.Forms.Label lblWarriorName;
        private System.Windows.Forms.Label lblWarriorLose;
        private System.Windows.Forms.Label lblWarriorFields;
        private System.Windows.Forms.TextBox txtWarrior;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExRed;
        private System.Windows.Forms.Button btnExBin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel lblTotalWarriors;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.Panel panel3;
    }
}

