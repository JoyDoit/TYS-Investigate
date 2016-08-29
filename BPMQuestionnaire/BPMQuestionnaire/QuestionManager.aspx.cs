using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPMQuestionnaire
{
    public partial class QuestionManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                String Name = Page.User.Identity.Name.ToString();


                string userAcc = System.Web.HttpContext.Current.User.Identity.Name.Trim();
                int len = userAcc.IndexOf('\\', 0);
                userAcc = userAcc.Substring(len + 1, userAcc.Length - len - 1);


                String ManchineName = Page.Server.MachineName; //获取服务器电脑名:
                string user = Page.User.ToString();  //获取用户信息: 
                string hostName = Page.Request.UserHostName;  //获取客户端电脑名：
                string HostAdd = Page.Request.UserHostAddress;  //获取客户端电脑IP: 
                //2. 在网络编程中的通用方法:
                string GetHostName = System.Net.Dns.GetHostName();//获取当前电脑名:
                //static System.Net.Dns.Resolve(电脑名).AddressList;  //根据电脑名取出全部IP地址: 
                //  string name = System.Net.Dns.Resolve(HostAdd).HostName;  //也可根据IP地址取出电脑名

                //Response.Write(Request.LogonUserIdentity.Name + "--" + Name +"---"+ userAcc + "--" + len);


                string name = Page.Request.UserHostName;

                string ip = Page.Request.UserHostName;

                string namesss = System.Net.Dns.Resolve(ip.ToString()).HostName;

              //  string namesssttt = System.Net.Dns.GetHostByAddress(ip).HostName;

               // System.Threading.Thread.Sleep(10000);  

                GetMac();


                Response.Write("<br>获取服务器电脑名:" + ManchineName);
               // Response.Write("<br>获取用户信息:" + mac);
                Response.Write("<br>获取客户端电脑名:" + namesss);
                Response.Write("<br>获取客户端电脑IP:" + ip);

                Response.Write("<br>电脑Info:" + GetNetworkAdpaterID());




            }


        }

        //获取cpu序列号,硬盘ID,网卡MAC地址
        private string GetInfo()
        {
            string cpuInfo = "";//cpu序列号
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                //   Label1.Text += "cpu序列号：" + cpuInfo.ToString();
            }

            //获取硬盘ID
            String HDid = "";
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
                //  Label1.Text += "硬盘序列号：" + HDid.ToString();
            }
            return "cpu序列号:" + cpuInfo + "<br>获取硬盘ID:" + HDid;

        }


        public static string GetNetworkAdpaterID()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac += mo["MacAddress"].ToString() + " ";
                        break;
                    }
                moc = null;
                mc = null;
                return mac.Trim();
            }
            catch (Exception e)
            {
                return "uMnIk";
            }
        }

        //获取MAC地址


        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        private void GetMac()
        {
            // 在此处放置用户代码以初始化页面
            try
            {
                string userip = Request.UserHostAddress;
                string strClientIP = Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip 
                Int32 lhost = inet_addr("");   //本地服务器的ip 
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                if (mac_src == "0")
                {
                    if (userip == "127.0.0.1")
                        Response.Write("正在访问Localhost!");
                    else
                        Response.Write("欢迎来自IP为" + userip + "的朋友！" + "");
                    return;
                }
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
                Response.Write("欢迎来自IP为" + userip + "" + ",MAC地址为" + mac_dest + "的朋友！" + "");
            }
            catch (Exception err)
            {
                Response.Write(err.Message);
            }

        }
    }


}