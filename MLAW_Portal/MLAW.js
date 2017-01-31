//Refactor of Angular

var arrFoundationStatuses = null;
var arrDivisions = null;
var arrClients = null;
var arrOrders = null;
var arrMyOrders = null;
var arrStatuses = null;
var arrSubdivisions = null;
var arrDownloadableFiles;

var arrClientList = null;
var arrSubdivisionList = null;

var filterClient = "";
var filterDivision = "";
var filterStatus = "";
var filterSubdivision = "";
var filterAddress = "";
var filterPlanNo = "";

var curOrderId = 0;
var editMode = 0;
var customerId = 0;
var displayOption = 1;
var OrderPlaceHolder = 0;

var currentSort = "";


function setCompanyName(thisName) {
    //set the company in the foundation order form
    //without a valid client id, we cannot load subdivisions
    $('#ord_company_holder').text(thisName);
    checkCompanyName();
}

function toggleAdmin(item)
{
    //shows/hides the admin screen
    $('.admin_screen').hide();
    var item_sub = item.replace("nav_", "");
    $('#' + item_sub).show();
}

function toggleAddClient() {
    //shows the client add client screen
    $('#add_client').show();
    $('#client_list').hide();
}

function closeAddClient() {
    //hides the client add client screen
    $('#add_client').hide();
    $('#client_list').show();
}


function toggleFoundationEntry() {
    //shows or hides the foundation entry screen
    //toggles are used not on for creation, but also editing
    //and the display screeen for foundation administrators
    orderFile == "";
    var myDate = new Date();

    if ($('#foundation_entry_container').is(":visible")) {
        $('#foundation_order_list').show();
        $('#foundation_entry_container').hide();

    } else {
        editMode = 0; 
        var stateObj = { id: "entry" };
        history.pushState(stateObj, "entry", "create_order.html");

        $('#foundation_order_list').hide();
        $('#foundation_entry_container').html('');
        $('#foundation_entry_container').load("html/create_foundation_order.html", function () {
            $('#foundation_entry_container').show();
            var obj = $("#dragandrophandler");
            var obj2 = $("#dragandrophandler2");

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
                handleFileUpload(files, obj, null);
            });


            obj2.on('dragenter', function (e) {
                e.stopPropagation();
                e.preventDefault();
                $(this).css('border', '2px solid #0B85A1');
            });
            obj2.on('dragover', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });
            obj2.on('drop', function (e) {

                $(this).css('border', '2px dotted #0B85A1');
                e.preventDefault();
                var files = e.originalEvent.dataTransfer.files;

                //We need to send dropped files to Server
                handleFileUpload2(files, obj2, null);
            });

            $('#ord_date_received').val((myDate.getMonth() + 1) + "-" + myDate.getDate() + "-" + myDate.getFullYear());


        });
        
    }
}

function addClient() {
    //adds a client - accepts a query string, not JSON
    var url = "../MiddleTier/Insert_Client.aspx?Short_Name=" + $('#client_short_name').val();
    url = url + "&Full_Name=" + $('#client_full_name').val();
    url = url + "&Billing_Address_1=" + encodeURIComponent($('#client_address').val());
    url = url + "&Billing_Address_2=" + encodeURIComponent($('#client_address2').val());
    console.log(encodeURI($('#client_address2').val()));
    url = url + "&Billing_City=" + $('#client_city').val();
    url = url + "&Billing_State=" + $('#client_state').val();
    url = url + "&Billing_Zip=" + $('#client_zip').val();
    url = url + "&Phone=" + $('#client_phone').val();
    url = url + "&Fax=" + $('#client_fax').val();
    url = url + "&Attn=" + $('#client_attn').val();
    url = url + "&Vendor_Number=" + $('#client_company_number').val();
    url = url + "&TurnAroundTime=" + $('#client_turnaround_time').val();
    url = url + "&PO_Flag=" + $('input:radio[name=client_po]:checked').val();
    url = url + "&Inspection_Flag=" + $('input:radio[name=client_inspection]:checked').val();
    url = url + "&Low_Range=" + $('#client_low_range').val();
    url = url + "&High_Range=" + $('#client_high_range').val();
    url = url + "&Invoice_Notes=" + encodeURIComponent($('#client_invoice_notes').val());
    console.log(url);
    url = url;
  
    var request = $.ajax({
        url: url,
        method: "POST",
        data: {},
        dataType: "text"
    });

    request.done(function (msg) {
        //alert(msg);
        loadClients(true);
    });

    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });

}

function loadClientListArray() {
    //gets our list of active clients
    $.ajax({
        url: '../MiddleTier/Get_Active_Clients.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            //alert(JSON.stringify(data));
            arrClientList = data;

            $.each(data, function (i) {
                var item = data[i];

                $('#new_sub_client').append($('<option/>', {
                    value: item.Client_Id,
                    text: item.Client_Short_Name
                }));
            });
        },
        error: function (e) {
            console.log(e.message);
        }
    });
}

function loadSubdivisionListArray() {
    //returns the list of active subdivisions
    $.ajax({
        url: '../MiddleTier/Get_Active_Subdivisions.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            //alert(JSON.stringify(data));
            arrSubdivisionList = data;

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}


function loadClients(showList) {
    //loads the list of all clients
    $('#client_listings').html('');

    $.ajax({
        url: '../MiddleTier/Get_Clients.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            //alert(JSON.stringify(data));
            arrClients = data;
            $.each(data, function (i) {
                var item = data[i];
                var strHTML = "<div style='margin-top:10px;'>";
                var inspection = "";
                var po = "";

                if (item.Inspection_Flag == null || item.Inspection_Flag == "" || item.Inspection_Flag == "null") {
                    inspection = "&nbsp;";
                } else {
                    inspection = item.Inspection_Flag;
                }

                if (item.PO_Flag == null || item.PO_Flag == "" || item.PO_Flag == "null") {
                    po = "&nbsp;";
                } else {
                    po = item.PO_Flag;
                }

                if (item.Is_Active == true) {
                    strHTML = strHTML + "<div class='lst_clickable'><input type='checkbox' id='active_" + item.Client_Id + "' checked onclick='toggleClientActive(" + item.Client_Id + ")'>&nbsp;&nbsp;&nbsp; " + item.Client_Short_Name + "</div>";
                } else {
                    strHTML = strHTML + "<div class='lst_clickable'><input type='checkbox' id='active_" + item.Client_Id + "' onclick='toggleClientActive(" + item.Client_Id + ")'>&nbsp;&nbsp;&nbsp; " + item.Client_Short_Name + "</div>";
                }

                strHTML = strHTML + "<div class='lst_item' style='width:240px;'>" + item.Client_Full_Name + "</div>";
                strHTML = strHTML + "<div class='lst_item'>" + item.Turn_Around_Time + " hours</div>";
                strHTML = strHTML + "<div class='lst_item'>" + po + " </div>";
                strHTML = strHTML + "<div class='lst_item'>" + inspection + " </div>";

                strHTML = strHTML + "<div class='lst_clickable' onclick='toggleSubdivision(\"" + item.Client_Id + "\")'>" + item.Subdivision_Count + "</div>";
                strHTML = strHTML + "<div class='lst_clickable' onclick='toggleDeliveryEmails(\"" + item.Client_Id + "\")'>Emails</div>";
                strHTML = strHTML + "<div class='lst_clickable' onclick='toggleCustomerPortal(\"" + item.Client_Id + "\")'>Customer Portal</div>";
                strHTML = strHTML + "<div style='clear:both'></div>";
                strHTML = strHTML + "<div style='height:5px'>&nbsp;</div>";
                strHTML = strHTML + "<div class='lst_item'>&nbsp;</div>";
                strHTML = strHTML + "<div style='float:left;width:380px;'>";
                strHTML = strHTML + "Attn: " + item.Attn + "<br>";
                strHTML = strHTML + item.Billing_Address_1 + " " + item.Billing_Address_2 + " " + item.Billing_City + ", " + item.Billing_State + ", " + item.Billing_Postal_Code;
                strHTML = strHTML + "</div>";
                strHTML = strHTML + "<div class='lst_item'>";
                strHTML = strHTML + "PHONE: " + item.Primary_Phone + "<br>";
                strHTML = strHTML + "FAX: " + item.Primary_Fax;
                strHTML = strHTML + "</div>";
  
                strHTML = strHTML + "<div style='clear:both'></div>";
               
                strHTML = strHTML + "</div>";
                strHTML = strHTML + "<div style='width:100%;height:1px;line-height:1px;background:#696969;margin-top:5px;'></div>";
                $('#client_listings').append(strHTML);
            });
            if (showList != null && showList != false) {
                $('#add_client').hide();
                $('#client_list').show();
            }
        },
        error: function (e) {
            console.log(e.message);
        }
    });
}

function toggleDeliveryEmails(client_id) {
    //hides/shows the list of delivery email addresses for a client
    $('#del_email_client_id').val(client_id);
    if ($('#client_list').is(':visible') == true) {
        renderDeliveryEmails(client_id);
        $('#client_list').hide();
        $('#client_email').show();
    } else {
        $('#client_list').show();
        $('#client_email').hide();
    }
}

function toggleCustomerPortal(client_id) {
    //hides/show the administrative info to manage client logins to their portal
    $('#cp_client_id').val(client_id);
    if ($('#client_list').is(':visible') == true) {
        renderDeliveryEmails(client_id);
        $('#client_list').hide();
        $('#Customer_Portal').show();
        loadClientPortalInfo(client_id);
    } else {
        $('#client_list').show();
        $('#Customer_Portal').hide();
    }
}

function loadClientPortalInfo(client_id) {
    //loads information on the screen for a client portal
    $('#Customer_Portal_Username').val('');
    $('#Customer_Portal_Password').val('');

    var URL = "../MiddleTier/Get_Client_Portal_Login.aspx?Client_Id=" + client_id;
    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            if (JSON.stringify(data) != "{}") {
                $('#Customer_Portal_Username').val(data[0].Login);
                $('#Customer_Portal_Password').val(data[0].Password);
            }
        }
    });
}

function updatePortalLogin() {
    //updates client portal login information
    var username = $('#Customer_Portal_Username').val();
    var password = $('#Customer_Portal_Password').val();
    var client_id = $('#cp_client_id').val();

    var URL = "../MiddleTier/Update_Client_Portal_Login.aspx?Client_Id=" + client_id + "&username=" + username + "&password=" + password;
    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            toggleCustomerPortal(0);
        }
    });
}

function addClientDeliveryMail() {
    //adds a client email for delivery
    var client_id = $('#del_email_client_id').val();
    var email = $('#new_email').val();

    var URL = "../MiddleTier/Add_Client_Delivery_Email.aspx?Client_Id=" + client_id + "&Email=" + email;
    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            renderDeliveryEmails(client_id);
        }
    });
}

function renderDeliveryEmails(client_id) {
    //displays the list of client delivery emails
    $('#email_listing').text('');
    var URL = "../MiddleTier/Get_Client_Delivery_Emails.aspx?Client_Id=" + client_id;

    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (i) {
                $('#email_listing').append("<div style='margin-top:4px'>");
                $('#email_listing').append("<div class='frm_label' style='width:250px'>" + data[i].Email_Address + "</div>");
                $('#email_listing').append("<div class='frm_label lst_clickable' style='margin-top:4px' onclick='remove_client_delivery_mail(" + data[i].Client_Delivery_Mail_Id + ", " + client_id + ")'>Remove</div>");
                $('#email_listing').append("<div style='clear:both'></div>");
                $('#email_listing').append("</div>");
            });
        }
    });
}

function remove_client_delivery_mail(client_mail_id, client_id) {
    //removes an email from the client delivery list
    var URL = "../MiddleTier/Remove_Client_Delivery_Mail.aspx?Client_Mail_Id=" + client_mail_id;

    console.log(URL);

    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            renderDeliveryEmails(client_id);
        }
    });
}



function loadCustomerOrders(client_id) {

    var URL = "../MiddleTier/Get_Client_Orders.aspx?Client_Id=" + client_id;

    console.log(URL);

    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrDownloadableFiles = data;
            renderDownloadableFiles();
        }
    });
}

function renderDownloadableFiles() {
    //renders the list of files associated with this order
    $.each(arrDownloadableFiles, function (i) {
        $('#recent_orders').append("<div class='lst_clickable' style='clear:both;width:200px;' onclick='downloadDeliverable(" + arrDownloadableFiles[i].Order_File_Id + ")'>" + arrDownloadableFiles[i].Order_File_Name + "</div>");
    });
}

function downloadDeliverable(Order_File_Id) {
    //starts the download for a file
    var url = "../display_file.aspx?Order_File_Id=" + Order_File_Id;
    window.open(url, '_blank');
}

function doCustomerQuickSearch() {
    //handles the search funciton in the customer portal

    var address = $('#quick_search').val();

    if (address.length > 2) {
        var URL = "Get_Customer_Quick_Search.aspx?Address=" + address;
        console.log(URL);

        $.ajax({
            url: URL,
            type: 'get',
            dataType: 'json',
            success: function (data) {
                $('#quick_search_results').html('');
                $.each(data, function (i) {
                    $('#quick_search_results').append("<div class='lst_clickable' style='clear:both;width:200px;' onclick='downloadDeliverable(" + data[i].Order_File_Id + ")'>" + data[i].Order_File_Name + "</div>");
                });

            }
        });
    }
}

function toggleClientActive(client_id) {
    //turns on/off a client in the admin interfaces
    var checked = $("#active_" + client_id).is(':checked');

    if (checked == true) {
        checked = 1;
    } else {
        checked = 0;
    }

    var URL = "../MiddleTier/Update_Client_Is_Active.aspx?Client_Id=" + client_id + "&Is_Active=" + checked;

    console.log(URL);

    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {

        }
    });

}

function loadFoundationStatuses() {
    //loads the list of statuses in the foundation editing screens
    $.ajax({
        url: '../MiddleTier/Get_Foundation_Statuses.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {

            arrStatuses = data;
            $.each(data, function (i) {
               
                $('#ord_edit_status').append($('<option/>', {
                    value: data[i].Order_Status_Id,
                    text: data[i].Order_Status_Desc
                }));
            });
        },
        error: function (e) {
            console.log(e.message);
        }
    });

}

function loadDivisions() {
   //loads the list of MLAW divisions
    $.ajax({
        url: '../MiddleTier/Get_Divisions.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {

            $.each(data, function (i) {
                var item = data[i];
                $('#divisions').append($('<option/>', {
                    value: item.Division_Id,
                    text: item.Division_Desc
                }));

                $('#new_sub_division').append($('<option/>', {
                    value: item.Division_Id,
                    text: item.Division_Desc
                }));
            });

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}

var curClientId = 0;
function toggleSubdivision(client_id) {
    //show/hides client subdivision list in the Admin areas
    curClientId = client_id;
    $('#client_list').hide();
    $('#client_subdivisions').show();
    loadClientSubdivisions(curClientId);
    $(document).scrollTop(0,0);
}

function toggleAddSubdivision() {
    //shows/hides add subdivisions screen
    $('#client_subdivisions').hide();
    $('#add_subdivision').show();
    $(document).scrollTop(0, 0);

    $('#btn_subdivision').off();
    $('#btn_subdivision').on("click", function () {
        addSubdivision();
    });    
}

function closeSubdivisions() {
    //close out of the subdivision administration
    $('#client_list').show();
    $('#client_subdivisions').hide();
}

function closeAddSubdivision() {
    //closes the add subdivision screen
    $('#client_subdivisions').show();
    $('#add_subdivision').hide();
}

function addSubdivision() {
    //adds a subdivisoion
    var url = "../MiddleTier/Insert_Subdivision.aspx?Client_Id=" + curClientId + "&Division_Id=" + $('#divisions').val() + "&Division_Name=" + $('#subdivision_name').val() + "&Subdivision_Number=" + $('#subdivision_number').val();
    console.log(url);

    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            $('#add_subdivision').hide();
            $('#client_subdivisions').show();
            loadClientSubdivisions(curClientId);
            //$('#divisions').val('1');
            $('#subdivision_name').val('');
            loadClients(false);
        },
        error: function (e) {
            console.log(e.message);
        }
    });
}


