<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inspection_Default.aspx.cs" Inherits="MLAW_Order_System.Inspection_Portal.Inpection_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="../MLAW.css" media="screen" />

    <script>



    </script>
</head>

    <style>
        .schedule_event {
            width: 140px;
            height: 200px;
            margin-top:10px;
            margin-left:10px;
            background: #ffffff;
            border: solid 1px #696969;
            position:absolute;
        }

        .schedule_address {
            padding:10px;
            width:120px;
        }
        
        .schedule_type {
            width:120px;
            padding-left:10px;
            padding-right:10px;
        }

        .schedule_details {
            width:120px;
            padding-left:10px;
            padding-right:10px;
        }

        .schedule_move {
            height:18px;
            background:#696969;
            color:#ffffff;
            position:absolute;
            bottom:0px;
            width:140px;
            text-align:center;
            cursor:pointer;
        }

            .schedule_move:hover {
                background:#000000;
            }

    </style>

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

        <div class="header_button" style="width:130px;height:50px;" onclick="window.location.href='Inspection_Default.aspx'">
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
                    <img src="../Images/white_down_arrow.png"  style="width:16px;height:9px;"/>
                </div>
                <div style="clear:both"></div>
            </div>
            <div style="margin-top:10px;margin-left:10px;display:none;" id="ddl_admin">
                <div style="float:left;margin-top:10px;cursor:pointer;"onclick="window.location.href='Inspection_Administration.aspx?Type=Inspections'"> 
                    INSPECTIONS
                </div>
                
                <div style="clear:both"></div>
                <div style="float:left;margin-top:20px;cursor:pointer" onclick="window.location.href='Inspection_Administration.aspx'"> 
                    EMPLOYEES
                </div>
                
                <div style="clear:both"></div>
            </div>
        </div>

    </div>
    <div class="app_container" style="margin-top:60px;margin-left:0px;">
         <div id="admin_container">
            
            <div class="admin_body" style="text-align:left;width:100%;margin-top:60px;">
                     <div class="header_generic" style="min-height:30px;border-top:1px solid #696969;margin-bottom:0px;padding-bottom:0px;">
                        <div class="header_button header_button_on" style="height:30px;width:200px;padding-top:12px;" onclick="toggleInspectionView('insp_calendar')" id="btn_insp_calendar">SCHEDULE</div>
                        <div class="header_button" style="height:30px;width:160px;padding-top:12px;" onclick="toggleInspectionView('insp_map')" id="btn_insp_map">MAP</div>
                        <div class="header_button" style="height:30px;width:180px;padding-top:12px;" onclick="toggleInspectionView('insp_order')" id="btn_insp_order">INSPECTION REQUEST</div>
                        <div class="header_button" style="height:30px;width:160px;padding-top:12px;" onclick="toggleInspectionView('insp_order_grid')" id="btn_insp_order_grid">ORDER LIST</div>
                        <div class="header_button" style="height:30px;width:160px;padding-top:12px;" onclick="toggleInspectionView('insp_statistics')" id="btn_insp_statistics">STATISTICS</div>
                        <div style="float:right;display:none" id="filter_button"><img src="../Images/filter_white.png" style="float:left;height:28px;width:28px;margin-right:20px;margin-top:8px;cursor:pointer;" onclick="toggleFilters()"/></div>
                        <div style="clear:both"></div>
                    </div>
                    <div id="insp_calendar" style="text-align:left;margin-top:0px;position:relative;">

                    </div>
                    <div id="insp_map" style="text-align:left;margin-top:0px;width:100%;height:600px;display:none;">
                        
                    </div>
                    <div id="insp_order" style="text-align:left;margin-top:40px;width:100%;margin-left:40px;display:none;">
                        
                    </div>
                    <div id="insp_order_grid" style="display:none;">

                     <div id="filters" style="display:none;background:#2f2f2f;color:#ffffff;padding-top:10px;border-top:1px solid #5a5a5a">
                        <div style="width:100%;text-align:left;margin-left:20px;">
                            <div style="width:200px;">
                                DATE FILTERS
                            </div>
                            <div style="width:200px;float:left;">
                                <select id="flt_date_type" style="width:182px;">
                                    <option value="1">Received Date</option>
                                    <option value="2">Completed Date</option>
                                </select>
                            </div>
                            <div style="width:200px;float:left;">
                                <input type="text" placeholder="begin date" id="flt_begin_date" style="width:180px;" />
                            </div>
                            <div style="width:200px;float:left;">
                                <input type="text" placeholder="end date" id="flt_end_date" style="width:180px;"/>
                            </div>
                            <div class="header_button header_button_on" style="width:90px;float:left;"  onclick="loadInspectionOrders();">
                                UPDATE
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="width:100%;text-align:left;margin-top:20px;margin-left:20px;">
                            <div style="width:200px;">
                                MORE FILTERS
                            </div>

                            <div style="width:200px;float:left;"><input type="text" placeholder="client name" style="width:180px" id="flt_client" onkeyup="renderInspectionOrders()"/></div>
                            <div style="width:200px;float:left;"><input type="text" placeholder="inspection type" style="width:180px" id="flt_type"  onkeyup="renderInspectionOrders()"/></div>
                            <div style="width:200px;float:left;"><input type="text" placeholder="subdivision" style="width:180px" id="flt_subdivision" onkeyup="renderInspectionOrders()"/></div>
                            <div style="width:200px;float:left;"><input type="text" placeholder="status" style="width:180px" id="flt_status" onkeyup="renderInspectionOrders()"/></div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="width:100%;text-align:left;margin-top:20px;margin-left:20px;padding-bottom:16px;">
                            <div style="width:200px;">
                                ADDRESS
                            </div>
                            <div style="width:600px;"><input type="text" placeholder="address" style="width:580px;" id="flt_address" onkeyup="renderInspectionOrders()"/></div>
                        </div>
                     </div>
                     <div style='border-bottom:1px solid #696969;width:100%;background:#2f2f2f;' id="ins_order_list_titles">
                        <div class="header_button" style="width:140px;height:24px;padding-top:10px;text-align:left;" onclick="resort_ins_orders('MLAW_Number')" id="ins_order_list_title_MLAW_Number">MLAW NUMBER</div>
                        <div class='header_button' style="text-align:left;width:90px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Client_Short_Name')" id="ins_order_list_title_Client_Short_Name">CLIENT</div>
                        <div class="header_button" style="text-align:left;width:140px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Subdivision_Name')" id="ins_order_list_title_Subdivision_Name">SUBDIVISION</div>
                        <div class="header_button" style="text-align:left;width:240px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Address')" id="ins_order_list_title_Address">ADDRESS</div>
                        <div class="header_button" style="text-align:left;width:120px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Inspection_Type')" id="ins_order_list_title_Inspection_Type">TYPE</div>
                        <div class="header_button" style="text-align:left;width:110px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Order_Date')" id="ins_order_list_title_Order_Date">ORDER DATE</div>
                        <div class='header_button' style="text-align:left;width:100px;height:24px;padding-top:10px;" onclick="resort_ins_orders('Inspection_Status')" id="ins_order_list_title_Inspection_Status">STATUS</div>

                        <div style='clear:both'></div>
                    </div>
                    <div id="inspection_order_list"></div>
                    
                </div>
                    <div id="insp_statistics" style="display:none;padding-left:20px;">
                        <div style="margin-top:80px;">
                            <div style="float:left">Date Range:</div>
                            <div style="float:left;margin-left:100px;"><input type="text" id="startdate"/></div>
                            <div style="float:left;margin-left:10px;margin-right:10px;">to</div>
                            <div style="float:left;margin-left:100px;"><input type="text" id="enddate"/></div>
                            <div style="float:left;margin-left:10px;margin-right:10px;"><input type="button" value="Get Stats" onclick="loadCompleteInspectionOrders();" /></div>
                            <div style="clear:both;"></div>
                        </div>
                        <div id="insp_stats" style="margin-top:80px;margin-bottom:100px;">
                        </div>
                    </div>
                    <div id="insp_edit_order" style="display:none;"></div>
                </div>


                 <div style="margin-top:20px;width:1400px;display:none;" id="view_form"></div>
            </div>
           
                    
        </div>
    </div>
    <div id="schedule_manager">


    </div>

    </form>

    <!-- Google Maps API Key AIzaSyAPRtIzZv21m47oWS4D73v5yYgv4oh72dQ -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAPRtIzZv21m47oWS4D73v5yYgv4oh72dQ"></script>
    <script src="../filedrag.js"></script>
    <script src="../inspections.js"></script>
    <script src="../MLAW.js"></script>
    <asp:Label ID="JS" runat="server"></asp:Label>
    <script>

            



        $(document).ready(function () {

            var url = '../html/create_inspection_order.html';
           
            loadInspectorList();

            $('#insp_order').load(url, function () {

            });

            $('#view_form').load("../html/view_foundation_order.html", function () {
            });


            $('#insp_edit_order').load("../html/edit_inspection_order.html", function () {

            });

            setTimeout(function () {
                getInspectionTypes()
            }, 2000);

            var today = new Date();

            var scheduleStartDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();


            today = addDays(today, 1);

            


            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            
            var yyyy = today.getFullYear();

            var strStartDate = yyyy + '-' + mm + '-01';
            var strEndDate = yyyy + '-' + mm + '-' + dd;
            
            $('#startdate').val(strStartDate);
            $('#enddate').val(strEndDate);

            loadCompleteInspectionOrders();
           

            var dtNow = new Date();
            dtNow = addDays(dtNow, 1);
            var dtBegin = addDays(dtNow, -7);
            var sdf = new SimpleDateFormat("MM/dd/yyyy");

            var end_date = sdf.format(dtNow);
            var begin_date = sdf.format(dtBegin);

            $('#flt_begin_date').val(begin_date);
            $('#flt_end_date').val(end_date);

            getDailySchedule(scheduleStartDate);
            loadInspectionOrders();
        });

        window.onpopstate = function (event) {
            doWindowState(event.state);
        };


        function doWindowState(state) {

            if (state == null) {
                toggleInspectionView('insp_order_grid');
            }

        }


    </script>
</body>
</html>

