<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inspection_Administration.aspx.cs" Inherits="MLAW_Order_System.Inspection_Portal.Inspection_Administration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="../MLAW.css" media="screen" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="../MLAW.js"></script>
    <script src="../inspections.js"></script>
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
                <div style="float:left;margin-top:20px;cursor:pointer" onclick="window.location.href='Inspection_Administration.aspx?Type=Employees'"> 
                    EMPLOYEES
                </div>
                
                <div style="clear:both"></div>
            </div>
        </div>

    </div>
    <div class="app_container" style="margin-top:60px;margin-left:0px;">
         <div id="admin_container">
            
            <div class="admin_body" style="text-align:left;margin-left:40px;margin-top:80px;">
                <div id="inspections" style="display:none;">
                    <div id="manage_inspection_types">
                        <div class="app_section_title" style="margin-bottom:20px;">INSPECTION TYPES</div>
                        <div id="inspection_type_list"></div>
                    </div>
                    
                    <div id="edit_inspection_type" style="display:none;">
                        <div class="app_section_title" style="margin-bottom:20px;">EDIT INSPECTION TYPE</div>
                        <div class="frm_label" style="width:250px;">
                            Inspection Type:
                        </div>
                        <div class="frm_label" id="edit_inspection_type_name">

                        </div>
                        <div style="clear:both;"></div>
                        <div style="height:10px;line-height:10px;"></div>
                        <div class="frm_label" style="width:250px;">
                           Time Needed:
                        </div>
                        <div class="frm_input" id="Div1">
                            <input type="text" id="edit_inspection_completion_time" />
                        </div>
                        <div style="clear:both"></div>
                        <div style="height:10px;line-height:10px;"></div>
                        <input type="hidden" id="edit_inspection_type_id" />
                        <input type="button" value="update" onclick="submitEditInspectionType()" />

                    </div>
                </div>
                <div id="employees" style="display:none;">
                    <div class="app_section_title" style="margin-bottom:20px;">INSPECTORS&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:16px;cursor:pointer;" onclick="addInspector()"> + ADD</span></div>
                    <div id="inspector_list"></div>
                    <div id="inspector_form" style="display:none;">
                        <div class="lst_clickable" onclick="cancelEmpAdmin()"><< CANCEL</div>
                        <br /><br />
                        <div style="clear:both"></div>
                        <input type="hidden" id="inspector_id" />
                        <div>
                            <div class="frm_label">
                                Active:
                            </div>
                            <div class="frm_input">
                                <input type="checkbox" id="ins_edit_active" />
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div>
                            <div class="frm_label">
                                First Name:
                            </div>
                            <div class="frm_input">
                                <input type="text" id="ins_edit_first_name" />
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="margin-top:10px;">
                            <div class="frm_label">
                                Last Name:
                            </div>
                            <div class="frm_input">
                                <input type="text" id="ins_edit_last_name" />
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="margin-top:10px;">
                            <div class="frm_label">
                                Email:
                            </div>
                            <div class="frm_input">
                                <input type="text" id="ins_edit_email" />
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="margin-top:10px;">
                            <div class="frm_label">
                                Password:
                            </div>
                            <div class="frm_input">
                                <input type="password" id="ins_edit_password" />
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="margin-top:10px;">
                            <div class="frm_label">
                                Home Office:
                            </div>
                            <div class="frm_input">
                                <select id="ins_edit_office" >
                                    <option value="1">Austin</option>
                                    <option value="2">San Antonio</option>
                                    <option value="3">Dallas</option>
                                    <option value="4">Richmond</option>
                                    <option value="5">Bryan</option>
                                </select>
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="margin-top:10px;">
                            <div class="frm_label">
                                Inspection Types:
                            </div>
                            <div class="frm_input" id="ins_edit_type_list">
                                
                            </div>
                            <div style="clear:both"></div>
                        </div>
                        <div style="margin-top:20px">
                            <input type="button" onclick="submitInsEditForm()" value="submit"/>
                        </div>
                    </div>
                </div>

                 <div style="margin-top:20px;width:1400px;display:none;" id="view_form"></div>
            </div>
           
                    
        </div>
    </div>
       <script>
       $(document).ready(function () {
           loadInspectionTypeList();
           loadInspectors();

           var Type = getParameterByName("Type");

           if (Type == "Inspections") {
               $('#inspections').show();
               $('#employees').hide();
           } else {
               $('#inspections').hide();
               $('#employees').show();

           }

       });

       function getParameterByName(name) {
           name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
           var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
               results = regex.exec(location.search);
           return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
       }

       </script>

    </form>
</body>
</html>
