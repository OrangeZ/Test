<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Test.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-3.3.1.js" type="text/javascript"></script>
    <style>
        .SearchText {
            height: 312px;
            resize: none;
            padding: 10px;
            border: solid 1px #b6cad2;
            margin-top: 30px;
        }

        .div_main {
            width: 100%;
            height: 100vh;
        }

        .d_left {
            width: 212px;
            height: 50vh;
            float: left;
        }

        .d_right {
            width: calc(100% - 242px);
            float: right;
            margin-left: 30px;
        }

        .d_center {
            float: right;
            margin-left: 30px;
            width: calc(100% - 242px);
        }

        a {
            display: inline-block;
            padding: 12px;
            text-decoration: none;
            color: rgba(0, 0, 0, 0.95);
            font-size: 13px;
            font-family: 'Microsoft YaHei';
            border: solid 0.5px #656565;
            margin-top: 30px;
            border-radius: 5px;
        }

        table {
            margin-top: 30px;
            width: 90%;
            padding: 0px;
            font-size: 13px;
            border: solid 1px #b6cad2;
        }

            table tr {
                height: 30px;
            }

            table thead {
                height: 30px;
                background-color: #f0f5f7;
            }

                table thead td {
                    height: 30px;
                    line-height: 30px;
                    text-align: center;
                    padding: 0px 10px;
                    border-bottom: solid 1px #b6cad2;
                }

            table tr td {
                height: 30px;
                line-height: 30px;
            }

        tbody tr td {
            text-align: center;
            padding: 0 10px;
            line-height: 30px;
            border-right: dotted 0.5px #b6cad2;
        }

        tbody tr:hover {
            background-color: #aafad5;
        }

        .tbColNumber {
            text-align: left;
            width: 80px;
        }

        .tbColTitle {
            width: 500px;
        }

        .tbColCount {
            width: 100px;
        }

        .tbColCurrent {
            width: 100px;
        }

        .OutData {
            padding: 10px;
            width: 90%;
            height: 50vh;
            border: solid 1px #b6cad2;
            resize: none;
            margin-top: 30px;
        }

        .div_top {
            width: 100%;
            height: 50px;
            line-height: 50px;
            font-size: 14px;
            background-color: #f0f5f7;
            text-indent: 20px;
        }

        .odd {
            background-color: #f0f5f7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_top">
            分类简表匹配筛查
        </div>
        <div class="div_main">
            <div class="d_left">
                <asp:TextBox runat="server" CssClass="SearchText" ID="txtNumberList" TextMode="MultiLine"></asp:TextBox>
                <asp:LinkButton runat="server" ID="btnSearch" Text="搜索" OnClick="btnSearchList_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton runat="server" ID="btnSearchList" Text="所有" OnClick="btnSearchList_Click1"></asp:LinkButton>
            </div>
            <div class="d_center">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="console" CssClass="OutData"></asp:TextBox>
            </div>
            <div class="d_right">
                <asp:Repeater runat="server" ID="rptTable">
                    <HeaderTemplate>
                        <table cellpadding="0" cellspacing="0">
                            <thead>
                                <tr>
                                    <td>编号
                                    </td>
                                    <td>类目名称
                                    </td>
                                    <%--<td>类目等级
                                    </td>--%>
                                    <td>次数
                                    </td>
                                    <td></td>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tbColNumber"><%# GetTempT(DataBinder.Eval(Container.DataItem, "Count",""))  %><%# DataBinder.Eval(Container.DataItem, "Number","") %></td>
                            <td class="tbColTitle"><%# Eval("Title") %></td>
                            <%--<td class="tbColCount"><%# Eval("Count") %></td>--%>
                            <td class="tbColCurrent"><%# Eval("CurrentCount") %></td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
    <script>
        $(function () {
            $('table tbody tr:odd').addClass('odd');
            //$('.oky:odd').addClass('odd');
        })
    </script>
</body>
</html>
