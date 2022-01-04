using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace GETMOOTOOL
{
    public partial class Form1 : Form
    {
        private static string strSearchUrl = "https://avmoo.sbs/cn/search/";
        public Form1()
        {
            InitializeComponent();
            this.lbl_Count.Text = "";
            this.textBoxSearchUrl.Text = strSearchUrl;
        }

        DataClass data = new DataClass();
        FileClass f = new FileClass();
        CommClass cc = new CommClass();

        //从code查询页面开始，多个code需要以逗号分隔开来
        private void but_ok1_Click(object sender, EventArgs e)
        {
            if (textBoxCode.Text.Trim().Length < 1)
            {
                SetListBoxMessage("Code不能为空");
                return;
            }
            string[] strCodes = textBoxCode.Text.Trim().ToUpper().Split(',');
            int intCount = 0;
            foreach (string code in strCodes)
            {
                intCount++;
                lbl_Count.Text = string.Format("{0}/{1}", intCount, strCodes.Length);
                if (code.Trim().Length > 0)
                {
                    if (data.CheckMo(code.Trim()))
                    {
                        SetListBoxMessage("已经收录此影片:"+ code.Trim());
                        continue;
                    }

                    string url = "";
                    string strSmallImgUrl = "";
                    try
                    {
                        //从CODE开始

                        string urlSearch = textBoxSearchUrl.Text.Trim() + code.Trim();
                        
                        HtmlWeb web = new HtmlWeb();
                        //从url中加载
                        HtmlDocument sdoc = web.Load(urlSearch);

                        //2种方式获取搜索结果的url
                        //HtmlNodeCollection sNode = sdoc.DocumentNode.SelectNodes("//*[@id='waterfall']");
                        //url = sNode[0].SelectSingleNode(".//a").Attributes["href"].Value;
                        try
                        {
                            HtmlNode tNode = sdoc.DocumentNode.SelectSingleNode("//h4");
                            if (tNode.InnerText.Trim().Contains("搜寻没有结果"))
                            {
                                SetListBoxMessage("Code：" + code + " 没有找到");
                                continue;
                            }
                        }
                        catch (Exception)
                        {

                        }

                        //HtmlNodeCollection aNode = doc.DocumentNode.SelectNodes("//*[@class='col-md-9 screencap']");  //*号代表通配符，表示所有class为此名的节点
                        //HtmlNode img = aNode[0].SelectSingleNode(".//img");
                        //strImgUrl = aNode[0].SelectSingleNode(".//img").Attributes["src"].Value;  //img.Attributes["src"].Value;

                        //HtmlNode sNode = sdoc.DocumentNode.SelectSingleNode("//div[@id='waterfall']");
                        HtmlNodeCollection nodes = sdoc.DocumentNode.SelectNodes("//a[@class='movie-box']");
                        foreach (var item in nodes)
                        {
                            HtmlNode sNode = item.SelectSingleNode(".//date");
                            if (sNode.InnerHtml.Trim().ToUpper() == code.Trim())
                            {
                                //HtmlNodeCollection htmlNodesUrl = item.SelectNodes(".//href");
                                url = item.Attributes["href"].Value;

                                //HtmlNodeCollection htmlNodesIndexImgUrl = item.SelectNodes(".//img");
                                strSmallImgUrl = item.SelectSingleNode(".//img").Attributes["src"].Value;//htmlNodesIndexImgUrl[0].Attributes["src"].Value;
                                break;
                            }
                        }
                        //HtmlNodeCollection htmlNodesUrl = sNode.SelectNodes(".//a");
                        //HtmlNodeCollection htmlNodesIndexImgUrl = sNode.SelectNodes(".//img");
                        //url = htmlNodesUrl[0].Attributes["href"].Value;
                        //strSmallImgUrl = htmlNodesIndexImgUrl[0].Attributes["src"].Value;

                        SetListBoxMessage(url);
                        SetListBoxMessage(strSmallImgUrl);
                    }
                    catch (Exception)
                    {
                        SetListBoxMessage("CODE没有搜索到结果，中断执行。");
                        return;
                    }

                    GetMovieIndexHtmlInfo(url, strSmallImgUrl);
                }


            }


        }

        //从影片主页开始
        private void but_ok2_Click(object sender, EventArgs e)
        {
            //从影片主页开始
            if (textBoxUrl.Text.Trim().Length < 1)
            {
                SetListBoxMessage("URL不能为空");
                return;
            }
            GetMovieIndexHtmlInfo(textBoxUrl.Text.Trim(), "");
        }

        /// <summary>
        /// GetMovieIndexPageInfo
        /// </summary>
        /// <param name="strUrl"></param>
        private void GetMovieIndexHtmlInfo(string strUrl, string strSmallImgUrl)
        {
            bool boolGetActerInfo = false;
            string strNoNameActer = "NoName";
            Movie m = new Movie();
            m.SmallImgUrl = strSmallImgUrl;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(cc.CheckUrl(strUrl));  //加载影片主页

            //获取影片代码和影片名字
            HtmlNode tNode = doc.DocumentNode.SelectSingleNode("//h3");

            //有些字符不能作为文件名使用，需要替换掉
            string[] strNonFileNames = new string[] { "?", "*", ":", "<", ">", "\\", "/", "|", "\"" };
            string strMovieNameTemp = tNode.InnerText;
            foreach (string strchar in strNonFileNames)
            {
                strMovieNameTemp = strMovieNameTemp.Replace(strchar, " ");
            }

            m.MovieName = strMovieNameTemp;  //用空格替换不能做文件名的字符
            m.Code = m.MovieName.Split(' ')[0];  //这里提前获取code以便判断是否已经获取过了，提前结束
            if (data.CheckMo(m.Code))
            {
                SetListBoxMessage("已经收录此影片");
                return;
            }

            SetListBoxMessage(m.MovieName);
            SetListBoxMessage(m.Code);

            //2种方式获取影片的海报
            //HtmlNodeCollection aNode = doc.DocumentNode.SelectNodes("//*[@class='col-md-9 screencap']");  //*号代表通配符，表示所有class为此名的节点
            //HtmlNode img = aNode[0].SelectSingleNode(".//img");
            //strImgUrl = aNode[0].SelectSingleNode(".//img").Attributes["src"].Value;  //img.Attributes["src"].Value;
            HtmlNode aNode = doc.DocumentNode.SelectSingleNode("//div[@class='col-md-9 screencap']");
            HtmlNodeCollection htmlNodesImgUrl = aNode.SelectNodes(".//img");
            m.ImgUrl = htmlNodesImgUrl[0].Attributes["src"].Value;
            SetListBoxMessage(m.ImgUrl);

            //获取影片的基本信息
            //HtmlNodeCollection bNode = doc.DocumentNode.SelectNodes("//*[@class='col-md-3 info']");
            HtmlNode bNode = doc.DocumentNode.SelectSingleNode("//div[@class='col-md-3 info']");
            HtmlNodeCollection bCollection = bNode.ChildNodes;

            string[] strsAllData = new string[100];
            int icount = 0;
            foreach (var item in bCollection)
            {
                if (item.ChildNodes.Count > 0)
                {
                    for (int i = 0; i < item.ChildNodes.Count; i++)
                    {
                        if (item.ChildNodes[i].InnerHtml.Trim().Length > 0)
                        {
                            string[] strs = item.ChildNodes[i].InnerHtml.Trim().Split('>');
                            if (strs.Length > 1)
                            {
                                SetListBoxMessage(strs[1].Trim().Replace("</a", ""));
                                strsAllData[icount] = strs[1].Trim().Replace("</a", "");
                                icount++;
                            }
                            else
                            {
                                SetListBoxMessage(item.ChildNodes[i].InnerHtml.Trim().Replace(":", ""));
                                strsAllData[icount] = item.ChildNodes[i].InnerHtml.Trim().Replace(":", "");
                                icount++;
                            }
                        }
                    }
                }
            }

            if (strsAllData.Length > 0)
            {
                for (int i = 0; i < strsAllData.Length; i++)
                {
                    switch (strsAllData[i])
                    {
                        case "识别码":
                            m.Code = strsAllData[i + 1];
                            break;
                        case "发行时间":
                            m.PublishTime = strsAllData[i + 1];
                            break;
                        case "长度":
                            m.Times = strsAllData[i + 1].Replace("分钟", "");
                            break;
                        case "导演":
                            m.Director = strsAllData[i + 1];
                            break;
                        case "制作商":
                            m.Maker = strsAllData[i + 1];
                            break;
                        case "发行商":
                            m.Publisher = strsAllData[i + 1];
                            break;
                        case "系列":
                            m.Series = strsAllData[i + 1];
                            break;
                        case "类别":
                            for (int j = i + 1; j < strsAllData.Length + 1; j++)
                            {
                                if (strsAllData[j] == null)
                                {
                                    break;
                                }
                                m.ListType.Add(strsAllData[j]);
                            }
                            break;
                        default:
                            break;

                    }
                }
            }

            try
            {
                //获取女演员的姓名、主页和照片
                HtmlNode cNode = doc.DocumentNode.SelectSingleNode("//div[@id='avatar-waterfall']");
                HtmlNodeCollection htmlNodesActerIndex = cNode.SelectNodes(".//a");  //获取下面的全部a标签
                HtmlNodeCollection htmlNodesActerImg = cNode.SelectNodes(".//img");  //获取下面的全部img标签
                HtmlNodeCollection htmlNodesActerName = cNode.SelectNodes(".//span");//获取下面的全部span标签

                foreach (var item in htmlNodesActerIndex)
                {
                    m.ListActerIndexUrl.Add(item.Attributes["href"].Value);
                    SetListBoxMessage(item.Attributes["href"].Value);
                }
                foreach (var item in htmlNodesActerImg)
                {
                    m.ListActerImgUrl.Add(item.Attributes["src"].Value);
                    SetListBoxMessage(item.Attributes["src"].Value);
                }
                foreach (var item in htmlNodesActerName)
                {
                    m.ListActerName.Add(item.InnerHtml.Trim());
                    SetListBoxMessage(item.InnerHtml.Trim());
                }
            }
            catch (Exception)
            {
                boolGetActerInfo = true;
                SetListBoxMessage("无演员名单");
            }

            try
            {
                //获取影片快照
                HtmlNodeCollection dNode = doc.DocumentNode.SelectNodes("//a[@class='sample-box']");
                foreach (var item in dNode)
                {
                    m.ListSnapshotUrl.Add(item.Attributes["href"].Value);
                    SetListBoxMessage(item.Attributes["href"].Value);
                }
            }
            catch (Exception)
            {
                SetListBoxMessage("无影片快照");
            }

            try
            {
                //获取缩小的影片快照
                HtmlNode eNode = doc.DocumentNode.SelectSingleNode("//div[@id='sample-waterfall']");
                HtmlNodeCollection htmlNodesShort = eNode.SelectNodes(".//img");  //获取下面的全部img标签
                foreach (var item in htmlNodesShort)
                {
                    m.ListSmallSnapshotUrl.Add(item.Attributes["src"].Value);
                    SetListBoxMessage(item.Attributes["src"].Value);
                }
            }
            catch (Exception)
            {
                SetListBoxMessage("无影片快照");
            }

            ////////////////////////////////////////////
            ////所有获取到的演员和影片信息插入数据库////
            ////////////////////////////////////////////

            //这里需要在数据库中判断表中是否有女演员信息，若没有的话，需要循环抓取插入
            //bool boolGetimg = true;
            if (m.ListActerName.Count > 0)
            {
                for (int i = 0; i < m.ListActerName.Count; i++)
                {
                    if (data.CheckActer(m.ListActerName[i].ToString()))  //判断是否有此女演员资料,若无，则进入演员主页抓取资料插入表中
                    {
                        boolGetActerInfo = true;
                    }
                    else
                    {
                        boolGetActerInfo = GetActerIndexHtmlInfo(m.ListActerName[i].ToString(), cc.CheckUrl(m.ListActerImgUrl[i].ToString()), cc.CheckUrl(m.ListActerIndexUrl[i].ToString()));
                        if (!boolGetActerInfo)
                        {
                            SetListBoxMessage("获取女演员：" + m.ListActerName[i].ToString() + " 资料失败");
                            break;
                        }
                    }

                    if (boolGetActerInfo)
                    {
                        //开始获取影片海报和快照图片，并写入到硬盘返回本地网站url更新list，
                        //由于多演员的影片每个演员专辑下都要保存图片，所以获取大小封面的方法要多跑
                        if (m.ImgUrl.Length > 0)
                        {
                            m.ImgUrl = f.SaveMovieImg(ref m, m.Code, m.MovieName, m.ListActerName[i].ToString(), m.ImgUrl);
                        }
                        if (m.SmallImgUrl.Length > 0)
                        {
                            m.SmallImgUrl = f.SaveMovieSmallImg(ref m, m.Code, m.MovieName, m.ListActerName[i].ToString(), m.SmallImgUrl);
                        }

                        for (int k = 0; k < m.ListSnapshotUrl.Count; k++)
                        {
                            m.bShotImg.Add(null);
                            m.bSmallShotImg.Add(null);
                            if (m.ListSnapshotUrl[k].ToString().Length > 0)
                            {
                                m.ListSnapshotUrl[k] = f.SaveMovieShotImg(ref m, m.Code, m.MovieName, m.ListActerName[i].ToString(), m.ListSnapshotUrl[k].ToString(), k + 1);
                            }
                            if (m.ListSmallSnapshotUrl[k].ToString().Length > 0)
                            {
                                m.ListSmallSnapshotUrl[k] = f.SaveMovieSmallShotImg(ref m, m.Code, m.MovieName, m.ListActerName[i].ToString(), m.ListSmallSnapshotUrl[k].ToString(), k + 1);
                            }
                        }

                    }
                }
            }
            else
            {
                //开始获取影片海报和快照图片，并写入到硬盘返回本地网站url更新list（无演员的情况）
                if (m.ImgUrl.Length > 0)
                {
                    m.ImgUrl = f.SaveMovieImg(ref m, m.Code, m.MovieName, strNoNameActer, m.ImgUrl);
                }
                if (m.SmallImgUrl.Length > 0)
                {
                    m.SmallImgUrl = f.SaveMovieSmallImg(ref m, m.Code, m.MovieName, strNoNameActer, m.SmallImgUrl);
                }

                for (int k = 0; k < m.ListSnapshotUrl.Count; k++)
                {
                    m.bShotImg.Add(null);
                    m.bSmallShotImg.Add(null);
                    if (m.ListSnapshotUrl[k].ToString().Length > 0)
                    {
                        m.ListSnapshotUrl[k] = f.SaveMovieShotImg(ref m, m.Code, m.MovieName, strNoNameActer, m.ListSnapshotUrl[k].ToString(), k + 1);
                    }
                    if (m.ListSmallSnapshotUrl[k].ToString().Length > 0)
                    {
                        m.ListSmallSnapshotUrl[k] = f.SaveMovieSmallShotImg(ref m, m.Code, m.MovieName, strNoNameActer, m.ListSmallSnapshotUrl[k].ToString(), k + 1);
                    }
                }
            }

            if (boolGetActerInfo)
            {
                //开始写入影片信息到数据表
                string strDirectorid = "";
                string strMakerid = "";
                string strPublisherid = "";
                string strSeriesid = "";

                //获取影片各属性id
                if (m.Director.Length > 0)
                {
                    strDirectorid = data.CheckInsertDirector(m.Director);
                }
                if (m.Maker.Length > 0)
                {
                    strMakerid = data.CheckInsertMaker(m.Maker);
                }
                if (m.Publisher.Length > 0)
                {
                    strPublisherid = data.CheckInsertPublisher(m.Publisher);
                }
                if (m.Series.Length > 0)
                {
                    strSeriesid = data.CheckInsertSeries(m.Series);
                }
                if (m.ListType.Count > 0)
                {
                    foreach (var item in m.ListType)
                    {
                        data.CheckInsertType(item.ToString());
                    }
                }

                if (data.InsertMovieInfo(m, strDirectorid, strMakerid, strPublisherid, strSeriesid))
                {
                    SetListBoxMessage("影片抓取成功");
                }
            }

            SetListBoxMessage("");
        }

        /// <summary>
        /// GetActerIndexPageInfo
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strPhotoUrl"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private bool GetActerIndexHtmlInfo(string strName, string strPhotoUrl, string strIndexUrl)
        {
            bool boolResult = false;
            Acter ac = new Acter();
            ac.Name = strName;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(strIndexUrl);  //加载女演员主页

            try
            {
                HtmlNode aNode = doc.DocumentNode.SelectSingleNode("//div[@class='photo-info']");
                HtmlNodeCollection htmlNodesActerInfo = aNode.SelectNodes(".//p");  //获取下面的全部p标签
                foreach (var item in htmlNodesActerInfo)
                {
                    if (item.InnerText.Trim().Length > 1)
                    {
                        if (item.InnerText.Trim().Contains("生日"))
                        {
                            ac.Birthday = item.InnerText.Trim().Split(':')[1].Trim();
                            SetListBoxMessage(ac.Birthday);
                        }
                        else if (item.InnerText.Trim().Contains("年龄"))
                        {
                            ac.Age = item.InnerText.Trim().Split(':')[1].Trim();
                            SetListBoxMessage(ac.Age);
                        }
                        else if (item.InnerText.Trim().Contains("身高"))
                        {
                            ac.Height = item.InnerText.Trim().Split(':')[1].Trim().Replace("cm", "");
                            SetListBoxMessage(ac.Height);
                        }
                        else if (item.InnerText.Trim().Contains("罩杯"))
                        {
                            ac.Cup = item.InnerText.Trim().Split(':')[1].Trim();
                            SetListBoxMessage(ac.Cup);
                        }
                        else if (item.InnerText.Trim().Contains("胸围"))
                        {
                            ac.Bust = item.InnerText.Trim().Split(':')[1].Trim().Replace("cm", "");
                            SetListBoxMessage(ac.Bust);
                        }
                        else if (item.InnerText.Trim().Contains("腰围"))
                        {
                            ac.Waistline = item.InnerText.Trim().Split(':')[1].Trim().Replace("cm", "");
                            SetListBoxMessage(ac.Waistline);
                        }
                        else if (item.InnerText.Trim().Contains("臀围"))
                        {
                            ac.Hips = item.InnerText.Trim().Split(':')[1].Trim().Replace("cm", "");
                            SetListBoxMessage(ac.Hips);
                        }
                        else if (item.InnerText.Trim().Contains("出生地"))
                        {
                            ac.BirthPlace = item.InnerText.Trim().Split(':')[1].Trim();
                            SetListBoxMessage(ac.BirthPlace);
                        }
                        else if (item.InnerText.Trim().Contains("爱好"))
                        {
                            ac.Hobby = item.InnerText.Trim().Split(':')[1].Trim();
                            SetListBoxMessage(ac.Hobby);
                        }
                    }
                }

                //女演员的头像下载并保存，并返回本地网址url路径
                strPhotoUrl = f.SaveActerImg(strName, strPhotoUrl);
                //女演员资料插入数据库返回布尔值
                boolResult = data.InsetActerInfo(ac, strPhotoUrl);

            }
            catch(Exception ex)
            {
                SetListBoxMessage("获取女演员基本资料时出错:" + ex.ToString());
            }

            return boolResult;
        }

        //ListBox输出信息
        internal void SetListBoxMessage(string str)
        {
            if (listBoxResult.InvokeRequired)
            {
                Action<string> actionDelegate = (x) =>
                {
                    listBoxResult.Items.Add(str);
                    listBoxResult.TopIndex = listBoxResult.Items.Count - (int)(listBoxResult.Height / listBoxResult.ItemHeight);
                };
                listBoxResult.Invoke(actionDelegate, str);
            }
            else
            {
                listBoxResult.Items.Add(str);
                listBoxResult.TopIndex = listBoxResult.Items.Count - (int)(listBoxResult.Height / listBoxResult.ItemHeight);
            }
        }
    }
}
