<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Warranties.aspx.cs" Inherits="MLAW_Order_System.AccountingPortal.Warranties" %>


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
        <div  onmouseover="$('#ddl_admin').show();" onmouseout="$('#ddl_admin').hide();" class="header_button" style="width:200px;min-height:50px;float:right;" >
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    FOUNDATION
                </div>
                
                <div style="float:left;margin-left:10px;">
                    <img src="../Images/white_down_arrow.png"  style="width:16px;height:9px;"/>
                </div>
                <div style="clear:both"></div>
            </div>
            <div style="margin-top:10px;margin-left:10px;display:none;" id="ddl_admin">
                <div style="float:left;margin-top:10px;cursor:pointer;"onclick="window.location.href='Accounting_Default.aspx?Type=Invoicing'"> 
                    INVOICING
                </div>
                
                <div style="clear:both"></div>
                <div style="float:left;margin-top:20px;cursor:pointer" onclick="window.location.href='Accounting_Default.aspx?Type=New'"> 
                    NEW ORDERS
                </div>
                
                <div style="clear:both"></div>
            </div>
        </div>
        <div  onmouseover="$('#ddl_insp').show();" onmouseout="$('#ddl_insp').hide();" class="header_button" style="width:200px;min-height:50px;float:right;" >
            <div style="margin-top:12px;margin-left:10px;">
                <div style="float:left;margin-top:1px;"> 
                    INSPECTIONS
                </div>
                
                <div style="float:left;margin-left:10px;">
                    <img src="../Images/white_down_arrow.png"  style="width:16px;height:9px;"/>
                </div>
                <div style="clear:both"></div>
            </div>
            <div style="margin-top:10px;margin-left:10px;display:none;" id="ddl_insp">
                <div style="float:left;margin-top:10px;cursor:pointer;" onclick="window.location.href='Accounting_Inspections.aspx'"> 
                        INVOICING
                </div>
                
                <div style="clear:both"></div>
                <!--
                <div style="float:left;margin-top:20px;cursor:pointer" onclick="window.location.href='Accounting_Default.aspx?Type=New'"> 
                    NEW ORDERS
                </div>-->
                
                <div style="clear:both"></div>
            </div>
        </div>
        <div class="header_button" style="width:200px;min-height:50px;float:right;" onclick="window.location.href='Warranties.aspx'" >
            <div style="float:left;margin-top:13px;margin-left:10px;"> 
                    WARRANTIES
            </div> 
        </div>

        <div style="clear:both"></div>
    </div>
    <div class="app_container" style="margin-top:60px;margin-left:0px;">
         <div id="admin_container">
            
            <div class="admin_body">
                <div id="warranty_order_list">
                    <div style="position:fixed;width:100%;top:40px;">     
                                  
                        <div id="header_filters">
                        <div style="height:20px;line-height:20px;">&nbsp;</div>
                        <div style="width:100%;text-align:left;border-top:1px solid #5a5a5a;border-bottom:1px solid #5a5a5a;min-height:40px;" id="order_types">
                            <div style="width:120px;float:right;height:30px;">
                                <img src="../Images/add_white.png" style="float:left;height:24px;width:24px;margin-right:10px;margin-top:8px;cursor:pointer;" onclick="toggleWarrantyEntry()"/>
                                <img src="../Images/filter_white.png" style="float:left;height:28px;width:28px;margin-right:10px;margin-top:6px;cursor:pointer;" onclick="toggleFilters3()"/>
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

                                <div style="width:200px;float:left;"><input type="text" placeholder="client name" style="width:180px" id="flt_client" onkeyup="renderWarrantyOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="division" style="width:180px" id="flt_division"  onkeyup="renderWarrantyOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="subdivision" style="width:180px" id="flt_subdivision" onkeyup="renderWarrantyOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="status" style="width:180px" id="flt_status" onkeyup="renderWarrantyOrders()"/></div>
                                <div style="width:200px;float:left;"><input type="text" placeholder="type" style="width:180px" id="flt_type" onkeyup="renderWarrantyOrders()"/></div>
                            
                                <div style="clear:both"></div>
                            </div>
                            <div style="width:100%;text-align:left;margin-top:20px;margin-left:20px;padding-bottom:16px;">
                                <div style="width:200px;">
                                    ADDRESS
                                </div>
                                <div style="width:600px;"><input type="text" placeholder="address" style="width:580px;" id="flt_address" onkeyup="renderWarrantyOrders()"/></div>
                            </div>
                            </div>
                        </div>
                        <div style='border-bottom:1px solid #696969;width:100%;background:#2f2f2f;' id="order_list_titles">
                            <div class="header_button" style="width:140px;height:24px;padding-top:10px;text-align:left;" onclick="resort_warranty_orders('MLAW_Number')" id="order_list_title_MLAW_Number">MLAW NUMBER</div>
                            <div class='header_button' style="text-align:left;width:90px;height:24px;padding-top:10px;" onclick="resort_warranty_orders('Client_Short_Name')" id="order_list_title_Client_Short_Name">CLIENT</div>
                            <div class="header_button" style="text-align:left;width:140px;height:24px;padding-top:10px;" onclick="resort_warranty_orders('Subdivision_Name')" id="order_list_title_Subdivision_Name">SUBDIVISION</div>
                            <div class="header_button" style="text-align:left;width:240px;height:24px;padding-top:10px;" onclick="resort_warranty_orders('Address')" id="order_list_title_Address">ADDRESS</div>
                            <div class="header_button" style="text-align:left;width:400px;height:24px;padding-top:10px;" onclick="resort_warranty_orders('Warranty_Notes')" id="order_list_title_Warranty_Notes">WARRANTY NOTES</div>
                            <div class='header_button header_button_on' style="text-align:left;width:80px;height:24px;padding-top:10px;" onclick="resort_warranty_orders('Order_Date')" id="order_list_title_Order_Date">DATE</div>
                            <div style='clear:both'></div>
                        </div>
                    </div>
                    <div id="warranty_order_grid" style="margin-top:140px;"></div>
                </div>
                <div id="warranty_form" style="display:none;margin-top:100px;">
                        <div style="margin-left:40px;margin-top:40px;text-align:left;">
                            <div class="frm_row" id="mlaw_row">
                                <div class="frm_label" style="width:120px;">
                                    MLAW Number:
                                </div>
                                <div class="frm_label">
                                    <div id="war_mlaw_number"></div>
                                </div>
                                <div style="clear:both"></div>
                            </div>
                            <div class="frm_row">
                                <div class="frm_label" style="width:120px;">
                                    Address:
                                </div>
                                <div class="frm_input">
                                    <input type="text" id="war_address" style="width:300px;" onkeydown="doWarAddressMatches()"/>
                                </div>
                                <div style="clear:both"></div>
                            </div>
                            <div class="frm_row">
                                <div class="frm_label" style="width:120px;">
                                    Matches:
                                </div>
                                <div class="frm_input" id="war_address_matches">


                                </div>
                                <input type="hidden" id="ord_war_order_id" />
                                <div style="clear:both"></div>
                            </div>
                            <div class="frm_row">
                                <div class="frm_label" style="width:120px;">
                                    Notes:
                                </div>
                                <div class="frm_input">
                                    <textarea id="war_notes" style="width:400px;height:70px;">

                                    </textarea>
                                <input type="hidden" id="war_order_id" />
                                </div>
                                <div style="clear:both"></div>
                            </div>
                            <div class="frm_row">
                                <div class="frm_label" style="width:120px;">&nbsp;</div>
                                <div class="frm_input">
                                    <div class="button" style="width:120px;" onclick="handleWarrantyOrder()">save</div>
                                </div>
                            </div>
                            <div class="frm_row"></div>




                        </div>

                </div>
            </div>
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

            $('#warranty_order_list').show();
            var strStartDate = (lastWeek.getMonth() + 1) + "/" + lastWeek.getDate() + "/" + lastWeek.getFullYear();
            var strEndDate = (tomorrow.getMonth() + 1) + "/" + tomorrow.getDate() + "/" + tomorrow.getFullYear();
            $('#flt_begin_date').val(strStartDate);
            $('#flt_end_date').val(strEndDate);
            getWarrantyOrders();

            $('#btn_update_list').on("click", function () {
                getWarrantyOrders();
            });
        });
    </script>
</body>
</html>
