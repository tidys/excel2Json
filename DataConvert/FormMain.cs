using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using SHDocVw;

namespace DataConvert {
    public partial class FormMain : Form {
        public enum WorkMode {
            None,
            Load,
            Convert
        }
        private delegate void DelegateAddLog(string s);

        public static FormMain frmMain = null;
        public string strTarPat = "";
        public string strDir = "";
        public List<string> rootListDir = new List<string>();// 根目录列表
        public List<DataXlsx> listXlsx = new List<DataXlsx>();

        public WorkMode nWorkMode = WorkMode.None;
        public int nPosition = 0;

        public FormMain() {
            InitializeComponent();
            FormMain.frmMain = this;
        }

        public void ToExcel(string strTitle) {
            int nMax = 9;
            int nMin = 4;
            int rowCount = nMax - nMin + 1;//总行数
            const int columnCount = 4;//总列数
            //创建Excel对象
            Excel.Application excelApp = new Excel.ApplicationClass();
            //新建工作簿
            Excel.Workbook workBook = excelApp.Workbooks.Add(true);
            //新建工作表
            Excel.Worksheet worksheet = workBook.ActiveSheet as Excel.Worksheet;
            ////设置标题
            //Excel.Range titleRange = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, columnCount]);//选取单元格
            //titleRange.Merge(true);//合并单元格
            //titleRange.Value2 = strTitle; //设置单元格内文本
            //titleRange.Font.Name = "宋体";//设置字体
            //titleRange.Font.Size = 18;//字体大小
            //titleRange.Font.Bold = false;//加粗显示
            //titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//水平居中
            //titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;//垂直居中
            //titleRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;//设置边框
            //titleRange.Borders.Weight = Excel.XlBorderWeight.xlThin;//边框常规粗细
            //设置表头
            string[] strHead = new string[columnCount] { "序号", "范围", "分组1", "分组2" };
            int[] columnWidth = new int[4] { 8, 16, 8, 10 };
            for (int i = 0; i < columnCount; i++) {
                //Excel.Range headRange = worksheet.Cells[2, i + 1] as Excel.Range;//获取表头单元格
                Excel.Range headRange = worksheet.Cells[1, i + 1] as Excel.Range;//获取表头单元格,不用标题则从1开始
                headRange.Value2 = strHead[i];//设置单元格文本
                headRange.Font.Name = "宋体";//设置字体
                headRange.Font.Size = 12;//字体大小
                headRange.Font.Bold = false;//加粗显示
                headRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//水平居中
                headRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;//垂直居中
                headRange.ColumnWidth = columnWidth[i];//设置列宽
                //  headRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;//设置边框
                // headRange.Borders.Weight = Excel.XlBorderWeight.xlThin;//边框常规粗细
            }
            //设置每列格式
            for (int i = 0; i < columnCount; i++) {
                //Excel.Range contentRange = worksheet.get_Range(worksheet.Cells[3, i + 1], worksheet.Cells[rowCount - 1 + 3, i + 1]);
                Excel.Range contentRange = worksheet.get_Range(worksheet.Cells[2, i + 1], worksheet.Cells[rowCount - 1 + 3, i + 1]);//不用标题则从第二行开始
                contentRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//水平居中
                contentRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;//垂直居中
                //contentRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;//设置边框
                // contentRange.Borders.Weight = Excel.XlBorderWeight.xlThin;//边框常规粗细
                contentRange.WrapText = true;//自动换行
                contentRange.NumberFormatLocal = "@";//文本格式
            }
            //填充数据
            for (int i = nMin; i <= nMax; i++) {
                int k = i - nMin;
                //excelApp.Cells[k + 3, 1] = string.Format("{0}", k + 1);
                //excelApp.Cells[k + 3, 2] = string.Format("{0}-{1}", i - 0.5, i + 0.5);
                //excelApp.Cells[k + 3, 3] = string.Format("{0}", k + 3);
                //excelApp.Cells[k + 3, 4] = string.Format("{0}", k + 4);
                excelApp.Cells[k + 2, 1] = string.Format("{0}", k + 1);
                excelApp.Cells[k + 2, 2] = string.Format("{0}-{1}", i - 0.5, i + 0.5);
                excelApp.Cells[k + 2, 3] = string.Format("{0}", k + 3);
                excelApp.Cells[k + 2, 4] = string.Format("{0}", k + 4);
            }
            //设置Excel可见
            excelApp.Visible = true;
        }

