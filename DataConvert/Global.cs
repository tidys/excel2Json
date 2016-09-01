using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Text.RegularExpressions; // 正则表达式

namespace DataConvert
{
    public class DataType
    {
        public string strName = "";
        public string strFullName = "";
        public List<DataXlsx> listXlsx = new List<DataXlsx>();
        public bool LoadXlsx()
        {
            DirectoryInfo diRoot = new DirectoryInfo(this.strFullName + "\\xlsx");
            DirectoryInfo[] diSubs = diRoot.GetDirectories();
            for (int i = 0; i < diSubs.Length; i++)
            {
                DirectoryInfo diSub = diSubs[i];
                FileInfo[] listFiles = diSub.GetFiles("*.xlsx");
                for (int j = 0; j < listFiles.Length; j++)
                {
                    DataXlsx xlsx = new DataXlsx();
                    xlsx.strCatlog = diSub.Name;
                    xlsx.strName = listFiles[j].Name;
                    xlsx.strFullName = listFiles[j].FullName;
                    xlsx.LoadConfigSheet();
                    this.listXlsx.Add(xlsx);
                    //Console.WriteLine(xlsx.strCatlog + ", " + xlsx.strName + ", " + xlsx.strFullName);
                }
            }
            return true;
        }
    }

    public class DataXlsx
    {
        public string strCatlog = "";
        public string strName = "";
        public string strFullName = "";
        public int nSheetChecked = 0;
        public List<DataSheet> listSheet = new List<DataSheet>();
        public bool LoadConfigSheet()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(this.strFullName)))
                {
                    var ws = package.Workbook.Worksheets[1];
                    if (ws == null) return false;
                    int row = 2;
                    while (true)
                    {
                        var sheetName = ws.Cells[row, 1].Value;
                        var outputName = ws.Cells[row, 2].Value;
                        var sheetIndex = ws.Cells[row, 3].Value;
                        if ((sheetName == null) && (outputName == null) && (sheetIndex == null)) //结束
                        {
                            break;
                        }

                        if (sheetName == null)
                        {
                            FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + ws.Name + "] 单元格[" + row.ToString() + ",1] 为空");
                            break;
                        }
                        if (outputName == null)
                        {
                            FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + ws.Name + "] 单元格[" + row.ToString() + ",2] 为空");
                            break;
                        }
                        if (sheetIndex == null)
                        {
                            FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + ws.Name + "] 单元格[" + row.ToString() + ",3] 为空");
                            break;
                        }
                        DataSheet ds = new DataSheet();
                        try
                        {
                            ds.nIndex = Convert.ToInt32(sheetIndex);
                            ds.strName = sheetName.ToString().Trim();
                            ds.strTxtFile = outputName.ToString().Trim() + ".txt";
                            ds.strJsonFile = outputName.ToString().Trim() + ".json";
                            this.listSheet.Add(ds);
                            row++;
                        }
                        catch (Exception)
                        {
                            string s = "错误: 文件[" + this.strName + "] 工作表[" + ws.Name + "] 单元格[" + row.ToString() + ",3] 数据不是整数";
                            Console.WriteLine(s);
                            FormMain.frmMain.AddLog(s);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 打开失败 " + ex.ToString());
                return false;
            }
        }

        public void ConvertSheet()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(this.strFullName)))
                {
                    foreach (DataSheet sheet in this.listSheet)
                    {
                        try
                        {
                            List<string> listItemEn = new List<string>();
                            List<string> listItemChs = new List<string>();
                            List<string> listData = new List<string>();
                            var ws = package.Workbook.Worksheets[sheet.strName];
                            if (ws == null)
                            {
                                FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 不存在");
                                continue;
                            }
                            int col = 1; //列
                            //读取列标题 第一行为英文 第二行为中文
                            bool bNull = false;
                            while (true)
                            {
                                var v1 = ws.Cells[1, col].Value; //英文
                                var v2 = ws.Cells[2, col].Value; //中文
                                if ((v1 == null) && (v2 == null)) //标题结束
                                    break;
                                if (v1 == null)
                                {
                                    FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 单元格[1, " + col.ToString() + "] 内容为空");
                                    break;
                                }
                                if (v2 == null)
                                {
                                    FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 单元格[2, " + col.ToString() + "] 内容为空");
                                    break;
                                }
                                string s1 = v1.ToString().Trim();
                                string s2 = v2.ToString().Trim();
                                if (s1.Length == 0)
                                {
                                    bNull = true;
                                    FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 单元格[1, " + col.ToString() + "] 内容为空");
                                    break;
                                }
                                if (s2.Length == 0)
                                {
                                    bNull = true;
                                    FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 单元格[2, " + col.ToString() + "] 内容为空");
                                    break;
                                }
                                col++;
                                listItemEn.Add(s1);
                                listItemChs.Add(s2);
                            }
                            if (bNull)
                                continue;
                            col--;
                            Console.WriteLine(ws.Name + "列数[" + col.ToString() + "]");
                            if (col <= 0)
                            {
                                FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 标题个数为0");
                                continue;
                            }
                            int row = 3; //第三行开始为数据
                            while (true)
                            {
                                List<string> listLine = new List<string>();
                                int nError = 0;
                                for (int i = 1; i <= col; i++)
                                {
                                    var cell = ws.Cells[row, i];
                                    var data = cell.Value;
                                    if (data == null)
                                    {
                                        listLine.Add("");
                                        nError++;
                                    }
                                    else
                                    {
                                        string s = data.ToString().Trim();
                                        if (s.Length == 0)
                                        {
                                            listLine.Add("");
                                            nError++;
                                        }
                                        else
                                        {
                                            listLine.Add(s);
                                            /*
                                            int style = cell.StyleID;
                                            if (style == 1)// 数值类型
                                            {
                                                listData.Add(data);
                                            }
                                            else
                                            {

                                            }
                                            */
                                        }
                                    }
                                }
                                if (nError == col) //空行 为数据结束
                                    break;
                                for (int i = 0; i < col; i++)
                                {
                                    listData.Add(listLine[i]);
                                }
                                row++;
                            }
                            //检查数据
                            int n = listData.Count / listItemEn.Count;
                            bool bCheckFailed = false;
                            for (int i = 0; i < n; i++)
                            {
                                for (int j = 0; j < listItemEn.Count; j++)
                                {
                                    string s = listData[i * listItemEn.Count + j];
                                    if (s == "")
                                    {
                                        if (listItemEn[j] == "*")
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            bCheckFailed = true;
                                            FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 单元格[" + (i + 3).ToString() + "," + (j + 1).ToString() + "] 为空");
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            if (bCheckFailed == false)
                            {
                                //保存为TXT
                                if (this.SaveToTxt(listItemEn, listItemChs, listData, sheet.strTxtFile) == false)
                                {
                                    FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 转换为[" + sheet.strTxtFile + "] 保存失败");
                                }
                                //保存为JSON
                                if (this.SaveToJson(listItemEn, listData, sheet.strJsonFile) == false)
                                {
                                    FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 转换为[" + sheet.strJsonFile + "] 保存失败");
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 工作表[" + sheet.strName + "] 处理过程中出现异常 " + ex.ToString());
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FormMain.frmMain.AddLog("错误: 文件[" + this.strName + "] 打开失败");
            }
        }

        public bool SaveToTxt(List<string> listItemEn, List<string> listItemChs, List<string> listData, string strFile)
        {
            try
            {
                string strFullFile = FormMain.frmMain.strRootPath + "\\" + FormMain.frmMain.strDir + "\\txt\\" + strFile;
                Console.WriteLine(strFullFile);
                StringBuilder sb = new StringBuilder();
                //英文标题
                for (int i = 0; i < listItemEn.Count; i++)
                {
                    string s = listItemEn[i].ToString();
                    if (i == 0)
                        sb.Append(s);
                    else
                        sb.Append("\t" + s);
                }
                sb.Append("\r\n");
                //中文标题
                for (int i = 0; i < listItemChs.Count; i++)
                {
                    string s = listItemChs[i].ToString();
                    if (i == 0)
                        sb.Append(s);
                    else
                        sb.Append("\t" + s);
                }
                sb.Append("\r\n");
                //数据内容
                int n = listData.Count / listItemEn.Count;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < listItemEn.Count; j++)
                    {
                        string s = listData[i * listItemEn.Count + j].ToString();
                        if (j == 0)
                            sb.Append(s);
                        else
                            sb.Append("\t" + s);
                    }
                    sb.Append("\r\n");
                }
                //UTF8Encoding utf8 = new UTF8Encoding(false);
                StreamWriter sw = new StreamWriter(strFullFile, false, Encoding.Default);
                sw.Write(sb.ToString());
                sw.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SaveToJson(List<string> listItem, List<string> listData, string strFile)
        {
            try
            {
                string strFullFile = FormMain.frmMain.strRootPath + "\\" + FormMain.frmMain.strDir + "\\json\\" + strFile;
                Console.WriteLine(strFullFile);
                StringBuilder sb = new StringBuilder();
                //有效数据项数
                int m = 0;
                foreach (string s in listItem)
                {
                    if (s == "*") continue;
                    m++;
                }
                //数据内容
                int n = listData.Count / listItem.Count;
                sb.Append("[");
                for (int i = 0; i < n; i++)
                {
                    sb.Append("{");
                    int count = 0;
                    for (int j = 0; j < listItem.Count; j++)
                    {
                        string strItem = listItem[j];
                        string strData = listData[i * listItem.Count + j];
                        if (strItem == "*") continue;
                        if (strData.Length == 0)
                            return false;
                        sb.Append("\"" + strItem + "\":");
                        // 对值做特殊的处理
                        // TODO
                      
                        sb.Append("\"" + strData + "\"");
                        count++;
                        if (count != m)
                            sb.Append(",");
                    }
                    sb.Append("}");
                    if (i != n - 1)
                        sb.Append(",");
                }
                sb.Append("]");

                UTF8Encoding utf8 = new UTF8Encoding(false);
                StreamWriter sw = new StreamWriter(strFullFile, false, utf8);
                sw.Write(sb.ToString());
                sw.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class DataSheet
    {
        public ListViewItem lvi = null;
        public bool bChecked = false;
        public int nIndex = 0;
        public string strName = "";
        public string strTxtFile = "";
        public string strTxtFullFile = "";
        public string strJsonFile = "";
        public string strJsonFullFile = "";

    }

}