function loadClientSubdivisions(client_id) {

    //gets the list of all the subdivisions for a client
    curClientId = client_id;
    var url = "../MiddleTier/GetClientSubdivisions.aspx?Client_Id=" + client_id;

    $('#subdivision_listing').html('');

    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrSubdivisions = data;
            var curDivision = "";
            $.each(data, function (i) {
                var item = data[i];
                
                if (curDivision != item.Division_Desc) {
                    $('#subdivision_listing').append("<div class='lst_title' style='margin-top:20px;'>" + item.Division_Desc + "</div><div style='clear:both'></div>");
                    curDivision = item.Division_Desc;
                }
                
                $('#subdivision_listing').append("<div style='float:left;width:140px'>" +pad(item.Subdivision_Number, 3) + "</div><div style='float:left;' class='lst_clickable' onclick='editSubdivision(" + item.Subdivision_Id + ")'>" + item.Subdivision_Name + "</div><div style='clear:both'></div>");
            });

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}

function editSubdivision(Subdivision_Id) {
    //shows the subdivision editing screen
    toggleAddSubdivision();
    $('#subdivision_id').val(Subdivision_Id);
    for (var i = 0; i < arrSubdivisions.length; i++) {
        var item = arrSubdivisions[i];
        if (item.Subdivision_Id == Subdivision_Id) {
            $('#subdivision_name').val(item.Subdivision_Name);
            $('#divisions').val(item.Division_Id);
            $('#subdivision_number').val(item.Subdivision_Number);
        }
    }
    $('#btn_subdivision').off();
    $('#btn_subdivision').on("click", function () {
        updateSubdivision();
    });

}

function updateSubdivision() {
    //updates the information for a subdivision
    var strSubdivisionName = $('#subdivision_name').val();
    var iDivisionId = $('#divisions').val();
    var strSubdivisionNumber = $('#subdivision_number').val();
    var iSubdivisionId = $('#subdivision_id').val();

    var url = "../MiddleTier/Update_Subdivision.aspx?Subdivision_Id=" + iSubdivisionId + "&Subdivision_Number=" + strSubdivisionNumber + "&Subdivision_Name=" + strSubdivisionName + "&Division_Id=" + iDivisionId;
    console.log(url);
    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        success: function (data)
        {
            $('#subdivision_name').val('');
            $('#divisions').val('');
            $('#subdivision_number').val('');
            $('#subdivision_id').val('');

            loadClientSubdivisions(curClientId);
            closeAddSubdivision();
        }
    });
}

function pad(num, size) {
    // pads numbers with trailing zeros 
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

function loadClientTypeAhead() {
    //loads the type-ahead list on the order screen when choosing a customer
    $.ajax({
        url: '../MiddleTier/Get_Active_Clients.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            $('#type_ahead').html('');
            $('#type_ahead').append('<ul id="lst_typeahead"></ul>');

            $.each(data, function (i) {
                var item = data[i];
                $('#lst_typeahead').append($('<li id="Client_' + item.Client_Id + '" onmouseover="highlightTypeAhead(\'Client_' + item.Client_Id + '\')" onclick="setTypeAhead(\'Client_' + item.Client_Id + '\')">' + item.Client_Short_Name + '</li>'));
                //$('#lst_typeahead').append($('<li id="Client_' + item.Client_Id + '">' + item.Client_Short_Name + '</li>'));
            });
            doClientTypeAhead();
        },
        error: function (e) {
            console.log(e.message);
        }
    });  

}

function doClientTypeAhead(e) {
    //handles the typing and the layout of the type-ahead
    curTypeAhead = 'client';
    e = e || window.event;

    if (e.keyCode == 40 && e.keyCode == 38) {
        e.preventDefault();
        return false;
    }
    var val, top, left;

    if (editMode == 0)
    {
        val = $('#ord_company_holder').text();
        top = $('#ord_company_holder').position().top + 20;
        left = $('#ord_company_holder').position().left;
    }else{
        val = $('#ord_edit_company_holder').text();
        top = $('#ord_edit_company_holder').position().top + 20;
        left = $('#ord_edit_company_holder').position().left;

    }

        $('#type_ahead').css('left', left);
        $('#type_ahead').css('top', top);

        $('#type_ahead').show();
        $('#type_ahead li').hide();

        $("#type_ahead li:contains(" + val + ")").show();

        setTimeout(function () {
            if($('#type_ahead li:visible.li_selected').index() == -1)
            {
                curTypeAheadPos = 0;
            }
        }, 100);
   
}




function checkCompanyName() {
    //this checks a company name to make sure it's valid - all kinds of junk comes in via the order forms customers send
    setTimeout(function () {

        $('#type_ahead').hide();
        var url = "../MiddleTier/Get_Client_By_Name.aspx?Name=" + $('#ord_company_holder').text();
      
        $.ajax({
            url: url,
            type: 'get',
            dataType: 'json',
            success: function (data) {

                if (data.length == 0) {
                    alert("we could not find a company with this name in the database");
                } else {

                    var item = data[0];
                    $('#ord_company').val(item.Client_Id);
                    $('#ord_subdivision_holder').text('');
                    $('#ord_subdivision_id').val('');

                    loadClientSubdivisionTypeAhead();
                    calcFoundationPricing();
                }

            },
            error: function (e) {
                console.log(e.message);
            }
        });
    }, 1000);
}




function loadClientSubdivisionTypeAhead()
{
    //handles retrieving and displaying the list of subdivions for the subdivision type ahead
    var client_id;
    if(editMode == 0)
    {
        client_id = $('#ord_company').val();
    }else{
        client_id = $('#ord_edit_company').val();
    }

    $('#subdivision_listing').html('');
    $('#type_ahead').html('');
    $('#type_ahead').append('<ul id="lst_typeahead"></ul>');

    for (var i = 0; i < arrSubdivisionList.length; i++) {
        var item = arrSubdivisionList[i];
        if (client_id == item.Client_Id) {
            $('#lst_typeahead').append($('<li id="Subdivision_' + item.Subdivision_Id + '" onmouseover="highlightTypeAhead(\'Subdivision_' + item.Subdivision_Id + '\')" onclick="setTypeAhead(\'Subdivision_' + item.Subdivision_Id + '\')" >' + item.Subdivision_Name + '</li>'));

        }
    }

    /*
    var url = "GetClientSubdivisions.aspx?Client_Id=" + client_id;

    $('#subdivision_listing').html('');

    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrSubdivisions = data;
            $('#type_ahead').html('');
            $('#type_ahead').append('<ul id="lst_typeahead"></ul>');

            $.each(data, function (i) {
                var item = data[i];
                $('#lst_typeahead').append($('<li id="Subdivision_' + item.Subdivision_Id + '" onmouseover="highlightTypeAhead(\'Subdivision_' + item.Subdivision_Id + '\')" onclick="setTypeAhead(\'Subdivision_' + item.Subdivision_Id + '\')" >' + item.Subdivision_Name + '</li>'));
                //$('#lst_typeahead').append($('<li id="Subdivision_' + item.Subdivision_Id + '" onmouseover="highlightTypeAhead("Subdivision_' + item.Subdivision_Id + '")" onclick="alert(\'foo\');">' + item.Subdivision_Name + '</li>'));
            });
            subIsSet = 1;
            console.log($('#type_ahead').html());
        },
        error: function (e) {
            console.log(e.message);
        }
    });
    */
}


function closeTypeAhead() {
    //close the type ahead after the selections are done
    setTimeout(function () {
        $('#type_ahead').hide();
        curTypeAheadPos = 0;
    }, 1000);

}

function highlightTypeAhead(el) {
    //highlights an element if it's matched in a typeahead
    $('#type_ahead li').removeClass('li_selected');

    $("#" + el).addClass('li_selected');
}

function setTypeAhead(el) {
    //set the element in a type-ahead list
    curTypeAheadPos = 0;
    if (curTypeAhead == 'client') {
        if (editMode == 0) {
            $('#ord_company_holder').text($('#' + el).text());
        } else {
            $('#ord_edit_company_holder').text($('#' + el).text());
        }
    } else {
        if (editMode == 0) {
            $('#ord_subdivision_holder').text($('#' + el).text());
            $('#ord_subdivision_id').val($('#' + el).attr('id'));
        } else {
            $('#ord_edit_subdivision_holder').text($('#' + el).text());
            $('#ord_edit_subdivision_id').val($('#' + el).attr('id'));
        }
    }
    $('#type_ahead').hide();
}

var curTypeAheadPos = 0;
var curTypeAhead = 'client';

function tabTypeAhead(e) {
    //handles special keys like tab and enter for the type-ahead
    e = e || window.event;

    var typeaheadLen = $("#type_ahead li:visible").length -1;

    if (e.keyCode == '38') {
        if (curTypeAheadPos != 0) {
            curTypeAheadPos = curTypeAheadPos - 1;
        }
    }

    if (e.keyCode == '40') {
        if (curTypeAheadPos != typeaheadLen) {

            curTypeAheadPos = curTypeAheadPos + 1;
        }
    }

    if (e.which == "13") {
        setTimeout(function () {
            if (curTypeAhead == 'client') {
                $('#ord_company_holder').text($('#type_ahead li.li_selected').text());
            } else {
             
                $('#ord_subdivision_holder').text($('#type_ahead li.li_selected').text());
                $('#ord_subdivision_id').val($('#type_ahead li.li_selected').attr('id'));
            } 
            $('#type_ahead').hide();
        }, 100);
    }

    $('#type_ahead li').removeClass('li_selected');

    $("#type_ahead li:visible:eq(" + curTypeAheadPos + ")").addClass('li_selected');

}


$.expr[":"].contains = $.expr.createPseudo(function (arg) {
    return function (elem) {
        return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
    };
});


function doSubdivisionTypeAhead(e) {
    //handles special keys like tab and enter for the type-ahead
    curTypeAhead = 'subdivision';
    e = e || window.event;

    if (e.keyCode == 40 && e.keyCode == 38) {
        e.preventDefault();
        return false;
    }

    if (e.keyCode == 9) {
        closeTypeAhead();

    }

    var val, top, left
    if (editMode == 0) {
        val = $('#ord_subdivision_holder').text();
        top = $('#ord_subdivision_holder').position().top + 20;
        left = $('#ord_subdivision_holder').position().left;
    } else {
        val = $('#ord_edit_subdivision_holder').text();
        top = $('#ord_edit_subdivision_holder').position().top + 20;
        left = $('#ord_edit_subdivision_holder').position().left;
    }

    $('#type_ahead').css('left', left);
    $('#type_ahead').css('top', top);

    $('#type_ahead').show();
    $('#type_ahead li').hide();

    $("#type_ahead li:contains(" + val + ")").show();

    setTimeout(function () {
        if ($('#type_ahead li:visible.li_selected').index() == -1) {
            curTypeAheadPos = 0;
        }
    }, 100);

}


