<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnswerDetail.aspx.cs" Inherits="BPMQuestionnaire.AnswerDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>问题答案</title>
    <link href="./Quest_files/base.css" rel="stylesheet" />
    <style type="text/css">
        td{
            padding:3px;
        }
    </style>
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
        
            <asp:DataList ID="dlt_list" runat="server" >
                 <HeaderTemplate>
                     <tr >
                            <td ><strong>IP</strong> </td>
                         <td ><strong>反馈信息</strong> </td>
                       
                     </tr>
                 
                 </HeaderTemplate>
                <ItemTemplate>
                      <tr  >
                          <td><%# GetIP(Eval("A_ID").ToString()) %>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                          <td ><%# Eval("DL_Text") %></td>
                     </tr>

                </ItemTemplate>
            </asp:DataList>
            
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
</body>
</html>
