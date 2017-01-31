

function acctAdminClient(client_id) {
    $('#acct_client_id').val(client_id);
    var strURL = "../Get_Client_By_Id.aspx?Client_Id=" + client_id;
    $('#inspection_pricing').html('');

    var stateObj = { id: "updateclient" };
    history.pushState(stateObj, "updateclient", client_id + ".html");

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var item = data[0];
            $('#admin_client_vendor_number').val(item.Vendor_Number);
            $('#admin_client_short_name').val(item.Client_Short_Name);
            $('#admin_client_full_name').val(item.Client_Full_Name);
            $('#admin_client_address1').val(item.Billing_Address_1);
            $('#admin_client_address2').val(item.Billing_Address_2);
            $('#admin_client_city').val(item.Billing_City);
            $('#admin_client_state').val(item.Billing_State);
            $('#admin_client_postal_code').val(item.Billing_Postal_Code);
            $('#admin_client_attn').val(item.Attn);
            $('#admin_client_phone').val(item.Primary_Phone);
            $('#admin_client_fax').val(item.Primary_Fax);
            if (item.Requires_VPO == 1) {
                $('#vpo').attr('checked', true);
            } else {
                $('#vpo').attr('checked', false);
            }

            $('#admin_client_list_holder').hide();
            $('#admin_client_form').show();
        }
    });

    strURL = "../Get_Client_Inspection_Pricing.aspx?Client_Id=" + client_id;

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (i) {
                var item = data[i];
                var strHTML = "<div class='frm_row insp_price' id='insp_price_" + item.Inspection_Type_Id + "' >";
                strHTML = strHTML + "<div class='frm_label' style='width:160px;'>" + item.Inspection_Type + ":</div>";
                strHTML = strHTML + "<div class='frm_input' style='width:220px;'><input type='text' id='insp_amt_" + item.Inspection_Type_Id + "' value='" + displayNumericItem(item.Amount) + "'></div>";
                strHTML = strHTML + "<div class='frm_label' style='width:160px;'>Re-Inspection:</div>";
                strHTML = strHTML + "<div class='frm_input' style='width:160px;'><input type='text' id='reinsp_amt_" + item.Inspection_Type_Id + "' value='" + displayNumericItem(item.ReInspAmount) + "'></div>";
                strHTML = strHTML + "<div style='clear:both'></div>";
                strHTML = strHTML + "</div>";
                $('#inspection_pricing').append(strHTML);
            });

        }
    });

    strURL = "../Get_Client_Foundation_Pricing.aspx?Client_Id=" + client_id;

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {

            $.each(data, function (i) {
                var item = data[i];

                $('input:radio[name=pay_tier_' + item.Pricing_Tier + '][value=' + item.Pricing_Type + ']').prop('checked', true);
                $('#admin_pay_tier_' + item.Pricing_Tier).val(item.Amount);
                $('#admin_threshold_' + item.Pricing_Tier).val(item.Sq_Ft_Threshold);
            });

        }
    });

    strURL = "../Get_Client_Foundation_Revision_Pricing.aspx?Client_Id=" + client_id;

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {

            $.each(data, function (i) {
                var item = data[i];
                $('#rev_base').val(item.Base);
                $('#rev_new_house').val(item.New_House);
            });

        }
    });
}

