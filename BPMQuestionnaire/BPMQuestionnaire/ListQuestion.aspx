<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListQuestion.aspx.cs" Inherits="BPMQuestionnaire.ListQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>问题列表</title>
     <link href="./Quest_files/base.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="wjContent clear" id="survey_page">
  <div class="content" id="begin_content" style="">
    <div class="wjtitle mtop project_title">
      <h1>“BPM”问题列表管理</h1>
    </div>
    <div class="wjintro mtop desc_begin"><br>
    </div>
    <div class="wjhr mtop"></div>
  </div>
  <div id="question_box">

    <!-- Append question's html code here. -->
 
        <div>
            <table>
            <asp:DataList ID="dlt_list" runat="server" >
                 <HeaderTemplate>
                     <tr>
                         <td style="width:110px" >问题类型</td>
                         <td style="width:500px">问题</td>
                         <td style="width:100px"><a href="<%# Eval("Q_Text") %>">操作</a></td>
                     </tr>
                 
                 </HeaderTemplate>
                <ItemTemplate>
                      <tr>
                          <td><%# Convert.ToInt32(Eval("Q_Type"))==0?"单选":"多选" %></td>
                          <td><a href="AnswerDetail.aspx?Q_ID=<%# Eval("ID") %>"  ><%# Eval("Q_Text") %></a></td>
                          <td><a href="#" onclick="DelQuestion(<%# Eval("ID") %>)" >删除</a></td>
                     </tr>

                </ItemTemplate>
            </asp:DataList>
                </table>
        </div>

</div>
</div>

<div class="footer_mini">
  <div class="footer">
    <div class="instructions" style="color: rgb(120, 120, 120);">
      <div class="footer_logo">
         
        <div class="wjtext">
             <a href="Statistics.aspx"  target="_blank">统计</a>&nbsp;
          <a href="QuestionManager.aspx"  target="_blank">问题录入</a>&nbsp;
          <a href="ListQuestion.aspx"  target="_blank">问题管理</a>&nbsp;
            &nbsp;&nbsp;&nbsp;
            <a href="http://www.tymphany.com/" target="_blank" style="color: rgb(120, 120, 120);">TYMPHANY IT Dep</a> <span>提供支持</span></div>
      </div>
     </div>
  </div>
</div>
    </form>

    <script src="Quest_files/jquery_1.10.2.min.js"></script>
    <script type="text/javascript">

        function DelQuestion(P_ID) {
            $.ajax({
                //要用post方式
                type: "POST",
                anync: true,
                //方法所在页面和方法名
                url: "./Json/JsonManager.ashx",
                cache: false,
                data: {
                    action: 'DelQuestion',
                    postText: '',
                    P_ID: P_ID
                },
                dataType: "text",
                success: function (data) {
                    //返回的数据用data.d获取内容
                    if (data == "OK") {
                        alert("删除成功");
                        window.location.href = "ListQuestion.aspx";
                        return false;
                    }
                },
                error: function (err) {

                }
            });

            }

     
    </script>
</body>
</html>
