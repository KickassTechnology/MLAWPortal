<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="MLAW_Order_System.Administration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="MLAW.css" media="screen" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="MLAW.js"></script>
    <script>
        function handleUserLevelSelection() {
            if ($('#User_Level_Id').val() == 3) {
                $('#Customer_List').show();
            } else {
                $('#Customer_List').val(0);
                $('#Customer_List').hide();

            }
        }

    </script>
</head>
<body>
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
    <div class="app_container" style="margin-top:60px;margin-left:40px;">
         <div id="admin_container">
             <!--
            <div id="admin_nav">
                <div class="admin_nav_item" style="height:0px;padding-top:5px;">&nbsp;</div>
                <div class="admin_nav_item admin_nav_item_on" id="nav_users" onclick="toggleAdmin('nav_users')">Users</div>
                <div class="admin_nav_item" id="nav_clients" onclick="toggleAdmin('nav_clients')">Clients</div>
                <div class="admin_nav_item" style="height:800px;"></div>
            </div>-->
            <div class="admin_body" style="margin-top:150px;">
                <div id="users" class="admin_screen">
                    <div>
                    <asp:TextBox ID="First_Name" runat="server" placeholder="first name"></asp:TextBox>

                    <asp:TextBox ID="Last_Name" runat="server" placeholder="last name"></asp:TextBox>
                    <asp:TextBox ID="Email" runat="server" placeholder="email"></asp:TextBox>
                    <asp:TextBox ID="Password" runat="server" placeholder="password"></asp:TextBox>
                    <asp:DropDownList ID="User_Level_Id" runat="server" onchange="handleUserLevelSelection()"></asp:DropDownList>
                    <asp:DropDownList ID="Customer_List" runat="server" style="display:none"></asp:DropDownList>
                    <asp:Button id="Button1" runat="server" Text="Add User" OnClick="add_user_Click"/>
                    <br />
                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                              ControlToValidate="First_Name"
                              ErrorMessage="First name is a required field.<br>"
                              ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server"
                              ControlToValidate="Last_Name"
                              ErrorMessage="Last name is a required field.<br>"
                              ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server"
                              ControlToValidate="Email"
                              ErrorMessage="Email is a required field.<br>"
                              ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server"
                              ControlToValidate="Password"
                              ErrorMessage="Password is a required field.<br>"
                              ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    </div>
                    <div style="width:1000px;margin-top:-20px;" id="user_list" runat="server">


                    </div>
                </div>
                <div id="clients" class="admin_screen">
                    <div id="client_list">
                        <input type="button" value="Add Client" onclick="toggleAddClient();" />
                        <div style="margin-top:10px;">

                            <div class="lst_title">Client</div>
                            <div class="lst_title" style="width:240px;">Full Name</div>
                            <div class="lst_title">Turn Around</div>
                            <div class="lst_title">PO Req'd</div>
                            <div class="lst_title">Inspection Req'd</div>
                            <div class="lst_title">Subdivisions</div>
                            <div class="lst_title">Delivery</div>
                            <div class="lst_title">Customer Portal</div>
                            <div style="clear:both"></div>
                            <div style='width:100%;height:1px;line-height:1px;background:#696969'></div>
                        </div>
                        <div id="client_listings" style="width:2000px;"></div>
                    </div>
                    <div id="client_subdivisions" style="display:none;">
                        <input type="button" value="<< Back to Client List " onclick="closeSubdivisions();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" value="Add Subdivision" onclick="toggleAddSubdivision();" />
 

                        <div id="subdivision_listing">

                        </div>
                    </div>
                    <div id="client_email" style="display:none;">
                        <input type="button" value="<< Back to Client List " onclick="toggleDeliveryEmails();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <div style="margin-top:40px;">
                            <input type="text" id="new_email"/>&nbsp;&nbsp;&nbsp;
                            <input type="button" value="Add Delivery Email" onclick="addClientDeliveryMail();" />
                            <input type="hidden" id="del_email_client_id" />
                        </div>
                        
                        <div id="email_listing" style="margin-top:40px;">

                        </div>
                       
                    </div>
                    <div id="Customer_Portal" style="display:none;">
                        <input type="button" value="<< Back to Client List " onclick="toggleCustomerPortal();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <div style="margin-top:40px;">
                            <div style="margin-bottom:10px;">
                                <input type="text" id="Customer_Portal_Username" placeholder="username" style="width:190px;"/>
                            </div>
                            <div style="margin-bottom:10px;">
                                <input type="text" id="Customer_Portal_Password" placeholder="password" style="width:190px;"/>
                            </div>

                            <div class="button" onclick="updatePortalLogin();" style="width:170px;" >ADD / UPDATE</div>
                            <input type="hidden" id="cp_client_id" />
                        </div>
                       
                    </div>
                    <div id="add_subdivision" style="display:none;">
                        <input type="button" value=" << Back to Subdivisions " onclick="closeAddSubdivision()" /><br /><br />
                        ADD SUBDIVISION
                        <div class="frm_row">
                            <div class="frm_label">Name:</div>
                            <div class="frm_input"><input type="text" id="subdivision_name" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Number:</div>
                            <div class="frm_input"><input type="text" id="subdivision_number" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Division:</div>
                            <div class="frm_input"><select id="divisions"></select></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label"></div>
                            <div class="frm_input">
                                <input type="hidden" id="subdivision_id" />
                                <input type="button" value="submit" id="btn_subdivision"/>

                            </div>
                            <div style="clear:both;"></div>
                        </div>

                    </div>
                        
                    <div id="add_client" style="display:none;">
                        <input type="button" value=" << Back to Client List " onclick="closeAddClient()" /><br /><br />
                        ADD CLIENT
                        <div class="frm_row">
                            <div class="frm_label">Short Name:</div>
                            <div class="frm_input"><input type="text" id="client_short_name" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Full Name:</div>
                            <div class="frm_input"><input type="text" id="client_full_name" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Address:</div>
                            <div class="frm_input"><input type="text" id="client_address" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">&nbsp;</div>
                            <div class="frm_input"><input type="text" id="client_address2" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">City:</div>
                            <div class="frm_input"><input type="text" id="client_city" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">State:</div>
                            <div class="frm_input"><input type="text" id="client_state" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Zip:</div>
                            <div class="frm_input"><input type="text" id="client_zip" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Phone:</div>
                            <div class="frm_input"><input type="text" id="client_phone" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Fax:</div>
                            <div class="frm_input"><input type="text" id="client_fax" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Attn:</div>
                            <div class="frm_input"><input type="text" id="client_attn" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">MLAW Company Number:</div>
                            <div class="frm_input"><input type="text" id="client_company_number" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Turn Around Time:</div>
                            <div class="frm_input"><input type="text" id="client_turnaround_time" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">PO:</div>
                            <div class="frm_input">
                                &nbsp;&nbsp;&nbsp;
                                <input type="radio" name="client_po" value="0" checked /> No &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="radio" name="client_po" value="1" /> Yes
                            </div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Inspection:</div>
                            <div class="frm_input">
                                &nbsp;&nbsp;&nbsp;
                                <input type="radio" name="client_inspection" value="0" checked /> No &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="radio" name="client_inspection" value="1" /> Yes
                            </div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Low Range:</div>
                            <div class="frm_input"><input type="text" id="client_low_range" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">High Range:</div>
                            <div class="frm_input"><input type="text" id="client_high_range" style="width:400px;" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">Invoice Notes:</div>
                            <div class="frm_input"><textarea id="client_invoice_notes"></textarea></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div class="frm_row">
                            <div class="frm_label">&nbsp;</div>
                            <div class="frm_input"><input type="button" value="submit" onclick="addClient()" /></div>
                            <div style="clear:both;"></div>
                        </div>
                    </div>
                    
                </div>
            </div>
             <div style="clear:both"></div>
         </div>
    </div>
    </form>
    <script>
        $('#users').show();

        loadClients();
        loadDivisions();

        var Type = getParameterByName("Type");

        if(Type == "Clients")
        {
            toggleAdmin('nav_clients');
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    </script>
</body>
</html>
