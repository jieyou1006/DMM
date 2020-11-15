using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GETMOOTOOL
{ 
    class DBClass
    {
        private string strConn = "server=172.16.1.2; Port=3306; user id=jy2029; password=123456; database=db; pooling=false; charset=utf8";
        public string strerr = "";
        /// <summary>
        /// 执行SQL返回DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable ReturnDataTable(string strSql)
        {
            DataTable dt = new DataTable();
            MySqlConnection con = new MySqlConnection(strConn);
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = strSql;
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dt = ds.Tables[0];
            }
            catch(Exception ex)
            {
                strerr = ex.ToString();
                con.Close();
                con.Dispose();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return dt;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public string ReturnString(string strSql)
        {
            string strResult = "OK";
            MySqlConnection con = new MySqlConnection(strConn);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, con);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                strResult = ex.ToString();
                con.Close();
                con.Dispose();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return strResult;
        }

        /// <summary>
        /// 执行SQL不管执行结果
        /// </summary>
        /// <param name="strSql"></param>
        public void NonResturn(string strSql)
        {
            MySqlConnection con = new MySqlConnection(strConn);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strerr = ex.ToString();
                con.Close();
                con.Dispose();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }


    }
}
