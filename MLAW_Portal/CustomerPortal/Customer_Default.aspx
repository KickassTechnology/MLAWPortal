<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer_Default.aspx.cs" Inherits="MLAW_Order_System.CustomerPortal.Customer_Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="../MLAW.css" media="screen" />
</head>

<body>
    <div id="type_ahead"></div>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="false">
    <div id="mlaw_header">
        <div class="logo"></div>

        <div style="clear:both"></div>
    </div>
    <div style="margin-top:80px;">

         <div class="app_container" style="text-align:left;">
            <div id="customer_portal">
                <div style="float:left">
                    <div style="font-weight:bold;margin-bottom:10px;font-size:22px;">RECENT ORDERS</div>
                    <div id="recent_orders"></div>
                </div>
                <div style="float:left;margin-left:80px;">
                    <div style="font-weight:bold;margin-bottom:10px;font-size:22px;">ORDER LOOKUP</div>
                        <input type="text" placeholder="Street Address" style="width:410px;" id="quick_search" onkeyup="doCustomerQuickSearch()"/>
                        <div id="quick_search_results" style="margin-top:20px;"></div>
                </div>
             <div style="clear:both"></div>
        </div>
        <asp:Label ID="ClientId" runat="server"></asp:Label>
    </div>

    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="../filedrag.js"></script>
    <script src="../MLAW.js"></script>
    <script>


        window.onpopstate = function (event) {
            doWindowState(event.state);
        };

        function doWindowState(state) {


        }

        $(document).ready(function () {
            loadCustomerOrders(Client_Id);
        });

    </script>
</body>
</html>


