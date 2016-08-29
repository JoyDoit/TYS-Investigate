<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BPMQuestionnaire.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BPM go-live survey</title>
    <link href="./Quest_files/base.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="wjContent clear" id="survey_page">
  <div class="content" id="begin_content" style="">
    <div class="wjtitle mtop project_title">
      <h1>BPM go-live survey</h1>
    </div>
    <div class="wjintro mtop desc_begin"><br>
    </div>
    <div class="wjhr mtop"></div>
  </div>
  <div id="question_box">

    <!-- Append question's html code here. -->
    
     <asp:DataList ID="DataList1" runat="server" >
            <ItemTemplate>
    <div class="wjques maxtop question jqtransformdone" id="question_<%# Eval("ID") %>"  >
      <div class="title"><%#Eval("Q_Text") %></div>
      <div class="matrix">
        
          <%# GetList(Convert.ToInt32(Eval("ID")),Convert.ToInt16(Eval("Q_Type")),Convert.ToInt16(Eval("Q_HaveReason"))) %>
         
      </div>
    </div>
   </ItemTemplate>
        </asp:DataList> 



</div>
  <div class="maxtop btns" id="control_panel" style="text-align:center">
    <div class="WJButton wj_color" id="next_button" style="">&nbsp;Submit&nbsp;</div>
  </div>
    <div id="errInfo" style="color:red">

    </div>
</div>

<div class="footer_mini">
  <div class="footer">
    <div class="instructions" style="color: rgb(120, 120, 120);">
      <div class="footer_logo">
        <div class="wjtext"><a href="http://www.tymphany.com/" target="_blank" style="color: rgb(120, 120, 120);">TYMPHANY IT Dept</a> <span>Offers Support</span></div>
      </div>
     </div>
  </div>
</div>
    <input type="hidden" runat="server" id="userinfo" />
</form>

<script src="Quest_files/jquery_1.10.2.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#next_button").click(function () {
                var ListQuest = $(".jqtransformdone");

                var postText = "";

                var NullText = "";
               
                for (var i = 0; i < ListQuest.size()-1 ; i++) {
                    if (typeof ($('input[name="' + $(ListQuest.get(i)).attr("ID") + '"]:checked').val()) == "undefined") {
                        NullText += '<p>' + $('#' + $(ListQuest.get(i)).attr("ID") + " .title").text() + '----Can not be empty</p>';
                    }
                }

                if (NullText.length > 1) {
                    $("#errInfo").html(NullText);
                    return false;
                }

                for (var i = 0; i < ListQuest.size()-1 ; i++) {
                    postText += $(ListQuest.get(i)).attr("ID") + ":";

                    $('input[name="' + $(ListQuest.get(i)).attr("ID") + '"]:checked').each(function () {
                        postText +=$(this).val();
                    });
                    postText += "/";
                }

                var dl_test = $(".dl_text");
                var DL_Texts = "";
                for (var i = 0; i < dl_test.size() ; i++) {
                    DL_Texts += $(dl_test.get(i)).attr("ID") + ":" + $(dl_test.get(i)).val()+"/"
                }


                $.ajax({
                    //要用post方式
                    type: "POST",
                    anync: true,
                    //方法所在页面和方法名
                    url: "./Json/JsonManager.ashx",
                    cache: false,
                    data: {
                        action: 'AddAnswer',
                        postText: postText,
                        DL_Texts: DL_Texts,
                        userinfo: $("#userinfo").val()
                      
                    },
                    dataType: "text",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                        if (data == "重复") {
                            if (confirm("Do you need to replace your submitted data.")) {
                                coverAnswer(postText, DL_Texts);
                            } else {
                                window.location.href = "http://www.tymphany.com/";
                                return false;
                            };
                            return false;
                        }
                        else if (data == "OK") {
                            alert("Form is submitted successfully .Thank you for your participation.");
                            window.location.href = "http://www.tymphany.com/";
                            return false;
                        }
                    },
                    error: function (err) {

                    }
                });
            })

            function coverAnswer(postText, DL_Text) {

                $.ajax({
                    //要用post方式
                    type: "POST",
                    anync: true,
                    //方法所在页面和方法名
                    url: "./Json/JsonManager.ashx",
                    cache: false,
                    data: {
                        action: 'CoverAnswer',
                        postText: postText,
                        DL_Texts: DL_Text,
                        userinfo: $("#userinfo").val()
                    },
                    dataType: "text",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                        if (data == "重复") {
                            return false;
                        }
                        else if (data == "OK") {
                            alert("Form is submitted successfully .Thank you for your participation.");
                            window.location.href = "http://www.tymphany.com/";
                            return false;
                        }
                    },
                    error: function (err) {

                    }
                });

            }


        });

    </script>
</body>
</html>