        private void FormMain_Load(object sender, EventArgs e) {

            //更新标题: 工具的目录
            this.Text += " - exe: " + AppData.exePath;
            this.rootDirComboBox.Items.Add("-- 文件夹列表 --");

            AppData.init();
            //初始化文件夹下边的 数据
            this.InitDirectory();
            this.initGenTypeCombox();
            // listDir 处理
            if (this.rootListDir.Count > 0) {
                for (int i = 0; i < this.rootListDir.Count; i++) {
                    this.rootDirComboBox.Items.Add(this.rootListDir[i]);
                }
                // 加载上次保存的selectIndex
                string indexStr = AppCfg.getItem(AppCfg.selectIndex);
                int index = 0;
                if (indexStr != null) {
                    index = int.Parse(indexStr);
                }
                if (index > this.rootDirComboBox.Items.Count - 1) {
                    index = 0;
                }
                this.rootDirComboBox.SelectedIndex = index;
            }

            // bat 文件配置
            string cfgBatName = AppCfg.getItem(AppCfg.batFilePath);
            if (cfgBatName != null) {
                this.batPathTextBox.Text = cfgBatName;
            }
            // 隐藏窗口的定时器
            this.hideWinTimer.Start();
            //this.Top = 0;
        }


        #region  init 生成类型Combox
        private void initGenTypeCombox() {
            this.genTypeCombox.Items.Add("ALL");
            this.genTypeCombox.Items.Add("服务端");
            this.genTypeCombox.Items.Add("客户端");
            EnumGenType type = AppData.getGenType();
            if (type == EnumGenType.All) {
                this.genTypeCombox.SelectedIndex = 0;
            } else if (type == EnumGenType.Server) {
                this.genTypeCombox.SelectedIndex = 1;
            } else if (type == EnumGenType.Client) {
                this.genTypeCombox.SelectedIndex = 2;
            }
            AppData.saveCfg();
        }

        private void genTypeSelectChange(object sender, EventArgs e) {
            int index = this.genTypeCombox.SelectedIndex;
            if (index == 0) {
                AppData.setGenType(EnumGenType.All);
            } else if (index == 1) {
                AppData.setGenType(EnumGenType.Server);
            } else if (index == 2) {
                AppData.setGenType(EnumGenType.Client);
            }
            AppData.saveCfg();
        }

        // 初始化文件夹目录
        private void InitDirectory() {
            string rootPath = Application.StartupPath;
            DirectoryInfo rootDir = new DirectoryInfo(rootPath);
            DirectoryInfo[] rootDirSub = rootDir.GetDirectories();
            if (rootDirSub.Length > 0) {
                for (int i = 0; i < rootDirSub.Length; i++) {
                    string strName = rootDirSub[i].Name;
                    this.rootListDir.Add(strName);

                    string strFullName = rootDirSub[i].FullName;
                    this.genDirectory(strFullName + "\\xlsx");
                    this.genDirectory(strFullName + "\\txt");
                    this.genDirectory(strFullName + "\\json");
                    //this.genDirectory(strFullName + "\\json\\server");
                    //this.genDirectory(strFullName + "\\json\\client");
                }
            } else {
                this.AddLog("[警告] " + rootPath + " 下没有发现的文件夹!");
            }
        }

        // 没有指定的目录则生成相应的目录
        private void genDirectory(string path) {
            bool isExists = Directory.Exists(path);
            if (!isExists) {
                Directory.CreateDirectory(path);
                this.AddLog("[创建文件夹] " + path);
            }
        }
        #endregion
        private void rootDirComboBoxSelectedChanged(object sender, EventArgs e) {
            this.listViewData.Items.Clear();
            this.listXlsx.Clear();
            int index = this.rootDirComboBox.SelectedIndex;
            AppCfg.setItem("selectIndex", index.ToString());
        }

