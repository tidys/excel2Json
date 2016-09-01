using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace DataConvert {
    class AppCfg {
        public static string selectIndex = "selectIndex";
        public static string batFilePath = "batFile";


        public static Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static void addItem(string key, string value) {
            System.Configuration.KeyValueConfigurationElement b = AppCfg.cfg.AppSettings.Settings[key];
            if (b == null) {
                AppCfg.cfg.AppSettings.Settings.Add(key, "0");
            }
            AppCfg.cfg.AppSettings.Settings[key].Value = value;
            AppCfg.cfg.Save();
        }
        public static string getItem(string key) {
            string s = System.Configuration.ConfigurationSettings.AppSettings[key];
            if (s == null) {
                return "";
            } else {
                return s.ToString();
            }
        }
        private void init() {
            //System.Configuration.ConfigurationManager.RefreshSection(string sectionName);
        }

    }
}