function validateOrderForm() {
    //makes sure the user has filled out all the necessary information 
    //to create an order
    var iFoundationOrderType = $('input:radio[name=ord_order_type]:checked').val();
    

    var bSubmit = true;
    if ($('#ord_company_holder').text().trim() == "") {
        alert("Please enter a company name");
        bSubmit = false;
    }

    if ($('#ord_subdivision_holder').text().trim() == "" && iFoundationOrderType == 0) {
        alert("Please enter a subdivision");
        bSubmit = false;
    }

    if ($('#ord_date_received').val().trim() == "") {
        alert("Please enter the date received");
        bSubmit = false;
    }

    if ($('#ord_address').val().trim() == "") {
        alert("Please enter an address");
        bSubmit = false;
    }

    if ($('#ord_plan_name').val().trim() == "" && $('#ord_plan_number').val().trim() == "") {
        if (iFoundationOrderType == 0) {
            alert("Please enter a plan name or a plan number");
            bSubmit = false;
        }
    }
    /*
    if ($('#ord_elevation').val().trim() == "") {
        alert("Please enter an elevation");
        bSubmit = false;
    }*/

    if ($('#ord_brg_cap').val() != "" && isNaN($('#ord_brg_cap').val()) == true) {
        alert("Brg Cap must be numeric");
        bSubmit = false;
    }

    if ($('#ord_em_ctr').val() != "" && isNaN($('#ord_em_ctr').val()) == true) {
        alert("Em Ctr must be numeric");
        bSubmit = false;
    }

    if ($('#ord_em_edg').val() != "" && isNaN($('#ord_em_edg').val()) == true) {
        alert("Em Edg must be numeric");
        bSubmit = false;
    }

    if ($('#ord_ym_ctr').val() != "" && isNaN($('#ord_ym_ctr').val()) == true) {
        alert("Ym Ctr must be numeric");
        bSubmit = false;
    }

    if ($('#ord_ym_edg').val() != "" && isNaN($('#ord_ym_edg').val()) == true) {
        alert("Ym Edg must be numeric");
        bSubmit = false;
    }

    if ($('#ord_status').val() == "4") {
        if ($('#ord_ym_edg').val() == "" || $('#ord_ym_ctr').val() == "" || $('#ord_em_edg').val() == "" || $('#ord_em_ctr').val() == "") {
            alert("The status is marked active, but the soils data is incomplete");
            bSubmit = false;
        }
    }

    if ($('#ord_ym_edg').val() != "" && $('#ord_ym_ctr').val() != "" && $('#ord_em_edg').val() != "" && $('#ord_em_ctr').val() != "" )
        {
            $('#ord_status').val("4");
        }else{
            $('#ord_status').val("3");
        }

    if ($('input:radio[name=ord_rev]:checked').val() == 1) {
        if ($('input:radio[name=ord_rev_match]:checked').val() == "undefined") {
            alert("You have indicated that this is a revision, but you have not selected a previous order");
            bSubmit = false;
        }
    }

    if ($('#ord_slab_sq_ft').val() == '' && $('#ord_price').val() == '' && iFoundationOrderType == 0) {
        alert("This client requires Square Footage to generate its invoice amount. Please enter the slab square footage");
        bSubmit = false;
    }

    if (bSubmit == true)
    {
        $('#grey_out').show();
        $('#spinner').show();
        $(document).scrollTop(0, 0);

        var strMLAWNumber = $("#mlaw_number").text();
        var strAddress = $("#ord_address").val();
        var strCity = $('#ord_city').val();
        
        var strState = "";
        var strZip = "";
        var strSubdivisionId = $('#ord_subdivision_id').val();
        strSubdivisionId = strSubdivisionId.replace("Subdivision_", "");
        var strStartDate = $('#ord_date_received').val();
        var strOrderStatusId = $('#ord_status').val();
        var strOrderWarrantyId = "0";
        var strOrderTypeId = "1";
        var strPlanNumber = $('#ord_plan_number').val();
        var strPlanName = $('#ord_plan_name').val();
        var strLot = $('#ord_lot').val();
        var strBlock = $('#ord_block').val();
        var strSection = $('#ord_section').val();
        var strDivisionId = "";
        var strComments = $('#ord_comments').val();
        var strPhase = $('#ord_phase').val();
        var strElevation = $('#ord_elevation').val();
        var strContact = $('#ord_contact').val();
        var strPhone = $('#ord_phone').val();
        var strFoundationType = $('input:radio[name=ord_foundation_type]:checked').val();

        var strCounty = $('#ord_county').val();
        var isRevision = $('input:radio[name=ord_rev]:checked').val();
        
        var strGarageType = $('input:radio[name=ord_garage]:checked').val();
        var strPatio = $('input:radio[name=ord_patio]:checked').val();
        var strFireplace = $('input:radio[name=ord_fireplace]:checked').val();
        var strPI = $('#ord_pi').val();
        var strEMCTR = $('#ord_em_ctr').val();
        var strEMEDG = $('#ord_em_edg').val();
        var strYMCTR = $('#ord_ym_ctr').val();
        var strYMEDG = $('#ord_ym_edg').val();
        var strSlabSqFt = $('#ord_slab_sq_ft').val();
        var strGoeDate = $('#ord_viz_geo_date').val();
        var strDataSource = $('#ord_soils_data_source').val();
        var strFill = $('#ord_fill_approved').val();
        var strBrgCap = $('#ord_brg_cap').val();
        var strSlope = $('#ord_slope').val();

        var strGarageOptions = $('#ord_garage_options').val();
        var strPatioOptions = $('#ord_patio_options').val();
        var iMasonrySides = $('input:radio[name=ord_masonry]:checked').val();

        var strFillDepth = $('#ord_fill_depth').val();
        var strSoilsComments = $('#ord_soils_comments').val();
        var strCustomerJobNumber = $('#ord_customer_job_num').val();

       
        var amount = $('#ord_price').val();
        if (iFoundationOrderType == 1)
        {
            amount = $('#ord_800_price').val();
        }
        var discount = $('#ord_discount').val();

        strDivisionId = $('#ord_division_id').val();
        var strClientId = $('#ord_company').val();

   
            
        if (discount == "") {
            discount = 0;
        }

        var parentId = 0;
        if ($('input:radio[name=ord_rev]:checked').val() == 1)
        {
            parentId = $('input:radio[name=ord_rev_match]:checked').val();
        }

        if (strFill == "on") {
            strFill = "1";
        } else {
            strFill = "0";
        }

        var strSlope = $('#ord_slope').val();

        var url = "../MiddleTier/Insert_Order.aspx";
        var postData = "MLAW_Number=" + strMLAWNumber + "&Address=" + encodeURIComponent(strAddress) + "&City=" + strCity + "&State=" + strState + "&Zip=" + strZip + "&Subdivision_Id=" + strSubdivisionId + "&Start_Date=" + strStartDate + "&Order_Status_Id=" + strOrderStatusId + "&Order_Warranty_Id=" + strOrderWarrantyId + "&Order_Type_Id=" + strOrderTypeId + "&Plan_Number=" + strPlanNumber + "&Plan_Name=" + strPlanName + "&Lot=" + strLot + "&Block=" + strBlock + "&Section=" + strSection + "&Division_Id=" + strDivisionId + "&Comments=" + encodeURIComponent(strComments) + "&Phase=" + strPhase + "&Contact=" + strContact + "&Phone=" + strPhone + "&Foundation_Type=" + strFoundationType + "&Elevation=" + strElevation + "&County=" + strCounty + "&IsRevision=" + isRevision + "&Garage=" + strGarageType + "&Patio=" + strPatio + "&Fireplace=" + strFireplace + "&PI=" + strPI + "&Slab_Square_Feet=" + strSlabSqFt + "&Em_ctr=" + strEMCTR + "&Em_edg=" + strEMEDG + "&Ym_ctr=" + strYMCTR + "&Ym_edg=" + strYMEDG + "&Visual_Geotec_Date=" + strGoeDate + "&Soils_Data_Source=" + strDataSource + "&Fill_Applied=" + strFill + "&Slope=" + strSlope + "&Brg_cap=" + strBrgCap + "&Garage_Options=" + strGarageOptions + "&Patio_Options=" + strPatioOptions + "&Masonry_Sides=" + iMasonrySides + "&Parent_Id=" + parentId + "&Order_File=" + encodeURIComponent(orderFile);
        postData = postData + "&Fill_Depth=" + strFillDepth + "&Soils_Comments=" + encodeURIComponent(strSoilsComments) + "&Customer_Job_Number=" + strCustomerJobNumber;
        postData = postData + "&Amount=" + amount + "&Discount=" + discount + "&FoundationOrderType=" + iFoundationOrderType + "&Client_Id=" + strClientId;
        console.log(postData);

        $.ajax({
            url: url,
            type: 'post',
            data: postData,
            dataType: 'json',
            success: function (data) {

                if (JSON.stringify(data) == '[{"Column1":null}]' || JSON.stringify(data) == '[{"Column1":-1}]')
                {
                    alert("An order with this address already exists. Did you mean to enter a revision?");
                    $('#grey_out').hide();
                    $('#spinner').hide();

                } else {
                    $('#foundation_entry_container').hide();
                    $('#foundation_order_list').show();
                    $('#grey_out').hide();
                    $('#spinner').hide();
                    orderFile = "";
                    loadOpenOrders();
                }
            
            },
            error: function (e) {
                console.log(e.message);
            }
        });



    }
}

function validateOrderFormUpdate() {
    //makes sure we have all the information we need for updating an order
    var bSubmit = true;
    var is_800 = $('#is_800').val();

    if (is_800 != 1) {
        if ($('#ord_edit_company_holder').text().trim() == "") {
            alert("Please enter a company name");
            bSubmit = false;
        }

        if ($('#ord_edit_subdivision_holder').text().trim() == "") {
            alert("Please enter a subdivision");
            bSubmit = false;
        }

        if ($('#ord_edit_date_received').val().trim() == "") {
            alert("Please enter the date received");
            bSubmit = false;
        }

        if ($('#ord_edit_address').val().trim() == "") {
            alert("Please enter an address");
            bSubmit = false;
        }

        if ($('#ord_edit_plan_name').val().trim() == "" && $('#ord_edit_plan_number').val().trim() == "") {
            alert("Please enter a plan name or a plan number");
            bSubmit = false;
        }

        if ($('#ord_edit_brg_cap').val() != "" && isNaN($('#ord_edit_brg_cap').val()) == true) {
            alert("Brg Cap must be numeric");
            bSubmit = false;
        }

        if ($('#ord_edit_em_ctr').val() != "" && isNaN($('#ord_edit_em_ctr').val()) == true) {
            alert("Em Ctr must be numeric");
            bSubmit = false;
        }

        if ($('#ord_edit_em_edg').val() != "" && isNaN($('#ord_edit_em_edg').val()) == true) {
            alert("Em Edg must be numeric");
            bSubmit = false;
        }

        if ($('#ord_edit_ym_ctr').val() != "" && isNaN($('#ord_edit_ym_ctr').val()) == true) {
            alert("Ym Ctr must be numeric");
            bSubmit = false;
        }

        if ($('#ord_edit_ym_edg').val() != "" && isNaN($('#ord_edit_ym_edg').val()) == true) {
            alert("Ym Edg must be numeric");
            bSubmit = false;
        }
    }

    if (bSubmit == true) {
   
        var strAddress = $("#ord_edit_address").val();
        var strCity = $('#ord_edit_city').val();
        var strState = "";
        var strZip = "";
        var strSubdivisionId = $('#ord_edit_subdivision_id').val();
        strSubdivisionId = strSubdivisionId.replace("Subdivision_", "");
        var strStartDate = $('#ord_edit_date_received').val();
        var strDueDate = $('#ord_edit_date_due').val();
        var strOrderStatusId = $('#ord_edit_status').val();
        var strOrderWarrantyId = "0";
        var strOrderTypeId = "1";
        var strPlanNumber = $('#ord_edit_plan_number').val();
        var strPlanName = $('#ord_edit_plan_name').val();
        var strLot = $('#ord_edit_lot').val();
        var strBlock = $('#ord_edit_block').val();
        var strSection = $('#ord_edit_section').val();
        var strComments = $('#ord_edit_comments').val();
        var strPhase = $('#ord_edit_phase').val();
        var strElevation = $('#ord_edit_elevation').val();
        var strContact = $('#ord_edit_contact').val();
        var strPhone = $('#ord_edit_phone').val();
        
        var strPI = $('#ord_edit_pi').val();
        var strEMCTR = $('#ord_edit_em_ctr').val();
        var strEMEDG = $('#ord_edit_em_edg').val();
        var strYMCTR = $('#ord_edit_ym_ctr').val();
        var strYMEDG = $('#ord_edit_ym_edg').val();
        var strSlabSqFt = $('#ord_edit_slab_sq_ft').val();
        var strGoeDate = $('#ord_edit_viz_geo_date').val();
        var strDataSource = $('#ord_edit_soils_data_source').val();
        var strFill = $('#ord_edit_fill_applied').val();
        var strBrgCap = $('#ord_edit_brg_cap').val();
        var strGarageOptions = $('#ord_edit_garage_options').val();
        var strPatioOptions = $('#ord_edit_patio_options').val();
        var strMasonrySides = $('input:radio[name=ord_edit_masonry]:checked').val();
        var strFoundationType = $('input:radio[name=ord_edit_foundation_type]:checked').val();
        

        if (strFill == "on") {
            strFill = "1";
        } else {
            strFill = "0";
        }
        var strSlope = $('#ord_edit_slope').val();

        var url = "../MiddleTier/Update_Order.aspx?Order_Id=" + curOrderId + "&Address=" + encodeURIComponent(strAddress) + "&City=" + strCity + "&State=" + strState + "&Zip=" + strZip + "&Subdivision_Id=" + strSubdivisionId + "&Due_Date=" + strDueDate + "&Start_Date=" + strStartDate + "&Order_Status_Id=" + strOrderStatusId + "&Order_Warranty_Id=0&Order_Type_Id=0&Plan_Number=" + strPlanNumber + "&Plan_Name=" + strPlanName + "&Lot=" + strLot + "&Block=" + strBlock + "&Section=" + strSection + "&Comments=" + encodeURIComponent(strComments) + "&Phase=&Contact=&Phone=&Foundation_Type=" + strFoundationType + "&Elevation=" + strElevation + "&PI=" + strPI + "&Slab_Square_Feet=" + strSlabSqFt + "&Em_ctr=" + strEMCTR + "&Em_edg=" + strEMEDG + "&Ym_ctr=" + strYMCTR + "&Ym_edg=" + strYMEDG + "&Visual_Geotec_Date=" + strGoeDate + "&Soils_Data_Source=" + strDataSource + "&Fill_Applied=" + strFill + "&Slope=" + strSlope + "&Brg_cap=" + strBrgCap + "&Garage_Options=" + strGarageOptions + "&Patio_Options=" + strPatioOptions + "&Masonry_Side=" + strMasonrySides;

        $.ajax({
            url: url,
            type: 'get',
            dataType: 'text',
            success: function (data) {
                
                $('#foundation_order_grid').html('');
                if (is_800 == 1)
                {
                    load800Orders();
                } else {
                    loadOpenOrders();
                }
                
                parent.history.back();

            },
            error: function (e) {
                console.log(e.message);
            }
        });



    }
}


function updateRevision() {
    //updates revision information
    var strComments = $('#revision_notes').val();
    var iOrderStatusId = $('#ord_edit_status').val();
    var iDesignerId = $('#ord_edit_assign').val();

    if (iDesignerId != 0 && iOrderStatusId <5) {
        iOrderStatusId = 5;
    }
    if (iDesignerId == null) {
        iDesignerId = 0;
    }

    var url = "../MiddleTier/Update_Revision.aspx?Order_Id=" + curOrderId + "&Comments=" + encodeURIComponent(strComments) + "&Order_Status_Id=" + iOrderStatusId + "&Designer_Id=" + iDesignerId;

    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        success: function (data) {

            $('#foundation_order_grid').html('');
            loadOpenOrders();
            parent.history.back();

        },
        error: function (e) {
            console.log(e.message);
        }
    });

}


function loadOpenOrders() {
    //loads open orders
    $('#foundation_order_grid').html('');

    $.ajax({
        url: '../MiddleTier/Get_Open_Orders.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            
            arrOrders = data;
            loadOpenRevisions();
            for (var i = 0; i < arrOrders.length; i++) {

                var item = arrOrders[i];

                if (arrClients == null) {
                    arrClients = new Array();
                    var jsonObj = { "Client_Short_Name": item.Client_Short_Name, "Client_Id": item.Client_Id };
                    arrClients.push(jsonObj);
                } else {
                    var bAdd = true;
                    for (var j = 0; j < arrClients.length; j++) {
                        if (arrOrders[i].Client_Id == arrClients[j].Client_Id) {
                            bAdd = false;
                        }
                    }

                    if (bAdd == true) {
                        var jsonObj = { "Client_Short_Name": item.Client_Short_Name, "Client_Id": item.Client_Id };
                        arrClients.push(jsonObj);
                    }
                }
            }

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}



function loadOrdersByStatusId(status_id) {
   //loads all orders that have a certain status
    $('#foundation_order_grid').html('');
    var URL = '../MiddleTier/Get_Orders_By_Status.aspx?Statuses=' + status_id;
    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
           
            arrOrders = data;
            renderOrders();

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}

function loadOpenRevisions() {
    //loads the list of open revision orders
    $.ajax({
        url: '../MiddleTier/Get_Open_Revisions.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (i) {
                arrOrders.push(data[i]);
            });

            if (currentSort == "") {
                currentSort = "Due_Date_String";
            }

            resort_orders(currentSort);
            renderOrders();
        },
        error: function (e) {
            console.log(e.message);
        }
    });

}

