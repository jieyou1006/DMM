using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GETMOOTOOL
{
    class Movie
    {
        public string Code = "";         //影片代码
        public string MovieName = "";    //名字
        public string PublishTime = "";  //发行时间
        public string Times = "";        //时长
        public string Director = "";     //导演
        public string Maker = "";        //制作商
        public string Publisher = "";    //发行商
        public string Series = "";       //系列
        public string ImgUrl = "";       //封面图url 
        public string SmallImgUrl = "";  //缩小的封面url
        public ArrayList ListType = new ArrayList(); //类型
        public ArrayList ListActerName = new ArrayList();  //演员
        public ArrayList ListActerIndexUrl = new ArrayList(); //演员主页url
        public ArrayList ListActerImgUrl = new ArrayList();  //演员头像url
        public ArrayList ListSnapshotUrl = new ArrayList();  //影片快照url
        public ArrayList ListSmallSnapshotUrl = new ArrayList();  //缩小的影片快照url
        public byte[] bImg;
        public byte[] bSmallImg;
        public List<byte[]> bShotImg = new List<byte[]>();
        public List<byte[]> bSmallShotImg = new List<byte[]>();
    }

}
