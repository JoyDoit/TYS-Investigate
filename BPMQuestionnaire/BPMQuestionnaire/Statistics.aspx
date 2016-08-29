<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="BPMQuestionnaire.Statistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>BPM使用调查结果统计</title>
    <link href="./Quest_files/base.css" rel="stylesheet" />

    <script type="text/javascript" src="JS/jquery-1.4.2.min.js" ></script>
    <script type="text/javascript" src="JS/highcharts.js" ></script>

</head>
<body>
<form id="form1" runat="server">
<div class="wjContent clear" id="survey_page">
  <div class="content" id="begin_content" >
    <div class="wjtitle mtop project_title">
      <h1>“BPM”使用调查结果统计</h1>
    </div>
    <div class="wjintro mtop desc_begin"><br>
    </div>
    <div class="wjhr mtop"></div>
  </div>
  <div id="question_box">

    <!-- Append question's html code here. -->
    
     <asp:DataList ID="DataList1" runat="server" >
            <ItemTemplate>
    <div class="wjques maxtop question jqtransformdone" id="question_<%#Eval("ID") %>"  >
      <div class="matrix">
        
          <%# GetList(Convert.ToInt32(Eval("ID")),Convert.ToInt16(Eval("Q_Type")),Eval("Q_Text").ToString()) %>

      </div>
    </div>
   </ItemTemplate>
        </asp:DataList> 
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
