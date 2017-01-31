<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Designer_Default.aspx.cs" Inherits="MLAW_Order_System.DesignerPortal.Designer_Default" %>
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
        <div class="header_button" style="width:420px;min-height:50px;" id="quick_search_holder">
            <div style="float:left;padding-top:3px;margin-right:2px;">
                <img src="../Images/white_search.png" style="height:15px;width:15px;margin-right:2px;" />
            </div>
            <div style="float:left">
                 QUICK SEARCH
            </div>
            <div style="clear:both"></div>
            <input type="text" placeholder="MLAW number or address" style="width:410px;" id="quick_search" onkeyup="doQuickSearch()"/>
            <div id="quick_search_results" style="display:none;"></div>
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
        <div style="clear:both"></div>
    </div>
    <div class="app_container" style="margin-top:80px;margin-left:0px;">
         <div id="admin_container">
            <div id="admin_nav">&nbsp;

            </div>
            <div class="admin_body">
            <div id="foundation_order_list">
                <div style="margin-top:20px;font-size:32px;text-align:left;margin-left:40px;">
                    ASSIGNED TO ME
                </div>
                <div>
                    <div class="lst_title">MLAW Number</div>
                    <div class='lst_title' onmouseover="showFoundationClientList()" id="Div1" style="width:100px;">Client</div>
                    <div class="lst_title">Subdivision</div>
                    <div class="lst_title">Address</div>
                    <div class="lst_title" style="width:60px;">L/B/S</div>
                    <div class='lst_title' style="width:100px;" onmouseover="showFoundationStatusList()" id="Div2">Status</div>
                    <div class='lst_title' style="width:120px;">ASSIGNED TO</div>
                    <div class='lst_title' style="width:80px;" >Received</div>
                    <div class='lst_title' style="width:60px;">Due</div>

                    <div style='clear:both'></div>
                </div>
                <div id="my_order_grid">

                </div>
                <div style="margin-top:80px;font-size:32px;text-align:left;margin-left:40px;">
                    UNASSIGNED ORDERS
                </div>
                <div id="header_filters">
                    <div style="width:100%;text-align:left;border-top:1px solid #5a5a5a;border-bottom:1px solid #5a5a5a" id="order_types">
                        <div style="width:180px;float:left;height:30px;" class="header_button header_button_on" onclick="doOpenOrders();" id="flt_open">OPEN</div>
                        <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doLateOrders();" id="flt_late">LATE</div>
                        <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doTodayOrders();" id="flt_today">DUE TODAY</div>

                        <div style="width:120px;float:right;height:30px;">
                            <img src="../Images/filter_white.png" style="float:left;height:28px;width:28px;margin-right:10px;margin-top:6px;cursor:pointer;" onclick="toggleFilters2()"/>
                            <div style="clear:both"></div>
                        </div>
                        <div style="width:300px;float:right;height:20px;">
                            <div style="float:left;width:100px;padding-top:10px;"><input type="checkbox" onclick="checkAllUnassigned()" id="check_all_unassigned" /> check all</div>
                            <div style="float:left;padding-top:10px;"><input type="button" onclick="assignOrders()" value="assign to me"/></div>
                            <div style="clear:both"></div>
                        </div>

                        <div style="clear:both"></div>
                    </div>
                    <div id="filters" style="display:none;">
                        <div style="width:100%;text-align:left;margin-top:16px;margin-left:20px;">
                            <div style="width:200px;">
                                DATE FILTERS
                            </div>
                            <div style="width:200px;float:left;">
                                <select id="flt_date_type" style="width:182px;">
                                    <option value="1">Received Date</option>
                                    <option value="2">Due Date</option>
                                    <option value="3">Completed Date</option>
                                    <option value="4">Delivered Date</option>
                                </select>
                            </div>
                            <div style="width:200px;float:left;">
                                <input type="text" placeholder="begin date" id="flt_begin_date" style="width:180px;" />
                            </div>
                            <div style="width:200px;float:left;">
                                <input type="text" placeholder="end date" id="flt_end_date" style="width:180px;"/>
                            </div>
                            <div class="header_button header_button_on" style="width:90px;float:left;"  onclick="doOrderSlice(3);">
                                UPDATE
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="width:100%;text-align:left;margin-top:20px;margin-left:20px;">
                            <div style="width:200px;">
                                MORE FILTERS
                            </div>

                            <div style="width:200px;float:left;"><input type="text" placeholder="client name" style="width:180px" id="flt_client" onkeyup="renderOrders()"/></div>
                            <div style="width:200px;float:left;"><input type="text" placeholder="division" style="width:180px" id="flt_division"  onkeyup="renderOrders()"/></div>
                            <div style="width:200px;float:left;"><input type="text" placeholder="subdivision" style="width:180px" id="flt_subdivision" onkeyup="renderOrders()"/></div>
                            <div style="width:200px;float:left;"><input type="text" placeholder="status" style="width:180px" id="flt_status" onkeyup="renderOrders()"/></div>
                            <div style="width:200px;float:left;"><input type="text" placeholder="plan number" style="width:180px" id="flt_plan_no" onkeyup="renderOrders()"/></div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="width:100%;text-align:left;margin-top:20px;margin-left:20px;padding-bottom:16px;">
                            <div style="width:200px;">
                                ADDRESS
                            </div>
                            <div style="width:600px;"><input type="text" placeholder="address" style="width:580px;" id="flt_address" onkeyup="renderOrders()"/></div>
                        </div>
                        </div>
                    </div>
                    <div style='border-bottom:1px solid #696969;width:100%;background:#2f2f2f;' id="order_list_titles">
                        <div class="header_button" style="width:140px;height:24px;padding-top:10px;text-align:left;" onclick="resort_orders('MLAW_Number')" id="order_list_title_MLAW_Number">MLAW NUMBER</div>
                        <div class='header_button' style="text-align:left;width:90px;height:24px;padding-top:10px;" onclick="resort_orders('Client_Short_Name')" id="order_list_title_Client_Short_Name">CLIENT</div>
                        <div class="header_button" style="text-align:left;width:140px;height:24px;padding-top:10px;" onclick="resort_orders('Subdivision_Name')" id="order_list_title_Subdivision_Name">SUBDIVISION</div>
                        <div class="header_button" style="text-align:left;width:240px;height:24px;padding-top:10px;" onclick="resort_orders('Address')" id="order_list_title_Address">ADDRESS</div>
                        <div class="header_button" style="text-align:left;width:70px;height:24px;padding-top:10px;" onclick="resort_orders('Lot')" id="order_list_title_Lot">L/B/S</div>
                        <div class='header_button' style="text-align:left;width:100px;height:24px;padding-top:10px;" onclick="resort_orders('Order_Status_Desc')" id="order_list_title_Order_Status_Desc">STATUS</div>
                        <div class='header_button' style="text-align:left;width:120px;height:24px;padding-top:10px;" onclick="resort_orders('Last_Name')" id="order_list_title_Last_Name">ASSIGNED TO</div>
                        <div class='header_button' style="text-align:left;width:80px;height:24px;padding-top:10px;" onclick="resort_orders('Received_Date')" id="order_list_title_Received_Date">RECEIVED</div>
                        <div class='header_button header_button_on' style="text-align:left;width:80px;height:24px;padding-top:10px;" onclick="resort_orders('Due_Date')" id="order_list_title_Due_Date">DUE</div>

                        <div style='clear:both'></div>
                    </div>
               


                <div id="foundation_order_grid">

                </div>


            </div>
            <div style="margin-left:40px;margin-top:20px;width:1400px;display:none;" id="view_form"></div>
                    
        </div>
    </div>


    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="../filedrag.js"></script>
    <script src="../MLAW.js"></script>
    <asp:Label ID="JS" runat="server"></asp:Label>
    <script>

        $(document).ready(function () {
            
            loadOpenOrders();
            loadUserOrders();
        });

        window.onpopstate = function (event) {
            doWindowState(event.state);
        };

        $('#view_form').load("../html/view_foundation_order.html", function () {

            var obj = $("#dragandrophandler");
            obj.on('dragenter', function (e) {
                e.stopPropagation();
                e.preventDefault();
                $(this).css('border', '2px solid #0B85A1');
            });
            obj.on('dragover', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });
            obj.on('drop', function (e) {

                $(this).css('border', '2px dotted #0B85A1');
                e.preventDefault();
                var files = e.originalEvent.dataTransfer.files;

                //We need to send dropped files to Server
                handleFileUpload(files, obj, 1);
            });


            $(document).on('dragenter', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });
            $(document).on('dragover', function (e) {
                e.stopPropagation();
                e.preventDefault();
                obj.css('border', '2px dotted #0B85A1');
            });
            $(document).on('drop', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });


        });

        window.onpopstate = function (event) {
            if (event.state == null) {
                $('#view_form').hide();
                $('#foundation_order_list').show();
                $(document).scrollTop(0, 0);
            }
            //alert("location: " + document.location + ", state: " + JSON.stringify(event.state));
        };

    </script>
</body>
</html>


