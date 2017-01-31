<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="data_dump.aspx.cs" Inherits="MLAW_Portal.data_dump" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:100px;margin-top:100px;">
        <div style="float:left;font-weight:bold;width:140px;">
            MLAW db file:
        </div>
        <div style="float:left">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <div style="clear:both"></div>
        <div style="height:20px;"></div>
        <div style="float:left;font-weight:bold;width:140px;">
            M-Revisn db file:
        </div>
        <div style="float:left">
            <asp:FileUpload ID="FileUpload2" runat="server" />
        </div>
        <div style="clear:both"></div>
        <div style="height:20px;"></div>
        <div style="float:left;font-weight:bold;width:140px;">
            M-ppour db file:
        </div>
        <div style="float:left">
            <asp:FileUpload ID="FileUpload3" runat="server" />
        </div>
        <div style="clear:both"></div>
        <div style="height:40px;"></div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload these files" />
        
    </div>
        
       
        
    </form>
</body>
</html>
