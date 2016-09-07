using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using SHDocVw;
using System.Runtime.InteropServices;

namespace DataConvert {
    public partial class DownLoadImg : Form {
        public static bool isShow = false;
        public static DownLoadImg instance = null;
        class Win32API {
            [DllImport("User32.dll")]
            public static extern bool PtInRect(ref Rectangle r, Point p);
            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);
        }

        public DownLoadImg() {
            InitializeComponent();
            DownLoadImg.isShow = true;
            DownLoadImg.instance = this;
        }

        private void DownLoadImg_Load(object sender, EventArgs e) {
            this.initHotKey();
            string dir = AppCfg.getItem(AppCfg.localImgDir);
            if (dir != null) {
                this.localDirTextBox.Text = dir;
            }
        }
        #region 注册热键
        //注册热键的api
        [DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);
        //解除注册热键的api
        [DllImport("user32")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private void initHotKey() {
            /*             
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。  
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Shift, Keys.S);  
            //注册热键Ctrl+B，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。  
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.B);  
            //注册热键Ctrl+Alt+D，Id号为102。HotKey.KeyModifiers.Alt也可以直接使用数字1来表示。  
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Alt | HotKey.KeyModifiers.Ctrl, Keys.D);  
            //注册热键F5，Id号为103。  
            HotKey.RegisterHotKey(Handle, 103, HotKey.KeyModifiers.None, Keys.F5);  
             */


            //注册热键 (窗体句柄,热键ID,辅助键,实键) 
            //辅助键说明: None = 0,   Alt = 1,  crtl= 2,  Shift = 4,   Windows = 8
            //如果有多个辅助键则,例如 alt+crtl是3 直接相加就可以了
            RegisterHotKey(this.Handle, 123, 1, Keys.Q);
        }

        private void DownLoadImg_FormClosed(object sender, FormClosedEventArgs e) {
            UnregisterHotKey(this.Handle, 123);
            DownLoadImg.isShow = false;
            DownLoadImg.instance = null;
        }
        private bool isPressHotKey = false;
        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case 0x0312:  //这个是window消息定义的注册的热键消息  
                    if (m.WParam.ToString() == "123")   // 按下CTRL+Q隐藏  
                    {
                        this.isPressHotKey = true;
                        // 将剪切板的内容复制出来
                        this.imgUrlTextBox_Enter(null, null);
                        // 下载
                        this.downBtn_Click(null, null);
                        this.isPressHotKey = false;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion



        private void addLog(string str) {
            this.logListView.Items.Add(str);

            // 滚动到底部
            int index = this.logListView.Items.Count;
            this.logListView.Items[index - 1].EnsureVisible();


            if (this.isPressHotKey == true) {
                this.showNotifity(str);
            }

        }
        private void showNotifity(string str) {
            this.notifyIcon1.Text = str;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(1000, "提示", str, ToolTipIcon.Info);
        }
        private void downBtn_Click(object sender, EventArgs e) {
            string url = this.imgUrlTextBox.Text;
            if (url.Length > 0) {
                int pos = url.IndexOf("http");
                if (pos < 0) {
                    this.addLog("不是合法的网址" + url);
                } else {
                    string dir = AppCfg.getItem(AppCfg.localImgDir);
                    if (dir != null) {
                        string[] urlArray = url.Split('/');
                        string fileName = urlArray[urlArray.Length - 1];
                        string[] fileArr = fileName.Split('.');
                        string fileNamefile = fileArr[0];
                        string fileExt = fileArr[1];
                        string path = dir + "\\" + fileName;
                        try {
                            WebClient client = new WebClient();
                            client.DownloadFile(url, path);
                            this.addLog("文件下载完成: " + fileName); ;
                        } catch (Exception ex) {
                            this.addLog("文件下载出错: " + fileName);

                        }
                    } else {
                        this.addLog("请选择文件存放目录");
                    }
                }
            } else {
                this.addLog("请输入下载地址");
            }
        }

        private void button1_Click(object sender, EventArgs e) {

        }

        private void localDirBtn_Click(object sender, EventArgs e) {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "请选择图片存放文件夹";
            if (dlg.ShowDialog() == DialogResult.OK) {
                string filepach = dlg.SelectedPath;//得到路径
                this.localDirTextBox.Text = filepach;
                AppCfg.setItem(AppCfg.localImgDir, filepach);
            };
        }

        private void localDirTextBox_TextChanged(object sender, EventArgs e) {
            return;
            string filepach = this.localDirTextBox.Text;
            AppCfg.setItem(AppCfg.localImgDir, filepach);
        }

        private void imgUrlTextBox_Enter(object sender, EventArgs e) {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text)) {
                this.imgUrlTextBox.Text = (String)iData.GetData(DataFormats.Text);


            }
        }

        private void openLocalDirBtn_Click(object sender, EventArgs e) {
            string dir = this.localDirTextBox.Text;
            if (dir != null) {
                if (this.onShowExplorePath(dir) == false) {
                    System.Diagnostics.Process.Start("Explorer.exe", dir);
                }

            } else {
                this.addLog("目录为空,不能打开");
            }
        }

        private bool onShowExplorePath(string path) {
            bool isFind = false;
            ShellWindows wins = new ShellWindows();
            foreach (InternetExplorer w in wins) {
                if (w.LocationURL.Contains(path.Replace('\\', '/'))) {
                    // 找到了窗口就置顶
                    Win32API.SetForegroundWindow((IntPtr)w.HWND);
                    isFind = true;
                    break;
                }
            }
            return isFind;
        }


    }
}
