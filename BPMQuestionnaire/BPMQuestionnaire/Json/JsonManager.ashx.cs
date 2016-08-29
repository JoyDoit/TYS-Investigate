using BPMQuestionnaire.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace BPMQuestionnaire.Json
{
    /// <summary>
    /// Summary description for JsonManager
    /// </summary>
    public class JsonManager : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";

            string Action = context.Request["action"];

            //变量
            string ddlLocationVal = string.Empty;
            string ddlLocationText = string.Empty;

            string postText = context.Request["postText"];


          //  string ip = context.Request.UserHostName;
          //  string userip = context.Request.UserHostAddress;
          //  string namesss = System.Net.Dns.GetHostByAddress(userip).HostName;
          //  string mac = GetMac(context);

           // string ComputerName = ip + "_" + namesss + "_" + mac;
          //  string ComputerName = System.Environment.MachineName + "_" + System.Environment.UserName;
           
           // string ComputerName = mac;


            string A_ID = string.Empty;
            String[] ArrayT;

            switch (Action)
            {
                case "AddAnswer":      //申请 
                  string ComputerName = context.Request["userinfo"];
                  if (SqlHelper.ExecuteScalar(CommandType.Text, "SELECT top 1 [ID] FROM [dbo].[t_Answer] where [A_computerName]='" + ComputerName + "'", null)!=null)
                  {
                      context.Response.Write("重复");  //完成
                      context.Response.End();
                  }
                  else { 
                    //插入答卷用户信息

                    SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_Answer] ([A_computerName],[A_Remark],[A_AddTime]) VALUES ('" + ComputerName + "','' ,getdate())", null);


                    //插入回答答案
                     A_ID = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT top 1 [ID] FROM [dbo].[t_Answer] order by ID desc", null).ToString();
                     ArrayT = postText.Split('/');
                    for (int i = 0; i < ArrayT.Length-1; i++)
                    {
                        String Q_ID= ArrayT[i].Split(':')[0].Substring(9);
                        String QL_ID = ArrayT[i].Split(':')[1].ToString();

                        SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_AnList] ([A_ID] ,[Q_ID],[QL_ID] ) VALUES ( " + A_ID + " ," + Q_ID + " ,'" + QL_ID + "')", null);
                    }

                      //插入回答备注信息
                    string DL_Texts = context.Request["DL_Texts"];
                    if (DL_Texts != null && DL_Texts != "")
                    {
                        String[] ArrayDLT = DL_Texts.Split('/');
                        for (int i = 0; i < ArrayDLT.Length - 1; i++)
                        {
                            String Q_ID = ArrayDLT[i].Split(':')[0].ToString();
                            String An_ListTx = ArrayDLT[i].Split(':')[1].ToString();
                            if (SqlHelper.ExecuteScalar(CommandType.Text, "SELECT TOP 1 [Q_Type]  FROM [BPM_Question].[dbo].[t_Question] where ID=" + Q_ID, null).ToString() == "2")
                            {
                                SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_AnList] ([A_ID] ,[Q_ID] ,[QL_ID]  ,[DL_Text]) VALUES (" + A_ID + " ," + Q_ID + " ,'' , '" + An_ListTx + "')", null);
                            }
                            else
                            {

                                SqlHelper.ExecuteScalar(CommandType.Text, "UPDATE [dbo].[t_AnList] SET   [DL_Text] ='" + An_ListTx + "' WHERE A_ID=" + A_ID + " and Q_ID=" + Q_ID + "", null);
                            }
                        }
                    }
                     
                      

                    context.Response.Write("OK");  //完成
                    context.Response.End();
                  }
                    break;
                case "CoverAnswer":

                    ComputerName = context.Request["userinfo"];
                    //删除数据DELETE FROM [dbo].[t_AnList] WHERE A_ID=''
                    string Del_A_ID = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT top 1 [ID] FROM [dbo].[t_Answer] where A_computerName='" + ComputerName + "'", null).ToString();
                    SqlHelper.ExecuteScalar(CommandType.Text, "DELETE FROM [dbo].[t_AnList] WHERE A_ID='" + Del_A_ID + "'", null);
                    SqlHelper.ExecuteScalar(CommandType.Text, "DELETE FROM [dbo].[t_Answer] WHERE A_computerName='" + ComputerName + "'", null);


                    //插入答卷用户信息
                    SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_Answer] ([A_computerName],[A_Remark],[A_AddTime]) VALUES ('" + ComputerName + "','' ,getdate())", null);

                    //插入回答答案
                     A_ID = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT top 1 [ID] FROM [dbo].[t_Answer] order by ID desc", null).ToString();
                     ArrayT = postText.Split('/');
                    for (int i = 0; i < ArrayT.Length - 1; i++)
                    {
                        String Q_ID = ArrayT[i].Split(':')[0].Substring(9);
                        String QL_ID = ArrayT[i].Split(':')[1].ToString();

                        SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_AnList] ([A_ID] ,[Q_ID],[QL_ID] ) VALUES ( " + A_ID + " ," + Q_ID + " ,'" + QL_ID + "')", null);
                    }

                    //插入回答备注信息
                    string ThisDL_Texts = context.Request["DL_Texts"];
                    if (ThisDL_Texts != null && ThisDL_Texts != "")
                    {
                        String[] ArrayDLT = ThisDL_Texts.Split('/');
                        for (int i = 0; i < ArrayDLT.Length - 1; i++)
                        {
                            String Q_ID = ArrayDLT[i].Split(':')[0].ToString();
                            String An_ListTx = ArrayDLT[i].Split(':')[1].ToString();
                            if (SqlHelper.ExecuteScalar(CommandType.Text, "SELECT TOP 1 [Q_Type]  FROM [BPM_Question].[dbo].[t_Question] where ID=" + Q_ID, null).ToString() == "2")
                            {
                                SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_AnList] ([A_ID] ,[Q_ID] ,[QL_ID]  ,[DL_Text]) VALUES (" + A_ID + " ," + Q_ID + " ,'' , '" + An_ListTx + "')", null);
                            }
                            else
                            {

                                SqlHelper.ExecuteScalar(CommandType.Text, "UPDATE [dbo].[t_AnList] SET   [DL_Text] ='" + An_ListTx + "' WHERE A_ID=" + A_ID + " and Q_ID=" + Q_ID + "", null);
                            }
                        }
                    }


                    context.Response.Write("OK");  //完成
                    context.Response.End();

                    break;
                case "AddQuestion":
                    string question = context.Request["question"];
                    string sel_qtype = context.Request["sel_qtype"];

                    //插入问题
                    SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_Question] ([Q_Text],[Q_Type],[Q_AddTime])  VALUES  ('" + question + "' ," + sel_qtype + " ,getdate())", null);


                    //插入选项答案
                    string Q_GetID = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT top 1 [ID] FROM [dbo].[t_Question] order by ID desc", null).ToString();

                    for (int i = 1; i < 11; i++)
                    {
                        string optionT=context.Request["option_"+i].Trim();
                        if (optionT != "")
                        {
                            SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[t_QuList] ([Q_ID]   ,[Q_ListText]) VALUES (" + Q_GetID + "  ,'" + optionT + "')", null);
                        }
                    }

                   
                    context.Response.Write("OK");  //完成
                    context.Response.End();
                    break;
                case "DelQuestion":

                    string P_ID = context.Request["P_ID"];
                    SqlHelper.ExecuteScalar(CommandType.Text, " DELETE FROM [dbo].[t_QuList]  WHERE Q_ID=" + P_ID + "", null);
                    SqlHelper.ExecuteScalar(CommandType.Text, " DELETE FROM [dbo].[t_Question]  WHERE ID=" + P_ID + "", null);
                   
                    context.Response.Write("OK");  //完成
                    context.Response.End();

                    break;
                default:
                    break;
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


     
    }
}