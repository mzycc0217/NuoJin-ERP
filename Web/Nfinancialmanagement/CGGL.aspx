<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CGGL.aspx.cs" Inherits="Maticsoft.Web.newadmin.CGGL" EnableEventValidation="false" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1">
<script src="../JS/funtion.js"></script>
<script src="../JS/jquery-2.1.0.min.js"></script>
<!-- 最新版本的 Bootstrap 核心 CSS 文件 -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

<!-- 可选的 Bootstrap 主题文件（一般不用引入） -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

<!-- 最新的 Bootstrap 核心 JavaScript 文件 -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
<script src="../My97DatePicker/My97DatePicker/WdatePicker.js"></script>
<script src="../My97DatePicker/My97DatePicker/calendar.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>申请采购</title>
    <style>
        .text {
            width: 300px;
            display: inline;
            height: 35px;
        }

        .top {
            margin-top: 20px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="main" class="container-fluid top">
            <div id="header">

                <div style="display: inline" class="col-md-8">
                    <ul class="nav nav-pills">
                        <li role="presentation"><a href="#" data-toggle="modal" data-target="#exampleModal" data-whatever="@mdo">原材料采购申请单</a></li>
                        <li role="presentation"><a href="#" data-toggle="modal" data-target="#exampleModa2" data-whatever="@mdo">科研生产耗材采购申请单</a></li>
                        <li role="presentation"><a href="#" data-toggle="modal" data-target="#exampleModa3" data-whatever="@mdo">办公用品采购单</a></li>
                        <li role="presentation"><a href="#" data-toggle="modal" data-target="#exampleModa4" data-whatever="@mdo">设备采购单</a></li>
                    </ul>
                </div>
                <%-- <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-whatever="@mdo">原材料采购申请</button>--%>
                <!--原材料采购申请-->

                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="exampleModalLabel">原材料采购申请</h4>
                            </div>
                            <div class="modal-body" style="text-align: center">
                                <div class="form-group">
                                    <label>项目名称</label>
                                    <div class="form-group" id="Yxmname" style="display: inline"></div>
                                </div>
                                <%-- <div class="form-group">
                                    <label for="Ydhtime" class="control-label" >期望到货日期</label>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox class="form-control" Style="width: 259px; display: inline" ID="Ydhtime" runat="server" onclick="WdatePicker({isShowWeek:true})"></asp:TextBox>

                                </div>--%>

                                <div class="form-group">
                                    <asp:Label runat="server" for="Yname" class="control-label" Text="原料名称"></asp:Label>
                                    <div class="form-group" id="Ynamediv" style="display: inline"></div>
                                </div>
                                <%--<div class="form-group">
                                    <asp:Label runat="server" for="Ycj" class="control-label" Text="供货商"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="Ycj" runat="server" class="form-control text"></asp:TextBox>
                                </div>--%>
                                <%-- <div class="form-group">
                                    <asp:Label runat="server" for="Yprice" class="control-label" Text="单    价"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox class="form-control text" onfocusout="Ygetzj()" name="price" ID="Yprice" onpropertychange="submitform()" onkeyup="onlyNumber(this)" runat="server"></asp:TextBox>
                                </div>--%>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Ysl" class="control-label" Text="申请数量"></asp:Label>
                                    <asp:TextBox class="form-control text" name="SL" ID="Ysl" onpropertychange="submitform()" runat="server" onkeyup="onlyNumber(this)"></asp:TextBox>

                                </div>
                                <%-- <div class="form-group">
                                    <asp:Label runat="server" for="Ysca" class="control-label" Text="CSA号"></asp:Label>&nbsp;&nbsp;&nbsp;
                                      <asp:TextBox ID="Ysca" runat="server" class="form-control text"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Ygg" class="control-label" Text="规格"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:TextBox ID="Ygg" runat="server" class="form-control text"></asp:TextBox>
                                </div>--%>
                                <div class="form-group row">

                                    <textarea class="form-control" runat="server" id="Ybz" rows="3" style="resize: none; width: 366px; margin-left: 110px;"></textarea>

                                </div>
                                <%--  <div class="form-group">
                                    <asp:FileUpload ID="YFileUpload" runat="server" Style="margin-left: 100px; width: 320px" />
                                </div>--%>
                                <div class="form-group" style="margin-left: 55%">
                                    <asp:Label runat="server" for="Ysqtime" class="control-label" Text="申请时间：" Style="font-weight: bold;"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <%-- <asp:Label ID="Ysqtime" class="control-label" runat="server" ></asp:Label>--%>
                                    <label id="Ysqtime" class="control-label"></label>
                                    <%-- <asp:Label runat="server" class="control-label" Text=" 总价：" Style="font-weight: bold;"></asp:Label>
                                    <asp:Label ID="YZJ" name="ZJ" runat="server" Text=""></asp:Label>--%>
                                </div>
                                <div class="form-group" style="margin-left: 70%">

                                    <asp:Label runat="server" class="control-label" Text=" 申请人：" Style="font-weight: bold;"></asp:Label>
                                    <!--申请人-->
                                    <asp:Label ID="Ysqr" runat="server" Text=""></asp:Label>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>

                                <button id="Ybtn" type="button" class="btn btn-primary" onclick="onclickYbtn()">提交采购申请</button>
                            </div>
                        </div>

                    </div>
                </div>
                <%--  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModa2" data-whatever="@mdo">科研生产耗材采购申请</button>--%>
                <!--科研生产耗材采购申请-->
                <div class="modal fade" id="exampleModa2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="exampleModalLabe2">科研生产耗材采购申请</h4>
                            </div>
                            <div class="modal-body" style="text-align: center">

                                <div class="form-group">
                                    <asp:Label runat="server" for="Kname" class="control-label" Text="耗材名称"></asp:Label>
                                    <div id="Knamediv" class="text"></div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" for="Kyt" class="control-label" Text="用    途"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:TextBox ID="Kyt" runat="server" class="form-control text"></asp:TextBox>
                                </div>
                                <%--<div class="form-group">
                                    <asp:Label runat="server" for="Kcj" class="control-label" Text="供货商"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="Kcj" runat="server" class="form-control text"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Kprice" class="control-label" Text="单    价"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox class="form-control text" onfocusout="Kgetzj()" name="price" ID="Kprice" onpropertychange="submitform()" onkeyup="onlyNumber(this)" runat="server"></asp:TextBox>
                                </div>--%>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Ksl" class="control-label" Text="申请数量"></asp:Label>
                                    <asp:TextBox class="form-control text" name="SL" ID="Ksl" onpropertychange="submitform()" runat="server" onkeyup="onlyNumber(this)"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <textarea class="form-control" id="Kbz" rows="3" style="resize: none; width: 366px; margin-left: 101px;"></textarea>
                                </div>
                                <%-- <div class="form-group">
                                    <asp:FileUpload ID="KFileUpload" runat="server" Style="margin-left: 100px; width: 320px" />
                                </div>--%>
                                <div class="form-group" style="margin-left: 55%">
                                    <asp:Label runat="server" for="Ksqtime" class="control-label" Text="申请时间"></asp:Label>&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="Ksqtime" class="control-label" runat="server" Text=""></asp:Label>
                                    <%-- <asp:Label runat="server" class="control-label" Text=" 总价：" Style="font-weight: bold;"></asp:Label>
                                    <asp:Label ID="Kzj" name="ZJ" runat="server" Text=""></asp:Label>--%>
                                </div>
                                <div class="form-group" style="margin-left: 70%">

                                    <asp:Label runat="server" class="control-label" Text=" 申请人：" Style="font-weight: bold;"></asp:Label>
                                    <!--申请人-->
                                    <asp:Label ID="Ksqr" runat="server" Text=""></asp:Label>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                                <input id="Kbtn" type="button" name="name" class="btn btn-primary" onclick="onclickKbtn()" value="提交采购申请" />
                                <%-- <asp:Button ID="Kbtn" runat="server" Text="提交采购申请" class="btn btn-primary" OnClick="onclickKbtn()"/>--%>
                            </div>
                        </div>

                    </div>
                </div>
                <%--  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModa3" data-whatever="@mdo">办公用品采购</button>--%>
                <!--办公用品采购-->
                <div class="modal fade" id="exampleModa3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe3">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="exampleModalLabe3">办公用品采购</h4>
                            </div>
                            <div class="modal-body" style="text-align: center">

                                <div class="form-group">
                                    <asp:Label runat="server" for="Byt" class="control-label" Text="用    途"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:TextBox ID="Byt" runat="server" class="form-control text"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Bname" class="control-label" Text="名    称"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      <div id="Bnamediv" class="text"></div>
                                </div>
                                <%--  <div class="form-group">
                                    <asp:Label runat="server" for="Bcj" class="control-label" Text="供货商"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="Bcj" runat="server" class="form-control text"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Bprice" class="control-label" Text="单    价"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox class="form-control text" onfocusout="Bgetzj()" name="price" ID="Bprice" onpropertychange="submitform()" onkeyup="onlyNumber(this)" runat="server"></asp:TextBox>
                                </div>--%>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Bsl" class="control-label" Text="申请数量"></asp:Label>
                                    <asp:TextBox class="form-control text" name="SL" ID="Bsl" onpropertychange="submitform()" runat="server" onkeyup="onlyNumber(this)"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <textarea class="form-control" runat="server" id="Bbz" rows="3" style="resize: none; width: 366px; margin-left: 101px;"></textarea>
                                </div>
                                <%--<div class="form-group">
                                    <asp:FileUpload ID="BFileUpload" runat="server" Style="margin-left: 100px; width: 320px" />
                                </div>--%>
                                <div class="form-group" style="margin-left: 55%">
                                    <asp:Label runat="server" for="Bsqtime" class="control-label" Text="申请时间"></asp:Label>&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="Bsqtime" class="control-label" runat="server" Text=""></asp:Label>
                                    <%--<asp:Label runat="server" class="control-label" Text=" 总价:" Style="font-weight: bold;"></asp:Label>
                                    <asp:Label ID="Bzj" name="ZJ" runat="server" Text=""></asp:Label>--%>
                                </div>
                                <div class="form-group" style="margin-left: 70%">

                                    <asp:Label runat="server" class="control-label" Text=" 申请人：" Style="font-weight: bold;"></asp:Label>
                                    <!--申请人-->
                                    <asp:Label ID="Bsqr" runat="server" Text=""></asp:Label>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>

                                <button id="Bbtn" type="button" class="btn btn-primary" onclick="onclickBbtn()">提交采购申请</button>
                            </div>
                        </div>

                    </div>
                </div>
                <%-- <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModa4" data-whatever="@mdo">设备采购</button>--%>
                <!--设备采购-->
                <div class="modal fade" id="exampleModa4" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe4">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="exampleModalLabe4">设备采购</h4>
                            </div>
                            <div class="modal-body" style="text-align: center">

                                <div class="form-group">
                                    <asp:Label runat="server" for="Syt" class="control-label" Text="用    途"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:TextBox ID="Syt" runat="server" class="form-control text"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Sname" class="control-label" Text="名    称"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <div id="Snamediv" class="text"></div>
                                </div>
                                <%-- <div class="form-group">
                                    <asp:Label runat="server" for="Scj" class="control-label" Text="供货商"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="Scj" runat="server" class="form-control text"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Sprice" class="control-label" Text="单    价"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox class="form-control text" onfocusout="Sgetzj()" name="price" ID="Sprice" onpropertychange="submitform()" onkeyup="onlyNumber(this)" runat="server"></asp:TextBox>
                                </div>--%>
                                <div class="form-group">
                                    <asp:Label runat="server" for="Ssl" class="control-label" Text="申请数量"></asp:Label>
                                    <asp:TextBox class="form-control text" name="SL" ID="Ssl" onpropertychange="submitform()" runat="server" onkeyup="onlyNumber(this)"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <textarea class="form-control" runat="server" id="Sbz" rows="3" style="resize: none; width: 366px; margin-left: 101px;"></textarea>
                                </div>
                                <%-- <div class="form-group">
                                    <asp:FileUpload ID="SFileUpload" runat="server" Style="margin-left: 100px; width: 320px" />
                                </div>--%>
                                <div class="form-group" style="margin-left: 55%">
                                    <asp:Label runat="server" for="Ssqtime" class="control-label" Text="申请时间"></asp:Label>&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="Ssqtime" class="control-label" runat="server" Text=""></asp:Label>
                                    <%-- <asp:Label runat="server" class="control-label" Text=" 总价:" Style="font-weight: bold;"></asp:Label>
                                    <asp:Label ID="Szj" name="ZJ" runat="server" Text=""></asp:Label>--%>
                                </div>
                                <div class="form-group" style="margin-left: 70%">

                                    <asp:Label runat="server" class="control-label" Text=" 申请人：" Style="font-weight: bold;"></asp:Label>
                                    <!--申请人-->
                                    <asp:Label ID="Ssqr" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>

                                <button id="Sbtn" type="button" class="btn btn-primary" onclick="onclickSbtn()">提交采购申请</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div id="nav" class="top">
                <div>
                    <select id="selecttabletype" class="text" style="float: left" onclick=" Cspz()">
                        <option value="0">原材料申请类型</option>
                        <option value="1">科研生产耗材申请类型</option>
                        <option value="2">办公用品申请类型</option>
                        <option value="3">设备采购申请类型</option>
                    </select>
                </div>
                <div class="btn-group btn-group-justified" role="group" aria-label="...">
                    <div class="btn-group" role="group">
                        <button type="button" class="btn btn-default">审批中</button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" class="btn btn-default">已审批</button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" class="btn btn-default">已完成</button>
                    </div>
                </div>
            </div>
            <div>
                <!--审批中-->
                <div id="Cspz">
                    <div id="Cspzpage"></div>
                </div>
                <!--已审批-->
                <div id="Cysp">
                    <div id="Cysppage"></div>
                </div>
                <!--已完成-->
                <div id="Cok">
                    <div id="Cokpage"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script>
    window.onload = function () {
        Cspz();
        showDate();
        Yproject();
        Yname();

        Kname();
        Bname();
        Sname();
    }
    var path = "http://192.168.3.26:9100";
    //价格数量文本框限制
    function onlyNumber(obj) {

        //得到第一个字符是否为负号    
        var t = obj.value.charAt(0);
        //先把非数字的都替换掉，除了数字和.和-号    
        obj.value = obj.value.replace(/[^\d\.\-]/g, '');
        //前两位不能是0加数字      
        obj.value = obj.value.replace(/^0\d[0-9]*/g, '');
        //必须保证第一个为数字而不是.       
        obj.value = obj.value.replace(/^\./g, '');
        //保证只有出现一个.而没有多个.       
        obj.value = obj.value.replace(/\.{2,}/g, '.');
        //保证.只出现一次，而不能出现两次以上       
        obj.value = obj.value.replace('.', '$#$').replace(/\./g, '').replace('$#$', '.');
        //如果第一位是负号，则允许添加    
        obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');
        if (t == '-') {
            return;
        }

    }
    function submitform() {
        var myForm = document.forms["myform"];
        myForm.action = "";
        myForm.submit();
    }
    function Yproject() {
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/Project/GetList",
            data: {
                PageIndex: 1, PageSize: 10
            },
            success: function (res) {
                var data = res[0].Count;
                var str = "<select class='text' id='Yxmnameselect'  class='text'>";
                for (var i = 0; i < data; i++) {
                    str = str + '<option value="' + res[i].ProjectId + '">' + res[i].Name + '</option>';

                }
                str = str + "</select>";
                $("#Yxmname").html(str);
            }

        })
    }
    function Yname() {
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/RawMaterial/GetList",
            data: {
                PageIndex: 0, PageSize: 0, WarehousingType: 0
            },
            success: function (res) {
                if (res == "") {
                    var str = "<select class='text' id='Yname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">暂无数据请添加后再试</option>';

                    }
                    str = str + "</select>";
                    $("#Ynamediv").html(str);
                }
                else {
                    var data = res[0].Count;
                    var str = "<select class='text' id='Yname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">' + res[i].Name + '</option>';

                    }
                    str = str + "</select>";
                    $("#Ynamediv").html(str);
                }
            }


        })
    }
    function Kname() {
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/RawMaterial/GetList",
            data: {
                PageIndex: 0, PageSize: 0, WarehousingType: 1
            },
            success: function (res) {
                if (res == "") {
                    var str = "<select class='text' id='Kname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">暂无数据请添加后再试</option>';

                    }
                    str = str + "</select>";
                    $("#Knamediv").html(str);
                }
                else {
                    var data = res[0].Count;
                    var str = "<select class='text' id='Kname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">' + res[i].Name + '</option>';

                    }
                    str = str + "</select>";
                    $("#Knamediv").html(str);
                }
            }

        })
    }
    function Bname() {
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/RawMaterial/GetList",
            data: {
                PageIndex: 0, PageSize: 0, WarehousingType: 2
            },
            success: function (res) {
                if (res == "") {
                    var str = "<select class='text' id='Bname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">暂无数据请添加后再试</option>';

                    }
                    str = str + "</select>";
                    $("#Bnamediv").html(str);
                }
                else {
                    var data = res[0].Count;
                    var str = "<select class='text' id='Bname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">' + res[i].Name + '</option>';

                    }
                    str = str + "</select>";
                    $("#Bnamediv").html(str);
                }
            }

        })
    }
    function Sname() {
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/RawMaterial/GetList",
            data: {
                PageIndex: 0, PageSize: 0, WarehousingType: 3
            },
            success: function (res) {
                if (res == "") {
                    var str = "<select class='text' id='Sname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">暂无数据请添加后再试</option>';

                    }
                    str = str + "</select>";
                    $("#Snamediv").html(str);
                }
                else {
                    var data = res[0].Count;
                    var str = "<select class='text' id='Sname'  class='text'>";
                    for (var i = 0; i < data; i++) {
                        str = str + '<option value="' + res[i].RawMaterialId + '">' + res[i].Name + '</option>';

                    }
                    str = str + "</select>";
                    $("#Snamediv").html(str);
                }
            }

        })
    }

    //四个采购模块总价计算
    //function Ygetzj() {

    //    var Cprice = document.getElementById("Yprice").value;
    //    var Csl = document.getElementById("Ysl").value;

    //    if (Cprice != "" || Csl !="" ) {
    //        var zj = Csl * Cprice;
    //        document.getElementById("YZJ").innerText = zj.toFixed(2);
    //    }
    //}
    //function Kgetzj() {

    //    var Cprice = document.getElementById("Kprice").value;
    //    var Csl = document.getElementById("Ksl").value;
    //    if (Cprice != "" || Csl != "") {
    //        var zj = Csl * Cprice;
    //        document.getElementById("Kzj").innerText = zj.toFixed(2);
    //    }
    //}
    //function Bgetzj() {

    //    var Cprice = document.getElementById("Bprice").value;
    //    var Csl = document.getElementById("Bsl").value;
    //    if (Cprice != "" || csl != "") {
    //        var zj = Csl * Cprice;
    //        document.getElementById("Bzj").innerText = zj.toFixed(2);
    //    }
    //}
    //function Sgetzj() {
    //    var Cprice = document.getElementById("Sprice").value;
    //    var Csl = document.getElementById("Ssl").value;
    //    if (Cprice != "" || csl != "") {
    //        var zj = Csl * Cprice;
    //        document.getElementById("Szj").innerText = zj.toFixed(2);
    //    }
    //}
    //function add() {
    //    document.getElementById("Cprice").value = "";
    //    document.getElementById("Csl").value = "";
    //}
    //获取当前时间及获取当前登录人

    function showDate() {
        var date = new Date(); //创建时间对象  

        var year = date.getFullYear(); //获取年   

        var month = date.getMonth() + 1;//获取月

        var day = date.getDate(); //获取当日  

        var time = year + "-" + month + "-" + day; //组合时间  
        document.getElementById("Ksqtime").innerText = time;
        document.getElementById("Ysqtime").innerText = time;
        document.getElementById("Bsqtime").innerText = time;
        document.getElementById("Ssqtime").innerText = time;

        document.getElementById("Ysqr").innerText = "ahsjkhasaj";
        document.getElementById("Bsqr").innerText = "ahsjkhasaj";
        document.getElementById("Ssqr").innerText = "ahsjkhasaj";
        document.getElementById("Ksqr").innerText = "ahsjkhasaj";
    }
    //原材料采购提交申请
    function onclickYbtn() {
        var Yxmname = document.getElementById("Yxmnameselect").value;
        var Yname = document.getElementById("Yname").value;
        //var YCJ = document.getElementById("YCJ").value;
        //var Yprice = document.getElementById("Yprice").value;
        var Ysl = document.getElementById("Ysl").value;
        var Ybz = document.getElementById("Ybz").value;
        var Ysqtime = document.getElementById("Ysqtime").innerText;


        if (Ysl == null && Ysl == "" && Sname == "暂无数据请添加后再试") {
            alert("请填写完整之后再提交");
        }
        else {
            $.ajax({
                type: "post",
                url: "http://192.168.3.26:9100/api/Purchase/Add",
                data: {
                    ProjectId: Yxmname, RawMaterialId: Yname, ApplyNumber: Ysl, ApplyTime: Ysqtime, ApplicantRemarks: Ybz
                },
                success: function (res) {
                    uptable(res);
                }
            })


        }
    }
    //设备采购提交申请
    function onclickSbtn() {


        var Sname = document.getElementById("Sname").value;
        var Syt = document.getElementById("Syt").value;
        var Ssl = document.getElementById("Ssl").value;
        var Sbz = document.getElementById("Sbz").value;
        var Ssqtime = document.getElementById("Ssqtime").innerText;
        if (Syt == "" && Syt == null && Ssl == "" && Ssl == null && Sname == "暂无数据请添加后再试") {
            alert("请填写完整之后再提交");
        }
        else {
            $.ajax({
                type: "post",
                url: "http://192.168.3.26:9100/api/Purchase/Add",
                data: {
                    RawMaterialId: Sname, Purpose: Syt, ApplyNumber: Ssl, ApplicantRemarks: Sbz, ApplyTime: Ssqtime
                },
                success: function (res) {
                    uptable(res);
                }
            })


        }
    }
    //科研
    function onclickKbtn() {
        var Kname = document.getElementById("Kname").value;
        var Kyt = document.getElementById("Kyt").value;
        var Ksl = document.getElementById("Ksl").value;
        var Kbz = document.getElementById("Kbz").value;
        var Ksqtime = document.getElementById("Ksqtime").innerText;
        if (Kyt == "" && Kyt == null && Ksl == "" && Ksl == null && Sname == "暂无数据请添加后再试") {
            alert("请填写完整之后再提交");
        }
        else {
            $.ajax({
                type: "post",
                url: "http://192.168.3.26:9100/api/Purchase/Add",
                data: {
                    RawMaterialId: Kname, Purpose: Kyt, ApplyNumber: Ksl, ApplicantRemarks: Kbz, ApplyTime: Ksqtime
                },
                success: function (res) {
                    uptable(res);
                }
            })


        }
    }
    //办公采购提交申请
    function onclickBbtn() {
        var Bname = document.getElementById("Bname").value;
        var Byt = document.getElementById("Byt").value;
        var Bsl = document.getElementById("Bsl").value;
        var Bbz = document.getElementById("Bbz").value;
        var Bsqtime = document.getElementById("Bsqtime").innerText;
        if (Kyt == "" && Kyt == null && Ksl == "" && Ksl == null && Sname == "暂无数据请添加后再试") {
            alert("请填写完整之后再提交");
        }
        else {
            $.ajax({
                type: "post",
                url: "" + path + "/api/Purchase/Add",
                data: {
                    RawMaterialId: Bname, Purpose: Byt, ApplyNumber: Bsl, ApplicantRemarks: Bbz, ApplyTime: Bsqtime
                },
                success: function (res) {
                    uptable(res);
                }
            })


        }
    }

    //审核
    function Cspz(page) {
        if (page == null) {
            page = 1;
        }
        var WarehousingType = document.getElementById("selecttabletype").value;
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/Purchase/GetPurchaseList",
            data: {
                PageIndex: page, PageSize: 10,WarehousingType:WarehousingType
            },
            success: function (res) {
                if (res == "" && res == null) {
                    alert("暂时未有提交得采购申请");
                }
                else {
                    var len = res[0].Count;
                    var data = res;
                    var tableStr = "<table class='table table-hover'>";
                    tableStr = tableStr + "<thead><tr><th>采购单类型</th><th>类别</th><th>用途</th><th>物品名称</th><th>申请数量</th><th>单位</th><th>部门</th><th>备注说明</th><th>提交人</th><th>提交时间</th></tr></thead><tbody>";
                    for (var i = 0; i < data.length; i++) {
                        tableStr = tableStr + "<tr><td>" + data[i].WarehousingTypeStr   + "</td><td>" + data[i].RawMaterial.RawMaterialType + "</td><td>" + data[i].Purpose + "</td><td>" + data[i].RawMaterial.Name + "</td><td>" + data[i].ApplyNumber + "</td><td>"+data[i].Company.Name+"</td><td>" + data[i].Department.Name + "</td><td>" + data[i].ApplicantRemarks + "</td><td>" + data[i].Applicant.RealName + "</td><td>" + data[i].ApplyTime + "</td></tr>";
                    }
                    tableStr = tableStr + "</tbody></table>";

                    //将动态生成的table添加的事先隐藏的div中.

                    $("#Cspz").html(tableStr);

                    var index = len / 10;
                    index = Math.round(index);
                    var str = "<p>";

                    for (var i = 1; i < index + 1; i++) {
                        str += "  <button type='button' onclick='Cspz(" + i + ")'>" + i + "</button>";
                    }
                    str += "</p>";
                    $("#Cspzpage").html(str);
                }

            }
        })
    }
    //采购部门已审核
    function Cysp(page) {
        if (page == null) {
            page = 1;
        }
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/Purchase/GetPurchaseList",
            data: {
                PageIndex: page, PageSize: 10,
            },
            success: function (res) {
                if (res == "" && res == null) {
                    alert("暂时未有提交得采购申请");
                }

                //var len = res[0].Count;
                var data = res;
                var tableStr = "<table class='table table-hover'>";
                tableStr = tableStr + "<thead><tr><th>类别</th><th>用途</th><th>物品名称</th><th>申请数量</th><th>部门</th><th>备注说明</th><th>提交人</th><th>提交时间</th><th>部门主管</th><th>采购部门</th><th>总经理</th></tr></thead><tbody>";
                for (var i = 0; i < data.length; i++) {
                    tableStr = tableStr + "<tr><td>" + data[i].RawMaterial.RawMaterialType + "</td><td>" + data[i].Purpose + "</td><td>" + data[i].RawMaterial.Name + "</td><td>" + data[i].ApplyNumber + "</td><td>" + data[i].Department.Name + "</td><td>" + data[i].ApplicantRemarks + "</td><td>" + data[i].Applicant.RealName + "</td><td>" + data[i].ApplyTime + "</td><td>" + data[i].PurchasingSpecialist.RealName + "</td><td>" + data[i].DepartmentLeader.RealName + "</td><td>" + data[i].GeneralManager.RealName + "</td></tr>";
                }
                tableStr = tableStr + "</tbody></table>";

                //将动态生成的table添加的事先隐藏的div中.

                $("#Cysp").html(tableStr);

                var index = len / 10;
                index = Math.round(index);
                var str = "<p>";

                for (var i = 1; i < index + 1; i++) {
                    str += "  <button type='button' onclick='Cspz(" + i + ")'>" + i + "</button>";
                }
                str += "</p>";
                $("#Cysppage").html(str);
            }

        })
    }
    //已完成订单
    function Cok(page) {
        if (page == null) {
            page = 1;
        }
        $.ajax({
            type: "POST",
            url: "http://192.168.3.26:9100/api/Purchase/GetPurchaseList",
            data: {
                PageIndex: page, PageSize: 10,
            },
            success: function (res) {
                if (res == "" && res == null) {
                    alert("暂时未有提交得采购申请");
                }

                //var len = res[0].Count;
                var data = res;
                var tableStr = "<table class='table table-hover'>";
                tableStr = tableStr + "<thead><tr><th>类别</th><th>用途</th><th>物品名称</th><th>申请数量</th><th>部门</th><th>备注说明</th><th>提交人</th><th>提交时间</th><th>部门主管</th><th>采购部门</th><th>总经理</th></tr></thead><tbody>";
                for (var i = 0; i < data.length; i++) {
                    tableStr = tableStr + "<tr><td>" + data[i].RawMaterial.RawMaterialType + "</td><td>" + data[i].Purpose + "</td><td>" + data[i].RawMaterial.Name + "</td><td>" + data[i].ApplyNumber + "</td><td>" + data[i].Department.Name + "</td><td>" + data[i].ApplicantRemarks + "</td><td>" + data[i].Applicant.RealName  + "</td><td>" + data[i].ApplyTime + "</td><td>" + data[i].PurchasingSpecialist.RealName + "</td><td>" + data[i].DepartmentLeader.RealName + "</td><td>" + data[i].GeneralManager.RealName + "</td></tr>";
                }
                tableStr = tableStr + "</tbody></table>";

                //将动态生成的table添加的事先隐藏的div中.

                $("#Cok").html(tableStr);

                var index = len / 10;
                index = Math.round(index);
                var str = "<p>";

                for (var i = 1; i < index + 1; i++) {
                    str += "  <button type='button' onclick='Cspz(" + i + ")'>" + i + "</button>";
                }
                str += "</p>";
                $("#Cokapge").html(str);

            }
        })
    }
</script>


