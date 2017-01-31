<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accounting_Administration.aspx.cs" Inherits="MLAW_Order_System.AccountingPortal.Accounting_Administration" %>

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

                <div id="administration" style="margin-top:61px;">
                    <div id="admin_client_list_holder">
                        <div id="admin_client_list_header" style='border-bottom:1px solid #696969;width:100%;background:#2f2f2f;height:38px;' >
                            <div class="header_button header_button_on" style="width:110px;height:24px;padding-top:10px;text-align:left;" onclick="resort_clients('Client_Short_Name')" id="client_list_Client_Short_Name">SHORT NAME</div>
                            <div class='header_button' style="text-align:left;width:190px;height:24px;padding-top:10px;" onclick="resort_clients('Client_Full_Name')" id="client_list_Client_Full_Name">FULL NAME</div>
                            <div class="header_button" style="text-align:left;width:190px;height:24px;padding-top:10px;" onclick="resort_clients('Billing_Address_1')" id="client_list_Billing_Address_1">ADDRESS 1</div>
                            <div class="header_button" style="text-align:left;width:100px;height:24px;padding-top:10px;" onclick="resort_clients('Billing_Address_2')" id="client_list_Billing_Address_2">ADDRESS 2</div>
                            <div class="header_button" style="text-align:left;width:100px;height:24px;padding-top:10px;" onclick="resort_clients('Billing_City')" id="client_list_Billing_City">CITY</div>
                            <div class="header_button" style="text-align:left;width:70px;height:24px;padding-top:10px;" onclick="resort_clients('Billing_State')" id="client_list_Billing_State">STATE</div>
                            <div class='header_button' style="text-align:left;width:120px;height:24px;padding-top:10px;" onclick="resort_clients('Billing_Postal_Code')" id="client_list_Billing_Postal_Code">ZIP</div>
                                                </div>
                        <div id="admin_client_list" style="margin-left:20px;"></div>
                    </div>

                    <div id="admin_client_form" style="display:none;text-align:left;margin-left:40px;">
                        <div style="font-size:22px;margin-bottom:10px;">BILLING INFORMATION</div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Vendor #:</div>
                            <div class="frm_input"><input type="text" id="admin_client_vendor_number" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Client Short Name:</div>
                            <div class="frm_input"><input type="text" id="admin_client_short_name" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Client Full Name:</div>
                            <div class="frm_input"><input type="text" id="admin_client_full_name" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Address:</div>
                            <div class="frm_input"><input type="text" id="admin_client_address1" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">&nbsp;</div>
                            <div class="frm_input"><input type="text" id="admin_client_address2" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">City:</div>
                            <div class="frm_input"><input type="text" id="admin_client_city" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">State:</div>
                            <div class="frm_input"><input type="text" id="admin_client_state" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Zip:</div>
                            <div class="frm_input"><input type="text" id="admin_client_postal_code" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Attn:</div>
                            <div class="frm_input"><input type="text" id="admin_client_attn" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Phone:</div>
                            <div class="frm_input"><input type="text" id="admin_client_phone" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Fax:</div>
                            <div class="frm_input"><input type="text" id="admin_client_fax" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="height:40px;">&nbsp;</div>
                        <div style="font-size:22px;margin-bottom:10px;">FOUNDATION PRICING</div>
                        <div class="frm_row">
                            <div style="width:320px;float:left;">&nbsp;</div>
                            <div class="frm_label" style="width:190px;">PRICE</div>
                            <div class="frm_label" style="width:200px;">SQ FT THRESHOLD</div>
                            <div style="clear:both"></div>

                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Pay Tier 1:</div>
                            <div class="frm_input" style="width:140px;">
                                <input type="radio" name="pay_tier_1" value="1" checked="checked" /> flat fee<br />
                                <input type="radio" name="pay_tier_1" value="2" /> by sq ft.<br />
                            </div>
                            <div class="frm_input"><input type="text" id="admin_pay_tier_1" placeholder="amount"/> &nbsp;&nbsp;&nbsp;<input type="text" id="admin_threshold_1" placeholder="threshold"/></div>
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Pay Tier 2:</div>
                            <div class="frm_input" style="width:140px;">
                                <input type="radio" name="pay_tier_2" value="1" checked="checked"/> flat fee<br />
                                <input type="radio" name="pay_tier_2" value="2" /> by sq ft.<br />
                            </div>
                            <div class="frm_input"><input type="text" id="admin_pay_tier_2"  placeholder="amount"/> &nbsp;&nbsp;&nbsp;<input type="text" id="admin_threshold_2" placeholder="threshold"/></div>
                            
                            <div style="clear:both"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Pay Tier 3:</div>
                            <div class="frm_input" style="width:140px;">
                                <input type="radio" name="pay_tier_3" value="1" checked="checked" /> flat fee<br />
                                <input type="radio" name="pay_tier_3" value="2" /> by sq ft.<br />
                            </div>
                            <div class="frm_input"><input type="text" id="admin_pay_tier_3"  placeholder="amount"/></div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="height:40px;">&nbsp;</div>
                        <div style="font-size:22px;margin-bottom:10px;">REVISION PRICING</div>
                        <div class="frm_row">
                            <div class="frm_label" style="width:180px;">Normal:</div>
                            <div class="frm_input"><input type="text" id="rev_base"/></div>
                            <div style="clear:both"></div>
                        </div>
                       <div class="frm_row">
                            <div class="frm_label" style="width:180px;">New House:</div>
                            <div class="frm_input"><input type="text" id="rev_new_house" /></div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="height:40px;">&nbsp;</div>
                        <div style="font-size:22px;margin-bottom:10px;">INSPECTION PRICING</div>
                        <div class="frm_row">
                            <input type="checkbox" id="vpo" /> VPO Required?
                        </div>
                        <div id="inspection_pricing"></div>
                        <input type="hidden" id="acct_client_id" />
                        <div class="button" onclick="updateAcctClient()" style="width:140px;margin-top:20px;margin-bottom:100px;">update</div>
                    </div>
                </div>
            </div>
            <div style="margin-top:20px;width:1400px;display:none;" id="view_form"></div>
                    
        </div>
    </div>


    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="../MLAW.js"></script>
    <script src="../inspections.js"></script>
    <script src="Accounting.js"></script>
    <asp:Label ID="JS" runat="server"></asp:Label>
    <script>


        window.onpopstate = function (event) {
            doWindowState(event.state);
        };


        $(document).ready(function () {
             loadClientPricing();
        });

        function doWindowState(state) {

            if (state == null) {
                $('#admin_client_form').hide();
                $('#admin_client_list_holder').show();
            }

        }

    </script>
</body>
</html>
