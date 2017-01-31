<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MLAW_Order_System.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="MLAW.css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="position:absolute;top:0px;left:0px;opacity:.7;background:#000000;width:100%;height:100%;z-index:2">
    </div>
    <div style="position:absolute;top:140px;width:100%;height:100%;text-align:center;z-index:3;margin:0 auto;" id="login_box">
        <div style="background:#f1f1f1;border-radius:15px;height:260px;width:380px;margin:0 auto;font-weight:600;">
            <div style="width:340px;background:#ffffff;height:140px;padding:20px;border-top-left-radius:15px;border-top-right-radius:15px;">
                <div style="margin-left:20px;margin-top:20px;">
                    <input type="text" placeholder="Username" id="login_username" runat="server" onfocus="this.placeholder = ''" onblur="this.placeholder = 'USERNAME'"/>
                </div>
                <div style="margin-left:20px;margin-top:20px;">
                    <input type="password" placeholder="PASSWORD" id="login_password" runat="server" onfocus="this.placeholder = ''" onblur="this.placeholder = 'PASSWORD'"/>
                </div>

            </div>
            <div style="padding-bottom:20px;text-align:center;margin:0 auto;">
                <div class="button" style="width:150px;margin: 0 auto;margin-top:20px;font-weight:400;height:30px;padding-top:8px;" onclick="form1.submit()">LOGIN</div>
            </div>
        </div>
    </div>
    <div id="mlaw_header">
        <div class="logo"></div>
        <div class="header_button" style="width:370px;height:50px;">
            <div style="float:left;padding-top:3px;margin-right:2px;">
                <img src="Images/white_search.png" style="height:15px;width:15px;margin-right:2px;" />
            </div>
            <div style="float:left">
                 QUICK SEARCH
            </div>
            <div style="clear:both"></div>
            <input type="text" placeholder="MLAW number or address" style="width:360px;" id="quick_search"/>

        </div>
        <div class="header_button" style="width:120px;height:50px;">
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    ORDERS
                </div>
                <div style="float:left;margin-left:10px;">
                    <img src="Images/white_down_arrow.png"  style="width:16px;height:9px;"/>
                </div>
                <div style="clear:both"></div>
            </div>
        </div>
        <div class="header_button" style="width:130px;height:50px;">
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    DASHBOARD
                </div>
                <!--
                <div style="float:left;margin-left:10px;">
                    <img src="Images/white_down_arrow.png"  style="width:16px;height:9px;"/>
                </div>-->
                <div style="clear:both"></div>
            </div>
        </div>
        <div class="header_button" style="width:160px;height:50px;float:right;">
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    ADMINISTRATION
                </div>
                <!--
                <div style="float:left;margin-left:10px;">
                    <img src="Images/white_down_arrow.png"  style="width:16px;height:9px;"/>
                </div>-->
                <div style="clear:both"></div>
            </div>
        </div>
        <div style="clear:both"></div>
    </div>
  
    <div class="app_container">
         <div id="admin_container" style="min-height:800px;">
            <div style="height:100px;">&nbsp;</div>
            <div style="font-size:28px;font-weight:500"> 
                USER LOGIN
             </div>
            <div style="height:40px;">&nbsp;</div>

            <asp:TextBox runat="server" ID="email" placeholder="email"></asp:TextBox>
            <br />
           
            <asp:TextBox runat="server" ID="password" TextMode="Password" placeholder="password"></asp:TextBox>
            <br />
            <br />

            <asp:Label ID="error_msg" runat="Server"></asp:Label>
            <asp:Button Text="Login" ID="login" runat="server" OnClick="login_Click" />
          </div>
      </div>
    </div>
    </form>    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="MLAW.js"></script>
    <script>
        $(document).ready(function () {
            $('#login_username').focus();
        });

    </script>
</body>
</html>
