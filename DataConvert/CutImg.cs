using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Reflection;
using SHDocVw;

namespace DataConvert {
    public partial class CutImg : Form {
        public static bool isShow = false;
        public static CutImg instance = null;
        public CutImg() {
            InitializeComponent();
            CutImg.instance = this;
            CutImg.isShow = true;
        }
        public void addLog(string log) {

            ListViewItem lvi = CutImg.instance.logListView.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            lvi.SubItems.Add(log);

            // 自动显示最下边的日志
            int index = this.logListView.Items.Count;
            this.logListView.Items[index - 1].EnsureVisible();
        }
        private static int processVal = 0;
        // 点击 浏览 按钮
        private void openFileBtnClick(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                imagePathLabel.Text = openFileDialog1.FileName;
                this.imagePath = openFileDialog1.FileName;
                this.dealImgPath(this.imagePath);
            }
        }

        private void CutImage() {

            // 加载图片
            System.Drawing.Image image = new System.Drawing.Bitmap(imagePath);
            this.formatName = this.txbFname.Text;
            // 144/144 =1  145/144=2
            int cutRow = image.Height / this.iHeight;
            if (image.Height % this.iHeight != 0) {
                cutRow++;
            }

            int cutColumn = image.Width / this.iWidth;
            if (image.Width % this.iWidth != 0) {
                cutColumn++;
            }
            for (int i = 0; i < cutRow; i++) {
                for (int j = 0; j < cutColumn; j++) {
                    int iTop = i * this.iHeight;
                    int iLeft = j * this.iWidth;
                    Rectangle destRect = new Rectangle(0, 0, iWidth, iHeight);// 目标区域
                    Rectangle srcRect = new Rectangle(iLeft, iTop, iWidth, iHeight); // 源图区域
                    // 新建Graphics对象
                    Bitmap newImage = new Bitmap(iWidth, iHeight);
                    Graphics g = Graphics.FromImage(newImage);
                    g.SmoothingMode = SmoothingMode.HighQuality;// 绘图平滑程序
                    g.CompositingQuality = CompositingQuality.HighQuality;   // 图片输出质量
                    g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);// 输出到newImage对象
                    g.Dispose();// 释放绘图对象
                    //string strDestFile = string.Format(path+@"\C{0}R{1}L{2}.jpg", i, j, level);
                    //string strDestFile = string.Format(path + @"\{2}_{0}_{1}.jpg", i, j, level);
                    string[] arr = this.imagePath.Split('.');
                    string type = ".png";
                    if (arr.Length == 2) {
                        type = "." + arr[1];
                    }

                    float finished = i * cutColumn + j;

                    bool isExists = Directory.Exists(outPutPath);
                    if (!isExists) {
                        Directory.CreateDirectory(outPutPath);
                        //CutImg.instance.addLog("[创建文件夹] " + outPutPath);
                    }

                    string strDestFile = outPutPath + "\\" + this.formatName + "_" + finished + type;
                    newImage.Save(strDestFile);

                    float total = cutRow * cutColumn;
                    float result = finished / total * 100;
                    Console.WriteLine(result);
                    setProcessVal(Convert.ToInt16(result));
                    Thread.Sleep(100);
                }
            }