function renderOrders() {

    //renders our order lists throughout the application

    $('#foundation_order_grid').html('');
    $('#foundation_order_grid').show();
    var url = window.location.href;
    var isAdmin = true;
    var isDesigner = false;
    var isCustomer = false;
    var isAccounting = false;

    if (url.indexOf('DesignerPortal') > -1){
        isAdmin = false;
        isDesigner = true;
    } else if (url.indexOf('CustomerPortal') > -1) {
        isAdmin = false;
        isCustomer = true;
    } else if (url.indexOf('AccountingPortal') > -1) {
        isAdmin = false;
        isAccounting = true;
    } else if (url.indexOf('FoundationQAPortal') > -1 || url.indexOf('DeliveryPortal') > -1) {
        isAdmin = false;
        isDesigner = true;
    }
    
    $.each(arrOrders, function (i) {
        var item = arrOrders[i];

        var dtReceived = item.Received_Date_String;
        var dt = new Date(dtReceived);
        dt = new Date(dt.getTime() + (dt.getTimezoneOffset() * 60000));

        var dtDue;
        var dtDelivered;
       
        if (item.Due_Date_String == null) {
            dtDue = new Date('1/1/2000');
        } else {
            dtDue = new Date(item.Due_Date_String);
        }

        if (item.Delivered_Date_String == null) {
            dtDelivered = new Date('1/1/2015');
        } else {
            dtDelivered = new Date(item.Delivered_Date_String);
        }

        var offset = new Date().getTimezoneOffset();
        dtDue = new Date(dtDue.getTime() + offset * 60 * 1000);
        
        var dtTomorrow = new Date();
        dtTomorrow = addDays(dtDue, 1);

        var rowcolor = "#ffffff";
        var dtNow = new Date();

        if (dtDue.getTime() < dtNow.getTime() && item.Order_Status_Id < 9) {
            rowcolor = "#b20000"
        }

        if (dtTomorrow.getTime() > dtNow.getTime() && dtDue.getTime() < dtNow.getTime() && item.Order_Status_Id < 9) {
            rowcolor = '#FFC0CB';
        }

        if (item.Order_Status_Id == 3 || item.Order_Status_Id == 6 || item.Order_Status_Id == 7) {
            rowcolor = "#ffff4c";
        }

       

        if ($('#flt_client').length != 0) {
            filterClient = $('#flt_client').val();
            filterDivision = $('#flt_division').val();
            filterStatus = $('#flt_status').val();
            filterSubdivision = $('#flt_subdivision').val();
            filterAddress = $('#flt_address').val();
            filterPlanNo = $('#flt_plan_no').val();
        } else {
            filterClient = "";
            filterDivision = "";
            filterStatus = "";
            filterSubdivision = "";
            filterAddress = "";
            filterPlanNo = "";
        }

  
        if (item.Plan_Number == null) {
            item.Plan_Number = "";
        }

        if ((filterClient == "" || item.Client_Short_Name.toLowerCase().indexOf(filterClient.toLowerCase()) != -1)
            && (filterDivision == "" || item.Division_Desc.toLowerCase().indexOf(filterDivision.toLowerCase()) != -1)
            && (filterStatus == "" || item.Order_Status_Desc.toLowerCase().indexOf(filterStatus.toLowerCase()) != -1)
            && (filterSubdivision == "" || item.Subdivision_Name.toLowerCase().indexOf(filterSubdivision.toLowerCase()) != -1)
            && (filterAddress == "" || item.Address.toLowerCase().indexOf(filterAddress.toLowerCase()) != -1)
            && (filterPlanNo== "" || item.Plan_Number.toString().toLowerCase().indexOf(filterPlanNo.toString().toLowerCase()) != -1)
        ) {
          
            var bDoRow = false;

            if ($('#flt_open').hasClass('header_button_on') || $('#flt_complete').hasClass('header_button_on') || $('#flt_everything').hasClass('header_button_on') || $('#flt_delivered').hasClass('header_button_on'))
            {
                bDoRow = true;
            }

            if ($('#flt_late').hasClass('header_button_on') && rowcolor == "#b20000") {
                bDoRow = true;
            }

            if ($('#flt_today').hasClass('header_button_on') && rowcolor == '#FFC0CB') {
                bDoRow = true;
            }

            if (url.indexOf('DesignerPortal') > -1 && item.Order_Status_Desc.toLowerCase().trim() != "assigned" && item.Order_Status_Desc.toLowerCase().trim() != "designer complete") {
                bDoRow = true;
            } else {
                if (url.indexOf('DesignerPortal') > -1) {
                    bDoRow = false;
                }
            }

            if (url.indexOf('FoundationQAPortal') > -1 || url.indexOf('DeliveryPortal') > -1 || url.indexOf('AccountingPortal') > -1) {
                bDoRow = true;
            }

            if (isAccounting == true && displayOption == -1) {
                /*if (item.Invoice_Number == null) {
                    bDoRow = false;
                }*/
            }
         
           
            if (bDoRow == true) {
                var strHTML = "<div style='padding-top:5px;padding-bottom:2px;background:" + rowcolor + ";border-bottom:1px solid #eeeeee;'>";
               
                if (isAdmin == true) {
                    strHTML = strHTML + "<div class='lst_clickable' style='width:170px;font-weight:600;'><div style='float:left;margin-right:3px;margin-left:3px;margin-top:2px;'><img src='../Images/close.png' style='width:18px;height:18px;' onclick='deleteFoundationOrder(\"" + item.Order_Id + "\" )'></div><div onclick='editFoundationOrder(\"" + item.Order_Id + "\", \"" + item.MLAW_Number + "\")' onmouseover='showOrderDetails(\"" + item.Order_Id + "\" )'  onmouseout='hideOrderDetails(\"" + item.Order_Id + "\" )' style='float:left;'>" + item.MLAW_Number + "</div><div style='clear:both'></div></div>";
                } else if (isDesigner == true) {
                    strHTML = strHTML + "<div class='lst_clickable' style='width:170px;font-weight:600;'><div style='float:left;margin-right:3px;margin-left:3px;margin-top:2px;'><input type='checkbox' id='chk_" + item.Order_Id + "'></div><div  onclick='displayFoundationOrder(\"" + item.Order_Id + "\", 4 )' style='float:left;' onmouseover='showOrderDetails(\"" + item.Order_Id + "\" )'  onmouseout='hideOrderDetails(\"" + item.Order_Id + "\" )' > " + item.MLAW_Number + "</div><div style='clear:both'></div></div>";
                } else {
                    strHTML = strHTML + "<div class='lst_clickable' style='width:170px;font-weight:600;' onclick='displayFoundationOrder(\"" + item.Order_Id + "\", 5 )' onmouseover='showOrderDetails(\"" + item.Order_Id + "\" )'  onmouseout='hideOrderDetails(\"" + item.Order_Id + "\" )' >" + item.MLAW_Number + "</div>";
                }
              
                strHTML = strHTML + "<div class='lst_item' style='width:84px;'>" + item.Client_Short_Name + " </div>";
                strHTML = strHTML + "<div class='lst_item' style='width:160px;'>" + item.Subdivision_Name + " </div>";
                strHTML = strHTML + "<div class='lst_item' style='width:240px;'>" + item.Address + "</div>";
                strHTML = strHTML + "<div class='lst_item' style='width:80px;'>" + item.Lot + "/" + item.Block + "/" + item.Section + " </div>";
                strHTML = strHTML + "<div class='lst_item' style='width:115px;'>" + item.Order_Status_Desc + "</div>";
                

                if (isAccounting == true) {
                        if (item.Invoice_Number == null) {
                            strHTML = strHTML + "<div class='lst_item' style='width:120px;'>&nbsp;</div>";
                        } else {
                            strHTML = strHTML + "<div class='lst_item' style='width:120px;'><a href='../AccountingPortal/Create_Invoice.aspx?Order_Id=" + item.Order_Id + "' target='_new'>" + item.Invoice_Number + "</a></div>";
                        }
                    
                } else {
                    if (item.First_Name == null) {
                        strHTML = strHTML + "<div class='lst_item' style='width:144px;'>&nbsp;</div>";
                    } else {

                        strHTML = strHTML + "<div class='lst_item' style='width:144px;'>" + item.First_Name + " " + item.Last_Name + "</div>";
                    }
                }

                strHTML = strHTML + "<div class='lst_item' style='width:100px;' >" + (dt.getMonth() + 1) + '/' + dt.getUTCDate() + '/' + dt.getFullYear() + "</div>";

                if (isAccounting != true) {
                    if (item.Due_Date_String == null) {
                        strHTML = strHTML + "<div class='lst_item' style='width:100px;'>&nbsp;</div>";
                    } else {
                        strHTML = strHTML + "<div class='lst_item' style='width:100px;'>" + (dtDue.getMonth() + 1) + '/' + dtDue.getUTCDate() + '/' + dtDue.getFullYear() + "</div>";
                    }
                } else {
                    if (item.Delivered_Date_String == null) {
                        strHTML = strHTML + "<div class='lst_item' style='width:100px;'>&nbsp;</div>";
                    } else {
                        strHTML = strHTML + "<div class='lst_item' style='width:100px;'>" + (dtDelivered.getMonth() + 1) + '/' + dtDelivered.getUTCDate() + '/' + dtDelivered.getFullYear() + "</div>";
                    }

                }


                if (isAccounting == true) {
                    if (displayOption == -1) {
                        var amt = item.Amount;
                        var dis = item.Discount;

                        if(amt == "null")
                        {
                            amt = 0;
                        }

                        if(dis == "null")
                        {
                            dis = 0;
                        }

                        strHTML = strHTML + "<div class='lst_item' style='width:80px;'>" + (amt - dis) + "</div>";
                    }

                }
                strHTML = strHTML + "<div style='clear:both'></div>";
                strHTML = strHTML + "</div>";
                console.log(strHTML);

                $('#foundation_order_grid').append(strHTML);
                
                if (OrderPlaceHolder != 0) {

                }
            }
        }
    });

}


function excelList() {
    //handles gathering the data for the Excel downloads
    var flt_Date_Type = $('#flt_date_type').val();
    var flt_Begin_Date = $('#flt_begin_date').val();
    var flt_End_Date = $('#flt_end_date').val();

    var flt_Client = $('#flt_client').val();
    var flt_Division = $('#flt_division').val();
    var flt_Subdivision = $('#flt_subdivision').val();
    var flt_Status = $('#flt_status').val();
    var flt_Address = $('#flt_address').val();

    var statuses = "";
    
    if ($('#flt_complete').hasClass('header_button_on')) {
        statuses = '9,10';
    } else if ($('#flt_everything').hasClass('header_button_on')) {
        statuses = '1,2,3,4,5,6,7,8,9,10';
    } else if ($('#flt_delivered').hasClass('header_button_on')) {
        statuses = '10';
    } else {
        statuses = '1,2,3,4,5,6,7,8';
    }

    var url = window.location.href;
    if (url.indexOf('AccountingPortal') > -1) {
        statuses = 11;
    }

    var strURL = "../MiddleTier/orders_excel.aspx?Statuses=" + statuses + "&Date_Type=" + flt_Date_Type;
    strURL = strURL + "&Start_Date=" + flt_Begin_Date;
    strURL = strURL + "&End_Date=" + flt_End_Date;
    strURL = strURL + "&Client=" + flt_Client;
    strURL = strURL + "&Division=" + flt_Division;
    strURL = strURL + "&Subdivision=" + flt_Subdivision;
    strURL = strURL + "&Status=" + flt_Status;
    strURL = strURL + "&Address=" + flt_Address;
    console.log(strURL);

    /*EXCEL AJAX DOWNLOAD HACK*/

    var $idown;  // Keep it outside of the function, so it's initialized once.

    if ($idown) {
        $idown.attr('src',strURL);
    } else {
        $idown = $('<iframe>', { id:'idown', src:strURL }).hide().appendTo('body');
    }
}




function renderMyOrders() {
    //Renders Orders assigned to a designer
    $('#my_order_grid').html('');

    $.each(arrMyOrders, function (i) {
        var item = arrMyOrders[i];
        var dtReceived = item.Received_Date_String;
        var dt = new Date(dtReceived);

        var dtDue = new Date(item.Due_Date_String);

        var rowcolor = "#ffffff";

        if (dtDue < Date.now()) {
            rowcolor = "red"
        }

        if (item.Order_Status_Id == 3 || item.Order_Status_Id == 6 || item.Order_Status_Id == 7) {
            rowcolor = "yellow";
        }

        if ((filterClient == 0 || filterClient == item.Client_Id) && (filterDivision == 0 || filterDivision == item.Division_Id) && (filterStatus == 0 || filterStatus == item.Order_Status_Id) && (filterPlanNo == 0 || filterPlanNo == item.Plan_Number)) {
            var strHTML = "<div style='padding-top:5px;padding-bottom:2px;background:" + rowcolor + ";border-bottom:1px solid #eeeeee;'>";

            strHTML = strHTML + "<div class='lst_clickable' style='font-weight:600;' onclick='displayFoundationOrder(\"" + item.Order_Id + "\", 4 )'>" + item.MLAW_Number + "</div>";
            strHTML = strHTML + "<div class='lst_item' style='width:100px;'>" + item.Client_Short_Name + " </div>";
            strHTML = strHTML + "<div class='lst_item'>" + item.Subdivision_Name + " </div>";
            strHTML = strHTML + "<div class='lst_item' style='width:140px;'>" + item.Address + "</div>";
            strHTML = strHTML + "<div class='lst_item' style='width:60px;'>" + item.Lot + "/" + item.Block + "/" + item.Section + " </div>";
            strHTML = strHTML + "<div class='lst_item' style='width:100px;'>" + item.Order_Status_Desc + "</div>";
            if (item.First_Name == null) {
                strHTML = strHTML + "<div class='lst_item' style='width:120px;'>&nbsp;</div>";
            } else {
                strHTML = strHTML + "<div class='lst_item' style='width:120px;'>" + item.First_Name + " " + item.Last_Name + "</div>";
            }

            strHTML = strHTML + "<div class='lst_item' style='width:80px;' >" + (dt.getMonth() + 1) + '/' + dt.getUTCDate() + '/' + dt.getFullYear() + "</div>";
            strHTML = strHTML + "<div class='lst_item' style='width:80px;'>" + (dtDue.getMonth() + 1) + '/' + dtDue.getUTCDate() + '/' + dtDue.getFullYear() + "</div>";
            strHTML = strHTML + "<div style='clear:both'></div>";
            $('#my_order_grid').append(strHTML);
        }
    });

}


function showFoundationStatusList() {
    //gets the list of foundation statuses
    $('#status_list_title').text("STATUS");
    if (arrFoundationStatuses == null) {
        $.ajax({
            url: '../MiddleTier/Get_Foundation_Statuses.aspx',
            type: 'get',
            dataType: 'json',
            success: function (data) {
                arrFoundationStatuses = data;
                showFoundationStatusList();
            },
            error: function (e) {
                console.log(e.message);
            }
        });
    } else {
        $('#status_list_container').html('');
        $('#status_list_container').append("<div id='f_status_0' class='ddl_item' onclick='filterFoundation(\"status\",0)'>All</div>");
        $.each(arrFoundationStatuses, function (i) {
            var item = arrFoundationStatuses[i];
            $('#status_list_container').append("<div id='f_status_" + item.Order_Status_Id + "' class='ddl_item' onclick='filterFoundation(\"status\"," + item.Order_Status_Id + ")'>" + item.Order_Status_Desc + "</div>");
            
        });

        var top = $('#foundationStatusTitle').position().top - 6;
        var left = $('#foundationStatusTitle').position().left - 6;

        $('#status_list').css('top', top + 'px');
        $('#status_list').css('left', left + 'px');
      
        $('#status_list').show();

    }
 
}



function showFoundationDivisionList() {
    //renderst the list of divisions 
    $('#status_list_title').text("DIVISION");
    if (arrDivisions == null) {
        $.ajax({
            url: '../MiddleTier/Get_Divisions.aspx',
            type: 'get',
            dataType: 'json',
            success: function (data) {
                arrDivisions = data;
                showFoundationDivisionList();
            },
            error: function (e) {
                console.log(e.message);
            }
        });
    } else {
        $('#status_list_container').html('');
        $('#status_list_container').append("<div id='division_0' class='ddl_item' onclick='filterFoundation(\"division\",0)'>All</div>");
        $.each(arrDivisions, function (i) {
            var item = arrDivisions[i];
            $('#status_list_container').append("<div id='division_" + item.Division_Id + "' class='ddl_item' onclick='filterFoundation(\"division\"," + item.Division_Id + ")'>" + item.Division_Desc + "</div>");

        });

        var top = $('#foundationDivisionTitle').position().top - 6;
        var left = $('#foundationDivisionTitle').position().left - 6;

        $('#status_list').css('top', top + 'px');
        $('#status_list').css('left', left + 'px');

        $('#status_list').show();

    }

}


