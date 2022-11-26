using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjMvc.Models
{
    public class CCustomerFactory
    {

        public void delete(int fid)
        {
            string sql = "UPDATE tCustomer SET fActived =1 WHERE fId=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fid));
            executesql(sql,paras);

        }

        public CCustomer queryById(int fid)
        {
            string sql = " SELECT * FROM tCustomer WHERE fid=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fid));
            List<CCustomer> list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];

        }

        internal List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = " SELECT * FROM tCustomer WHERE fName LIKE @K_KEYWORD";
            sql += " OR fPhone LIKE @K_KEYWORD ";
            sql += " OR fEmail LIKE @K_KEYWORD ";
            sql += " OR fAddress LIKE @K_KEYWORD ";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_KEYWORD","%"+ (object)keyword+"%") );
            return queryBySql(sql, paras);
        }

        public List<CCustomer> queryAll()
        {
            string sql = " SELECT * FROM tCustomer WHERE fActived=0";
            return queryBySql(sql, null);

        }

        public List<CCustomer> queryBySql(string sql, List<SqlParameter> paras)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CCustomer x = new CCustomer();
                x.fid = (int)reader["fid"];
                x.fName = reader["fName"].ToString();
                x.fPhone = reader["fPhone"].ToString();
                x.fAddress = reader["fAddress"].ToString();
                x.fEmail = reader["fEmail"].ToString();
                x.fPassword = reader["fPassword"].ToString();
                list.Add(x);
            }
            con.Close();
            return list;
        }


        private static void executesql(string sql, List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void update(CCustomer p)
        {
            string sql = "UPDATE tCustomer SET ";
            sql += " fName=@K_FNAME,";
            sql += " fPhone=@K_FPHONE,";
            sql += " fEmail=@K_FEMAIL,";
            sql += " fAddress=@K_FADDRESS,";
            sql += " fPassword=@K_PASSWORD,";
            sql += " fActived=@K_fACTIVED ";
            sql += " WHERE fId=@K_FID";

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)p.fid));
            paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            paras.Add(new SqlParameter("K_PASSWORD", (object)p.fPassword));
            paras.Add(new SqlParameter("K_fACTIVED", (object)p.fActived));

            executesql(sql, paras);
        }

        public void create(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer(";
            if(!string.IsNullOrEmpty(p.fName))
                sql += "fName,";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += "fPhone,";  //要新增欄位加這順位第二
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += "fEmail,";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += "fAddress,";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += "fPassword,";
                sql += "fActived,";
            sql = sql.Trim();   //前後空白省去
            if (sql.Substring(sql.Length - 1, 1) == ",")  //if後面最後面是逗號  只取 前面到最後前一個
                sql = sql.Substring(0, sql.Length - 1);
            sql += ") VALUES (";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "@K_PASSWORD,";
                paras.Add(new SqlParameter("K_PASSWORD", (object)p.fPassword));
            }
            sql += "'0',";
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " )";
            executesql(sql,paras);
        }

    }
}