function updateAcctClient() {
    var client_id = $('#acct_client_id').val();
    var client_short_name = $('#admin_client_short_name').val();
    var client_full_name = $('#admin_client_full_name').val();
    var billing_address_1 = $('#admin_client_address1').val();
    var billing_address_2 = $('#admin_client_address2').val();
    var city = $('#admin_client_city').val();
    var state = $('#admin_client_state').val();
    var postal_code = $('#admin_client_postal_code').val();
    var attn = $('#admin_client_attn').val();
    var phone = $('#admin_client_phone').val();
    var fax = $('#admin_client_fax').val();
    var vendor_number = $('#admin_vendor_number').val();
    var requires_vpo = $('#vpo').is(':checked');
    if (requires_vpo == true) {
        requires_vpo = 1;
    } else {
        requires_vpo = 0;
    }

    strURL = "../Update_Client.aspx?Client_Id=" + client_id + "&Client_Short_Name=" + client_short_name + "&Client_Full_Name=" + encodeURIComponent(client_full_name) + "&Billing_Address_1=" + encodeURIComponent(billing_address_1) + "&Billing_Address_2=" + encodeURIComponent(billing_address_2) + "&Billing_City=" + city + "&Billing_State=" + state + "&Billing_Zip=" + postal_code + "&Phone=" + phone + "&Fax=" + fax + "&Attn=" + attn + "&Vendor_Number=" + vendor_number + "&VPO=" + requires_vpo;
    console.log(strURL);
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'text',
        success: function (data) {

        }
    });

    strURL = "Delete_Client_Foundation_Pricing.aspx?Client_Id=" + client_id;
    console.log(strURL);
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'text',
        success: function (data) {

            for (var i = 1; i < 4; i++) {
                var paytier = i;
                var paytype = $('input:radio[name=pay_tier_' + i + ']').filter(":checked").val();
                var amount = $('#admin_pay_tier_' + i).val();
                var threshold = 0;
                if (i != 3) {
                    threshold = $('#admin_threshold_' + i).val();
                }

                strURL = "Insert_Client_Foundation_Pricing.aspx?Client_Id=" + client_id + "&Pricing_Type=" + paytype + "&Amount=" + amount + "&Pricing_Tier=" + paytier + "&Threshold=" + threshold;

                $.ajax({
                    url: strURL,
                    type: 'get',
                    dataType: 'text',
                    success: function (data) {
                    }
                });
            }

        }
    });

    strURL = "Delete_Client_Inspection_Pricing.aspx?Client_Id=" + client_id;
    console.log(strURL);
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'text',
        success: function (data) {

            $('.insp_price').each(function (j, element) {

                var id = $(element).attr('id');

                var inspectionid = id.replace('insp_price_', '');

                var amount = $('#insp_amt_' + inspectionid).val();
                var reinspAmount = $('#reinsp_amt_' + inspectionid).val();


                if (amount != "") {
                    strURL = "Insert_Client_Inspection_Pricing.aspx?Client_Id=" + client_id + "&Inspection_Type_Id=" + inspectionid + "&Amount=" + amount + "&ReinspAmount=" + reinspAmount;
                    console.log(strURL);
                    $.ajax({
                        url: strURL,
                        type: 'get',
                        dataType: 'text',
                        success: function (data) {
                        }
                    });
                }
            });
        }
    });

    var baseRev = $('#rev_base').val();
    var newHouseRev = $('#rev_new_house').val();

    strURL = "../Update_Client_Foundation_Revision_Pricing.aspx?Client_Id=" + client_id + "&Base=" + baseRev + "&New_Home=" + newHouseRev;

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'text',
        success: function (data) {
        }
    });

    history.back(1);

    $('#admin_client_form').hide();
    $('#admin_client_list_holder').show();

}

function resort_clients(id) {
    $('#client_list_header div').removeClass('header_button_on');
    $('#client_list_title_' + id).addClass('header_button_on');

    arrAdminClients.sort(function (el1, el2) {
        return compare(el1, el2, id)
    });

    renderAdminClients();
}




