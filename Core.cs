using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ftpdown
{
    public class Core
    {

        private string suf = "";
        private string lastBar = "";

        public void downFiles(String orderNo)
        {
            suf = getUrlSuf();
            if (AppConfig.downType == 0)
            {
                this.downByContractNo(orderNo);
            }
            else if (AppConfig.downType == 1)
            {
                this.downByOrderNo(orderNo);
            }
            else if (AppConfig.downType == 2)
            {
                this.downByBarCode(orderNo);
            }
            else if (AppConfig.downType == 3)
            {
                this.downByProject(orderNo);
            }
            suf = "";
        }

        private void downByProject(string str)
        {
            int i = 2;
            downByOrderNo(str);
            while (true)
            {
                try
                {
                    downByOrderNo(str + "_" + i.ToString().PadLeft(3, '0'));
                    i++;
                }
                catch (Exception)
                {
                    break;
                }

            }
            MessageBox.Show("已下载 " + (i - 1) + "套");
        }

        /**
         * 通过合同号下载板件mpr
         */
        private void downByContractNo(string str)
        {
            int i = str.IndexOf("_T");
            if (i == -1) throw new Exception("无法解析合同号");
            string ContractNo = str.Substring(0, i);
            int c = 0;
            try
            {
                string v = str.Substring(i + 2, 2);
                c = int.Parse(v);
            }
            catch (Exception)
            {
                throw new Exception("无法解析合同号");
            }
            for (int j = 1; j <= c; j++)
            {
                string orderNo = ContractNo + "_T" + c.ToString().PadLeft(2, '0') + "_" + j.ToString().PadLeft(2, '0');
                try
                {
                    this.downByOrderNo(orderNo);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /**
         * 通过订单号下载板件mpr
         */
        private void downByOrderNo(string orderNo)
        {
            List<string> fileNames = ftpListDir(orderNo);
            foreach (string file in fileNames)
            {
                this.downFile(orderNo, file);
            }
        }

        /**
         * 通过条形码下载板件mpr
         */
        private void downByBarCode(string barCode)
        {
            String orderNo = barCode.Substring(0, barCode.Length - 4);
            downFile(orderNo, barCode + ".mpr");
            AppConfig.fileN = 1;
            this.lastBar = barCode;
            if (AppConfig.openFile)
            {
                this.openLastFile(true);
            }
            try
            {
                downFile(orderNo, barCode + "R.mpr");
                AppConfig.fileN = 2;
            }
            catch (Exception) { }
        }

        public void openLastFile(bool f)
        {
            if (this.lastBar.Length == 0) return;
            try
            {
                if (f)
                {
                    System.Diagnostics.Process.Start(AppConfig.baseDir + "\\" + lastBar + ".mpr");
                }
                else
                {
                    System.Diagnostics.Process.Start(AppConfig.baseDir + "\\" + lastBar + "R.mpr");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("本地MPR已被删除", "错误提示");
            }

        }

        /**
         * 返回目录中的mrp文件名称列表
         */
        private List<string> ftpListDir(string orderNo)
        {
            string url = orderNo + this.suf;
            FtpWebResponse response = createFtpResStream(url, WebRequestMethods.Ftp.ListDirectory);
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string line = null;
            List<string> names = new List<string>();
            while ((line = reader.ReadLine()) != null)
            {
                if (line.EndsWith(".mpr"))
                {
                    names.Add(line);
                }
            }
            reader.Close();
            response.Close();
            return names;
        }

        private FtpWebResponse createFtpResStream(string url, string method)
        {
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(AppConfig.url + url);
            ftp.Credentials = new NetworkCredential(AppConfig.username, AppConfig.password);
            ftp.Method = method;
            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            return response;
        }

        private string getUrlSuf()
        {
            switch (AppConfig.fileType)
            {
                case 0: return "/PXM/PXM_Standard/PXM_MPR5/";
                case 1: return "/PXM/PXM_Standard/PXM_MPR6/";
                case 2: return "/PXM/PXM_Standard/PXM_MPR7/";
                default: return "";
            }
        }

        private void downFile(string orderNo, string fileName)
        {
            string url = orderNo + this.suf + "/" + fileName;
            FtpWebResponse response = createFtpResStream(url, WebRequestMethods.Ftp.DownloadFile);
            string localFile = Path.Combine(AppConfig.baseDir, fileName);
            FileStream fs = new FileStream(localFile, FileMode.Create);
            int buffer = 1024;
            byte[] b = new byte[buffer];
            int i = 0;
            Stream stream = response.GetResponseStream();
            while ((i = stream.Read(b, 0, buffer)) > 0)
            {
                fs.Write(b, 0, i);
            }
            stream.Flush();
            fs.Flush();
            stream.Close();
            fs.Close();
            response.Close();
        }
    }
}
