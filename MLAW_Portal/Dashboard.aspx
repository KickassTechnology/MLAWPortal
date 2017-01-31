<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MLAW_Order_System.Dashboard" %>

<!DOCTYPE html>
<head id="Head1" runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="MLAW.css" media="screen" />
        <style>

        path {
          stroke: #fff;
          fill-rule: evenodd;
        }

        text {
          font-family: Arial, sans-serif;
          font-size: 12px;
        }

        #completed_orders rect{
          fill: #4aaeea;
        }

        #completed_orders text{
          fill: #000000;
          font: 10px sans-serif;
          text-anchor: end; 
        }

        .axis text{
          font: 10px sans-serif;
        }

        .axis path, .axis line{
          fill: none;
          stroke : #000;
          shape-rendering: crispEdges;
        }


        </style>
</head>
<body>

    <div id="type_ahead">


    </div>
    <div id="mlaw_header">
        <div class="logo"></div>
        <div class="header_button" style="width:420px;min-height:50px;" id="quick_search_holder">
            <div style="float:left;padding-top:3px;margin-right:2px;">
                <img src="Images/white_search.png" style="height:15px;width:15px;margin-right:2px;" />
            </div>
            <div style="float:left">
                 QUICK SEARCH
            </div>
            <div style="clear:both"></div>
            <input type="text" placeholder="MLAW number or address" style="width:410px;" id="quick_search" onkeyup="doQuickSearch()"/>
            <div id="quick_search_results" style="display:none;"></div>
        </div>
        <div class="header_button" style="width:120px;min-height:50px;" onclick="window.location.href='default.aspx'">
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    ORDERS
                </div>
                <div style="clear:both"></div>
            </div>
        </div>
        <div class="header_button" style="width:240px;min-height:50px;" onclick="window.location.href='CC_800s.aspx'">
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    CUSTOM / COMMERCIAL
                </div>
                <div style="clear:both"></div>
            </div>
        </div>
        <div class="header_button" style="width:130px;height:50px;" onclick="window.location.href='Dashboard.aspx'">
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
        <div  onmouseover="$('#ddl_admin').show();" onmouseout="$('#ddl_admin').hide();" class="header_button" style="width:200px;min-height:50px;float:right;" >
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    ADMINISTRATION
                </div>
                
                <div style="float:left;margin-left:10px;">
                    <img src="Images/white_down_arrow.png"  style="width:16px;height:9px;"/>
                </div>
                <div style="clear:both"></div>
            </div>
            <div style="margin-top:10px;margin-left:10px;display:none;" id="ddl_admin">
                <div style="float:left;margin-top:10px;cursor:pointer;"onclick="window.location.href='Administration.aspx?Type=Clients'"> 
                    CLIENTS
                </div>
                
                <div style="clear:both"></div>
                <div style="float:left;margin-top:20px;cursor:pointer" onclick="window.location.href='Administration.aspx'"> 
                    USERS
                </div>
                
                <div style="clear:both"></div>
            </div>
        </div>

        <div style="clear:both"></div>
    </div>

    <form id="form1" runat="server">
    <div class="app_container">
        <div style="height:60px;line-height:60px;">&nbsp;</div>
         <div id="admin_container" style="margin:0 auto;text-align:center;width:100%">
             <!--
            <div id="admin_nav">
                <div class="admin_nav_item" style="height:0px;padding-top:5px;">&nbsp;</div>
                <div class="admin_nav_item admin_nav_item_on">by Customer</div>
                <div class="admin_nav_item">By employee</div>
                <div class="admin_nav_item">By status</div>
                <div class="admin_nav_item" style="height:800px;"></div>
            </div>-->
            <div style="margin:0 auto; text-align:center;width:100%">
                <div style="float:left;width:50%;">
                    <div style="margin-top:20px;font-size:32px;">
                    OPEN ORDERS BY STATUS
                    </div>
                    <div id="chrt_open_orders" style="width: 600px; height: 600px; margin: 0 auto"></div>
                </div>
                <div style="float:left;width:50%;">
                    <div style="margin-top:20px;font-size:32px;"> 
                    OPEN ORDERS BY DESIGNER
                    </div>
                <div id="chrt_designer_open_orders" style="width: 600px; height: 600px; margin: 0 auto"></div>
                </div>
                <div style="clear:both"></div>
                <div style="width:100%;text-align:left;margin-top:20px;">
                    <div style="margin-bottom:40px;">
                        <div style="font-size:32px;float:left;margin-left:40px;">
                            DELIVERED ORDERS
                        </div>
                        <div style="font-size:22px;float:left;margin-left:40px;padding-top:5px;">    
                             <input type="text" placeholder="begin date" id="comp_order_begin" /> TO <input type="text" placeholder="end date" id="comp_order_end" />
                        </div>
                        
                        <div style="float:left;margin-left:50px;padding-top:15px;"><input type="button" value="update" onclick="loadCompletedOrders()"/></div>
                        <div style="clear:both;"></div>
                    </div>
                    <div id="total_orders" style="margin-left:40px;font-size:30px;margin-bottom:10px;"></div>
                    <div id="completed_orders" style="width: 1200px; height: 600px;margin-left:20px;"></div>
                    <div id="completed_orders_details" style="width: 1200px;margin-left:40px;"></div>


                </div>

            </div>
            <div style="clear:both"></div>
         </div>
    </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
   
        <script src="MLAW.js"></script>
        <script src="js/d3.js"></script>
        <script src="js/charting.js"></script>
        <script>
            $(document).ready(function () {
                loadChartData();
                loadDesignerChartData();

                var date = new Date();
                var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
                $('#comp_order_begin').val((firstDay.getMonth() + 1) + "/" + firstDay.getDate() + "/" + firstDay.getFullYear());
                $('#comp_order_end').val((date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear());

                loadCompletedOrders();
                
            });

        </script>
    </form>
</body>
</html>