function renderAdminClients() {
    var strHTML = "";
    $('#admin_client_list').html('');
    $.each(arrAdminClients, function (i) {
        var item = arrAdminClients[i];
        strHTML = "<div style='text-align:left;margin-top:3px;'>";
        strHTML = strHTML + "<div style='float:left;width:100px;margin-right:10px;' class='lst_clickable' onclick='acctAdminClient(" + item.Client_Id + ")'>" + displayItem(item.Client_Short_Name) + "</div>";
        strHTML = strHTML + "<div style='float:left;width:200px;margin-right:10px;'>" + displayItem(item.Client_Full_Name) + "</div>";
        strHTML = strHTML + "<div style='float:left;width:200px;margin-right:10px;'>" + displayItem(item.Billing_Address_1) + "</div>";
        
        if (item.Billing_Address_2 == null || item.Billing_Address_2 == '') {
            strHTML = strHTML + "<div style='float:left;width:110px;margin-right:10px;'>&nbsp;</div>";
        } else {
            strHTML = strHTML + "<div style='float:left;width:110px;margin-right:10px;'>" + displayItem(item.Billing_Address_2) + "</div>";
        }
        strHTML = strHTML + "<div style='float:left;width:100px;margin-right:10px;'>" + displayItem(item.Billing_City) + "</div>";
        strHTML = strHTML + "<div style='float:left;width:80px;margin-right:10px;'>" + displayItem(item.Billing_State) + "</div>";
        strHTML = strHTML + "<div style='float:left;width:100px;margin-right:10px;'>" + displayItem(item.Billing_Postal_Code) + "</div>";
        strHTML = strHTML + "<div style='clear:both'></div>";
        strHTML = strHTML + "</div>";
        $('#admin_client_list').append(strHTML);
    });

}

var arrWarrantyOrders;
function getWarrantyOrders() {

    var strStartDate = $('#flt_begin_date').val();
    var strEndDate = $('#flt_end_date').val();

    var strURL = "../Get_Warranty_Order_Slice.aspx?Start_Date=" + strStartDate + "&End_Date=" + strEndDate;
    console.log(strURL);
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {

            arrWarrantyOrders = data;
            resort_warranty_orders('Order_Date');

        }
    });
}


function resort_warranty_orders(id) {
    $('#order_list_titles div').removeClass('header_button_on');
    $('#order_list_title_' + id).addClass('header_button_on');

    if (id == 'Order_Date') {
        arrWarrantyOrders.sort(function (el1, el2) {
            return duedatecompare(el1, el2, 'Order_Date_String');
        });
    } else {
        arrWarrantyOrders.sort(function (el1, el2) {
            return compare(el1, el2, id)
        });
    }

    renderWarrantyOrders();
}


function renderWarrantyOrders() {
    $('#warranty_order_grid').html('');

    if ($('#flt_client').length != 0) {
        filterClient = $('#flt_client').val();
        filterDivision = $('#flt_division').val();
        filterStatus = $('#flt_status').val();
        filterSubdivision = $('#flt_subdivision').val();
        filterAddress = $('#flt_address').val();
    } else {
        filterClient = "";
        filterDivision = "";
        filterStatus = "";
        filterSubdivision = "";
        filterAddress = "";
    }

    $.each(arrWarrantyOrders, function (i) {
        var item = arrWarrantyOrders[i];

        if ((filterClient == "" || item.Client_Short_Name.toLowerCase().indexOf(filterClient.toLowerCase()) != -1)
            && (filterDivision == "" || item.Division_Desc.toLowerCase().indexOf(filterDivision.toLowerCase()) != -1)
            && (filterStatus == "" || item.Order_Status_Desc.toLowerCase().indexOf(filterStatus.toLowerCase()) != -1)
            && (filterSubdivision == "" || item.Subdivision_Name.toLowerCase().indexOf(filterSubdivision.toLowerCase()) != -1)
            && (filterAddress == "" || item.Address.toLowerCase().indexOf(filterAddress.toLowerCase()) != -1)
        ) {

            var dtOrder = item.Order_Date_String;
            var dt = new Date(dtOrder);
            dt = new Date(dt.getTime() + (dt.getTimezoneOffset() * 60000));

            var strHTML = "<div style='margin-top:5px;'>";
            strHTML = strHTML + "<div style='float:left;width:150px;margin-left:20px;font-weight:600;' class='lst_clickable' onclick='editWarrantyOrder(" + item.Warranty_Order_Id + ");'>" + item.MLAW_Number + "</div>";
            strHTML = strHTML + "<div class='lst_item' style='width:104px;'>" + item.Client_Short_Name + " </div>";
            strHTML = strHTML + "<div class='lst_item' style='width:160px;'>" + item.Subdivision_Name + " </div>";
            strHTML = strHTML + "<div class='lst_item' style='width:260px;'>" + item.Address + "</div>";
            strHTML = strHTML + "<div class='lst_item' style='width:400px;'>" + item.Warranty_Notes + "</div>";
            strHTML = strHTML + "<div class='lst_item' style='width:100px;'>" + (dt.getMonth() + 1) + '/' + dt.getUTCDate() + '/' + dt.getFullYear() + "</div>";
            strHTML = strHTML + "<div style='clear:both'></div>";
            strHTML = strHTML + "</div>";
            $('#warranty_order_grid').append(strHTML);
        }

    });

}

