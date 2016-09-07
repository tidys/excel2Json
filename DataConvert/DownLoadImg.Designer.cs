namespace DataConvert {
    partial class DownLoadImg {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownLoadImg));
            this.downBtn = new System.Windows.Forms.Button();
            this.imgUrlTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.localDirTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.localDirBtn = new System.Windows.Forms.Button();
            this.logListView = new System.Windows.Forms.ListView();
            this.listTitle1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openLocalDirBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(463, 104);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(62, 23);
            this.downBtn.TabIndex = 0;
            this.downBtn.Text = "下载";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // imgUrlTextBox
            // 
            this.imgUrlTextBox.Location = new System.Drawing.Point(12, 106);
            this.imgUrlTextBox.Name = "imgUrlTextBox";
            this.imgUrlTextBox.Size = new System.Drawing.Size(369, 21);
            this.imgUrlTextBox.TabIndex = 1;
            this.imgUrlTextBox.Enter += new System.EventHandler(this.imgUrlTextBox_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "图片网址";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(388, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "遍历";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // localDirTextBox
            // 
            this.localDirTextBox.Location = new System.Drawing.Point(12, 56);
            this.localDirTextBox.Name = "localDirTextBox";
            this.localDirTextBox.ReadOnly = true;
            this.localDirTextBox.Size = new System.Drawing.Size(369, 21);
            this.localDirTextBox.TabIndex = 4;
            this.localDirTextBox.TextChanged += new System.EventHandler(this.localDirTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "存放目录";
            // 
            // localDirBtn
            // 
            this.localDirBtn.Location = new System.Drawing.Point(388, 54);
            this.localDirBtn.Name = "localDirBtn";
            this.localDirBtn.Size = new System.Drawing.Size(49, 23);
            this.localDirBtn.TabIndex = 6;
            this.localDirBtn.Text = "...";
            this.localDirBtn.UseVisualStyleBackColor = true;
            this.localDirBtn.Click += new System.EventHandler(this.localDirBtn_Click);
            // 
            // logListView
            // 
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listTitle1});
            this.logListView.Location = new System.Drawing.Point(0, 261);
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size(525, 159);
            this.logListView.TabIndex = 7;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            // 
            // listTitle1
            // 
            this.listTitle1.Text = "msg";
            this.listTitle1.Width = 507;
            // 
            // openLocalDirBtn
            // 
            this.openLocalDirBtn.Location = new System.Drawing.Point(443, 54);
            this.openLocalDirBtn.Name = "openLocalDirBtn";
            this.openLocalDirBtn.Size = new System.Drawing.Size(75, 23);
            this.openLocalDirBtn.TabIndex = 8;
            this.openLocalDirBtn.Text = "打开目录";
            this.openLocalDirBtn.UseVisualStyleBackColor = true;
            this.openLocalDirBtn.Click += new System.EventHandler(this.openLocalDirBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Alt+Q键 快捷下载 Alt+A 打开下载目录";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // DownLoadImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 418);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.openLocalDirBtn);
            this.Controls.Add(this.logListView);
            this.Controls.Add(this.localDirBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.localDirTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgUrlTextBox);
            this.Controls.Add(this.downBtn);
            this.Name = "DownLoadImg";
            this.Text = "图片下载小助手";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DownLoadImg_FormClosed);
            this.Load += new System.EventHandler(this.DownLoadImg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.TextBox imgUrlTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox localDirTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button localDirBtn;
        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ColumnHeader listTitle1;
        private System.Windows.Forms.Button openLocalDirBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}