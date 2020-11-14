using Aim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testkzt
{
    class Program
    {
        static void Main(string[] args)
        {

            using (AimDbContext db = new AimDbContext())
            { 
               
            
            }//.Database.Connection.ConnectionString 
                //try
                //{
                //    //SqlUtil.GetConnection();
                //    JClog.WriteLog("123");
                //    SqlConnection sql1 = new SqlConnection(db.Database.Connection.ConnectionString);
                //    sql1.Open();
                //    string sql = "select Count(*) from TB_TaobaoShop";
                //    SqlCommand cmd = new SqlCommand(sql, sql1);
                //    //打开连接执行查询
                //    object result1 = cmd.ExecuteScalar();
                //    //关闭连接处理结果
                //    sql1.Close();
                //    JClog.WriteLog(result1.ToString());
                //    //var result = db.Shops.Where(m => m.AccessEnd > DateTime.Now).OrderByDescending(m => m.get_order).Select(m => new { m.id, m.AccessToken }).ToList();
                //}
                //catch (Exception ex)
                //{
                //    JClog.WriteLog("erro" + ex.Message);
                //}
                //JClog.WriteLog("333");
                //JClog.WriteLog(db.Database.Connection.ConnectionString);
               // var result = db.Shops.Where(m => m.AccessEnd > DateTime.Now).OrderByDescending(m => m.get_order).Select(m => new { m.id, m.AccessToken }).ToList();
               // JClog.WriteLog("可以了");



        }
    }
}
