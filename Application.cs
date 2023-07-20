using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ftpdown
{
    internal static class Application
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 加载系统配置
            AppConfig.downType = Properties.Settings.Default.downType;
            AppConfig.fileType = Properties.Settings.Default.fileType;

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new AppForm());
        }
    }
}