function showFoundationClientList() {
    //show the list of foundation clients
    $('#status_list_title').text("CLIENT");

        var iCount = 1;
        var strHTML = "";
        $('#status_list_container').html('');

        strHTML = '<div style="float:left;width:160px;">';
        strHTML = strHTML + "<div id='client_0' class='ddl_item' onclick='filterFoundation(\"client\",0)'>All</div>";
        $.each(arrClients, function (i) {
            var item = arrClients[i];
            if (iCount == 15) {
           
                strHTML = strHTML + '</div><div style="float:left;width:160px;">';
                iCount = 0;
            }

            strHTML = strHTML + "<div id='client_" + item.Client_Id + "' class='ddl_item' onclick='filterFoundation(\"client\"," + item.Client_Id + ")'>" + item.Client_Short_Name + "</div>";
            iCount = iCount + 1;
        });
        strHTML = strHTML + '</div><div style="clear:both"></div>';
        $('#status_list_container').html(strHTML);

        var top = $('#foundationClientTitle').position().top - 6;
        var left = $('#foundationClientTitle').position().left - 6;

        $('#status_list').css('top', top + 'px');
        $('#status_list').css('left', left + 'px');

        $('#status_list').show();

}


function filterFoundation(item, value) {
    //filte
    if (item == "status") {
        filterStatus = value;
    }

    if (item == "division") {
        filterDivision = value;
    }

    if (item == "client") {
        filterClient = value;
    }

    renderOrders();
    $('#status_list').hide();
}

$('#status_list').mouseleave(function () {
    $('#status_list').hide();
});


function editFoundationOrder(order_id, mlaw_number) {
    //loads the necessary items for editing a foundation
    var stateObj = { id: order_id };
    history.pushState(stateObj, "order number:" + order_id, order_id + ".html");

    var url = "html/edit_foundation_order.html";

    if (mlaw_number.match(/[a-z]/i)) {
        url = "html/edit_revision_order.html";
    }

    
    $('#foundation_editing').load(url, function () {
        loadFoundationStatuses();
        loadFoundationDesigners();
    
        $('#foundation_editing').show();

        curOrderId = order_id;
        editMode = 1;
  
            $('#foundation_order_list').hide();
        
            var strUrl = "../MiddleTier/Get_Order_Info.aspx?Order_Id=" + order_id;
            console.log(strUrl);
            $.ajax({
                url: strUrl,
                type: 'get',
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                    var item = data[0];
                    
                    $('#foundation_editing').find('input:text').val('');

                    if (url.indexOf("foundation") > -1) {
                        $('#ord_edit_company').val('');
                        $('#ord_edit_company_holder').text('');
                        $('#ord_edit_subdivision_id').val('');
                        $('#ord_edit_subdivision_holder').text('');
                        $('#ord_edit_comments').val('');

                        $('#ord_edit_mlaw_number').text(item.MLAW_Number);
                        $('#ord_edit_status').val(item.Order_Status_Id);
                        $('#ord_edit_section').val(item.Section);
                        $('#ord_edit_phase').val(item.Phase);
                        $('#ord_edit_lot').val(item.Lot);
                        $('#ord_edit_block').val(item.Block);
                        $('#ord_edit_contact').val(item.Contact);
                        $('#ord_edit_phone').val(item.Phone);
                        $('#ord_edit_elevation').val(item.Elevation);
                        $('#ord_edit_plan_name').val(item.Plan_Name);
                        $('#ord_edit_plan_number').val(item.Plan_Number);

                        $('#ord_edit_date_received').val(item.Received_Date_String);
                        $('#ord_edit_date_due').val(item.Due_Date_String);
                        $('#ord_edit_address').val(item.Address);
                        $('#ord_edit_company_holder').text(item.Client_Short_Name);
                        $('#ord_edit_company').val(item.Client_Id);
                        $('#ord_edit_subdivision_holder').text(item.Subdivision_Name);
                        $('#ord_edit_subdivision_id').val(item.Subdivision_Id);
                        $('#ord_edit_assign').val(item.Designer_Id);

                        $('#ord_edit_county').val(item.County);
                        $('#ord_edit_comments').val(item.Comments);
                        $('#ord_edit_garage_options').val(item.Garage_Options);
                        $('#ord_edit_patio_options').val(item.Patio_Options);

                        $('#ord_edit_soils_data_source').val(item.Soils_Data_Source);
                        $('#ord_edit_viz_geo_date').val(item.Geotec_Date_String);
                        $('#ord_edit_slab_sq_ft').val(item.Slab_Square_Feet);

                        $('#ord_edit_pi').val(item.PI);
                        $('#ord_edit_fill_applied').val(item.Fill_Applied);
                        $('#ord_edit_slope').val(item.Slope);
                        $('#ord_edit_brg_cap').val(item.Brg_cap);

                        $('#ord_edit_em_ctr').val(quickNumberFormat(item.Em_ctr));
                        $('#ord_edit_em_edg').val(quickNumberFormat(item.Em_edg));
                        $('#ord_edit_ym_ctr').val(quickNumberFormat(item.Ym_ctr));
                        $('#ord_edit_ym_edg').val(quickNumberFormat(item.Ym_edg));

                        $('#ord_edit_customer_job_number').val(item.Customer_Job_Number);
                        $('#ord_edit_fill_depth').val(item.Fill_Depth);
                        $('#ord_edit_soils_comments').val(item.Soils_Comments);
                        $('#ord_edit_status_text').text(item.Designer_Comments);
                        $('#is_800').val(item.Is_800);

                        if (item.Designer_Comments) {
                            if (item.Designer_Comments.trim() != "") {
                                $('#ord_edit_status_text_holder').show();
                            }
                        }

                        RadionButtonSelectedValueSet('ord_edit_rev', item.Is_Revision);
                        RadionButtonSelectedValueSet('ord_edit_garage', item.Garage);
                        RadionButtonSelectedValueSet('ord_edit_fireplace', item.Fireplace);
                        RadionButtonSelectedValueSet('ord_edit_patio', item.Patio);
                        RadionButtonSelectedValueSet('ord_edit_foundation_type', item.Foundation_Type);
                        RadionButtonSelectedValueSet('ord_edit_masonry', item.Masonry_Sides);

                        loadClientSubdivisionTypeAhead();
                    } else {
                        $('#ord_edit_company').text(item.Client_Short_Name);
                        $('#ord_edit_subdivision').text(item.Subdivision_Name);
                        $('#ord_edit_comments').text(item.Comments);

                        $('#ord_edit_mlaw_number').text(item.MLAW_Number);
                        $('#ord_edit_status').val(item.Order_Status_Id);
                        $('#ord_edit_section').text(item.Section);
                        $('#ord_edit_phase').text(item.Phase);
                        $('#ord_edit_lot').text(item.Lot);
                        $('#ord_edit_block').text(item.Block);
                        $('#ord_edit_contact').text(item.Contact);
                        $('#ord_edit_phone').text(item.Phone);
                        $('#ord_edit_elevation').text(item.Elevation);
                        $('#ord_edit_plan_name').text(item.Plan_Name);
                        $('#ord_edit_plan_number').text(item.Plan_Number);

                        $('#ord_edit_date_received').text(item.Received_Date_String);
                        $('#ord_edit_date_due').text(item.Due_Date_String);
                        $('#ord_edit_address').text(item.Address);
                        $('#ord_edit_company_holder').text(item.Client_Short_Name);
                        $('#ord_edit_company').text(item.Client_Id);
                        $('#ord_edit_subdivision_holder').text(item.Subdivision_Name);
                        $('#ord_edit_subdivision_id').text(item.Subdivision_Id);
                        $('#ord_edit_assign').val(item.Designer_Id);

                        $('#ord_edit_county').text(item.County);
                        $('#revision_notes').text(item.Comments);
                        $('#ord_edit_garage_options').text(item.Garage_Options);
                        $('#ord_edit_patio_options').text(item.Patio_Options);

                        $('#ord_edit_soils_data_source').text(item.Soils_Data_Source);
                        $('#ord_edit_viz_geo_date').text(item.Geotec_Date_String);
                        $('#ord_edit_slab_sq_ft').text(item.Slab_Square_Feet);

                        $('#ord_edit_pi').text(item.PI);
                        $('#ord_edit_fill_applied').text(item.Fill_Applied);
                        $('#ord_edit_slope').text(item.Slope);
                        $('#ord_edit_brg_cap').text(item.Brg_cap);


                        $('#ord_edit_em_ctr').text(quickNumberFormat(item.Em_ctr));
                        $('#ord_edit_em_edg').text(quickNumberFormat(item.Em_edg));
                        $('#ord_edit_ym_ctr').text(quickNumberFormat(item.Ym_ctr));
                        $('#ord_edit_ym_edg').text(quickNumberFormat(item.Ym_edg));

                        $('#ord_edit_customer_job_number').text(item.Customer_Job_Number);
                        $('#ord_edit_fill_depth').text(item.Fill_Depth);
                        $('#ord_edit_soils_comments').text(item.Soils_Comments);
                        $('#ord_edit_status_text').text(item.Designer_Comments);

                        if (item.Designer_Comments) {
                            if (item.Designer_Comments.trim() != "") {
                                $('#ord_edit_status_text_holder').show();
                            }
                        }

                        $('#ord_edit_garage').text(item.Garage);
                        $('#ord_edit_fireplace').text(item.Fireplace);
                        $('#ord_edit_patio').text(item.Patio);
                        $('#ord_edit_foundation_type').text(item.Foundation_Type);
                        $('#ord_edit_masonry').text(item.Masonry_Sides);

                    }

                    $(document).scrollTop(0, 0);

                    var strUrl = "../MiddleTier/Get_Order_History.aspx?Order_Id=" + order_id;
                    $('#ord_history').html('');
                    $.ajax({
                        url: strUrl,
                        type: 'get',
                        dataType: 'json',
                        success: function (data) {

                            var MLAW_Number = "";
                            $.each(data, function (i) {
                                var thisItem = data[i];
                                if (MLAW_Number != thisItem.MLAW_Number) {
                                    $('#ord_history').append("<div style='font-weight:600;margin-top:10px;'>" + thisItem.MLAW_Number + "</div>");
                                    $('#ord_history').append("<div style='font-weight:400;margin-bottom:5px;'>" + thisItem.Comments + "</div>");
                                    MLAW_Number = thisItem.MLAW_Number;
                                }

                                $('#ord_history').append("<div><div style='float:left;width:120px;'>" + thisItem.Date_String + "</div><div style='float:left'>" + thisItem.Order_Status_Desc + "</div><div style='clear:both'></div></div>");

                            });


                        }
                    });

                    loadOrderFiles(order_id);
                    

               

                },
                error: function (e) {
                    console.log(e.message);
                }

        });
    });
}

function loadOrderFiles(order_id) {
    //loads the list of files associated with an order
    var strUrl = "../MiddleTier/Get_Order_Files.aspx?Order_Id=" + order_id;
    $.ajax({
        url: strUrl,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            $('#ord_edit_file_list').html('');
            $.each(data, function (i) {
                $('#ord_edit_file_list').append("<div><a href='../display_file.aspx?Order_File_Id=" + data[i].Order_File_Id + "' target='_new'>" + data[i].Order_File_Name + "</a></div>");
            });
            

        },
        error: function (e) {
            console.log(e.message);
        }

    });

}
function RadionButtonSelectedValueSet(name, SelectdValue) {
    $('input[name="' + name+ '"][value="' + SelectdValue + '"]').prop('checked', true);
}

function loadFoundationDesigners() {
    //loads the list of foundation designers
    $.ajax({
        url: '../MiddleTier/Get_Foundation_Designers.aspx',
        type: 'get',
        dataType: 'json',
        success: function (data) {
           
            $('#ord_edit_assign').append($('<option/>', {
                value: 0,
                text: "Unassigned"
            }));

            $.each(data, function (i) {


                $('#ord_edit_assign').append($('<option/>', {
                    value: data[i].User_Id,
                    text: data[i].Email
                }));
            });
        },
        error: function (e) {
            console.log(e.message);
        }
    });
}


function assignJob() {
    //assigns an order to a designer
    var userId = $('#ord_edit_assign').val();
    
    if (isNaN(userId)) {
        userId = current_user_id;
    }
    var strUrl = "../MiddleTier/Assign_Order.aspx?Order_Id=" + curOrderId + "&User_Id=" + userId;

    $.ajax({
        url: strUrl,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            numAssigned = numAssigned + 1;
            $('#ord_edit_status').val("5");
        },
        error: function (e) {
            console.log(e.message);
        }

    });
}


function loadUnassignedOrders() {
    //loads a list of active orders that have not been assigned to a designer
    var strURL = '../DesignerPortal/Get_Unassigned_Orders.aspx';

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            console.log("UNNASSIGNED ORDERS:" + JSON.stringify(data));
            arrOrders = data;
            renderOrders();
        },
        error: function (e) {
            console.log(e.message);
        }

    });
}

function loadUserOrders() {
    //Gets the list of Orders assigned to a Designer
    var strURL = '../DesignerPortal/Get_User_Orders.aspx';
    
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
           
            arrMyOrders = data;
            renderMyOrders();
        },
        error: function (e) {
            console.log(e.message);
        }

    });
}

function loadCustomerOpenOrders() {
    //loads a list of all open orders for a customer
    var strURL = '../MiddleTier/Get_Open_Orders.aspx?Customer_Id=' + customerId;
    $('#foundation_order_grid').html('');

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            //alert(JSON.stringify(data));
            arrOrders = data;
            renderOrders();

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}





