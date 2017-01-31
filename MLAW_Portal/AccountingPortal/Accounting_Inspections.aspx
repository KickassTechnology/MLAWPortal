<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accounting_Inspections.aspx.cs" Inherits="MLAW_Order_System.AccountingPortal.Accounting_Inspections" %>


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
        <div class="nav">
        </div>

        <div class="header_button" style="width:200px;min-height:50px;float:right;" onclick="window.location.href='Accounting_Administration.aspx'" >
            <div style="float:left;margin-top:13px;margin-left:10px;"> 
                    ADMINISTRATION
            </div> 
        </div>
        <div  onmouseover="$('#ddl_admin').show();" onmouseout="$('#ddl_admin').hide();" class="header_button" style="width:200px;min-height:50px;float:right;"  onclick="window.location.href='Accounting_Default.aspx'">
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    FOUNDATION
                </div>
                <div style="clear:both"></div>
            </div>
        </div>
        <div  onmouseover="$('#ddl_insp').show();" onmouseout="$('#ddl_insp').hide();" class="header_button" style="width:200px;min-height:50px;float:right;" onclick="window.location.href='Accounting_Inspections.aspx'">
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    INSPECTIONS
                </div>
                <div style="clear:both"></div>
            </div>
        </div>
        <div style="clear:both"></div>
    </div>

    <div class="app_container" style="margin-top:60px;margin-left:0px;">
         <div id="admin_container">
            
            <div class="admin_body">
                <div id="foundation_order_list">
                    <div style="position:fixed;width:100%;top:40px;">     
                                  
                        <div id="header_filters">
                        <div style="height:20px;line-height:20px;">&nbsp;</div>
                        <div style="width:100%;text-align:left;border-top:1px solid #5a5a5a;border-bottom:1px solid #5a5a5a;min-height:40px;" id="order_types">
                            <div style="float:left">
                                <div style="width:180px;float:left;height:30px;" class="header_button header_button_on" onclick="loadInspectionAccountingOrders();" id="flt_ready">READY TO INVOICE</div>
                                <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doAcctInsNew();" id="flt_new">NEW</div>
                                <div style="width:180px;float:left;height:30px;" class="header_button" onclick="doAcctInsEverything();" id="flt_everything">EVERYTHING</div>
                            </div>                            
                            <div style="width:120px;float:right;height:30px;">
                                <img src="../Images/filter_white.png" style="float:left;height:28px;width:28px;margin-right:10px;margin-top:6px;cursor:pointer;" onclick="toggleFilters3()"/>
                                <img src="../Images/excel_white.png" style="float:left;height:24px;width:24px;margin-right:10px;margin-top:8px;cursor:pointer;" onclick="accountingExcelList(2)"/>

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
                                <div class="button" style="width:90px;float:left;" id="btn_update_list">
                                    UPDATE
                                </div>
                                <div style="clear:both"></div>
                            </div>
                            <div style="width:100%;text-align:left;margin-top:20px;margin-left:20px;">
                                <div style="width:200px;">
                                    MORE FILTERS
                                </div>

                                <div style="width:200px;float:left;"><input type="text" placeholder="client name" style="width:180px" id="flt_client" onkeyup="renderInspectionOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="division" style="width:180px" id="flt_division"  onkeyup="renderInspectionOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="subdivision" style="width:180px" id="flt_subdivision" onkeyup="renderInspectionOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="status" style="width:180px" id="flt_status" onkeyup="renderInspectionOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="type" style="width:180px" id="flt_type" onkeyup="renderInspectionOrders()"/></div>
                            
                                <div style="clear:both"></div>
                            </div>
                            <div style="width:100%;text-align:left;margin-top:20px;margin-left:20px;padding-bottom:16px;">
                                <div style="width:200px;">
                                    ADDRESS
                                </div>
                                <div style="width:600px;"><input type="text" placeholder="address" style="width:580px;" id="flt_address" onkeyup="renderInspectionOrders()"/></div>
                            </div>
                            </div>
                        </div>
                        <div style='border-bottom:1px solid #696969;width:100%;background:#2f2f2f;' id="ins_order_list_titles">
                            <div class="header_button" style="width:140px;height:24px;padding-top:10px;text-align:left;" onclick="resort_ins_orders('MLAW_Number')" id="ins_order_list_title_MLAW_Number">MLAW NUMBER</div>
                            <div class='header_button' style="text-align:left;width:90px;height:24px;padding-top:10px;" onclick="resort_insp_orders('Client_Short_Name')" id="ins_order_list_title_Client_Short_Name">CLIENT</div>
                            <div class="header_button" style="text-align:left;width:140px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Subdivision_Name')" id="ins_order_list_title_Subdivision_Name">SUBDIVISION</div>
                            <div class="header_button" style="text-align:left;width:240px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Address')" id="ins_order_list_title_Address">ADDRESS</div>
                            <div class="header_button" style="text-align:left;width:110px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Type')" id="ins_order_list_title_Type">TYPE</div>
                            <div class='header_button header_button_on' style="text-align:left;width:80px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Order_Date')" id="ins_order_list_title_Order_Date">DELIVERED</div>
                            <div class='header_button' style="text-align:left;width:80px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Amount')" id="ins_order_list_title_Amount">PRICE</div>
                        
                            <div style='clear:both'></div>
                        </div>
                    </div>
                    <div id="foundation_order_grid" style="margin-top:140px;"></div>
                </div>
          
                
            </div>
            <div style="margin-top:20px;width:1400px;display:none;" id="view_form"></div>
                    
        </div>
    </div>


    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="../filedrag.js"></script>
    <script src="../MLAW.js"></script>
    <script src="../inspections.js"></script>
    <script src="Accounting.js"></script>
    <asp:Label ID="JS" runat="server"></asp:Label>
    <script>


        window.onpopstate = function (event) {
            doWindowState(event.state);
        };


        $(document).ready(function () {
            var dToday = new Date();
            var lastWeek = addDays(dToday, -7);
            var yesterday = addDays(dToday, -1);
            var daybefore = addDays(dToday, -2);
            var tomorrow = addDays(dToday, 1);

            $('#foundation_order_list').show();


            var strStartDate = '10/1/2015';
            //var strStartDate = (lastWeek.getMonth() + 1) + "/" + lastWeek.getDate() + "/" + lastWeek.getFullYear();
            var strEndDate = (yesterday.getMonth() + 1) + "/" + yesterday.getDate() + "/" + yesterday.getFullYear();
            $('#flt_begin_date').val(strStartDate);
            $('#flt_end_date').val(strEndDate);
            loadInspectionAccountingOrders();

            $('#btn_update_list').on("click", function () {
                loadInspectionAccountingOrders();
            });

        });

        function doWindowState(state) {


        }


    </script>
</body>
</html>
