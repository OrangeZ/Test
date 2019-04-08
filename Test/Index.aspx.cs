using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public class Column
    {
        public Column()
        {
            CurrentCount = 0;
        }

        public string Number { get; set; }
        public string Title { get; set; }
        public string Count { get; set; }
        public int CurrentCount { get; set; }
    }

    public partial class Index : System.Web.UI.Page
    {
        List<Column> list = new List<Column>();
        List<string[]> slist = new List<string[]>();
        List<Column> coll = new List<Column>();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetExcelData();
            //NewMethod();
        }

        //private void NewMethod()
        //{
        //    slist.Add(new string[] { "A8", "马克思主义、列宁主义、毛泽东思想、邓小平理论的学习和研究", "1" });
        //    slist.Add(new string[] { "A81", "马克思主义的学习和研究", "2" });
        //    slist.Add(new string[] { "A82", "列宁主义的学习和研究", "2" });
        //    slist.Add(new string[] { "A83", "斯大林的思想的学习和研究", "2" });
        //    slist.Add(new string[] { "A84", "毛泽东思想的学习和研究", "2" });
        //    slist.Add(new string[] { "A849", "邓小平理论的学习和研究", "2" });
        //    slist.Add(new string[] { "A85", "著作汇编的学习和研究", "2" });

        //    foreach (var item in slist)
        //    {
        //        list.Add(new Column() { Number = item[0], Title = item[1], Count = item[2] });
        //    }

        //    rptTable.DataSource = list;
        //    rptTable.DataBind();
        //}

        private List<Column> NewMethod1(string dsdsd)
        {
            var col = list.Where(x => x.Number.ToUpper().Equals(dsdsd.ToUpper())).ToList();
            if (col.Count <= 0)
            {
                console.Text += "搜索:[" + dsdsd + "] 结果:未找到\r\n";
                if (dsdsd.Length > 1)
                {
                    dsdsd = dsdsd.Substring(0, dsdsd.Length - 1);
                    col = NewMethod1(dsdsd);
                }
                else
                {
                    console.Text += "搜索结束\r\n\r\n";
                }

                return col;
            }
            else
            {
                foreach (var item in col)
                {
                    console.Text += "搜索:[" + item.Number + "] 结果:找到了\r\n";
                    item.CurrentCount++;
                }
                console.Text += "搜索结束\r\n\r\n";
                return col;
            }
        }

        protected void btnSearchList_Click(object sender, EventArgs e)
        {
            console.Text = "";
            var startT = DateTime.Now;
            if (!string.IsNullOrEmpty(txtNumberList.Text.Trim()))
            {
                console.Visible = true;
                string searchTxt = txtNumberList.Text.Replace("\r\n", "|");
                var txtList = searchTxt.Split('|');
                List<Column> col = new List<Column>();
                foreach (var item in txtList)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        console.Text += "搜索:[" + item + "] 开始\r\n";
                        var l = NewMethod1(item);
                        if (l.Count > 0)
                        {
                            foreach (var item2 in l)
                            {
                                col.Add(item2);
                            }
                        }
                        else
                        {

                        }
                    }
                }

                var endT = DateTime.Now;
                var timespan = endT - startT;

                rptTable.DataSource = col;
                rptTable.DataBind();
            }
        }

        private void GetExcelData()
        {
            string strPath = Server.MapPath("/Content/DOC/test.xlt");
            DataSet ds = GetExcelData(strPath);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    list.Add(new Column()
                    {
                        Number = item["类号"].ToString(),
                        Title = item["类目名称"].ToString(),
                        Count = item["类目等级"].ToString(),
                    });
                }
            }
        }

        /// <summary>
        /// 唯一需要注意的是，如果目标机器的操作系统，是64位的话。
        /// 项目需要 编译为 x86，而不是简单的使用默认的 Any CPU.
        /// </summary>
        /// <param name="strExcelFileName"></param>
        /// <returns></returns>
        private string GetOleDbConnectionString(string strExcelFileName)
        {
            // Office 2007 以及 以下版本使用.
            string strJETConnString =
              String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'", strExcelFileName);
            // xlsx 扩展名 使用.
            string strASEConnXlsxString =
              String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"", strExcelFileName);
            // xls 扩展名 使用.
            string strACEConnXlsString =
              String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES\"", strExcelFileName);
            //其他
            string strOtherConnXlsString =
              String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'", strExcelFileName);

            //尝试使用 ACE. 假如不发生错误的话，使用 ACE 驱动.
            try
            {
                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(strACEConnXlsString);
                cn.Open();
                cn.Close();
                // 使用 ACE
                return strACEConnXlsString;
            }
            catch (Exception)
            {
                // 启动 ACE 失败.
            }

            // 尝试使用 Jet. 假如不发生错误的话，使用 Jet 驱动.
            try
            {
                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(strJETConnString);
                cn.Open();
                cn.Close();
                // 使用 Jet
                return strJETConnString;
            }
            catch (Exception)
            {
                // 启动 Jet 失败.
            }

            // 尝试使用 Jet. 假如不发生错误的话，使用 Jet 驱动.
            try
            {
                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(strASEConnXlsxString);
                cn.Open();
                cn.Close();
                // 使用 Jet
                return strASEConnXlsxString;
            }
            catch (Exception)
            {
                // 启动 Jet 失败.
            }
            // 假如 ACE 与 JET 都失败了，默认使用 JET.
            return strOtherConnXlsString;
        }

        /// <summary>
        /// 获取Excel数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private DataSet GetExcelData(string strFilePath)
        {
            try
            {
                //获取连接字符串
                // @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;HDR=YES;";
                string strConn = GetOleDbConnectionString(strFilePath);

                DataSet ds = new DataSet();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    //打开连接
                    conn.Open();
                    System.Data.DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    // 取得Excel工作簿中所有工作表  
                    System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    OleDbDataAdapter sqlada = new OleDbDataAdapter();

                    foreach (DataRow dr in schemaTable.Rows)
                    {
                        try
                        {
                            string strSql = "Select * From [" + dr[2].ToString().Trim() + "]";
                            if (strSql.Contains("$"))
                            {
                                OleDbCommand objCmd = new OleDbCommand(strSql, conn);
                                sqlada.SelectCommand = objCmd;
                                sqlada.Fill(ds, dr[2].ToString().Trim());
                            }
                        }
                        catch { }
                    }
                    //关闭连接
                    conn.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "err", "alert('" + ex.Message + "');", false);
                return null;
            }
        }

        protected void btnSearchList_Click1(object sender, EventArgs e)
        {
            txtNumberList.Text = "";
            console.Visible = false;
            rptTable.DataSource = list;
            rptTable.DataBind();
        }


        protected string GetTempT(string t)
        {
            int d = int.Parse(t);
            string s = "";
            for (var i = 0; i < d; i++)
            {
                s += "&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            return s;
        }
    }
}