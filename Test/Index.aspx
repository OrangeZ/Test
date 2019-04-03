<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Test.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="width: 500px;">
                <asp:TextBox runat="server" ID="txtNumber"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtNumberList" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <asp:LinkButton runat="server" ID="btnSearch" Text="搜索" OnClick="btnSearch_Click"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnSearchList" Text="批量搜索" OnClick="btnSearchList_Click"></asp:LinkButton>
            </div>

            <asp:Repeater runat="server" ID="rptTable">
                <HeaderTemplate>
                    <table>
                        <thead>
                            <tr>
                                <td>编号
                                </td>
                                <td>书名
                                </td>
                                <td>数量
                                </td>
                                <td>次数
                                </td>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Number") %></td>
                        <td><%# Eval("Title") %></td>
                        <td><%# Eval("Count") %></td>
                        <td><%# Eval("CurrentCount") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <asp:TextBox runat="server" TextMode="MultiLine" ID="console" Width="300" Height="500"></asp:TextBox>
    </form>
</body>
</html>