        #region 载入数据
        private void btnLoad_Click(object sender, EventArgs e) {
            this.listViewLog.Items.Clear();
            this.listViewData.Items.Clear();//控件数据清空
            this.listXlsx.Clear();// xlsx arr清空
            this.SetButtonsEnable(false);

            int nSelect = this.rootDirComboBox.SelectedIndex;
            if (nSelect == 0) {
                MessageBox.Show("请选择需要载入的文件夹!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SetButtonsEnable(true);
                return;
            }
            this.strDir = this.rootDirComboBox.SelectedItem.ToString();
            string xlsxPath = AppData.exePath + "\\" + this.strDir + "\\xlsx";

            // 先统计所有的目录
            List<string> allDir = new List<string>();
            this.getDirectories(xlsxPath, allDir);
            // 在统计出来的目录中寻找xlsx
            foreach (string path in allDir) {
                this.getDirXlsxFile(path);
            }
            if (this.listXlsx.Count == 0) {
                this.AddLog("[警告]" + xlsxPath + " 下没有xlsx文件!");
                this.SetButtonsEnable(true);
                return;
            }
            this.nPosition = 0;
            this.progressBar.Minimum = 0;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = this.listXlsx.Count;
            this.nWorkMode = WorkMode.Load;
            this.timer.Enabled = true;
            ThreadPool.SetMinThreads(10, 10);
            ThreadPool.QueueUserWorkItem(new WaitCallback(FormMain.LoadXlsxConfig), null);
        }

        private void getDirectories(string path, List<string> arr) {
            arr.Add(path);
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] rootSub = root.GetDirectories();
            if (rootSub.Length > 0) {
                for (int i = 0; i < rootSub.Length; i++) {
                    DirectoryInfo diSub = rootSub[i];
                    this.getDirectories(diSub.FullName, arr);
                }
            }
        }
        // 获取目录下的文件
        private void getDirXlsxFile(string path) {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] listFiles = dir.GetFiles("*.xlsx");
            for (int j = 0; j < listFiles.Length; j++) {
                // 获取相对根目录的路径
                string[] splitArr = path.Split("xlsx".ToCharArray());
                string name = splitArr[splitArr.Length - 1];

                DataXlsx xlsx = new DataXlsx();
                xlsx.strCatlog = name;// dir.Name;
                xlsx.strName = listFiles[j].Name;
                xlsx.strFullName = listFiles[j].FullName;
                Console.WriteLine(xlsx.strFullName);
                this.listXlsx.Add(xlsx);
            }
        }

        public static void LoadXlsxConfig(Object obj) {
            FormMain frm = FormMain.frmMain;
            Parallel.ForEach(frm.listXlsx, (xlsx) => {
                xlsx.LoadConfigSheet();
                frm.Invoke(new Action(() => {
                    frm.nPosition++;
                }));
            });
        }
        #endregion

        #region 添加日志信息
        public void AddLog(string s) {
            if (this.listViewLog.InvokeRequired) {
                DelegateAddLog dc = new DelegateAddLog(AddLog);
                this.BeginInvoke(dc, new object[] { s }); //通过代理调用刷新方法
            } else {
                ListViewItem lvi = this.listViewLog.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                lvi.SubItems.Add(s);
                // 自动显示最下边的日志
                int index = this.listViewLog.Items.Count;
                this.listViewLog.Items[index - 1].EnsureVisible();
            }
        }
        #endregion

        #region 全选
        private void btnSelectAll_Click(object sender, EventArgs e) {
            this.isSelectAllItem = true;
            for (int i = 0; i < this.listViewData.Items.Count; i++) {
                ListViewItem lvi = this.listViewData.Items[i];
                lvi.Checked = true;
            }
        }
        #endregion

        #region 反选
        private void btnUnAll_Click(object sender, EventArgs e) {
            this.isSelectAllItem = false;
            for (int i = 0; i < this.listViewData.Items.Count; i++) {
                ListViewItem lvi = this.listViewData.Items[i];
                lvi.Checked = !lvi.Checked;
            }
        }
        #endregion