function editWarrantyOrder(order_id) {
    $('#warranty_form').show();
    $('#warranty_order_list').hide();
    $('#war_order_id').val(order_id);

    for (var i = 0; i < arrWarrantyOrders.length; i++) {
        var item = arrWarrantyOrders[i];
        if (parseInt(item.Warranty_Order_Id) == parseInt(order_id)) {
            $('#mlaw_row').show();
            $('#war_mlaw_number').html(item.MLAW_Number);
            $('#war_address').val(item.Address);
            $('#ord_war_order_id').val(item.Order_Id);
            $('#war_notes').val(item.Warranty_Notes);
        }
    }
}


function doWarAddressMatches() {
    var val = $('#war_address').val();
    if (val.length == 5) {

        var strURL = '../Find_MLAW_Number.aspx?Address=' + val + '&Lot=&Block=';
        $.ajax({
            url: strURL,
            type: 'get',
            dataType: 'json',
            success: function (data) {
                arrMatches = data;

                $.each(data, function (i) {
                    var item = data[i];
                    var address = item.Address + ", Lot " + item.Lot + ", Block " + item.Block;

                    if (address.toLowerCase().indexOf(val.toLowerCase()) > -1) {
                        $('#war_address_matches').append("<input type='radio' name='ord_rev_match' value='" + data[i].Order_Id + "' onclick='setWarOrderId(" + item.Order_Id + ")'><b>" + data[i].MLAW_Number + "</b> - " + data[i].Address + ", Lot " + data[i].Lot + ", Block " + data[i].Block + ", " + data[i].City + ", TX<br>");
                    }
                });

            }
        });

    } else {
        if (arrMatches) {
            if (val.length != 5) {
                $('#war_address_matches').html('');

                $.each(arrMatches, function (i) {
                    var item = arrMatches[i];
                    var address = item.Address + ", Lot " + item.Lot + ", Block " + item.Block;

                    if (address.toLowerCase().indexOf(val.toLowerCase()) > -1) {
                        $('#war_address_matches').append("<input type='radio' name='ord_ins_rev_match' value='" + item.Order_Id + "' onclick='setWarOrderId(" + item.Order_Id + ")'><b>" + item.MLAW_Number + "</b> - " + item.Address + ", Lot " + item.Lot + ", Block " + item.Block + ", " + item.City + ", TX<br>");
                    }
                });
            }

            if (val.length == 0) {
                $('#war_address_matches').html('');
                arrMatches = null;

            }
        }
    }
}


function setWarOrderId(order_id) {
    $('#ord_war_order_id').val(order_id);
}

function handleWarrantyOrder() {
    var war_order_id = $('#war_order_id').val();
    var order_id = $('#ord_war_order_id').val();
    var war_notes = $('#war_notes').val();


    if (order_id == "") {
        alert("Please choose an address from the list of matches");
    }
    var strURL = "";

    if (war_order_id == "") {
        strURL = "Insert_Warranty_Order.aspx?Order_Id=" + order_id + "&Warranty_Notes=" + encodeURIComponent(war_notes);
    } else {
        strURL = "Update_Warranty_Order.aspx?Warranty_Order_Id=" + war_order_id + "&Order_Id=" + order_id + "&Warranty_Notes=" + encodeURIComponent(war_notes);
    }
    console.log(strURL);
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {

            getWarrantyOrders();
            $('#warranty_form').hide();
            $('#warranty_order_list').show();
        }
    });
}