function displayFoundationOrder(order_id, user_level) {
    //displays information about a foundation order on mouseovers 
    $('#quick_search_results').hide();
    var stateObj = { page: "order" };
    history.pushState(stateObj, "order", order_id + ".html");

    curOrderId = order_id;

    var strURL = '../MiddleTier/Get_Order_Info.aspx?Order_Id=' + order_id;


    
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var item = data[0];
          
            $('#foundation_order_list').hide();

            $('#create_inspection_order').hide();
            $('#inspection_order_grid').hide();

            if ($('#view_form').length == 0) {
                document.body.innerHTML += '<div id="view_form"></div>';
            }

            $('#view_form').load("../html/view_foundation_order.html", function () {

                $('#view_form').show();

                $('#vw_mlaw_number').html(item.MLAW_Number);
                if (item.Is_Revision == "1") {
                    $('#vw_rev').html('yes');
                } else {
                    $('#vw_rev').html('no');
                }

                if ($.trim(item.Invoice_Number) != "") {
                    $('#vw_invoice_number').html("<a href='../AccountingPortal/Create_Invoice.aspx?Order_Id=" + item.Order_Id + "' target='_new'>" + item.Invoice_Number + "</a>");
                }

                $('#vw_foundation_type').html(item.Foundation_Type);
                $('#vw_date_received').html(item.Received_Date_String);
                $('#vw_subdivision').html(item.Subdivision_Name);
                $('#vw_client_name').html(item.Client_Short_Name);
                $('#vw_contact').html(item.Contact);
                $('#vw_phone').html(item.Phone);
                $('#vw_address').html(item.Address);
                $('#vw_section').html(item.Section);
                $('#vw_phase').html(item.Phase);
                $('#vw_lot').html(item.Lot);
                $('#vw_block').html(item.Block);
                $('#vw_county').html(item.County);
                $('#vw_city').html(item.City);
                $('#vw_plan_name').html(item.Plan_Name);
                $('#vw_elevation').html(item.Elevation);
                $('#vw_plan_number').html(item.Plan_Number);
                $('#vw_garage').html(item.Garage + ", " + item.Garage_Options);
                $('#vw_patio').html(item.Patio + ", " + item.Patio_Options);
                $('#vw_masonry_sides').html(item.Masonry_Sides);
                $('#vw_fireplace').html(item.Fireplace);
                $('#vw_comments').html(item.Comments);
                $('#vw_soils_data_source').html(item.Soils_Data_Source);
                $('#vw_viz_geo_date').html(item.Geotec_Date_String);
                $('#vw_slab_sq_ft').html(item.Slab_Square_Feet);
                $('#vw_PI').html(item.PI);
                $('#vw_fill_applied').html(item.Fill_Applied);
                $('#vw_slope').html(item.Slope);
                $('#vw_brg_cap').html(item.Brg_cap);

                $('#vw_em_ctr').html(quickNumberFormat(item.Em_ctr));
                $('#vw_em_edg').html(quickNumberFormat(item.Em_edg));
                $('#vw_ym_ctr').html(quickNumberFormat(item.Ym_ctr));
                $('#vw_ym_edg').html(quickNumberFormat(item.Ym_edg));

                $('#vw_ord_status').val(item.Order_Status_Id);

                if (item.Order_Status_Id == 6 || item.Order_Status_Id == 7) {
                    $('#vw_designer_comments').text(item.Designer_Comments);
                    $('#designer_comments_holder').show();
                }

                if (user_level == 5) {
                    $('#designer_info').hide();
                }

            });

            loadOrderFiles(order_id);
            $(document).scrollTop(0, 0);

        },
        error: function (e) {
            console.log(e.message);
        }
    });

}




function setSubdivision(thisName) {
   //sets the subdivision in the order form
    if (thisName.trim() != '') {
        $.each(arrSubdivisionList, function (i) {
            var item = arrSubdivisionList[i];
            if (item.Client_Id == $('#ord_company').val()) {

                var thisNameFormatted = thisName.replace("@", "at");
                var itemNameFormatted = item.Subdivision_Name.replace("@", "at");

                console.log(thisNameFormatted + ":" + itemNameFormatted);

                if (thisNameFormatted.toLowerCase().indexOf(itemNameFormatted.toLowerCase()) > -1 || itemNameFormatted.toLowerCase().indexOf(thisNameFormatted.toLowerCase()) > -1) {

                    $('#ord_subdivision_holder').text(item.Subdivision_Name);
                    $('#ord_subdivision_id').val(item.Subdivision_Id);
                }

            }
        });
    }
}

function deleteFoundationOrder(order_id) {
    //removes a foundation order from the display
    if (confirm("Please Confirm that you would like to remove this order from the system.")) {
        var strURL = '../Delete_Order.aspx?Order_Id=' + order_id;
    
        $.ajax({
            url: strURL,
            type: 'get',
            dataType: 'json',
            success: function (data) {
                loadOpenOrders();
            }
        });
    } 
}

function checkAllUnassigned() {
    //allows a designer to check all the orders in a filtered list of unassigned orders
    var isChecked = $('#check_all_unassigned').is(':checked');

    if (isChecked == true) {
        $("#foundation_order_grid :checkbox").prop('checked', true);
    } else {
        $("#foundation_order_grid :checkbox").prop('checked', false);
    }
}

var numAssigned = 0;
function assignOrders() {
    //assigns all order that checked
    numAssigned = 0;
    var selected = [];
    $('#foundation_order_grid input:checked').each(function () {
        selected.push($(this).attr('id'));
    });


    for (var i = 0; i < selected.length; i++) {
        curOrderId = selected[i].replace("chk_", "");
        assignJob();
    }

    reloadOpenOrders(selected.length);

    
}

function setFoundationOrderStatus(Status_Id) {
    //changes the order status of a foundation order
    var selected = [];
    $('#foundation_order_grid input:checked').each(function () {
        selected.push($(this).attr('id'));
    });

    var numOrders = selected.length;
 
    var iCount = 0;
    for (var i = 0; i < selected.length; i++) {
        curOrderId = selected[i].replace("chk_", "");
        var strURL = "../MiddleTier/Update_Order_Status.aspx?Order_Id=" + curOrderId + "&Status_Id=" + Status_Id;

        $.ajax({
            url: strURL,
            type: 'get',
            dataType: 'json',
            success: function (data) {
                iCount = iCount + 1;
                if (iCount == numOrders) {
                    loadOrdersByStatusId((Status_Id - 1));
                }
            }
        });
    }

    reloadOpenOrders(selected.length);

}

function updateFoundationOrderStatus(order_id, status_id) {
    //deprecated
}

function reloadOpenOrders(totalAssigned) {
    //UI Cleanup after order information is changed
    if (totalAssigned == numAssigned) {
        loadOpenOrders();
        loadUserOrders();
    } else {
        setTimeout(function () {
            reloadOpenOrders(totalAssigned);
        }, 1000);
    }
}

function toggleDesigner() {
    //displays/hides designer commments about an order per Jamie - deprecated
    $('#designer_comments_holder').hide();
    $('#design_upload').hide();
    $('#vw_update_button').hide();

    var iVal = $('#vw_ord_status').val();

    if (iVal == 6 || iVal == 7) {
        $('#designer_comments_holder').show();
        $('#vw_update_button').show();
    }

    if (iVal == 8) {
        $('#design_upload').show();
        $('#vw_update_button').show();
    }
}


function updateForDesigner() {
    //updates an order when a designer is done per Jamie - deprecated
    var iOrderId = curOrderId;
    var iStatusId = $('#vw_ord_status').val();
    var MLAWNumber = $('#vw_mlaw_number').text();
    var strFile = "";
    var strComments = "";

    var bSubmit = true;

    if (iStatusId == 6 || iStatusId == 7) {
        strComments = $('#vw_designer_comments').val();

        if (strComments == "") {
            alert('Please describe why this order is being placed on hold');
            bSubmit = false;
        }
    }


    if (bSubmit == true) {
        var strUrl = "../DesignerPortal/Update_For_Designer.aspx?Order_Id=" + iOrderId + "&Status_Id=" + iStatusId + "&MLAW_Num=" + encodeURIComponent(MLAWNumber) + "&Comments=" + encodeURIComponent(strComments) + "&File=" + encodeURIComponent(strFile);
        console.log(strUrl);
        $.ajax({
            url: strUrl,
            type: 'get',
            dataType: 'text',
            success: function (data) {
                alert('the status of this order has been updated');

                loadUserOrders();

                $('#foundation_order_list').show();
                $('#view_form').hide();
            }
        });
    }
}


function doAddressSearch() {
    if ($('#ord_address').val().length > 3) {
 
        findMLAWNum();
    }
}

var revMatchOrderId = 0;

function doRevMatch(order_id) {

    //attempts to find the parent order for a revision
    revMatchOrderId = order_id;

    var strUrl = "../MiddleTier/Get_Order_Info.aspx?Order_Id=" + order_id;
    $.ajax({
        url: strUrl,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var item = data[0];

            $('#foundation_entry_container').html('');
            $('#foundation_entry_container').load("html/create_revision_order.html", function () {

                if (item.Is_800 == "1") {
                    $('#ord_client_id').val(item.Client_Id);
                } else {
                    $('#ord_client_id').val(item.Client_Id1);
                }
                $('#ord_company').text('');
                $('#ord_company_holder').text('');
                $('#ord_subdivision_id').text('');
                $('#ord_subdivision_holder').text('');
                $('#ord_comments').text('');
                if (tempRevisionComments != "") {
                    $('#revision_notes').val(tempRevisionComments);
                    tempRevisionComments = "";
                }

                $('#ord_mlaw_number').text(item.MLAW_Number);
                $('#ord_status').text(item.Order_Status_Desc);
                $('#ord_section').text(item.Section);
                $('#ord_phase').text(item.Phase);
                $('#ord_lot').text(item.Lot);
                $('#ord_block').text(item.Block);
                $('#ord_contact').text(item.Contact);
                $('#ord_phone').text(item.Phone);
                $('#ord_elevation').text(item.Elevation);
                $('#ord_plan_name').val(item.Plan_Name);
                $('#ord_plan_number').val(item.Plan_Number);
                $('#ord_date_received').text(item.Received_Date_String);
                $('#ord_address').text(item.Address);
                $('#ord_company').text(item.Client_Id1);
                $('#ord_subdivision').text(item.Subdivision_Name);
                $('#ord_mlaw_number').text(item.MLAW_Number);

                $('#ord_county').text(item.County);
                $('#ord_comments').text(item.Comments);
                $('#ord_garage_options').text(item.Garage_Options);
                $('#ord_patio_options').text(item.Patio_Options);

                $('#ord_soils_data_source').text(item.Soils_Data_Source);
                $('#ord_viz_geo_date').text(item.Geotec_Date_String);
                $('#ord_slab_sq_ft').text(item.Slab_Square_Feet);

                $('#ord_pi').text(item.PI);
                $('#ord_fill_applied').text(item.Fill_Applied);
                $('#ord_slope').text(item.Slope);
                $('#ord_brg_cap').text(item.Brg_cap);
                $('#ord_em_ctr').text(item.Em_ctr);
                $('#ord_em_edg').text(item.Em_edg);
                $('#ord_ym_ctr').text(item.Ym_ctr);
                $('#ord_ym_edg').text(item.Ym_edg);

                $('#ord_customer_job_number').text(item.Customer_Job_Number);
                $('#ord_fill_depth').text(item.Fill_Depth);
                $('#ord_soils_comments').text(item.Soils_Comments);
                $('#ord_status_text').text(item.Designer_Comments);

                
                getRevisionPrice();

                var objRev = $("#dragandrophandlerRevision");

                objRev.on('dragenter', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    $(this).css('border', '2px solid #0B85A1');
                });

                objRev.on('dragover', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
   
                });

                objRev.on('drop', function (e) {

                    $(this).css('border', '2px dotted #0B85A1');
                    e.preventDefault();
                    var files = e.originalEvent.dataTransfer.files;
                  
                    //We need to send dropped files to Server
                    handleFileUpload2(files, objRev, null);
                });


                var strUrl = "../MiddleTier/Get_Order_History.aspx?Order_Id=" + order_id;
                $.ajax({
                    url: strUrl,
                    type: 'get',
                    dataType: 'json',
                    success: function (data) {
                      
                        var MLAW_Number = "";
                        $.each(data, function (i) {
                            var thisItem = data[i];
                            if (MLAW_Number != thisItem.MLAW_Number) {
                                $('#ord_history').append("<div style='font-weight:600;margin-top:10px;'>" + thisItem.MLAW_Number + "</div>");
                                $('#ord_history').append("<div style='font-weight:400;margin-bottom:5px;'>" + thisItem.Comments + "</div>");
                                MLAW_Number = thisItem.MLAW_Number;
                            }

                            $('#ord_history').append("<div><div style='float:left;width:120px;'>" + thisItem.Date_String + "</div><div style='float:left'>" + thisItem.Order_Status_Desc + "</div><div style='clear:both'></div></div>");

                        });


                    }
                });


            });
            

            
           
        }
        });

}

function insertRevision() {
    //creates a revision order

    var order_id = revMatchOrderId;

    if (order_id == null) {
        alert("This order is marked as a revision, but you have not chosen an order to be revised");
    }
    else {
        var strText = $('#revision_notes').val();
        var strPlanName = $('#ord_plan_name').val();
        var strPlanNumber = $('#ord_plan_number').val();
        var amount = $('#ord_rev_price').val();

        strURL = "../MiddleTier/Insert_Revision.aspx?Order_Id=" + order_id + "&Revision_Text=" + strText + "&Order_Files=" + encodeURIComponent(orderFile) + "&Plan_Name=" + strPlanName + "&Plan_Number=" + strPlanNumber + "&Amount=" + amount;

        $.ajax({
            url: strURL,
            type: 'get',
            dataType: 'json',
            success: function (data) {

                $('#foundation_entry_container').hide();
                $('#foundation_order_list').show();
                loadOpenOrders();


            }
        });
    }
}

function hideAddressMatches() {
    //toggles the list of addresses that are near matches for a revision order 
    $('#ord_rev_match_holder').hide();
}

function findMLAWNum() {
   //attempts to get an order based on the address
    var doRevision = $('input:radio[name=ord_rev]:checked').val();
   
    if (doRevision == 1) {
        $('#ord_rev_match_holder').show();

        var address = $('#ord_address').val();
        var lot = $('#ord_lot').val();
        var block = $('#ord_block').val();
        
        if (address.trim() != "") {

            var strURL = '../MiddleTier/Find_MLAW_Number.aspx?Address=' + address + '&Lot=' + lot + '&Block=' + block;

            $.ajax({
                url: strURL,
                type: 'get',
                dataType: 'json',
                success: function (data) {

                    if (JSON.stringify(data).trim() == "[]" && address != "") {
                        alert('there are no address matches');
                    }
                    $('#ord_rev_match_list').html('');

                    $.each(data, function (i) {
                        $('#ord_rev_match_list').append("<input type='radio' name='ord_rev_match' value='" + data[i].Order_Id + "' onclick='doRevMatch(" + data[i].Order_Id + ")'><b>" + data[i].MLAW_Number + "</b> - " + data[i].Address + ", Lot " + data[i].Lot + ", Block " + data[i].Block + "<br>");
                    });

                }
            });
        } else {
            //alert('Please enter an address to search');
        }
    }
}

var isSearching = 0;
var searchLength = 0;
function doQuickSearch() {
    //handles the quick search
    if ($('#quick_search').val().length > 3 && searchLength != $('#quick_search').val().length) {
        searchLength = $('#quick_search').val().length;
        $('#quick_search_results').show();

        if (isSearching == 0) {
            isSearching = 1;
            var strURL = '../MiddleTier/Do_Quick_Search.aspx?Address=' + $('#quick_search').val();

            $.ajax({
                url: strURL,
                type: 'get',
                dataType: 'json',
                success: function (data) {
                    $('#quick_search_results').html('<div style="height:10px;line-height:10px;"></div>');
                    if (JSON.stringify(data).trim() == "[]") {
                        $('#quick_search_results').append("<div>no matches</div>");
                    }
                    else {
                        $.each(data, function (i) {
                            var url = window.location.href;

                            if (url.indexOf('InspectionPortal') > -1) {
                                //$('#quick_search_results').append("<div class='quick_search_result' onclick='displayFoundationOrder(" + data[i].Order_Id + ", \"" + data[i].MLAW_Number + "\")'><b>" + data[i].MLAW_Number + "</b> - " + data[i].Address + ", Lot " + data[i].Lot + ", Block " + data[i].Block + "</div>");
                                // NEED TO COME UP WITH SOMETHING HERE //
                            } else if (url.indexOf('AccountingPortal') > -1 || url.indexOf('Accounting_Portal') > -1) {
                                $('#quick_search_results').append("<div class='quick_search_result' onclick='displayFoundationOrder(" + data[i].Order_Id + ", \"" + data[i].MLAW_Number + "\")'><b>" + data[i].MLAW_Number + "</b> - " + data[i].Address + ", Lot " + data[i].Lot + ", Block " + data[i].Block + "</div>");
                            }else if (url.indexOf('DesignerPortal') > -1 || url.indexOf('Inspection_Portal') > -1) {
                                $('#quick_search_results').append("<div class='quick_search_result' onclick='displayFoundationOrder(" + data[i].Order_Id + ", \"" + data[i].MLAW_Number + "\")'><b>" + data[i].MLAW_Number + "</b> - " + data[i].Address + ", Lot " + data[i].Lot + ", Block " + data[i].Block + "</div>");
                            } else {
                                $('#quick_search_results').append("<div class='quick_search_result' onclick='loadOrder(" + data[i].Order_Id + ", \"" + data[i].MLAW_Number + "\")'><b>" + data[i].MLAW_Number + "</b> - " + data[i].Address + ", Lot " + data[i].Lot + ", Block " + data[i].Block + "</div>");
                            }
                        });
                    }
                    isSearching = 0;
                }
            });
        } else {

        }

    } else {
        if($('#quick_search').val().length < 4)
        {
            $('#quick_search_results').html('');
            $('#quick_search_results').hide();
        }
    }

}