            // 打开文件
            this.showExplore(this.outPutPath);
            //return true;


        }
        class Win32API {
            [DllImport("User32.dll")]
            public static extern bool PtInRect(ref Rectangle r, Point p);
            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);
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

        public void showExplore(string filePath) {
            bool b = this.onShowExplorePath(filePath);
            if (!b) {
                System.Diagnostics.Process.Start("Explorer.exe", filePath);
            }
        }
        private void setProcessVal(int val) {
            processVal = val;
        }

        /// 根据给定的方框切割图片
        private Bitmap getImgByRect(Rectangle rectangle, Bitmap img) {
            Bitmap bit = img.Clone(rectangle, img.PixelFormat);
            return bit;
        }

        Thread doProcess = null;
        string imagePath;// 图片的路径
        int iWidth;// 剪切宽度
        int iHeight;//剪切高度

        string outPutPath;
        string formatName;// 格式化名字

        // 判断文件是否是图片
        public bool IsPicture(string fileName) {
            if (fileName == null) {
                return false;
            }

            int beganPos = fileName.LastIndexOf(".");
            int endPos = fileName.Length - fileName.LastIndexOf(".");
            string fileExt = fileName.Substring(beganPos, endPos);

            string strFilter = ".jpeg|.gif|.jpg|.png|.bmp|.pic|.tiff|.ico|.iff|.lbm|.mag|.mac|.mpt|.opt|";
            char[] separtor = { '|' };
            string[] tempFileds = StringSplit(strFilter, separtor);
            foreach (string str in tempFileds) {
                if (str.ToUpper() == fileExt.ToUpper()) {
                    return true;
                }
            }
            return false;
        }
        // 通过字符串，分隔符返回string[]数组 
        public string[] StringSplit(string s, char[] separtor) {
            string[] tempFileds = s.Trim().Split(separtor); return tempFileds;
        }

        // 点击切割图片按钮
        private void cutImgBtnClick(object sender, EventArgs e) {
            this.progressBar1.Value = 0;
            this.imagePath = imagePathLabel.Text.Trim();
            if (this.imagePath == null || this.imagePath.Length <= 0) {
                this.addLog("请选择要切割的图片");
                return;
            } else {
                // 判断图片存在不
                if (File.Exists(this.imagePath)) {

                } else {
                    this.addLog("文件不存在,请检查路径");
                    return;
                }
            }

            iWidth = Convert.ToInt32(txbWidth.Text);
            if (this.iWidth <= 0) {
                this.addLog("剪切的宽度必须 > 0");
                return;
            }

            iHeight = Convert.ToInt32(txbHeight.Text);
            if (this.iHeight <= 0) {
                this.addLog("剪切的高度必须 > 0");
                return;
            }
            this.outPutPath = txtOutPut.Text;
            if (this.outPutPath.Length <= 0) {
                this.addLog("请选择图片保存路径");
                return;
            }
            if ((doProcess == null) || (doProcess.ThreadState == ThreadState.Stopped)) {
                doProcess = new Thread(new ThreadStart(CutImage));
                this.timer1.Enabled = true;
                this.cutImageBtn.Enabled = false;
                doProcess.Start();
            }
            //Thread.CurrentThread.Join();
        }

        // 选择保存位置
        private void selectSavePathBtn_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                txtOutPut.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        // 进度
        private void timer1_Tick(object sender, EventArgs e) {
            if (processVal >= 0 && processVal <= 100) {
                this.progressBar1.Value = processVal;
            }
            if (doProcess != null && doProcess.ThreadState == ThreadState.Stopped) {
                this.progressBar1.Value = 100;
                Thread.Sleep(1500);
                this.timer1.Enabled = false;
                this.cutImageBtn.Enabled = true;
                this.addLog("切割完成!");
            }
        }

        // 点击进度条的X
        private void cleanProcessBtnClick(object sender, EventArgs e) {
            this.progressBar1.Value = 0;
        }

        // 拖动文件到控件上
        private void imagePathDragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Link;
                this.imagePathLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            } else {
                e.Effect = DragDropEffects.None;
            }
        }

        private void imagePathDragDrop(object sender, DragEventArgs e) {
            System.Array arr = (System.Array)e.Data.GetData(DataFormats.FileDrop);
            string path = arr.GetValue(0).ToString();
            this.dealImgPath(path);

        }
        private void dealImgPath(string path) {
            bool b = this.IsPicture(path);
            if (b == false) {
                this.addLog("文件不是图片!");
                this.imagePathLabel.Text = "";
                this.imagePath = "";
            } else {
                this.imagePathLabel.Text = path;
                this.imagePath = path;

                System.Drawing.Image image = new System.Drawing.Bitmap(this.imagePath);
                if (txbWidth.Text.Equals("0")) {
                    txbWidth.Text = image.Width.ToString();
                }
                if (txbHeight.Text.Equals("0")) {
                    txbHeight.Text = image.Height.ToString();
                }



                // 文件名
                string[] splitArr = this.imagePath.Split('\\');
                string fileName = splitArr[splitArr.Length - 1];
                string[] nameArr = fileName.Split('.');
                string name = nameArr[0];
                this.txbFname.Text = name;
                this.formatName = name;

                this.txbWidth.Focus();

                // 设置保存地址
                // 
                string[] tmpArr = this.imagePath.Split('.');
                string filePath = tmpArr[0];
                // 文件所在的路径
                //int beganPos = path.LastIndexOf("\\");
                //string filePath = path.Substring(0, beganPos);
                txtOutPut.Text = filePath;

            }
        }
        private void label6_Click(object sender, EventArgs e) {

        }

        private void txbFname_TextChanged(object sender, EventArgs e) {

        }

        private void CutImg_Load(object sender, EventArgs e) {

        }

        private void progressBar1_Click(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void CutImg_FormClosed(object sender, FormClosedEventArgs e) {
            CutImg.isShow = false;
        }

    }
}
