using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DataConvert {
    class AppData {
        public static string exePath = Application.StartupPath;// exe 启动路径
        public static List<string> rootListDir = new List<string>();// 根目录列表
        public static EnumGenType genType = EnumGenType.All;// 生成的类型

        public static void init() {
            // 生成的类型
            string genType = AppCfg.getItem(AppCfg.genType);
            if (genType == null) {
                AppData.genType = EnumGenType.All;
            } else {
                AppData.genType = (EnumGenType)int.Parse(genType);
            }



        }
        public static void setGenType(EnumGenType type) {
            AppData.genType = type;
            AppCfg.setItem(AppCfg.genType, type.ToString());
        }
        public static EnumGenType getGenType() {
            return AppData.genType;
        }

        // 保存配置
        public static void saveCfg() {
            int genType = (int)(AppData.genType);
            AppCfg.setItem(AppCfg.genType, genType.ToString());
        }
    }
}
