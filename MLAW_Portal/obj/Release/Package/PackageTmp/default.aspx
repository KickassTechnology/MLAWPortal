<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MLAW_Order_System._default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="MLAW.css" media="screen" />
</head>

<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        
    <div class="header">
    
    </div>
    <div class="app_container">
        <div id="order_entry_container">
            <div class="app_section_title">ORDER ENTRY</div>
            <div id="dragandrophandler">Drag & Drop Files Here</div>
            <br><br>
            <div id="status1"></div>
            <div style="margin-top:20px;">
                <div class="frm_row">
                    <div class="frm_label">Company:</div>
                    <div class="frm_input"><input type="text" id="ord_company" style="width:400px;" /></div>
                    <div style="clear:both;"></div>
                </div>
                <div class="frm_row">
                    <div class="frm_label">Subdivision:</div>
                    <div class="frm_input"><input type="text" id="ord_subdivision" style="width:400px;" /></div>
                    <div style="clear:both;"></div>
                </div>
                <div class="frm_row">
                    <div class="frm_label">Address:</div>
                    <div class="frm_input"><input type="text" id="ord_address" style="width:400px;" /></div>
                    <div style="clear:both;"></div>
                </div>
                <div class="frm_row">
                    <div class="frm_label">City:</div>
                    <div class="frm_input"><input type="text" id="ord_city" style="width:400px;" /></div>
                    <div style="clear:both;"></div>
                </div>
                <div class="frm_row">
                    <div class="frm_label">Date Received:</div>
                    <div class="frm_input"><input type="text" id="ord_date_received" style="width:400px;" /></div>
                    <div style="clear:both;"></div>
                </div>
                <div class="frm_row">
                    <div class="frm_label">Order Type:</div>
                    <div class="frm_input"><input type="text" id="ord_type" style="width:400px;" /></div>
                    <div style="clear:both;"></div>
                </div>
            </div>
            <div>
                <!--
                <textarea id="test_results" style="width:400px;height:600px;margin-top:40px;"></textarea>
                -->

            </div>
        </div>

    </div>
    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="filedrag.js"></script>
</body>
</html>
