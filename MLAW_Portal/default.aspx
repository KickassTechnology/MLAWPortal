<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MLAW_Order_System._default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="MLAW.css" media="screen" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
</head>

<body>
    <div id="type_ahead"></div>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="false">
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
        </div>
        <div style="clear:both"></div>
    </div>
    <div class="app_container">
            <div style="height:40px;line-height:40px;">&nbsp;</div>
         <div id="admin_container" style="width:100%;">
            <div class="admin_body">
              
            <div id="foundation_order_list">
                <div style="position:fixed;width:100%;top:40px;">
                    <div id="header_filters">
                        <div style="height:20px;line-height:20px;">&nbsp;</div>
                        <div style="width:100%;text-align:left;border-top:1px solid #5a5a5a;border-bottom:1px solid #5a5a5a" id="order_types">
                            <div style="width:180px;float:left;height:30px;" class="header_button header_button_on" onclick="doOpenOrders();" id="flt_open">OPEN</div>
                            <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doLateOrders();" id="flt_late">LATE</div>
                            <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doTodayOrders();" id="flt_today">DUE TODAY</div>
                            <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doOrderSlice(1);" id="flt_complete">COMPLETED</div>
                            <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doOrderSlice(3);" id="flt_delivered">DELIVERED</div>
                            <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doOrderSlice(2);" id="flt_everything">EVERYTHING</div>
                            <div style="width:120px;float:right;height:30px;">
                                <img src="Images/add_white.png" style="float:left;height:24px;width:24px;margin-right:10px;margin-top:8px;cursor:pointer;" onclick="toggleFoundationEntry()"/>
                                <img src="Images/filter_white.png" style="float:left;height:28px;width:28px;margin-right:10px;margin-top:6px;cursor:pointer;" onclick="toggleFilters()"/>
                                <img src="Images/excel_white.png" style="float:left;height:24px;width:24px;margin-right:10px;margin-top:8px;cursor:pointer;" onclick="excelList()"/>
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
                        <div class='header_button' style="text-align:left;width:70px;height:24px;padding-top:10px;" onclick="resort_orders('Client_Short_Name')" id="order_list_title_Client_Short_Name">CLIENT</div>
                        <div class="header_button" style="text-align:left;width:140px;height:24px;padding-top:10px;" onclick="resort_orders('Subdivision_Name')" id="order_list_title_Subdivision_Name">SUBDIVISION</div>
                        <div class="header_button" style="text-align:left;width:220px;height:24px;padding-top:10px;" onclick="resort_orders('Address')" id="order_list_title_Address">ADDRESS</div>
                        <div class="header_button" style="text-align:left;width:70px;height:24px;padding-top:10px;" onclick="resort_orders('Lot')" id="order_list_title_Lot">L/B/S</div>
                        <div class='header_button' style="text-align:left;width:100px;height:24px;padding-top:10px;" onclick="resort_orders('Order_Status_Desc')" id="order_list_title_Order_Status_Desc">STATUS</div>
                        <div class='header_button' style="text-align:left;width:120px;height:24px;padding-top:10px;" onclick="resort_orders('Last_Name')" id="order_list_title_Last_Name">ASSIGNED TO</div>
                        <div class='header_button' style="text-align:left;width:80px;height:24px;padding-top:10px;" onclick="resort_orders('Received_Date')" id="order_list_title_Received_Date">RECEIVED</div>
                        <div class='header_button header_button_on' style="text-align:left;width:80px;height:24px;padding-top:10px;" onclick="resort_orders('Due_Date')" id="order_list_title_Due_Date">DUE</div>

                        <div style='clear:both'></div>
                    </div>
                </div>
                <div id="foundation_order_grid" style="margin-top:100px;">

                </div>
            </div>
            <div id="foundation_editing" style="display:none;">


            </div>
            <div id="foundation_entry_container" style="display:none;text-align:left;padding:40px;width:1200px;overflow:hidden;"></div>


        </div>

    </div>
    <div id="grey_out" style="position:absolute;top:0px;left:0px;opacity:.7;background:#000000;width:100%;height:100%;z-index:2;display:none;">
    <i id="spinner" class="fa fa-spinner fa-pulse fa-5x" style="color:#ffffff;z-index:3;position:absolute;top:240px;display:none;"></i>
    </div>
        <div id="order_popup"></div>
        <div id="status_list">
            <div class="lst_title" id="status_list_title"></div>
            <div style="clear:both"></div>
            
            <div id="status_list_container"></div>

        </div>

        <div id="create_subdivision_popup">
            <div style="width:100%;margin-bottom:30px;margin-top:10px;">
                <div style="float:left;font-weight:bold;margin-left:20px;margin-top:3px; text-align:left;">CREATE SUBDIVISION</div>
                <div style="float:right"><img src="Images/close.png" style="width:20px;height:20px;cursor:pointer;margin-right:10px;" onclick="hide_subdivision_popup();" /></div>
                <div style="clear:both"></div>
            </div>
            <div class="frm_row" style="width:400px;">
                <div class="frm_label" style="width:100px;margin-right:20px;text-align:right;">
                    CLIENT:
                </div>
                <div class="frm_input" style="width:280px;text-align:left;">
                    <select id="new_sub_client" style="width:174px;">
                    </select>
                </div>
                <div style="clear:both"></div>
            </div>
            <div class="frm_row" style="width:400px;">
                <div class="frm_label" style="width:100px;margin-right:20px;text-align:right;">
                    NAME:
                </div>
                <div class="frm_input" style="width:280px;text-align:left;">
                    <input type="text" id="new_sub_name" />
                </div>
                <div style="clear:both"></div>
            </div>
            <div class="frm_row" style="width:400px;">
                <div class="frm_label" style="width:100px;margin-right:20px;text-align:right;">
                    NUMBER:
                </div>
                <div class="frm_input" style="width:280px;text-align:left;">
                    <input type="text" id="new_sub_number" />
                </div>
                <div style="clear:both"></div>
            </div>
            <div class="frm_row" style="width:400px;">
                <div class="frm_label" style="width:100px;margin-right:20px;text-align:right;">
                    DIVISION:
                </div>
                <div class="frm_input" style="width:280px;text-align:left;">
                    <select id="new_sub_division" style="width:174px;">
                    </select>
                </div>
                <div style="clear:both"></div>
            </div>
            <div class="frm_row" style="width:400px;margin-top:10px;">
                <div class="frm_label" style="width:100px;margin-right:20px;text-align:right;">
                    &nbsp;&nbsp;&nbsp;
                </div>
                <div class="frm_input" style="width:280px;">
                    <div class="button" style="width:150px;" onclick="createNewSubdivision()">create</div>
                </div>
                <div style="clear:both"></div>
            </div>
        </div>
    </form>
    <script src="js/jquery-1.11.0.min.js"></script>
    <script src="filedrag.js"></script>
    <script src="MLAW.js"></script>
    <script>

        $(document).ready(function () {
            loadSubdivisionListArray();
            loadClientListArray();
            loadDivisions();
        });

        if (window.location.href.indexOf("Order_Id") < 0) {
            if (window.location.href.indexOf("create_order") != -1) {
                toggleFoundationEntry();
            } else {
                loadOpenOrders();
            }
        } else {
            var orderId = getParameterByName("Order_Id");
            var mlawNumber = getParameterByName("MLAW_Number");
            if (editMode == 0) {
                editFoundationOrder(orderId, mlawNumber);
            } else {
                displayFoundationOrder(orderId);
            }
        }


        /*
        $('#foundation_entry_container').load("html/create_foundation_order.html", function () 
        {

        });*/



        window.onpopstate = function (event) {
               doWindowState(event.state);
        };

        function doWindowState(state) {
       
            if (state == null) {
                $('#foundation_editing').hide();
                $('#foundation_entry_container').hide();
                $('#foundation_order_list').show();
            } 

        }


        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
      
        
        $(document).on('dragenter', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });
        $(document).on('dragover', function (e) {
            e.stopPropagation();
            e.preventDefault();
            //obj.css('border', '2px dotted #0B85A1');
        });
        $(document).on('drop', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });

    </script>
</body>
</html>


