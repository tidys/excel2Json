namespace DataConvert {
    partial class CutImg {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button4 = new System.Windows.Forms.Button();
            this.imagePathLabel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.selectSavaPthBtn = new System.Windows.Forms.Button();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.txbFname = new System.Windows.Forms.TextBox();
            this.txbWidth = new System.Windows.Forms.TextBox();
            this.txbHeight = new System.Windows.Forms.TextBox();
            this.logListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cutImageBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择图片";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "瓦片宽";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "输出文件夹";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "瓦片高";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "文件名";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(542, 27);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Right;
            this.button4.Location = new System.Drawing.Point(571, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 27);
            this.button4.TabIndex = 2;
            this.button4.Text = "X";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.cleanProcessBtnClick);
            // 
            // imagePathLabel
            // 
            this.imagePathLabel.AllowDrop = true;
            this.imagePathLabel.Location = new System.Drawing.Point(83, 9);
            this.imagePathLabel.Name = "imagePathLabel";
            this.imagePathLabel.Size = new System.Drawing.Size(459, 21);
            this.imagePathLabel.TabIndex = 3;
            this.imagePathLabel.DragDrop += new System.Windows.Forms.DragEventHandler(this.imagePathDragDrop);
            this.imagePathLabel.DragEnter += new System.Windows.Forms.DragEventHandler(this.imagePathDragEnter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(559, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.openFileBtnClick);
            // 
            // selectSavaPthBtn
            // 
            this.selectSavaPthBtn.Location = new System.Drawing.Point(559, 42);
            this.selectSavaPthBtn.Name = "selectSavaPthBtn";
            this.selectSavaPthBtn.Size = new System.Drawing.Size(75, 23);
            this.selectSavaPthBtn.TabIndex = 1;
            this.selectSavaPthBtn.Text = "...";
            this.selectSavaPthBtn.UseVisualStyleBackColor = true;
            this.selectSavaPthBtn.Click += new System.EventHandler(this.selectSavePathBtn_Click);
            // 
            // txtOutPut
            // 
            this.txtOutPut.Location = new System.Drawing.Point(83, 39);
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.Size = new System.Drawing.Size(459, 21);
            this.txtOutPut.TabIndex = 6;
            // 
            // txbFname
            // 
            this.txbFname.Location = new System.Drawing.Point(83, 99);
            this.txbFname.Name = "txbFname";
            this.txbFname.Size = new System.Drawing.Size(404, 21);
            this.txbFname.TabIndex = 3;
            // 
            // txbWidth
            // 
            this.txbWidth.Location = new System.Drawing.Point(83, 68);
            this.txbWidth.Name = "txbWidth";
            this.txbWidth.Size = new System.Drawing.Size(130, 21);
            this.txbWidth.TabIndex = 8;
            this.txbWidth.Text = "100";
            // 
            // txbHeight
            // 
            this.txbHeight.Location = new System.Drawing.Point(300, 68);
            this.txbHeight.Name = "txbHeight";
            this.txbHeight.Size = new System.Drawing.Size(130, 21);
            this.txbHeight.TabIndex = 4;
            this.txbHeight.Text = "100";
            // 
            // logListView
            // 
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.logListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logListView.FullRowSelect = true;
            this.logListView.GridLines = true;
            this.logListView.Location = new System.Drawing.Point(0, 27);
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size(646, 205);
            this.logListView.TabIndex = 2;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "时间";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "内容";
            this.columnHeader2.Width = 474;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cutImageBtn
            // 
            this.cutImageBtn.Location = new System.Drawing.Point(559, 97);
            this.cutImageBtn.Name = "cutImageBtn";
            this.cutImageBtn.Size = new System.Drawing.Size(75, 23);
            this.cutImageBtn.TabIndex = 0;
            this.cutImageBtn.Text = "开始切割";
            this.cutImageBtn.UseVisualStyleBackColor = true;
            this.cutImageBtn.Click += new System.EventHandler(this.cutImgBtnClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.logListView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 232);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txbFname);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cutImageBtn);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txbHeight);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txbWidth);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtOutPut);
            this.panel2.Controls.Add(this.imagePathLabel);
            this.panel2.Controls.Add(this.selectSavaPthBtn);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(646, 141);
            this.panel2.TabIndex = 1;
            // 
            // CutImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 379);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CutImg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CutImg";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CutImg_FormClosed);
            this.Load += new System.EventHandler(this.CutImg_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox imagePathLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button selectSavaPthBtn;
        private System.Windows.Forms.TextBox txtOutPut;
        private System.Windows.Forms.TextBox txbFname;
        private System.Windows.Forms.TextBox txbWidth;
        private System.Windows.Forms.TextBox txbHeight;
        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cutImageBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}