        #region 转换
        private void btnConvert_Click(object sender, EventArgs e) {
            //string strPathTar = this.strRootPath + "\\" + this.strType + "\\json";
            //DirectoryInfo diTar = new DirectoryInfo(strPathTar);
            //FileInfo[] listFileInfo = diTar.GetFiles();
            //foreach (FileInfo fi in listFileInfo)
            //    File.Delete(fi.FullName);

            this.SetButtonsEnable(false);
            ListView.CheckedIndexCollection ck = this.listViewData.CheckedIndices;
            if (ck.Count == 0) {
                this.AddLog("[!错误] 请选中需要转换的工作表!");
                this.SetButtonsEnable(true);
                return;
            }
            string str = "需要转换的工作表共计" + ck.Count + "项!";
            this.AddLog(str);
            for (int i = 0; i < this.listXlsx.Count; i++) {
                DataXlsx xlsx = this.listXlsx[i];
                int n = 0;
                for (int j = 0; j < xlsx.listSheet.Count; j++) {
                    DataSheet sheet = xlsx.listSheet[j];
                    sheet.bChecked = false;
                    if (sheet.lvi != null) {
                        if (sheet.lvi.Checked) {
                            sheet.bChecked = true;
                            n++;
                        }
                    }
                }
                xlsx.nSheetChecked = n;
            }
            this.nPosition = 0;
            this.progressBar.Minimum = 0;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = ck.Count;
            this.nWorkMode = WorkMode.Convert;
            this.timer.Enabled = true;
            ThreadPool.SetMinThreads(10, 10);
            ThreadPool.QueueUserWorkItem(new WaitCallback(FormMain.ConvertXlsxSheet), null);
        }
        public static void ConvertXlsxSheet(Object obj) {
            FormMain frm = FormMain.frmMain;
            Parallel.ForEach(frm.listXlsx, (xlsx) => {
                if (xlsx.nSheetChecked > 0) {
                    xlsx.ConvertSheet();
                    frm.Invoke(new Action(() => {
                        frm.nPosition += xlsx.nSheetChecked;
                    }));
                }

            });
        }
        #endregion

        public void SetButtonsEnable(bool b) {
            this.rootDirComboBox.Enabled = b;
            this.btnLoad.Enabled = b;
            this.btnUnAll.Enabled = b;
            this.btnConvert.Enabled = b;
        }

        #region 定时器
        private void timer_Tick(object sender, EventArgs e) {
            switch (this.nWorkMode) {
                case WorkMode.Load:
                    Console.WriteLine(this.nPosition.ToString());
                    if (this.nPosition != this.progressBar.Value)
                        this.progressBar.Value = this.nPosition;
                    if (this.nPosition >= this.progressBar.Maximum) {
                        this.progressBar.Value = this.progressBar.Maximum;
                        this.nPosition = 0;
                        this.timer.Enabled = false;
                        this.SetButtonsEnable(true);
                        for (int i = 0; i < this.listXlsx.Count; i++) {
                            DataXlsx xlsx = this.listXlsx[i];
                            for (int j = 0; j < xlsx.listSheet.Count; j++) {
                                DataSheet sheet = xlsx.listSheet[j];
                                ListViewItem lvi = this.listViewData.Items.Add("");
                                lvi.SubItems.Add(xlsx.strCatlog);
                                lvi.SubItems.Add(xlsx.strName);
                                lvi.SubItems.Add(sheet.strName);
                                lvi.SubItems.Add(sheet.strTxtFile);
                                lvi.SubItems.Add(sheet.strJsonFile);
                                sheet.lvi = lvi;
                            }
                        }
                        string str = "载入完成, 共计" + this.listViewData.Items.Count.ToString() + "项!";
                        //this.listViewLog.Items.Clear();
                        this.AddLog(str);
                        this.progressBar.Value = 0;
                        this.btnSelectAll_Click(null, null);//载入完成后默认全部选择
                    }
                    break;
                case WorkMode.Convert:
                    Console.WriteLine(this.nPosition.ToString());
                    if (this.nPosition != this.progressBar.Value)
                        this.progressBar.Value = this.nPosition;
                    if (this.nPosition >= this.progressBar.Maximum) {
                        this.progressBar.Value = this.progressBar.Maximum;
                        this.nPosition = 0;
                        this.timer.Enabled = false;
                        //Process p = new Process();
                        //p.StartInfo.FileName = "cmd.exe";
                        //p.StartInfo.UseShellExecute = false;
                        //p.StartInfo.RedirectStandardInput = true;
                        //p.StartInfo.RedirectStandardOutput = true;
                        //p.StartInfo.RedirectStandardError = true;
                        //p.StartInfo.CreateNoWindow = true;
                        //p.Start();
                        //p.StandardInput.WriteLine("cd " + this.strRootPath + "\\" + this.strType);
                        //p.StandardInput.WriteLine("tar cvf json.tar json");
                        //p.StandardInput.WriteLine("exit");
                        //p.Close();
                        this.SetButtonsEnable(true);

                        int selectIndex = this.rootDirComboBox.SelectedIndex;
                        string dir = this.rootListDir[selectIndex - 1];
                        string path = AppData.exePath + "\\" + dir + "\\json";
                        bool b = this.onShowExplorePath(path);
                        if (!b) {
                            System.Diagnostics.Process.Start("Explorer.exe", path);
                        }
                        string logStr = "转换完成, 共计" + this.progressBar.Maximum.ToString() + "项!";
                        this.AddLog(logStr);
                        this.AddLog("json文件存放路径:" + path);
                    }
                    break;
                default:
                    this.timer.Enabled = false;
                    break;
            }
        }
        #endregion

