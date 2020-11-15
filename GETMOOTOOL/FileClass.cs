using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GETMOOTOOL
{
    class FileClass
    {
        private static string strPath1 = "O";
        private static string strPath2 = "P";
        private static string strActerPathd = strPath1 + ":\\Other\\{0}\\{0}.jpg";
        private static string strActerPathe = strPath2 + ":\\Other\\{0}\\{0}.jpg";
        private static string strMoviePathd = strPath1 + ":\\Other\\{0}\\{1}\\{2}.jpg";
        private static string strMoviePathe = strPath2 + ":\\Other\\{0}\\{1}\\{2}.jpg";
        private static string strMoviePathSmalld = strPath1 + ":\\Other\\{0}\\{1}\\{2}_small.jpg";
        private static string strMoviePathSmalle = strPath2 + ":\\Other\\{0}\\{1}\\{2}_small.jpg";
        private static string strMovieShotPathd = strPath1 + ":\\Other\\{0}\\{1}\\img\\{2}-{3}.jpg";
        private static string strMovieShotPathe = strPath2 + ":\\Other\\{0}\\{1}\\img\\{2}-{3}.jpg";
        private static string strMovieShotSmallPathd = strPath1 + ":\\Other\\{0}\\{1}\\img\\small\\{2}-{3}m.jpg";
        private static string strMovieShotSmallPathe = strPath2 + ":\\Other\\{0}\\{1}\\img\\small\\{2}-{3}m.jpg";

        private static string strActerUrld = "http://172.16.1.2:8000/{0}/{0}.jpg";  //http://172.16.1.2:8000/名字/名字.jpg";
        private static string strActerUrle = "http://172.16.1.2:8001/{0}/{0}.jpg";
        private static string strMovieUrld = "http://172.16.1.2:8000/{0}/{1}/{2}.jpg";  //"http://172.16.1.2:8000/名字/影片名/code.jpg";
        private static string strMovieUrle = "http://172.16.1.2:8001/{0}/{1}/{2}.jpg";
        private static string strMovieUrlSmalld = "http://172.16.1.2:8000/{0}/{1}/{2}_small.jpg";  //"http://172.16.1.2:8000/名字/影片名/code_small.jpg";
        private static string strMovieUrlSmalle = "http://172.16.1.2:8001/{0}/{1}/{2}_small.jpg";
        private static string strMovieShotUrld = "http://172.16.1.2:8000/{0}/{1}/img/{2}-{3}.jpg";  //"http://172.16.1.2:8000/名字/影片名/img/code-1.jpg";
        private static string strMovieShotUrle = "http://172.16.1.2:8001/{0}/{1}/img/{2}-{3}.jpg";
        private static string strMovieShotSmallUrld = "http://172.16.1.2:8000/{0}/{1}/img/small/{2}-{3}m.jpg";  //"http://172.16.1.2:8000/名字/影片名/img/small/code-1m.jpg";
        private static string strMovieShotSmallUrle = "http://172.16.1.2:8001/{0}/{1}/img/small/{2}-{3}m.jpg";

        /// <summary>
        /// 保存演员头像并返回url
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string SaveActerImg(string strName, string strUrl)
        {
            string strPathd = string.Format(strActerPathd, strName);
            string strPathe = string.Format(strActerPathe, strName);
            FileInfo d = new FileInfo(strPathd);  //若文件存在则直接跳过
            FileInfo e = new FileInfo(strPathe);
            if (d.Exists)
            {
                return string.Format(strActerUrld, strName);
            }
            else if (e.Exists)
            {
                return string.Format(strActerUrle, strName);
            }
            else
            {
                byte[] b = GetImgFromUrl(strPathd, strUrl, null);
                if (b != null)
                {
                    return string.Format(strActerUrld, strName);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 保存影片海报并返回url
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="strMoName"></param>
        /// <param name="strActerName"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string SaveMovieImg(ref Movie m, string strCode, string strMoName, string strActerName, string strUrl)
        {
            string strPathd = string.Format(strMoviePathd, strActerName, strMoName, strCode);
            string strPathe = string.Format(strMoviePathe, strActerName, strMoName, strCode);
            FileInfo d = new FileInfo(strPathd);  //若文件存在则直接跳过
            FileInfo e = new FileInfo(strPathe);
            if (d.Exists && m.bImg != null)
            {
                return string.Format(strMovieUrld, strActerName, strMoName, strCode);
            }
            else if (e.Exists && m.bImg != null)
            {
                return string.Format(strMovieUrle, strActerName, strMoName, strCode);
            }
            else
            {
                m.bImg = GetImgFromUrl(strPathd, strUrl, m.bImg);
                if (m.bImg != null)
                {
                    return string.Format(strMovieUrld, strActerName, strMoName, strCode);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 保存影片小海报并返回url
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="strMoName"></param>
        /// <param name="strActerName"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string SaveMovieSmallImg(ref Movie m, string strCode, string strMoName, string strActerName, string strUrl)
        {
            string strPathd = string.Format(strMoviePathSmalld, strActerName, strMoName, strCode);
            string strPathe = string.Format(strMoviePathSmalle, strActerName, strMoName, strCode);
            FileInfo d = new FileInfo(strPathd);  //若文件存在则直接跳过
            FileInfo e = new FileInfo(strPathe);
            if (d.Exists && m.bSmallImg != null)
            {
                return string.Format(strMovieUrlSmalld, strActerName, strMoName, strCode);
            }
            else if (e.Exists && m.bSmallImg != null)
            {
                return string.Format(strMovieUrlSmalle, strActerName, strMoName, strCode);
            }
            else
            {
                m.bSmallImg = GetImgFromUrl(strPathd, strUrl, m.bSmallImg);
                if (m.bSmallImg != null)
                {
                    return string.Format(strMovieUrlSmalld, strActerName, strMoName, strCode);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 保存影片大快照并返回url
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="strMoName"></param>
        /// <param name="strActerName"></param>
        /// <param name="strUrl"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string SaveMovieShotImg(ref Movie m, string strCode, string strMoName, string strActerName, string strUrl,int order)
        {
            string strPathd = string.Format(strMovieShotPathd, strActerName, strMoName, strCode, order);
            string strPathe = string.Format(strMovieShotPathe, strActerName, strMoName, strCode, order);
            FileInfo d = new FileInfo(strPathd);  //若文件存在则直接跳过
            FileInfo e = new FileInfo(strPathe);
            if (d.Exists && m.bShotImg[order - 1] != null)
            {
                return string.Format(strMovieShotUrld, strActerName, strMoName, strCode, order);
            }
            else if (e.Exists && m.bShotImg[order - 1] != null)
            {
                return string.Format(strMovieShotUrle, strActerName, strMoName, strCode, order);
            }
            else
            {
                byte[] b = GetImgFromUrl(strPathd, strUrl, m.bShotImg[order - 1]);
                if (b != null)
                {
                    m.bShotImg[order - 1] = b;
                    return string.Format(strMovieShotUrld, strActerName, strMoName, strCode, order);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 保存影片小快照并返回url
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="strMoName"></param>
        /// <param name="strActerName"></param>
        /// <param name="strUrl"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string SaveMovieSmallShotImg(ref Movie m, string strCode, string strMoName, string strActerName, string strUrl, int order)
        {
            string strPathd = string.Format(strMovieShotSmallPathd, strActerName, strMoName, strCode, order);
            string strPathe = string.Format(strMovieShotSmallPathe, strActerName, strMoName, strCode, order);
            FileInfo d = new FileInfo(strPathd);  //若文件存在则直接跳过
            FileInfo e = new FileInfo(strPathe);
            if (d.Exists && m.bSmallShotImg[order - 1] != null)
            {
                return string.Format(strMovieShotSmallUrld, strActerName, strMoName, strCode, order);
            }
            else if (e.Exists && m.bSmallShotImg[order - 1] != null)
            {
                return string.Format(strMovieShotSmallUrle, strActerName, strMoName, strCode, order);
            }
            else
            {
                byte[] b = GetImgFromUrl(strPathd, strUrl, m.bSmallShotImg[order - 1]);
                if (b != null)
                {
                    m.bSmallShotImg[order - 1] = b;
                    return string.Format(strMovieShotSmallUrld, strActerName, strMoName, strCode, order);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 根据url获取图片数据并保存到硬盘
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private byte[] GetImgFromUrl(string strPathFull, string strUrl, byte[] bFrom)
        {
            string[] strPaths = strPathFull.Split('\\');
            string strPath = "";
            for (int i = 0; i < strPaths.Length - 1; i++)
            {
                strPath += strPaths[i] + "\\";
            }

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
                if (bFrom != null)
                {
                    w.Write(bFrom);
                    return bFrom;
                }
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
            catch(Exception ex)
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
