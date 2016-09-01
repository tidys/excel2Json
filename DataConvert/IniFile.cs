using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataConvert
{
    class IniFile
    {
        private List<string> listContent = new List<string>();
        private List<string> listToken = new List<string>();
        private int nLastLine = 0;

        public IniFile()
        {

        }

        public bool Load(string strFile)
        {
            try
            {
                this.listContent.Clear();
                using (StreamReader sr = new StreamReader(strFile, Encoding.GetEncoding("GBK")))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length == 0) continue;
                        if (line.Substring(0, 1) == "#") continue;
                        if (line.Substring(0, 1) == ";") continue;
                        this.listContent.Add(line);
                    }
                    sr.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public string GetString(string strSection, string strItem, string strValue)
        {
            string strResult = strValue;
            strSection = "[" + strSection + "]";
            bool bFindSection = false;
            int nTempLine = this.nLastLine;
            for (int i = 0; i < this.listContent.Count; i++)
            {
                int t = (nTempLine + i) % this.listContent.Count;
                string s = this.listContent[t];
                if (s.Substring(0, 1) == "[")
                {
                    if (bFindSection) break; //找到下一个section
                    if (s == strSection)
                    {
                        bFindSection = true;
                        this.nLastLine = t;
                    }
                }
                else
                {
                    if (bFindSection)
                    {
                        int nEqual = s.IndexOf("=");
                        if ((nEqual <= 0) || (nEqual == s.Length - 1)) continue;
                        string s1 = s.Substring(0, nEqual).Trim();
                        string s2 = s.Substring(nEqual + 1).Trim();
                        if (s1 != strItem) continue;
                        strResult = s2;
                        break;
                    }
                }
            }
            return strResult;
        }

        public uint GetDec(string strSection, string strItem, uint nValue)
        {
            string s = this.GetString(strSection, strItem, "");
            if (s.Length == 0) return nValue;
            try
            {
                return Convert.ToUInt32(s, 10);
            }
            catch (Exception ex)
            {
                return nValue;
            }
        }

        public uint GetHex(string strSection, string strItem, uint nValue)
        {
            string s = this.GetString(strSection, strItem, "");
            if (s.Length == 0) return nValue;
            try
            {
                return Convert.ToUInt32(s, 16);
            }
            catch (Exception ex)
            {
                return nValue;
            }
        }

        public uint GetTokenCount(string strSection, string strItem, string strValue)
        {
            this.listToken.Clear();
            string s = this.GetString(strSection, strItem, "");
            if (s.Length == 0) return 0;
            string[] arr = s.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                string ss = arr[i].Trim();
                this.listToken.Add(ss);
            }
            return (uint)this.listToken.Count;
        }

        public string GetTokenString(int nIndex)
        {
            string strResult = "";
            if ((nIndex < 0) || (nIndex >= this.listToken.Count))
                return strResult;
            return this.listToken[nIndex];
        }

        public uint GetTokenHex(int nIndex)
        {
            uint nResult = 0;
            if ((nIndex < 0) || (nIndex >= this.listToken.Count))
                return nResult;
            try
            {
                return Convert.ToUInt32(this.listToken[nIndex], 16);
            }
            catch (Exception ex)
            {
                return nResult;
            }
        }

        public uint GetTokenDec(int nIndex)
        {
            uint nResult = 0;
            if ((nIndex < 0) || (nIndex >= this.listToken.Count))
                return nResult;
            try
            {
                return Convert.ToUInt32(this.listToken[nIndex], 10);
            }
            catch (Exception ex)
            {
                return nResult;
            }
        }
    }
}