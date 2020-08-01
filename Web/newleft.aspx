<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newleft.aspx.cs" Inherits="Maticsoft.Web.newadmin.newleft" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1">

<script src="JS/jquery-2.1.0.min.js"></script>
<!-- 最新版本的 Bootstrap 核心 CSS 文件 -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

<!-- 可选的 Bootstrap 主题文件（一般不用引入） -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

<!-- 最新的 Bootstrap 核心 JavaScript 文件 -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #nav {
            width: 204px;
            height: 100%;
            text-align:center;
        }

        .side-nav-item {
            display: block;
            padding: 10px 15px 10px 15px;
            background-color: #FFFFFF;
            cursor: pointer;
            box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
        }

        .item-title {
            background-color: #F5F5F5;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
            border-bottom: 1px solid #DDDDDD;
        }

        .panel-heading {
            margin-top: 5px;
            padding: 0;
            border-radius: 3px;
            border: 1px solid transparent;
            border-color: #DDDDDD;
        }

        .item-body {
            padding: 10px 15px 5px 15px;
            border-bottom: 1px solid #DDDDDD;
        }

        .item-second {
            margin-top: 5px;
            cursor: pointer;
        }

            .item-second a {
                display: block;
                height: 100%;
                width: 100%;
            }

        .at {
            color: red;
        }

        .u {
            text-align: center;
            width: 100%;
        }
    </style>
    <script>

        $(document).ready(function () {
            var path = window.location.pathname;  //先得到地址栏内容
            var regExp = /[\/\.\?]+/;
            str = path.split(regExp);
            var node = str.slice(-2, -1);   //截取地址栏信息得到文件名
            $('#' + node + ' a').addClass('at');  //提前写好对应的id,菜单加亮
            $('#' + node).parent().parent().parent().addClass('in'); //id父级的父级的父级添加展开class 
        })
    </script>
</head>
<body style="background-color:#d1e4fa">
    <form id="form1" runat="server">

        <div id="nav">
            <div class="side-nav">
                <div class="panel-group" id="accordion">
                    <!--财务管理-->
                    <div class="panel-heading panel">
                        <a data-toggle="collapse" data-parent="#accordion" href="#item-cwgl" id="headcwgl" class="side-nav-item item-title a">财务管理
                        </a>
                        <div id="item-cwgl" class="panel-collapse collapse">
                            <div class="item-body">
                                <ul class="list-unstyled nav nav-pills ">
                                    <li class="item-second u"><a href="#" onclick="aclick('CGGL.aspx')">采购管理</a></li>
                                    <li class="item-second u"><a href="#">库房管理</a></li>
                                    <li class="item-second u"><a href="#">销售管理</a></li>
                                    <li class="item-second u"><a href="#">工资管理</a></li>
                                    <li class="item-second u"><a href="#">设备管理</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    
                    <!--人事管理-->
                    <div class="panel-heading panel">
                        <a data-toggle="collapse" data-parent="#accordion" href="#item-rsgl" id="headrsgl" class="side-nav-item item-title a">人事管理
                        </a>
                        <div id="item-rsgl" class="panel-collapse collapse">
                            <div class="item-body">
                                <ul class="list-unstyled nav nav-pills ">
                                    <li class="item-second u"><a href="#">部门人员需求</a></li>
                                    <li class="item-second u"><a href="#">入职</a></li>
                                    <li class="item-second u"><a href="#">培训</a></li>
                                    <li class="item-second u"><a href="#">离职</a></li>
                                    <li class="item-second u"><a href="#">在职人员信息</a></li>
                                     <li class="item-second u"><a href="#">用户类型</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //function aclick(a) {
    //     $.ajax({
    //    type: "post",
    //    url: "WebForm1.aspx",
    //    data: a,
    //         success: function () {
    //             return;
    //         }
    //})
    //}
   
</script>