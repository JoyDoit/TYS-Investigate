using BPMQuestionnaire.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPMQuestionnaire
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataList1.DataSource = SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_Question ", null);
                DataList1.DataBind();

                //获取用户信息
                userinfo.Value = GetMac();
            }
        }

        protected string GetList(int QID,int QType,int HaveMark)
        {
            String outHtml = String.Empty;
            //QType为0问题为单选，值为1为多选
            if (QType == 0)
            {
                foreach (DataRow item in SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_QuList where Q_ID=" + QID, null).Tables[0].Rows)
                {
                    outHtml += "<div class=\"icheckbox_div\" ><input type=\"radio\" name=\"question_" + QID + "\" value=\"Qlist_" + item["ID"] + "\" id=\"Qlist_" + item["ID"] + "\" class=\"option jqTransformHidden\"><label  class=\"option_label\" >&nbsp;&nbsp;" + item["Q_ListText"] + "</label></div>";
                }
                if (HaveMark == 1)
                {
                    outHtml += "<div > Remark: <textarea type=\"text\"  class=\"blank dl_text option\" id=" + QID + " cols=\"70\" rows=\"1\"></textarea></div>";

                }
            }
            else if (QType == 1)
            {
                foreach (DataRow item in SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_QuList where Q_ID=" + QID, null).Tables[0].Rows)
                {
                    outHtml += "<div class=\"icheckbox_div\" ><input type=\"checkbox\" name=\"question_" + QID + "\" value=\"Qlist_" + item["ID"] + "\" id=\"Qlist_" + item["ID"] + "\" class=\"option jqTransformHidden\"><label  class=\"option_label\" >&nbsp;&nbsp;" + item["Q_ListText"] + "</label></div>";
                }
            }
            else if (QType == 2)
            {
                outHtml = "<div  ><textarea type=\"text\"  class=\"blank dl_text option\" id=" + QID + " cols=\"90\" rows=\"5\"></textarea></div>";
            }
            return outHtml;
        }



        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        private string GetMac()
        {
            // 在此处放置用户代码以初始化页面
            try
            {
                string userip = Page.Request.UserHostAddress;
                string strClientIP = Page.Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip 
                Int32 lhost = inet_addr("");   //本地服务器的ip 
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");


                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }
                string mac_dest = "";
                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
                string namesss = System.Net.Dns.GetHostByAddress(userip).HostName;
                return namesss+"_"+ userip + "_" + mac_dest;
            }
            catch (Exception err)
            {
                return err.Message;
            }

        }
    }
}