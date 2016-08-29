using BPMQuestionnaire.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPMQuestionnaire
{
    public partial class AnswerDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String Q_ID = Request.QueryString["Q_ID"].ToString();
                dlt_list.DataSource = SqlHelper.ExecuteDataSet(CommandType.Text, "SELECT * FROM [BPM_Question].[dbo].[t_AnList]  where DL_Text is not null and Dl_Text<>'' and  Q_ID=" + Q_ID, null);
                dlt_list.DataBind();
            }
        }

        protected string GetIP(string A_ID)
        {
            string retSt = SqlHelper.ExecuteScalar(CommandType.Text, " select top 1 [A_computerName] from [t_Answer] where ID=" + A_ID).ToString();
            if (retSt.Split('_').Length > 1)
            {
                return retSt.Split('_')[0].ToString();
            }
            else
            {
                return retSt;
            }
         
        }
    }
}