function loadOrder(order_id) {
    //loads an order if you just go to http://[portal_ip_address]/[Order_Id].html
    window.location.href = "../default.aspx?Order_Id=" + order_id;
}


function toggleFilters2() {
    //shows/hides the list of filters for the order lists
    if ($('#filters').is(":visible")) {
        $('#filters').hide();
    } else {
        $('#filters').show();
    }
}

function toggleFilters() {
    //shows/hides the list of filters for the order lists
    if ($('#filters').is(":visible")) {
        $('#filters').hide();
        $('#foundation_order_grid').css('margin-top', '100px');

    } else {
        $('#filters').show();
        $('#foundation_order_grid').css('margin-top', '300px');
    }
}
function toggleFilters3() {
    //shows/hides the list of filters for the order lists
    if ($('#filters').is(":visible")) {
        $('#filters').hide();
        $('#foundation_order_grid').css('margin-top', '140px');
        $('#warranty_order_grid').css('margin-top', '140px');
    } else {
        $('#filters').show();
        $('#foundation_order_grid').css('margin-top', '340px');
        $('#warranty_order_grid').css('margin-top', '340px');
    }
}

function doOpenOrders() {
    //loads all open orders
    $('#order_types .header_button').removeClass('header_button_on');
    $('#flt_open').addClass('header_button_on');
    loadOpenOrders();
}

function doLateOrders() {
    //loads all late orders
    $('#order_types .header_button').removeClass('header_button_on');
    $('#flt_late').addClass('header_button_on');
    loadOpenOrders();
}

function doTodayOrders() {
    //loads all orders due today
    $('#order_types .header_button').removeClass('header_button_on');
    $('#flt_today').addClass('header_button_on');
    loadOpenOrders();
}




function doOrderSlice(option) {
    //grabs a slice of orders based on what's been entered in ths order list filters
    if($('#filters').is(":visible") == true)
    {
        $('#foundation_order_grid').css('margin-top', '300px');
    }

    var statuses = "";
    var date_type = "";

    var begin_date = $('#flt_begin_date').val();
    var end_date = $('#flt_end_date').val();

    var dtNow = new Date();
    var dtBegin = addDays(dtNow, -7);
    var sdf = new SimpleDateFormat("MM-dd-yyyy");

    if (option == 1) {
        statuses = '9';
        date_type = '3';
        
        end_date = sdf.format(dtNow);
        begin_date = sdf.format(dtBegin);

        $('#flt_begin_date').val(begin_date);
        $('#flt_end_date').val(end_date);
        $('#flt_date_type').val(3);

        $('#order_types .header_button').removeClass('header_button_on');
        $('#flt_complete').addClass('header_button_on');
    }

    if (option == 3) {
        statuses = '10';
        date_type = '4';

        end_date = sdf.format(dtNow);
        begin_date = sdf.format(dtBegin);

        $('#flt_begin_date').val(begin_date);
        $('#flt_end_date').val(end_date);
        $('#flt_date_type').val(4);

        $('#order_types .header_button').removeClass('header_button_on');
        $('#flt_delivered').addClass('header_button_on');
    }

    if (option == 2) {
        statuses = '1,2,3,4,5,6,7,8,9,10,11';
        date_type = '1';
        end_date = sdf.format(dtNow);
        begin_date = sdf.format(dtBegin);
    
        $('#flt_begin_date').val(begin_date);
        $('#flt_end_date').val(end_date);
        $('#flt_date_type').val(1);
        
        $('#order_types .header_button').removeClass('header_button_on');
        $('#flt_everything').addClass('header_button_on');
    }
    /*
    if (option == 3) {
        date_type = $('#flt_date_type').val();
        if ($('#flt_complete').hasClass('header_button_on')) {
            statuses = '9,10';
        } else if ($('#flt_everything').hasClass('header_button_on')) {
            statuses = '1,2,3,4,5,6,7,8,9,10';
        } else {
            statuses = '1,2,3,4,5,6,7,8';
        }
    }
    */

    if (option == 4) {
        statuses = '11';
        date_type = $('#flt_date_type').val();
    }

    if (option == 5) {
        statuses = '1, 2, 3, 4';
        date_type = $('#flt_date_type').val();
    }

    if (option == 6) {
        statuses = '1,2,3,4,5,6,7,8,9,10,11';
        date_type = '1';
    }

   

    var strURL = "../MiddleTier/Get_Order_Slice.aspx?Statuses=" + statuses + "&Date_Type=" + date_type + "&Start_Date=" + begin_date + "&End_Date=" + end_date;
    console.log(strURL);
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrOrders = data;

            if (option == 4 || option == 5) {
                resort_orders("Delivered_Date");
            }
            console.log(JSON.stringify(data));
            renderOrders();
            
        }
    });
}

function addDays(date, days) {
    //simple functio add days to a javascript Date
    var result = new Date(date);
    result.setDate(date.getDate() + days);
    return result;
}


function resort_orders(col) {
    //Sorts the order list by column specified
    currentSort = col;
    $('#order_list_titles div').removeClass('header_button_on');
    $('#order_list_title_' + col).addClass('header_button_on');
    
    if (col == "Received_Date" || col == "Due_Date" || col == "Delivered_Date") {
        arrOrders.sort(function (el1, el2) {
            return datecompare(el1, el2, col + "_String")
        });
    }else if(col == "Client_Short_Name")
    {
        arrOrders.sort(function (el1, el2) {
            return clientcompare(el1, el2, col);
        });

    }else{

        arrOrders.sort(function (el1, el2) {
            return compare(el1, el2, col)
        });
    }

    renderOrders();
    
}

//simple comparison
function compare(el1, el2, index) {
    return el1[index] == el2[index] ? 0 : (el1[index] < el2[index] ? -1 : 1);
}

//date comparison
function datecompare(a, b, index, x, y, index2) {

    var retVal = new Date(a[index]).getTime() - new Date(b[index]).getTime();
    if (retVal == 0) {
        retVal = compare(a, b, 'Client_Short_Name');
    }
    return retVal;
}

//Ordered by client, then due date
function clientcompare(a, b, index) {

    var retVal = a[index] == b[index] ? 0 : (a[index] < b[index] ? -1 : 1);
    if (retVal == 0) {
        retVal = simpledatecompare(a,b,"Due_Date_String");
    }
    return retVal;
}

function simpledatecompare(a,b,index) {
    var retVal = new Date(a[index]).getTime() - new Date(b[index]).getTime();
    return retVal;
}


function duedatecompare(a, b, index) {
    return new Date(a[index]).getTime() - new Date(b[index]).getTime();
}


function showOrderDetails(order_id) {
    //displays order details on mouseover
    if ($("#order_popup").length == 0) {
        document.body.innerHTML += '<div id="order_popup"></div>';
    }

    var scrollDistance = $(window).scrollTop();
    $('#order_popup').css('top', scrollDistance + 80 + 'px');
    $('#order_popup').css('zIndex', 100);
    $('#order_popup').show();
    $('#order_popup').html('');

    $('#order_popup').load("../html/quick_view_foundation_order.html", function () {
        for (var i = 0; i < arrOrders.length; i++) {
            var item = arrOrders[i];
            
            if (item.Order_Id == order_id) {
                $('#vw_mlaw_number').html(item.MLAW_Number);
                $('#vw_client_name').html(item.Client_Short_Name);
                $('#vw_status').html(item.Order_Status_Desc);
                $('#vw_designer_comments').html(item.Designer_Comments);
                $('#vw_foundation_type').html(item.Foundation_Type);
                $('#vw_date_received').html(item.Received_Date_String);
                $('#vw_date_due').html(item.Due_Date_String);
                $('#vw_date_complete').html(item.Complete_Date_String);
                $('#vw_date_delivered').html(item.Delivered_Date_String);
                $('#vw_address').html(item.Address);
                $('#vw_subdivision').html(item.Subdivision_Name);
                $('#vw_lot').html(item.Lot);
                $('#vw_block').html(item.Block);
                $('#vw_section').html(item.Section);
                $('#vw_city').html(item.City);
                $('#vw_county').html(item.County);
                $('#vw_plan_number').html(item.Plan_Number);
                $('#vw_plan_name').html(item.Plan_Name);
                $('#vw_elevation').html(item.Elevation);
                $('#vw_garage').html(item.Garage);
                $('#vw_patio').html(item.Patio);
                $('#vw_masonry_sides').html(item.Masonry_Sides);
                $('#vw_comments').html(item.Comments);
                $('#vw_soils_data_source').html(item.Soils_Data_Source);
                $('#vw_slab_sq_ft').html(item.Slab_Square_Feet);
                $('#vw_pi').html(item.PI);
                $('#vw_fill_applied').html(item.Fill_Applied);
                $('#vw_slope').html(item.Slope);
                $('#vw_brg_cap').html(item.Brg_cap);
                $('#vw_em_ctr').html(quickNumberFormat(item.Em_ctr));
                $('#vw_em_edg').html(quickNumberFormat(item.Em_edg));
                $('#vw_ym_ctr').html(quickNumberFormat(item.Ym_ctr));
                $('#vw_ym_edg').html(quickNumberFormat(item.Ym_edg));

                var strURL = "../MiddleTier/Get_Order_History.aspx?Order_Id=" + item.Order_Id;
                $.ajax({
                    url: strURL,
                    type: 'get',
                    dataType: 'json',
                    success: function (data) {

                       
                        var MLAW_Number = "";
                        $.each(data, function (i) {
                            var thisItem = data[i];
                            if (MLAW_Number != thisItem.MLAW_Number) {
                                $('#quick_view_order_history').append("<div style='font-weight:600;margin-top:10px;'>" + thisItem.MLAW_Number + "</div>");
                                $('#quick_view_order_history').append("<div style='font-weight:400;margin-bottom:5px;'>" + thisItem.Comments + "</div>");
                                MLAW_Number = thisItem.MLAW_Number;
                            }
                           
                            $('#quick_view_order_history').append("<div><div style='float:left;width:120px;'>" + thisItem.Date_String + "</div><div style='float:left'>" + thisItem.Order_Status_Desc + "</div><div style='clear:both'></div></div>");

                        });

                        
                    }
                });
            }
        }
    });

}

function hideOrderDetails(order_id) {
    //toggles off the Order popup on mouseout
    $('#order_popup').html('');
    $('#order_popup').hide();

}


/* utility for working with date formats in Javascript*/
var SimpleDateFormat;

