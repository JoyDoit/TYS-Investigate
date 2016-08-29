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
    public partial class ListQuestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dlt_list.DataSource = SqlHelper.ExecuteDataSet(CommandType.Text, "select * from t_Question ", null);
                dlt_list.DataBind();
            }
        }
    }
}