function toggleWarrantyEntry() {
    $('#mlaw_row').hide();
    $('#war_mlaw_number').html('');
    $('#war_address').val('');
    $('#war_notes').val('');
    $('#war_order_id').val('');

    $('#warranty_form').show();
    $('#warranty_order_list').hide();


}


function accountingExcelList(delivery_type) {

    var flt_Begin_Date = $('#flt_begin_date').val();
    var flt_End_Date = $('#flt_end_date').val();

    if (delivery_type == 1) {
        if (displayOption == 1) {
            delivery_type = 0;
        }
    }

    var url = window.location.href;

    var strURL = "output_excel_accounting.aspx?Delivery_Type=" + delivery_type;
    strURL = strURL + "&Start_Date=" + flt_Begin_Date;
    strURL = strURL + "&End_Date=" + flt_End_Date;

    console.log(strURL);

    /*EXCEL AJAX DOWNLOAD HACK*/

    var $idown;  // Keep it outside of the function, so it's initialized once.

    if ($idown) {
        $idown.attr('src', strURL);
    } else {
        $idown = $('<iframe>', { id: 'idown', src: strURL }).hide().appendTo('body');
    }
}


function doAcctNew() {
    $('#header_filters .header_button').removeClass('header_button_on');
    $('#flt_new').addClass('header_button_on');

    var dToday = new Date();
    var lastWeek = addDays(dToday, -7);
    var yesterday = addDays(dToday, -1);
    var daybefore = addDays(dToday, -2);
    var tomorrow = addDays(dToday, 1);

    $('#foundation_order_list').show();

    var strStartDate = (lastWeek.getMonth() + 1) + "/" + lastWeek.getDate() + "/" + lastWeek.getFullYear();
    var strEndDate = (dToday.getMonth() + 1) + "/" + dToday.getDate() + "/" + dToday.getFullYear();

    $('#flt_date_type').val(1);
    $('#flt_begin_date').val(strStartDate);
    $('#flt_end_date').val(strEndDate);
    $('#flt_status').val('');

    doOrderSlice(5);
    $('#btn_update_list').on("click", function () {
        doOrderSlice(5);
    });

}

function doAcctEverything() {
    $('#header_filters .header_button').removeClass('header_button_on');
    $('#flt_everything').addClass('header_button_on');

    var dToday = new Date();
    var lastWeek = addDays(dToday, -7);
    var yesterday = addDays(dToday, -1);
    var daybefore = addDays(dToday, -2);
    var tomorrow = addDays(dToday, 1);

    $('#foundation_order_list').show();

    var strStartDate = (lastWeek.getMonth() + 1) + "/" + lastWeek.getDate() + "/" + lastWeek.getFullYear();
    var strEndDate = (dToday.getMonth() + 1) + "/" + dToday.getDate() + "/" + dToday.getFullYear();


    $('#flt_date_type').val(1);
    $('#flt_begin_date').val(strStartDate);
    $('#flt_end_date').val(strEndDate);
    $('#flt_status').val('');
    
    doOrderSlice(6);

    $('#btn_update_list').off();
    $('#btn_update_list').on("click", function () {
        doOrderSlice(6);
    });

}

function doAcctReady() {

  
    $('#header_filters .header_button').removeClass('header_button_on');
    $('#flt_ready').addClass('header_button_on');

    var dToday = new Date();
    var lastWeek = addDays(dToday, -7);
    var yesterday = addDays(dToday, -1);
    var daybefore = addDays(dToday, -2);
    var tomorrow = addDays(dToday, 1);

    $('#foundation_order_list').show();

    var strStartDate = (lastWeek.getMonth() + 1) + "/" + lastWeek.getDate() + "/" + lastWeek.getFullYear();
    var strEndDate = (tomorrow.getMonth() + 1) + "/" + tomorrow.getDate() + "/" + tomorrow.getFullYear();

    $('#flt_date_type').val(5);
    $('#flt_begin_date').val(strStartDate);
    $('#flt_end_date').val(strEndDate);
    $('#flt_status').val('');

    displayOption = 0;

    doOrderSlice(4);

    $('#btn_update_list').off();
    $('#btn_update_list').on("click", function () {
        doOrderSlice(4);
    });

}