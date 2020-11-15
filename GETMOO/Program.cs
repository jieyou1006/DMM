using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GETMOO
{
    class Program
    {
        static void Main(string[] args)
        {
            string strCode = "";         //影片代码
            string strMovieName = "";    //名字
            string strPublishTime = "";  //发行时间
            string strTimes = "";        //时长
            string strDirector = "";     //导演
            string strMaker = "";        //制作商
            string strPublisher = "";    //发行商
            string strSeries = "";       //系列
            string strImgUrl = "";       //封面图url 
            string strSmallImgUrl = "";  //缩小的封面url
            ArrayList listType = new ArrayList(); //类型
            ArrayList listActerName = new ArrayList();  //演员
            ArrayList listActerIndexUrl = new ArrayList(); //演员主页url
            ArrayList listActerImgUrl = new ArrayList();  //演员头像url
            ArrayList listSnapshotUrl = new ArrayList();  //影片快照url
            ArrayList listSmallSnapshotUrl = new ArrayList();  //缩小的影片快照url


            string urlSearch = "https://avmask.com/cn/search/ATID-394";
            string url = "https://avmask.com/cn/movie/b655b2cc19e43c81";
            HtmlWeb web = new HtmlWeb();
            //从url中加载
            HtmlDocument sdoc = web.Load(urlSearch);

            try
            {
                //2种方式获取搜索结果的url
                //HtmlNodeCollection sNode = sdoc.DocumentNode.SelectNodes("//*[@id='waterfall']");
                //url = sNode[0].SelectSingleNode(".//a").Attributes["href"].Value;
                HtmlNode sNode = sdoc.DocumentNode.SelectSingleNode("//div[@id='waterfall']");
                HtmlNodeCollection htmlNodesUrl = sNode.SelectNodes(".//a");
                HtmlNodeCollection htmlNodesIndexImgUrl = sNode.SelectNodes(".//img");
                url = htmlNodesUrl[0].Attributes["href"].Value;
                strSmallImgUrl = htmlNodesIndexImgUrl[0].Attributes["src"].Value;
            }
            catch(Exception ex)
            {
                Console.WriteLine("CODE没有搜索到结果，中断执行。");
                return;
            }

            HtmlDocument doc = web.Load(url);  //加载影片主页
   
            //获取影片代码和影片名字
            HtmlNode tNode = doc.DocumentNode.SelectSingleNode("//h3");
            strMovieName = tNode.InnerText;
            strCode = strMovieName.Split(' ')[0];  //这里提前获取code以便判断是否已经获取过了，提前结束
            Console.WriteLine(strMovieName);
            Console.WriteLine(strCode);

            //2种方式获取影片的海报
            //HtmlNodeCollection aNode = doc.DocumentNode.SelectNodes("//*[@class='col-md-9 screencap']");  //*号代表通配符，表示所有class为此名的节点
            //HtmlNode img = aNode[0].SelectSingleNode(".//img");
            //strImgUrl = aNode[0].SelectSingleNode(".//img").Attributes["src"].Value;  //img.Attributes["src"].Value;
            HtmlNode aNode = doc.DocumentNode.SelectSingleNode("//div[@class='col-md-9 screencap']");
            HtmlNodeCollection htmlNodesImgUrl = aNode.SelectNodes(".//img");
            strImgUrl = htmlNodesImgUrl[0].Attributes["src"].Value;
            Console.WriteLine(strImgUrl);

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
                                Console.WriteLine(strs[1].Trim().Replace("</a", ""));
                                strsAllData[icount] = strs[1].Trim().Replace("</a", "");
                                icount++;
                            }
                            else
                            {
                                Console.WriteLine(item.ChildNodes[i].InnerHtml.Trim().Replace(":", ""));
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
                            strCode = strsAllData[i + 1];
                            break;
                        case "发行时间":
                            strPublishTime = strsAllData[i + 1];
                            break;
                        case "长度":
                            strTimes = strsAllData[i + 1].Replace("分钟", "");
                            break;
                        case "导演":
                            strDirector = strsAllData[i + 1];
                            break;
                        case "制作商":
                            strMaker = strsAllData[i + 1];
                            break;
                        case "发行商":
                            strPublisher = strsAllData[i + 1];
                            break;
                        case "系列":
                            strSeries = strsAllData[i + 1];
                            break;
                        case "类别":
                            for (int j = i + 1; j < strsAllData.Length + 1; j++)
                            {
                                if (strsAllData[j] == null)
                                {
                                    break;
                                }
                                listType.Add(strsAllData[j]);
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
                    listActerIndexUrl.Add(item.Attributes["href"].Value);
                    Console.WriteLine(item.Attributes["href"].Value);
                }
                foreach (var item in htmlNodesActerImg)
                {
                    listActerImgUrl.Add(item.Attributes["src"].Value);
                    Console.WriteLine(item.Attributes["src"].Value);
                }
                foreach (var item in htmlNodesActerName)
                {
                    listActerName.Add(item.InnerHtml.Trim());
                    Console.WriteLine(item.InnerHtml.Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无演员名单");
            }

 
            try
            {
                //获取影片快照
                HtmlNodeCollection dNode = doc.DocumentNode.SelectNodes("//a[@class='sample-box']");
                foreach (var item in dNode)
                {
                    listSnapshotUrl.Add(item.Attributes["href"].Value);
                    Console.WriteLine(item.Attributes["href"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无影片快照");
            }

            try
            {
                //获取缩小的影片快照
                HtmlNode eNode = doc.DocumentNode.SelectSingleNode("//div[@id='sample-waterfall']");
                HtmlNodeCollection htmlNodesShort = eNode.SelectNodes(".//img");  //获取下面的全部img标签
                foreach(var item in htmlNodesShort)
                {
                    listSmallSnapshotUrl.Add(item.Attributes["src"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无影片快照");
            }

            Console.ReadKey();
        }
    }
}