        private void OpenDirBtn_Click(object sender, EventArgs e) {
            // 打开excel的目录
            int selectIndex = this.rootDirComboBox.SelectedIndex;
            string dir = this.rootListDir[selectIndex - 1];
            string path = AppData.exePath + "\\" + dir + "\\xlsx";
            bool b = this.onShowExplorePath(path);
            if (!b) {
                System.Diagnostics.Process.Start("Explorer.exe", path);
            }

        }
        // 执行bat文件
        private void batBtn_Click(object sender, EventArgs e) {
            string batFile = this.batPathTextBox.Text;
            this.runCmd(batFile);

        }
        // 快捷键
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData) {
            if (keyData == Keys.F12) {
                string batFile = this.batPathTextBox.Text;
                this.runCmd(batFile);
            } else if (keyData == Keys.Space) {
                // 快捷打开excel
                var len = this.listViewData.SelectedItems.Count;
                if (len > 0) {
                    this.ClickToolStripMenuItem_OpenExcelFile(null, null);
                } else {
                    this.AddLog("[警告] listView 没有 item 被选择!");
                }
            } else if (keyData == Keys.F1) {
                if (DownLoadImg.isShow == false) {
                    this.Top = 0;
                    Form win = new DownLoadImg();
                    win.Show();
                } else {
                    if (DownLoadImg.instance!= null) {
                        DownLoadImg.instance.Activate();
                    }
                }
            }
            return true;
        }
        // 运行bat文件
        private void runCmd(string cmdFile) {
            if (File.Exists(cmdFile)) {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = cmdFile;
                p.Start();
            } else {
                this.AddLog("bat文件不存在:" + cmdFile);
            }
        }
        // 选择bat文件
        private void selectBatBtn_Click(object sender, EventArgs e) {

            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Title = "请选择bat文件";
            fileDlg.Filter = "bat文件|*.bat|所有文件|*.*";
            string cfgBatPath = AppCfg.getItem("batPath");

            if (cfgBatPath == null)// 目录为空
            {
                // 去json目录找
                string path = AppData.exePath;
                int selectIndex = this.rootDirComboBox.SelectedIndex;
                if (selectIndex > 0) {
                    // 选中了目录文件
                    string dir = this.rootListDir[selectIndex - 1];
                    path = AppData.exePath + "\\" + dir + "\\json";
                }

                fileDlg.InitialDirectory = path;
            } else {
                fileDlg.InitialDirectory = cfgBatPath;
            }

            fileDlg.Multiselect = false;
            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK) {
                string filename = System.IO.Path.GetFileName(fileDlg.FileName);//得到文件名
                string filepach = System.IO.Path.GetDirectoryName(fileDlg.FileName);//得到路径
                this.batPathTextBox.Text = fileDlg.FileName;
                AppCfg.setItem("batFile", fileDlg.FileName);
                AppCfg.setItem("batPath", filepach);
            }
        }

        private void listViewData_SelectedIndexChanged(object sender, EventArgs e) {

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
        #region 右键菜单
        private void listViewData_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                this.listViewData.ContextMenuStrip = null;
                int i = this.listViewData.SelectedItems.Count;
                if (i > 0) {
                    // listView 有选中项
                    contextMenuStrip1.Show(this.listViewData, e.Location);
                }
            }
        }

        private int preIndex = -1;
        private void listView_MouseMove(object sender, MouseEventArgs e) {
            return;
            ListViewItem item = this.listViewData.GetItemAt(e.X, e.Y);
            if (item != null) {
                if (item.Index != this.preIndex) {

                }
                string excelFileName = item.SubItems[2].Text;
                string jsonFileName = item.SubItems[5].Text;

                this.preIndex = item.Index;
                toolTip1.Show(excelFileName, this.listViewData, new Point(e.X + 15, e.Y + 15));

                toolTip1.GetToolTip(this.listViewData);
                toolTip1.Active = true;
            } else {
                //toolTip1.Active = false;
            }
        }
        // 打开excel文件所在文件夹
        private void ToolStripMenuItem_OpenExcelDir(object sender, EventArgs e) {
            string path = this.listViewData.SelectedItems[0].SubItems[1].Text;
            string file = this.listViewData.SelectedItems[0].SubItems[2].Text;
            string json = this.listViewData.SelectedItems[0].SubItems[5].Text;
            string xlsxPath = AppData.exePath + "\\" + this.strDir + "\\xlsx";
            var filePath = xlsxPath + path;

            bool b = this.onShowExplorePath(filePath);
            if (!b) {
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + filePath + "\\" + file);
            }


            // 打开文件夹：
            //System.Diagnostics.Process.Start(FilePath);
            //打开文件夹中某个文件：
            //System.Diagnostics.Process.Start(FilePath+"/"+FileName);
            //打开文件夹并选中单个文件：
            //System.Diagnostics.Process.Start("Explorer", "/select,"+ FilePath+"\"+FileName);
        }

        // 打开excel文件
        private void ClickToolStripMenuItem_OpenExcelFile(object sender, EventArgs e) {
            string path = this.listViewData.SelectedItems[0].SubItems[1].Text;
            string file = this.listViewData.SelectedItems[0].SubItems[2].Text;
            string json = this.listViewData.SelectedItems[0].SubItems[5].Text;
            string xlsxPath = AppData.exePath + "\\" + this.strDir + "\\xlsx";
            var filePath = xlsxPath + path;
            System.Diagnostics.Process.Start("Explorer.exe", filePath + "\\" + file);
        }
        // 打开json文件
        private void ToolStripMenuItem_OpenJsonFile(object sender, EventArgs e) {
            string path = this.listViewData.SelectedItems[0].SubItems[1].Text;
            string file = this.listViewData.SelectedItems[0].SubItems[2].Text;
            string json = this.listViewData.SelectedItems[0].SubItems[5].Text;
            string jsonPath = AppData.exePath + "\\" + this.strDir + "\\json";
            var filePath = jsonPath + "\\" + json;
            //判断文件是否存在
            if (File.Exists(@filePath)) {
                System.Diagnostics.Process.Start("Explorer.exe", filePath);
            } else {
                this.AddLog("[错误] " + filePath + " 文件不存在,请生成Json");
            }
        }


        // 打开Json文件夹
        private void ToolStripMenuItem1_OpenJsonDir(object sender, EventArgs e) {
            string path = this.listViewData.SelectedItems[0].SubItems[1].Text;
            string file = this.listViewData.SelectedItems[0].SubItems[2].Text;
            string json = this.listViewData.SelectedItems[0].SubItems[5].Text;
            string jsonPath = AppData.exePath + "\\" + this.strDir + "\\json";
            bool b = this.onShowExplorePath(jsonPath);
            if (!b) {
                System.Diagnostics.Process.Start(jsonPath);
            }
        }
        #endregion

        #region 窗体隐藏
        // 隐藏窗口,类似qq那样
        private void hideWindowTick(object sender, EventArgs e) {
            System.Drawing.Point pp = new Point(Cursor.Position.X, Cursor.Position.Y);//获取鼠标在屏幕的坐标点
            Rectangle rects = new Rectangle(this.Left, this.Top, this.Left + this.Width, this.Top + this.Height);//存储当前窗体在屏幕的所在区域 
            if (this.Top < 0) {//当鼠标在当前窗体内，并且窗体的Top属性小于0 
                if (Win32API.PtInRect(ref rects, pp)) {
                    this.Top = 0;//设置窗体的Top属性为0 ,窗口显示出来并且置顶
                    //this.Activate();
                    this.TopMost = true;
                }
            } else {
                if (this.Top < 5) {
                    if (!(Win32API.PtInRect(ref rects, pp))) {
                        this.Top = 5 - this.Height;//将QQ窗体隐藏到屏幕的顶端 
                    } else {
                        this.TopMost = false;
                    }
                } else {
                }
            }
        }
        #endregion

        private void listViewData_SelectedIndexChanged_1(object sender, EventArgs e) {

        }

        // 点击标头
        private bool isSelectAllItem = true;
        private void ClickListViewColumn(object sender, ColumnClickEventArgs e) {
            if (e.Column == 0) {
                this.isSelectAllItem = !this.isSelectAllItem;
                for (int i = 0; i < this.listViewData.Items.Count; i++) {
                    ListViewItem lvi = this.listViewData.Items[i];
                    lvi.Checked = this.isSelectAllItem;
                }
            }
        }


    }
}
