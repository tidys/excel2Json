using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


public enum EnumGenType {
    All = 0,
    Server = 1,
    Client = 2,
}

namespace DataConvert {
    class AppCfg {
        public static string selectIndex = "selectIndex";
        public static string batFilePath = "batFile";
        public static string genType = "genType";// 生成的类型
        public static string localImgDir = "localImgDir";// 本地目录



        public static Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static void setItem(string key, string value) {
            System.Configuration.KeyValueConfigurationElement b = AppCfg.cfg.AppSettings.Settings[key];
            if (b == null) {
                AppCfg.cfg.AppSettings.Settings.Add(key, "0");
            }
            AppCfg.cfg.AppSettings.Settings[key].Value = value;
            AppCfg.cfg.Save();
        }
        public static string getItem(string key) {
            string s = System.Configuration.ConfigurationManager.AppSettings[key];
            if (s == null) {
                return null;
            } else {
                return s.ToString();
            }
        }
        private void init() {
            //System.Configuration.ConfigurationManager.RefreshSection(string sectionName);
           
        }

    }
}