(function () {
    function isUndefined(obj) {
        return typeof obj == "undefined";
    }

    var regex = /('[^']*')|(G+|y+|M+|w+|W+|D+|d+|F+|E+|a+|H+|k+|K+|h+|m+|s+|S+|Z+)|([a-zA-Z]+)|([^a-zA-Z']+)/;
    var monthNames = ["January", "February", "March", "April", "May", "June",
		"July", "August", "September", "October", "November", "December"];
    var dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    var TEXT2 = 0, TEXT3 = 1, NUMBER = 2, YEAR = 3, MONTH = 4, TIMEZONE = 5;
    var types = {
        G: TEXT2,
        y: YEAR,
        M: MONTH,
        w: NUMBER,
        W: NUMBER,
        D: NUMBER,
        d: NUMBER,
        F: NUMBER,
        E: TEXT3,
        a: TEXT2,
        H: NUMBER,
        k: NUMBER,
        K: NUMBER,
        h: NUMBER,
        m: NUMBER,
        s: NUMBER,
        S: NUMBER,
        Z: TIMEZONE
    };
    var ONE_DAY = 24 * 60 * 60 * 1000;
    var ONE_WEEK = 7 * ONE_DAY;
    var DEFAULT_MINIMAL_DAYS_IN_FIRST_WEEK = 1;

    var newDateAtMidnight = function (year, month, day) {
        var d = new Date(year, month, day, 0, 0, 0);
        d.setMilliseconds(0);
        return d;
    }

    Date.prototype.getDifference = function (date) {
        return this.getTime() - date.getTime();
    };

    Date.prototype.isBefore = function (d) {
        return this.getTime() < d.getTime();
    };

    Date.prototype.getUTCTime = function () {
        return Date.UTC(this.getFullYear(), this.getMonth(), this.getDate(), this.getHours(), this.getMinutes(),
				this.getSeconds(), this.getMilliseconds());
    };

    Date.prototype.getTimeSince = function (d) {
        return this.getUTCTime() - d.getUTCTime();
    };

    Date.prototype.getPreviousSunday = function () {
        // Using midday avoids any possibility of DST messing things up
        var midday = new Date(this.getFullYear(), this.getMonth(), this.getDate(), 12, 0, 0);
        var previousSunday = new Date(midday.getTime() - this.getDay() * ONE_DAY);
        return newDateAtMidnight(previousSunday.getFullYear(), previousSunday.getMonth(),
				previousSunday.getDate());
    }

    Date.prototype.getWeekInYear = function (minimalDaysInFirstWeek) {
        if (isUndefined(this.minimalDaysInFirstWeek)) {
            minimalDaysInFirstWeek = DEFAULT_MINIMAL_DAYS_IN_FIRST_WEEK;
        }
        var previousSunday = this.getPreviousSunday();
        var startOfYear = newDateAtMidnight(this.getFullYear(), 0, 1);
        var numberOfSundays = previousSunday.isBefore(startOfYear) ?
			0 : 1 + Math.floor(previousSunday.getTimeSince(startOfYear) / ONE_WEEK);
        var numberOfDaysInFirstWeek = 7 - startOfYear.getDay();
        var weekInYear = numberOfSundays;
        if (numberOfDaysInFirstWeek < minimalDaysInFirstWeek) {
            weekInYear--;
        }
        return weekInYear;
    };

    Date.prototype.getWeekInMonth = function (minimalDaysInFirstWeek) {
        if (isUndefined(this.minimalDaysInFirstWeek)) {
            minimalDaysInFirstWeek = DEFAULT_MINIMAL_DAYS_IN_FIRST_WEEK;
        }
        var previousSunday = this.getPreviousSunday();
        var startOfMonth = newDateAtMidnight(this.getFullYear(), this.getMonth(), 1);
        var numberOfSundays = previousSunday.isBefore(startOfMonth) ?
			0 : 1 + Math.floor((previousSunday.getTimeSince(startOfMonth)) / ONE_WEEK);
        var numberOfDaysInFirstWeek = 7 - startOfMonth.getDay();
        var weekInMonth = numberOfSundays;
        if (numberOfDaysInFirstWeek >= minimalDaysInFirstWeek) {
            weekInMonth++;
        }
        return weekInMonth;
    };

    Date.prototype.getDayInYear = function () {
        var startOfYear = newDateAtMidnight(this.getFullYear(), 0, 1);
        return 1 + Math.floor(this.getTimeSince(startOfYear) / ONE_DAY);
    };

    /* ----------------------------------------------------------------- */

    SimpleDateFormat = function (formatString) {
        this.formatString = formatString;
    };

    /**
	 * Sets the minimum number of days in a week in order for that week to
	 * be considered as belonging to a particular month or year
	 */
    SimpleDateFormat.prototype.setMinimalDaysInFirstWeek = function (days) {
        this.minimalDaysInFirstWeek = days;
    };

    SimpleDateFormat.prototype.getMinimalDaysInFirstWeek = function (days) {
        return isUndefined(this.minimalDaysInFirstWeek) ?
            DEFAULT_MINIMAL_DAYS_IN_FIRST_WEEK : this.minimalDaysInFirstWeek;
    };

    SimpleDateFormat.prototype.format = function (date) {
        var formattedString = "";
        var result;

        var padWithZeroes = function (str, len) {
            while (str.length < len) {
                str = "0" + str;
            }
            return str;
        };

        var formatText = function (data, numberOfLetters, minLength) {
            return (numberOfLetters >= 4) ? data : data.substr(0, Math.max(minLength, numberOfLetters));
        };

        var formatNumber = function (data, numberOfLetters) {
            var dataString = "" + data;
            // Pad with 0s as necessary
            return padWithZeroes(dataString, numberOfLetters);
        };

        var searchString = this.formatString;
        while ((result = regex.exec(searchString))) {
            var matchedString = result[0];
            var quotedString = result[1];
            var patternLetters = result[2];
            var otherLetters = result[3];
            var otherCharacters = result[4];

            // If the pattern matched is quoted string, output the text between the quotes
            if (quotedString) {
                if (quotedString == "''") {
                    formattedString += "'";
                } else {
                    formattedString += quotedString.substring(1, quotedString.length - 1);
                }
            } else if (otherLetters) {
                // Swallow non-pattern letters by doing nothing here
            } else if (otherCharacters) {
                // Simply output other characters
                formattedString += otherCharacters;
            } else if (patternLetters) {
                // Replace pattern letters
                var patternLetter = patternLetters.charAt(0);
                var numberOfLetters = patternLetters.length;
                var rawData = "";
                switch (patternLetter) {
                    case "G":
                        rawData = "AD";
                        break;
                    case "y":
                        rawData = date.getFullYear();
                        break;
                    case "M":
                        rawData = date.getMonth();
                        break;
                    case "w":
                        rawData = date.getWeekInYear(this.getMinimalDaysInFirstWeek());
                        break;
                    case "W":
                        rawData = date.getWeekInMonth(this.getMinimalDaysInFirstWeek());
                        break;
                    case "D":
                        rawData = date.getDayInYear();
                        break;
                    case "d":
                        rawData = date.getDate();
                        break;
                    case "F":
                        rawData = 1 + Math.floor((date.getDate() - 1) / 7);
                        break;
                    case "E":
                        rawData = dayNames[date.getDay()];
                        break;
                    case "a":
                        rawData = (date.getHours() >= 12) ? "PM" : "AM";
                        break;
                    case "H":
                        rawData = date.getHours();
                        break;
                    case "k":
                        rawData = date.getHours() || 24;
                        break;
                    case "K":
                        rawData = date.getHours() % 12;
                        break;
                    case "h":
                        rawData = (date.getHours() % 12) || 12;
                        break;
                    case "m":
                        rawData = date.getMinutes();
                        break;
                    case "s":
                        rawData = date.getSeconds();
                        break;
                    case "S":
                        rawData = date.getMilliseconds();
                        break;
                    case "Z":
                        rawData = date.getTimezoneOffset(); // This is returns the number of minutes since GMT was this time.
                        break;
                }
                // Format the raw data depending on the type
                switch (types[patternLetter]) {
                    case TEXT2:
                        formattedString += formatText(rawData, numberOfLetters, 2);
                        break;
                    case TEXT3:
                        formattedString += formatText(rawData, numberOfLetters, 3);
                        break;
                    case NUMBER:
                        formattedString += formatNumber(rawData, numberOfLetters);
                        break;
                    case YEAR:
                        if (numberOfLetters <= 3) {
                            // Output a 2-digit year
                            var dataString = "" + rawData;
                            formattedString += dataString.substr(2, 2);
                        } else {
                            formattedString += formatNumber(rawData, numberOfLetters);
                        }
                        break;
                    case MONTH:
                        if (numberOfLetters >= 3) {
                            formattedString += formatText(monthNames[rawData], numberOfLetters, numberOfLetters);
                        } else {
                            // NB. Months returned by getMonth are zero-based
                            formattedString += formatNumber(rawData + 1, numberOfLetters);
                        }
                        break;
                    case TIMEZONE:
                        var isPositive = (rawData > 0);
                        // The following line looks like a mistake but isn't
                        // because of the way getTimezoneOffset measures.
                        var prefix = isPositive ? "-" : "+";
                        var absData = Math.abs(rawData);

                        // Hours
                        var hours = "" + Math.floor(absData / 60);
                        hours = padWithZeroes(hours, 2);
                        // Minutes
                        var minutes = "" + (absData % 60);
                        minutes = padWithZeroes(minutes, 2);

                        formattedString += prefix + hours + minutes;
                        break;
                }
            }
            searchString = searchString.substr(result.index + result[0].length);
        }
        return formattedString;
    };
})();


function quickNumberFormat(num) {
    
    var temp = 0;
    if (num != null) {
        if (num % 1 == 0) {
            temp = num.toFixed(1);
        } else {
            temp = num;
        }
    }
    return (temp);

}


function getAccountingOrders(begin_date, end_date) {
    //Gets the list of accouting orders
    displayOption = -1;
    end_date = addDays(end_date, 1);
    var strStartDate = (begin_date.getMonth() + 1) + "/" + begin_date.getDate() + "/" + begin_date.getFullYear();
    var strEndDate = (end_date.getMonth() + 1) + "/" + end_date.getDate() + "/" + end_date.getFullYear();

    $('#flt_begin_date').val(strStartDate);
    $('#flt_end_date').val(strEndDate);
   
    $('#flt_date_type').val(4);


    var strURL = "../MiddleTier/Get_Order_Slice.aspx?Statuses=10&Date_Type=4&Start_Date=" + strStartDate + "&End_Date=" + strEndDate;
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {

            arrOrders = data;
            resort_orders("Delivered_Date");
            renderOrders();
        }
    });
}

var arrInspectionTypes;
function loadInspectionTypeList()
{
    //loads the list of Inspection Types
    var strURL = "../MiddleTier/Get_All_Inspection_Types.aspx";

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrInspectionTypes = data;
            $.each(data, function (i) {
                $('#inspection_type_list').append('<div><div style="width:200px;float:left" onclick="editCompletionType(' + data[i].Inspection_Type_Id + ');" class="lst_clickable">' + data[i].Inspection_Type + '</div><div style="width:100px;float:left">' + data[i].Completion_Time + '</div><div style="clear:both"></div></div>');
                $('#ins_edit_type_list').append('<div><div style=""><input type="checkbox" value="' + data[i].Inspection_Type_Id + '" > &nbsp;&nbsp;' + data[i].Inspection_Type + '</div></div>');
            });
        }
    });
}

function editCompletionType(id) {
    //Allows for the changing of inspection types
    $.each(arrInspectionTypes, function (i) {
        if (arrInspectionTypes[i].Inspection_Type_Id == id) {
            $('#edit_inspection_type').show();
            $('#manage_inspection_types').hide();

            $('#edit_inspection_type_name').html(arrInspectionTypes[i].Inspection_Type);
            $('#edit_inspection_completion_time').val(arrInspectionTypes[i].Completion_Time);
            $('#edit_inspection_type_id').val(arrInspectionTypes[i].Inspection_Type_Id);

        }
    });
    
}

function submitEditInspectionType() {
    //submits any changes
    var iCompletionTime = $('#edit_inspection_completion_time').val();
    if (iCompletionTime.trim() == "") {
        alert("Please enter the amount of time needed complete this type of inspectoin");

    } else {
        var iInspection_Type_Id = $('#edit_inspection_type_id').val();
        var strURL = "Update_Inspection_Type.aspx?Inspection_Type_Id=" + iInspection_Type_Id + "&Completion_Time=" + iCompletionTime;

        $.ajax({
            url: strURL,
            type: 'get',
            dataType: 'text',
            success: function (data) {
                $('#inspection_type_list').html('');
                loadInspectionTypeList();
                $('#edit_inspection_type').hide();
                $('#manage_inspection_types').show();
            },
            error: function (e) {
                console.log(e.message);
            }
        });

    }

}

function addInspector() {

    $('#inspector_form').show();
    $('#inspector_list').hide();
    $('#ins_edit_type_list').find('input[type=checkbox]').prop("checked", false);
    $('#ins_edit_first_name').val('');
    $('#ins_edit_last_name').val('');
    $('#ins_edit_email').val('');
    $('#ins_edit_password').val('');
    $('#ins_edit_lat').val('');
    $('#ins_edit_long').val('');
    $('#inspector_id').val(0);

}

function loadInspectors() {
    //loads the list of all inspectors
    $('#inspector_list').html('');
    var strURL = "Get_All_Inspectors.aspx";
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            
            $.each(data, function (i) {
                $('#inspector_list').append("<div class='lst_clickable' onclick='editInspector(" + data[i].User_Id + ")'>" + data[i].First_Name + " " + data[i].Last_Name + "</div><div style='clear:both'></div>");
            });

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}

function editInspector(id) {
    //Edits an inspector
    $('#ins_edit_type_list').find('input[type=checkbox]').prop("checked", false);
    $('#inspector_form').show();
    $('#inspector_list').hide();

    var strURL = "Get_Inspector_Info.aspx?Inspector_Id=" + id;
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            console.log(JSON.stringify(data));

            if (data[0].Active == 1) {
                $('#ins_edit_active').prop("checked", true);
            } else {
                $('#ins_edit_active').prop("checked", false);
            }

            $('#ins_edit_first_name').val(data[0].First_Name);
            $('#ins_edit_last_name').val(data[0].Last_Name);
            $('#ins_edit_email').val(data[0].Email);
            $('#ins_edit_password').val(data[0].Password);

            var strGeo = data[0].Inspector_Home_Geolocation;
            var arrGeo = strGeo.split(",");
            $('#ins_edit_lat').val(arrGeo[0].trim());
            $('#ins_edit_long').val(arrGeo[1].trim());

            $('#inspector_id').val(id);
        },
        error: function (e) {
            console.log(e.message);
        }
    });

    var strURL = "Get_Inspector_Inspection_Types.aspx?Inspector_Id=" + id;
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            console.log(JSON.stringify(data));
            $.each(data, function(i)
            {
                $("#ins_edit_type_list input:checkbox[value=" + data[i].Inspection_Type_Id + "]").prop("checked", true);

                var selected = [];
                $('#ins_edit_type_list input:checked').each(function () {
                    selected.push($(this).val());
                });
            });

        },
        error: function (e) {
            console.log(e.message);
        }
    });
}


function cancelEmpAdmin() {
    //toggles the inspector form on/off
    $('#inspector_form').hide();
    $('#inspector_list').show();

}


function getAccountingSlice() {
    //gets a list of orders for accounting. They have some different requirements
    if (displayOption == -1) {
        doOrderSlice(4);
    } else {
        doOrderSlice(5);
    }
}


var arrAdminClients;

function loadClientPricing() {
    //loads the pricing information for a client
    var strURL = "../MiddleTier/Get_Clients.aspx";
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrAdminClients = data;
            renderAdminClients();

        },
        error: function (e) {
            console.log(e.message);
        }
    });

}

function displayItem(value) {
    return (value == null) ? "&nbsp;" : value
}

function displayNumericItem(value) {
    return (value == null) ? "" : value
}

function calcFoundationPricing() {
    //calculates foundation pricing on the fly for an order at entry time
    var client_id = $('#ord_company').val();
    var sqft = $('#ord_slab_sq_ft').val();

    if (sqft == '') {
        sqft = 0;
    }

    var strURL = "../MiddleTier/Get_Foundation_Order_Price.aspx?Client_Id=" + client_id + "&Sq_Ft=" + sqft;

    $('#lbl_ord_price').html('');
    $('#ord_price').val('');

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            if (data[0].Amount != -1) {
                $('#lbl_ord_price').html("$" + data[0].Amount);
                $('#ord_price').val(data[0].Amount);
            }
        },
        error: function (e) {
            console.log(e.message);
        }
    });


}


function getRevisionPrice() {

    //get revision pricing on the fly

    var client_id = $('#ord_client_id').val();

    if (client_id == 0 || client_id == '')
    {
        client_id = $('#ord_company').val();
    }
 
    strURL = "../MiddleTier/Get_Client_Foundation_Revision_Pricing.aspx?Client_Id=" + client_id;

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var item = data[0];
            if ($('#is_new_home').is(':checked') == false) {
                $('#Revision_Price').html('$' + item.Base);
                $('#ord_rev_price').val(item.Base);
            } else {
                $('#Revision_Price').html('$' + item.New_House);
                $('#ord_rev_price').val(item.New_House);

            }
        }
    });
    
}

function toggleFoundationOrderType()
{
    //show hide foundation order types
    var type = $("input[name=ord_order_type]:checked").val();

    if(type == 1)
    {
        $('#Division_Selection').show();
        $('#lbl_ord_800_price').show();
        $('#lbl_ord_price').hide();

    } else {
        $('#Division_Selection').hide();
        $('#lbl_ord_800_price').hide();
        $('#lbl_ord_price').show();
    }

}

function create_subdivision_window() {
    //popups the add subdivion window on the order entry screen
    $('#grey_out').show();
    $('#create_subdivision_popup').show();
}

function hide_subdivision_popup() {
    $('#grey_out').hide();
    $('#create_subdivision_popup').hide();
}

function createNewSubdivision() {
    //adds a new subdivion
    var url = "../MiddleTier/Insert_Subdivision.aspx?Client_Id=" + $('#new_sub_client').val() + "&Division_Id=" + $('#new_sub_division').val() + "&Division_Name=" + $('#new_sub_name').val() + "&Subdivision_Number=" + $('#new_sub_number').val();
    console.log(url);

    $.ajax({
        url: url,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            
            $('#ord_subdivision_id').val(JSON.stringify(data));
            $('#ord_subdivision_holder').text($('#new_sub_name').val());

            $('#new_sub_client').val(0);
            $('#new_sub_division').val(0);
            $('#new_sub_name').val('');
            $('#new_sub_number').val('');

            $('#grey_out').hide();
            $('#create_subdivision_popup').hide();
        },
        error: function (e) {
            console.log(e.message);
        }
    });
}