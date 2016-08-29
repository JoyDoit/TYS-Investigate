<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionManager.aspx.cs" Inherits="BPMQuestionnaire.QuestionManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>问题提交表</title>
    <link href="./Quest_files/base.css" rel="stylesheet" />
    <script type="text/javascript">
        var WshShell = new ActiveXObject("WScript.Shell");
        alert("计算机名 = " + WshShell.ExpandEnvironmentStrings("%COMPUTERNAME%") + "<br/>");
        alert("登录用户名 = " + WshShell.ExpandEnvironmentStrings("%USERNAME%") + "<br/>");
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wjContent clear" id="survey_page">


            <div class="content" id="begin_content" style="">
                <div class="wjtitle mtop project_title">
                    <h1>调查问题添加</h1>
                </div>
                <div class="wjintro mtop desc_begin">
                    <br>
                </div>
                <div class="wjhr mtop"></div>
            </div>


            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">问题类型</div>
                <div class="matrix">
                    <div id="sele" class="red"></div>
                    <div>
                        <select id="sel_qtype" style="width:500px;">
                            <option value="0">单选</option>
                            <option value="1">多选</option>
                            <option value="2">文本</option>
                        </select>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">问题:</div>
                <div class="matrix">
                    <div id="tip_56a10097a320fca11fdc8c7d" class="red"></div>
                    <div>
                        <textarea type="text" value="" class="blank option" id="text_question" cols="68" rows="5" option_id="56a10097a320fca11fdc8c94"></textarea>
                        <br>
                        <span class="red unique_tip" style="display: none;" id="tip_56a10097a320fca11fdc8c94">填写的内容已存在</span>
                    </div>
                </div>
            </div>


            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项1:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_1" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项2:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_2" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项3:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_3" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项4:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_4" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项5:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_5" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项6:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_6" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项7:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_7" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项8:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_8" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项9:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_9" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="wjques maxtop question jqtransformdone" >
                <div class="title">选项10:</div>
                <div class="matrix">
                    <div>
                        <asp:TextBox ID="option_10" runat="server" Width="500"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="maxtop btns" id="control_panel" style="text-align: center">
                <div class="WJButton wj_color" id="next_button" style="">&nbsp;提  交&nbsp;</div>
            </div>
        </div>




    </form>

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

    
<script src="Quest_files/jquery_1.10.2.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#next_button").click(function () {
                if ($("#text_question").val().trim()=="") {
                    alert("问题不能为空");
                    return false;
                }

                if ($("#sel_qtype").val() != "2") {
                    if ($("#option_1").val().trim() == "" || $("#option_2").val().trim() == "") {
                        alert("选项必须有两条或以上");
                        return false;
                    }
                }


                $.ajax({
                    //要用post方式
                    type: "POST",
                    anync: true,
                    //方法所在页面和方法名
                    url: "./Json/JsonManager.ashx",
                    cache: false,
                    data: {
                        action: 'AddQuestion',
                        postText: '',
                        sel_qtype:$("#sel_qtype").val(),
                        question: $("#text_question").val().trim(),

                        option_1: $("#option_1").val().trim(),
                        option_2: $("#option_2").val().trim(),
                        option_3: $("#option_3").val().trim(),
                        option_4: $("#option_4").val().trim(),
                        option_5: $("#option_5").val().trim(),
                        option_6: $("#option_6").val().trim(),
                        option_7: $("#option_7").val().trim(),
                        option_8: $("#option_8").val().trim(),
                        option_9: $("#option_9").val().trim(),
                        option_10: $("#option_10").val().trim()

                    },
                    dataType: "text",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                     
                        if (data == "OK") {
                            alert("问题录入成功！");
                            window.location.href = "QuestionManager.aspx";
                            return false;
                        }
                    },
                    error: function (err) {

                    }
                });

                
            })


        });

    </script>


</body>
</html>
