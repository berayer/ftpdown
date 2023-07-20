using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ftpdown
{
    public class AppConfig
    {
        public static readonly string baseDir = "D:\\ww4\\a1\\mp4";
        public static readonly string username = "ptp160";
        public static readonly string password = "abc.123456";
        public static readonly string url = "ftp://172.16.30.253/imorder/";

        // 下载方式类型: 0=合同;1=订单;2=板件;3=合同;
        public static int downType = 2;
        // 文件下载类型: 0=MPR5;1=MPR6;2=MPR7;
        public static int fileType = 1;
        // 条形码下载后是否打开文件
        public static bool openFile = false;
        public static int fileN = 0;

        public static void switchDownType(int param)
        {
            Properties.Settings.Default.downType = param;
            Properties.Settings.Default.Save();
            downType = param;
        }

        public static void switchFileType(int param)
        {
            Properties.Settings.Default.fileType = param;
            Properties.Settings.Default.Save();
            fileType = param;
        }

    }
}
