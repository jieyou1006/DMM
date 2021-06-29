using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetImgByUrl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] strurls = textBox1.Text.Split(',');
            foreach (var item in strurls)
            {
                GetImgFromUrl(item);
            }

            label1.Text = "end.";
        }

        /// <summary>
        /// 根据url获取图片数据并保存到硬盘
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private byte[] GetImgFromUrl(string strUrl)
        {
            //string[] strPaths = strPathFull.Split('\\');
            //string strPath = "";
            //for (int i = 0; i < strPaths.Length - 1; i++)
            //{
            //    strPath += strPaths[i] + "\\";
            //}

            string strtimes = DateTime.Now.ToString("yyyyMMddHHmmss");
            string strms = DateTime.Now.Millisecond.ToString();
            if (strms.Length == 1)
            {
                strtimes += "00" + strms;
            }
            else if (strms.Length == 2)
            {
                strtimes += "0" + strms;
            }
            else
            {
                strtimes += strms;
            }

            string strPath = "E:\\BaiduNetdiskDownload\\";
            string strPathFull = "E:\\BaiduNetdiskDownload\\" + strtimes + ".jpg";

            DirectoryInfo dir = new DirectoryInfo(strPath);  //若路径存在则跳过，不存在则创建路径
            FileInfo file = new FileInfo(strPathFull);  //若文件存在则直接跳过，若不存在则判断路径是否存在
            if (file.Exists)
            {
                return null;
            }
            else
            {
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }

            byte[] b;
            FileStream fs = new FileStream(strPathFull, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                //若有byte数据流则直接生成图片写入对应路径
                //if (bFrom != null)
                //{
                //    w.Write(bFrom);
                //    return bFrom;
                //}
                //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                //WebResponse webResponse = webRequest.GetResponse();
                //Stream stream = webResponse.GetResponseStream();
                //using (BinaryReader br = new BinaryReader(stream))
                //{
                //    b = br.ReadBytes(2000000);  //1953 KB
                //    br.Close();
                //}
                //webResponse.Close();

                b = Getbyte(strUrl);

                w.Write(b);
                return b;
            }
            catch (Exception ex)
            {
                string strerr = ex.ToString();
                fs.Close();
                w.Close();
                return null;
            }
        }



        /// <summary>
        /// 递归抓取图片byte防止抓取的图片不全
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private byte[] Getbyte(string strUrl)
        {
            byte[] b;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(strUrl);
            WebResponse webResponse = webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(2000000);  //1953 KB
                br.Close();
            }
            webResponse.Close();

            if (b == null)
            {
                Getbyte(strUrl);
            }

            return b;
        }
    }
}
