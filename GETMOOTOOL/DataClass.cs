using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GETMOOTOOL
{
    class DataClass
    {
        DBClass db = new DBClass();

        /// <summary>
        /// 检查电影表中是否有此影片
        /// </summary>
        /// <param name="strMoCode"></param>
        /// <returns></returns>
        public bool CheckMo(string strMoCode)
        {
            string strSQL = string.Format("select count(*) as qty from c_movie where del_flag = '0' and code = '{0}'", strMoCode);
            DataTable dt = db.ReturnDataTable(strSQL);
            if (dt.Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查演员表中是否有此演员
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public bool CheckActer(string strName)
        {
            string strSQL = string.Format("select count(*) as qty from c_actresses where del_flag = '0' and (jp_name = '{0}' or cn_name = '{0}' or en_name = '{0}' or old_name = '{0}')", strName);
            DataTable dt = db.ReturnDataTable(strSQL);
            if (dt.Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 插入女演员个人信息
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="strImgUrl"></param>
        /// <returns></returns>
        public bool InsetActerInfo(Acter ac, string strImgUrl)
        {
            string strI = string.Format(@"insert into c_actresses(jp_name,birthday,age,height,cup,bust,waistline,hips,birth_place,hobby,img_url,add_date) 
values('{0}',date_format('{1}','%Y-%m-%d'),{2},{3},'{4}',{5},{6},{7},'{8}','{9}','{10}',date_format('{11}','%Y-%m-%d'))",
ac.Name,
ac.Birthday == "" ? "1900-01-01" : ac.Birthday,
ac.Age == "" ? "0" : ac.Age,
ac.Height == "" ? "0" : ac.Height,
ac.Cup == "" ? "A" : ac.Cup,
ac.Bust == "" ? "0" : ac.Bust,
ac.Waistline == "" ? "0" : ac.Waistline,
ac.Hips == "" ? "0" : ac.Hips, 
ac.BirthPlace == "" ? "noinfo" : ac.BirthPlace, 
ac.Hobby == "" ? "noinfo" : ac.Hobby, 
strImgUrl, 
DateTime.Now.ToString("yyyy-MM-dd"));
            try
            { 
                db.NonResturn(strI);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查插入导演表
        /// </summary>
        /// <param name="strName"></param>
        /// <returns>导演id</returns>
        public string CheckInsertDirector(string strName)
        {
            string strS = string.Format("select id from c_director where del_flag = '0' and name = '{0}'", strName);
            string strI = string.Format("insert into c_director(name) values('{0}')", strName);
            DataTable dt = db.ReturnDataTable(strS);
            if (dt.Rows.Count == 0)
            {
                dt = db.ReturnDataTable("select case when isnull(max(id)) then 0 else max(id) end as id from c_director");
                db.NonResturn(strI);
                return (Convert.ToInt32(dt.Rows[0][0]) + 1).ToString();
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 检查插入制作商表
        /// </summary>
        /// <param name="strName"></param>
        /// <returns>制作商id</returns>
        public string CheckInsertMaker(string strName)
        {
            string strS = string.Format("select id from c_maker where del_flag = '0' and name = '{0}'", strName);
            string strI = string.Format("insert into c_maker(name) values('{0}')", strName);
            DataTable dt = db.ReturnDataTable(strS);
            if (dt.Rows.Count == 0)
            {
                dt = db.ReturnDataTable("select case when isnull(max(id)) then 0 else max(id) end as id from c_maker");
                db.NonResturn(strI);
                return (Convert.ToInt32(dt.Rows[0][0]) + 1).ToString();
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 检查插入发行商表
        /// </summary>
        /// <param name="strName"></param>
        /// <returns>发行商id</returns>
        public string CheckInsertPublisher(string strName)
        {
            string strS = string.Format("select id from c_publisher where del_flag = '0' and name = '{0}'", strName);
            string strI = string.Format("insert into c_publisher(name) values('{0}')", strName);
            DataTable dt = db.ReturnDataTable(strS);
            if (dt.Rows.Count == 0)
            {
                dt = db.ReturnDataTable("select case when isnull(max(id)) then 0 else max(id) end as id from c_publisher");
                db.NonResturn(strI);
                return (Convert.ToInt32(dt.Rows[0][0]) + 1).ToString();
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 检查插入系列表
        /// </summary>
        /// <param name="strName"></param>
        /// <returns>系列id</returns>
        public string CheckInsertSeries(string strName)
        {
            string strS = string.Format("select id from c_series where del_flag = '0' and name = '{0}'", strName);
            string strI = string.Format("insert into c_series(name) values('{0}')", strName);
            DataTable dt = db.ReturnDataTable(strS);
            if (dt.Rows.Count == 0)
            {
                dt = db.ReturnDataTable("select case when isnull(max(id)) then 0 else max(id) end as id from c_series");
                db.NonResturn(strI);
                return (Convert.ToInt32(dt.Rows[0][0]) + 1).ToString();
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 检查插入类别表
        /// </summary>
        /// <param name="strName"></param>
        /// <returns>返回类别id</returns>
        public void CheckInsertType(string strName)
        {
            string strS = string.Format("select id from c_type where del_flag = '0' and name = '{0}'", strName);
            string strI = string.Format("insert into c_type(name) values('{0}')", strName);
            DataTable dt = db.ReturnDataTable(strS);
            if (dt.Rows.Count == 0)
            {
                db.NonResturn(strI);
            }
        }

        /// <summary>
        /// 插入影片基本信息
        /// </summary>
        /// <param name="m"></param>
        /// <param name="strDirectorid"></param>
        /// <param name="strMakerid"></param>
        /// <param name="strPublisherid"></param>
        /// <param name="strSeriesid"></param>
        /// <returns></returns>
        public bool InsertMovieInfo(Movie m, string strDirectorid, string strMakerid, string strPublisherid, string strSeriesid)
        {
            bool boolResult = false;
            string strMovieid;
            string strI = string.Format(@"insert into c_movie(code,name,publish_time,times,director_id,director,maker_id,maker,publisher_id,publisher,series_id,series,img_url,small_img_url,add_date) 
values('{0}','{1}',date_format('{2}','%Y-%m-%d'),{3},{4},'{5}',{6},'{7}',{8},'{9}',{10},'{11}','{12}','{13}',date_format('{14}','%Y-%m-%d'))",
m.Code,
m.MovieName,
m.PublishTime == "" ? "1010-01-01" : m.PublishTime,
m.Times == "" ? "0" : m.Times,
strDirectorid == "" ? "0" : strDirectorid,
m.Director,
strMakerid == "" ? "0" : strMakerid,
m.Maker,
strPublisherid == "" ? "0" : strPublisherid,
m.Publisher,
strSeriesid == "" ? "0" : strSeriesid,
m.Series,
m.ImgUrl,
m.SmallImgUrl,
DateTime.Now.ToString("yyyy-MM-dd")
);
            try
            {
                //插入电影信息
                db.NonResturn(strI);
                //获取电影id
                DataTable dt = db.ReturnDataTable(string.Format("select id from c_movie where del_flag = '0' and code = '{0}'", m.Code));
                if (dt.Rows.Count > 0)
                {
                    strMovieid = dt.Rows[0][0].ToString();

                    //写影片-类型关系表
                    foreach (var item in m.ListType)
                    {
                        DataTable dt2 = db.ReturnDataTable(string.Format("select id from c_type where del_flag = '0' and name = '{0}'", item.ToString()));
                        if(dt2.Rows.Count > 0)
                        {
                            db.NonResturn(string.Format("insert into c_type_mo(mo_id,type_id) values({0},{1})", strMovieid, dt2.Rows[0][0].ToString()));
                        }
                    }

                    //写影片-演员关系表（有演员才写）
                    if (m.ListActerName.Count > 0)
                    {
                        foreach (var item in m.ListActerName)
                        {
                            DataTable dt3 = db.ReturnDataTable(string.Format("select id from c_actresses where del_flag = '0' and (jp_name = '{0}' or cn_name = '{0}' or en_name = '{0}' or old_name = '{0}')", item.ToString()));
                            if (dt3.Rows.Count > 0)
                            {
                                db.NonResturn(string.Format("insert into c_actr_mo(actr_id,mo_id,mo_code) values({0},{1},'{2}')", dt3.Rows[0][0].ToString(), strMovieid, m.Code));
                            }
                        }
                    }

                    //写影片快照表
                    if (m.ListSnapshotUrl.Count > 0)
                    {
                        for (int i=0;i< m.ListSnapshotUrl.Count;i++)
                        {
                            boolResult = InsertMovieShot(strMovieid, m.ListSnapshotUrl[i].ToString());
                            boolResult = InsertMovieSmallShot(strMovieid, m.ListSmallSnapshotUrl[i].ToString());
                        }
                    }
                    else
                    {
                        boolResult = true;
                    }
                }
            }
            catch (Exception)
            {
                boolResult = false;
            }

            return boolResult;
        }

        public bool InsertMovieShot(string strMovieid, string strUrl)
        {
            try
            {
                db.NonResturn(string.Format("insert into c_mo_shot(mo_id,snapshot_url) values({0},'{1}')", strMovieid, strUrl));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertMovieSmallShot(string strMovieid, string strUrl)
        {
            try
            {
                db.NonResturn(string.Format("insert into c_mo_shot_small(mo_id,m_snapshot_url) values({0},'{1}')", strMovieid, strUrl));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
