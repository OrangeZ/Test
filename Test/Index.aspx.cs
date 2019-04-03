﻿using System;
using System.Collections.Generic;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            slist.Add(new string[] { "A8", "马克思主义、列宁主义、毛泽东思想、邓小平理论的学习和研究", "1" });
            slist.Add(new string[] { "A81", "马克思主义的学习和研究", "2" });
            slist.Add(new string[] { "A82", "列宁主义的学习和研究", "2" });
            slist.Add(new string[] { "A83", "斯大林的思想的学习和研究", "2" });
            slist.Add(new string[] { "A84", "毛泽东思想的学习和研究", "2" });
            slist.Add(new string[] { "A849", "邓小平理论的学习和研究", "2" });
            slist.Add(new string[] { "A85", "著作汇编的学习和研究", "2" });

            foreach (var item in slist)
            {
                list.Add(new Column() { Number = item[0], Title = item[1], Count = item[2] });
            }

            rptTable.DataSource = list;
            rptTable.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTxt = txtNumber.Text;
            list = NewMethod1(searchTxt);
            rptTable.DataSource = list;
            rptTable.DataBind();
        }

        private List<Column> NewMethod1(string searchTxt)
        {
            var col = list.Where(x => x.Number.Equals(searchTxt));
            if (col.Count() <= 0)
            {
                searchTxt = searchTxt.Substring(0, searchTxt.Length - 1);
                NewMethod1(searchTxt);
            }
            else
            {
                foreach (var item in col)
                {
                    console.Text += item.Number + "\r\n";
                    item.CurrentCount++;
                }
                return col.ToList();
            }
            return col.ToList();
        }

        protected void btnSearchList_Click(object sender, EventArgs e)
        {
            string searchTxt = txtNumberList.Text.Replace("\r\n", "|");
            var txtList = searchTxt.Split('|');
            List<Column> col = new List<Column>();
            foreach (var item in txtList)
            {
                console.Text += "搜索：" + item + "\r\n";
                if (!string.IsNullOrEmpty(item))
                {
                    var l = NewMethod1(item);
                    if (l.Count > 0)
                    {
                        foreach (var item2 in l)
                        {
                            
                            col.Add(item2);
                        }
                    }
                }
            }
            rptTable.DataSource = col;
            rptTable.DataBind();
        }
    }
}