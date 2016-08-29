using BPMQuestionnaire.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPMQuestionnaire
{
    public partial class Statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
               if (!IsPostBack)
            {
                DataList1.DataSource = SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_Question ", null);
                DataList1.DataBind();
            }
        
        }

        protected string GetList(int QID, int QType,String Title)
        {
            string outHtml = string.Empty;

            //获取ListItme
            string listItem = string.Empty;
            foreach (DataRow item in SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_QuList where Q_ID=" + QID, null).Tables[0].Rows)
            {
                listItem += "'" + item["Q_ListText"] + "', ";
            }
            listItem=listItem.Substring(0, listItem.Length - 1);

            //获取答案得票数
            string listItemValue = string.Empty;
            Dictionary<int,int> Dic=new Dictionary<int,int>();
            foreach (DataRow item in SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_QuList where Q_ID=" + QID, null).Tables[0].Rows)
            {
                Dic.Add(Convert.ToInt32(item["ID"]), 0);
            }

            foreach (DataRow item in SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_AnList where Q_ID=" + QID, null).Tables[0].Rows)
            {
                String[] anLists = item["QL_ID"].ToString().Split('Q');
              
                for (int i = 1; i < anLists.Length; i++)
                {
                    int ival = Convert.ToInt32(anLists[i].Substring(5, anLists[i].Length-5));
                    Dic[ival] = Dic[ival]+1;
                }
            }


            foreach (var item in Dic)
            {
                listItemValue += item.Value + ", ";
            }
            listItemValue = listItemValue.Substring(0, listItemValue.Length - 1);

            outHtml = "<div style=\"width:700px;margin:20px auto 0 auto;\"><div id=\"chart_combo" + QID + "\" class=\"chart_combo" + QID + "\"></div></div><script type=\"text/javascript\">    var chart;    $(function () {        chart = new Highcharts.Chart({            chart: {                renderTo: 'chart_combo" + QID + "'             },            title: {                 text: '" + Title + "'            },            xAxis: {                 categories: [" + listItem + "],                 labels: { y: 18 }            },            yAxis: {                  title: { text: '获得票数' },                 lineWidth: 2             },            exporting: {                enabled: false              },            series: [{                type: 'column',                name: '获得票数',                data: [" + listItemValue + "]            },            ]        });    });</script>";

            return outHtml;
        }
    }
}