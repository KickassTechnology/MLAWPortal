
var arrMatches;
var arrInspectors;
var arrSchedule;
var arrInspectionOrders;

function doInsAddressMatches() {
    var val = $('#ord_ins_address').val();
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
                        $('#ord_ins_address_matches').append("<input type='radio' name='ord_rev_match' value='" + data[i].Order_Id + "' onclick='setOrderId(" + item.Order_Id + ")'><b>" + data[i].MLAW_Number + "</b> - " + data[i].Address + ", Lot " + data[i].Lot + ", Block " + data[i].Block + ", " + data[i].City + ", TX<br>");
                    }
                });

            }
        });

    } else {
        if (arrMatches) {
            if (val.length != 5) {
                $('#ord_ins_address_matches').html('');

                $.each(arrMatches, function (i) {
                    var item = arrMatches[i];
                    var address = item.Address + ", Lot " + item.Lot + ", Block " + item.Block;

                    if (address.toLowerCase().indexOf(val.toLowerCase()) > -1) {
                        $('#ord_ins_address_matches').append("<input type='radio' name='ord_ins_rev_match' value='" + item.Order_Id + "' onclick='setOrderId(" +  item.Order_Id  + ")'><b>" + item.MLAW_Number + "</b> - " + item.Address + ", Lot " + item.Lot + ", Block " + item.Block + ", " + item.City + ", TX<br>");
                    }
                });
            }

            if (val.length == 0) {
                $('#ord_ins_address_matches').html('');
                arrMatches = null;

            }
        }
    }
}

function setOrderId(order_id) {
    $('#ord_ins_order_id').val(order_id);
}

function getInspectionTypes() {
    var strURL = '../Get_Inspection_Types.aspx';
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            
            $.each(data, function (i) {
                $('#ord_ins_inspection_type').append($('<option/>', {
                    value: data[i].Inspection_Type_Id,
                    text: data[i].Inspection_Type
                }));

                $('#ord_edit_inspection_type').append($('<option/>', {
                    value: data[i].Inspection_Type_Id,
                    text: data[i].Inspection_Type
                }));

            });
            $('#ord_ins_inspection_type').append($('<option/>', {
                value: 0,
                text: "Other / Please Explain"
            }));



        }
    });
}

function createInspectionOrder() {
    var bSubmit = true;

    var strOrderId = $('#ord_ins_order_id').val();
    var strInspectionType = $('#ord_ins_inspection_type').val();
    var strIsReInspection = $('input:radio[name=ord_ins_re_inspection]:checked').val();
    var strIsMultiPour = $('input:radio[name=ord_ins_multi_pour]:checked').val();
    var strTimePref = $('input:radio[name=ord_ins_time]:checked').val();
    var strPhone = $('#ord_ins_phone').val();
    var strCanText = $('input:radio[name=ord_ins_can_text]:checked').val();
    var strSpecialNotes = $('#ord_ins_special_notes').val();
    var strEmail = $('#ord_ins_email').val();

    if (strOrderId == "") {
        alert("Please choose and MLAW order from the list of address matches");
        bSubmit = false;
    }

    if (strPhone == "") {
        alert("Please enter a Phone Number");
        bSubmit = false;
    }

    if (bSubmit == true) {
        var url = "Insert_Inspection_Order.aspx?Order_Id=" + strOrderId + "&Inspection_Type=" + strInspectionType + "&IsReInspection=" + strIsReInspection + "&IsMultiPour=" + strIsMultiPour + "&Phone=" + strPhone + "&Can_Text=" + strCanText + "&SpecialNotes=" + encodeURIComponent(strSpecialNotes) + "&Email=" + strEmail + "&TimePref=" + strTimePref;
        
        $.ajax({
            url: url,
            type: 'get',
            dataType: 'text',
            success: function (data) {
                loadInspectionOrders();
                toggleInspectionView('insp_order_grid');
            }
        });

    }
}

function checkOther() {
    var test = $('#ord_ins_inspection_type').val();

    if (test == 0) {
        $('#other_row').show();
    } else {
        $('#other_row').hide();

    }
}

function loadInspectorList() {

    $('#inspector_list').html('');
    var strURL = "Get_All_Inspectors.aspx";
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrInspectors = data;
        },
        error: function (e) {
            console.log(e.message);
        }
    });
}

function getDailySchedule(startDate) {
    var strURL = "Get_Daily_Inspection_Schedule.aspx?StartDate=" + startDate;
 
    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            
            arrSchedule = data;
            
            var iCount = 0;
            var curInspector = "";
            var top = 0;
            var mapCoords = new Array();
            mapData = {};
            var inspectorGeo = "";

            $.each(data, function (i) {
                var item = data[i];
                var left = iCount * 150;
                var strName = item.First_Name + " " + item.Last_Name;
                
                

                if (curInspector != strName) {
                    if (curInspector != "") {
                        top = top + 210;
                        mapCoords.unshift(inspectorGeo);
                        mapCoords.push(inspectorGeo);
                        mapData[curInspector] = mapCoords.slice(0);
                    }

                    iCount = 0;
                    left = 0;
                    var nameTop = top + 100;
                    var nameLeft = left - 110;
                    $('#insp_calendar').append("<div style='width:204px;color:#ffffff;transform:rotate(90deg);text-align:center;position:absolute;left:" + nameLeft + "px;top:" + nameTop + "px;background:#000000;'>" + strName + "</div>");
                    mapCoords.length = 0;
                    mapCoords.push(item.Address_Geocode);
                    curInspector = strName;
                    inspectorGeo = item.Inspector_Home_Geolocation;
                }

                var strHTML = '<div class="schedule_event" id="event_' + item.Schedule_Id + '" onclick="manageScheduleItem(this);" style="left:' + left + 'px;top:' + top + 'px;">';
                strHTML = strHTML +'<div class="schedule_address">' + item.Est_Date_String + "<br>" + item.Address + '</div>';
                strHTML = strHTML + '<div class="schedule_type">' + item.Inspection_Type + '</div>';
                strHTML = strHTML + '<div class="schedule_details">' + item.Client_Short_Name + '</div>';
                strHTML = strHTML + '<div class="schedule_move" style="bottom:18px;">move</div>';
                strHTML = strHTML + '<div class="schedule_move">re-assign</div>';
                strHTML = strHTML + '</div>';

                $('#insp_calendar').append(strHTML);
                mapCoords.push(item.Address_Geocode);
                
                iCount = iCount + 1;

            });

            mapCoords.unshift(inspectorGeo);
            mapCoords.push(inspectorGeo);
            mapData[curInspector] = mapCoords;

            console.log(JSON.stringify(mapData));

        },
        error: function (e) {
            console.log(e.message);
        }
    });

}

function submitInsEditForm() {
    var Active = 0;
    if ($('#ins_edit_active').is(':checked')) {
        Active = 1;
    };

    var FirstName = $('#ins_edit_first_name').val();
    var LastName = $('#ins_edit_last_name').val();
    var Email = $('#ins_edit_email').val();
    var Password = $('#ins_edit_password').val();
    var strGeo;
    
    if ($('#ins_edit_office').val() == 1) {
        strGeo = "30.381892, -97.727127";
    }

    if ($('#ins_edit_office').val() == 2) {
        strGeo = "33.000550, -96.799895";
    }

    if ($('#ins_edit_office').val() == 3) {
        strGeo = "29.600419, -98.554735";
    }
    
    if ($('#ins_edit_office').val() == 4) {
        strGeo = "29.686256, -95.797382";
    }

    if ($('#ins_edit_office').val() == 5) {
        strGeo = "30.655105, -96.337065";
    }

    var selected = [];
    $('#ins_edit_type_list input:checked').each(function () {
        selected.push($(this).val());
    });

    var iInspectorId = $('#inspector_id').val();

    if (iInspectorId !== "" && iInspectorId != 0) {
        var strURL = "Delete_Inspector_Inspection_Types.aspx?Inspector_Id=" + iInspectorId;

        $.ajax({
            url: strURL,
            type: 'get',
            dataType: 'text',
            success: function (data) {

                //for (var i = 0; i < selected.length; i++) {
                //var URL = "Insert_Inspector_Inspection_Types.aspx?Inspector_Id=" + iInspectorId + "&Inspection_Type_Id=" + selected[i];
                var URL = "Update_Inspector.aspx?Inspector_Id=" + iInspectorId + "&Email=" + Email + "&FirstName=" + FirstName + "&LastName=" + LastName + "&Password=" + Password + "&Geolocation=" + strGeo + "&Inspection_Types=" + selected + "&Active=" + Active;
                console.log(URL);

                $.ajax({
                    url: URL,
                    type: 'get',
                    dataType: 'text',
                    success: function (data) {
                        loadInspectors();
                    },
                    error: function (e) {
                        console.log(e.message);
                    }
                });
                //}

            },
            error: function (e) {
                console.log(e.message);
            }
        });
    } else {
        var URL = "Insert_Inspector.aspx?&Email=" + Email + "&FirstName=" + FirstName + "&LastName=" + LastName + "&Password=" + Password + "&Geolocation=" + strGeo + "&Inspection_Types=" + selected;
        console.log(URL);

        $.ajax({
            url: URL,
            type: 'get',
            dataType: 'text',
            success: function (data) {
                loadInspectors();
            },
            error: function (e) {
                console.log(e.message);
            }
        });

    }

    $('#inspector_form').hide();
    $('#inspector_list').show();
    $(document).scrollTop(0, 0);
    
}


var map;
var mapData;

function toggleInspectionView(id) {
    $('#insp_calendar').hide();
    $('#insp_order').hide();
    $('#insp_map').hide();
    $('#insp_order_grid').hide();
    $('#insp_statistics').hide();
    $('#filter_button').hide();
    $('#insp_edit_order').hide();

    $('#' + id).show();


    $('.header_button').removeClass('header_button_on');
    $('#btn_' + id).addClass('header_button_on');


    if (id == "insp_order_grid") {
        $('#filter_button').show();
    }

    if (id == 'insp_map') {
        
        
        var mapCanvas = document.getElementById('insp_map');
        
        var mapOptions = {
            center: new google.maps.LatLng(30.380924, -97.726623),
            zoom: 8,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        
        map = new google.maps.Map(mapCanvas, mapOptions);
        
        var directionsService = new google.maps.DirectionsService();
        var num, map, data;
        var requestArray = [], renderArray = [];


        // 16 Standard Colours for navigation polylines
        var colourArray = ['navy', 'grey', 'fuchsia', 'black', 'white', 'lime', 'maroon', 'purple', 'aqua', 'red', 'green', 'silver', 'olive', 'blue', 'yellow', 'teal'];

        // Let's make an array of requests which will become individual polylines on the map.
        requestArray = [];

        for (var route in mapData) {
            // This now deals with one of the people / routes

            // Somewhere to store the wayoints
            var waypts = [];

            // 'start' and 'finish' will be the routes origin and destination
            var start, finish

            // lastpoint is used to ensure that duplicate waypoints are stripped
            var lastpoint

            data = mapData[route]

            limit = data.length
            for (var waypoint = 0; waypoint < limit; waypoint++) {
                if (data[waypoint] === lastpoint) {
                    // Duplicate of of the last waypoint - don't bother
                    continue;
                }

                // Prepare the lastpoint for the next loop
                lastpoint = data[waypoint]

                // Add this to waypoint to the array for making the request
                waypts.push({
                    location: data[waypoint],
                    stopover: true
                });
            }

            // Grab the first waypoint for the 'start' location
            start = (waypts.shift()).location;
            // Grab the last waypoint for use as a 'finish' location
            finish = waypts.pop();
            if (finish === undefined) {
                // Unless there was no finish location for some reason?
                finish = start;
            } else {
                finish = finish.location;
            }

            // Let's create the Google Maps request object
            var request = {
                origin: start,
                destination: finish,
                waypoints: waypts,
                travelMode: google.maps.TravelMode.DRIVING
            };

            // and save it in our requestArray
            requestArray.push({ "route": route, "request": request });
            
            processRequests();
         }
    }

    function processRequests() {

        // Counter to track request submission and process one at a time;
        var i = 0;

        // Used to submit the request 'i'
        function submitRequest() {
            directionsService.route(requestArray[i].request, directionResults);
        }

        // Used as callback for the above request for current 'i'
        function directionResults(result, status) {
            
            if (status == google.maps.DirectionsStatus.OK) {
                // Create a unique DirectionsRenderer 'i'
                renderArray[i] = new google.maps.DirectionsRenderer();
                renderArray[i].setMap(map);

                // Some unique options from the colorArray so we can see the routes
                renderArray[i].setOptions({
                    preserveViewport: true,
                    suppressInfoWindows: true,
                    polylineOptions: {
                        strokeWeight: 4,
                        strokeOpacity: 0.8,
                        strokeColor: colourArray[i]
                    },
                    markerOptions: {
                        icon: {
                            path: google.maps.SymbolPath.BACKWARD_CLOSED_ARROW,
                            scale: 3,
                            strokeColor: colourArray[i]
                        }
                    }
                });

                // Use this new renderer with the result
                renderArray[i].setDirections(result);
                // and start the next request
                nextRequest();
            }

        }

        function nextRequest() {
            // Increase the counter
            i++;
            // Make sure we are still waiting for a request
            if (i >= requestArray.length) {
                // No more to do
                return;
            }
            // Submit another request
            submitRequest();
        }

        // This request is just to kick start the whole process
        submitRequest();
    }

    // Called Onload
    function init() {

        // Some basic map setup (from the API docs) 
        var mapCanvas = document.getElementById('insp_map');
        /*
        var mapOptions = {
            center: new google.maps.LatLng(30.380924, -97.726623),
            zoom: 8,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }*/
        

        var mapOptions = {
            center: new google.maps.LatLng(50.677965, -3.768841),
            zoom: 8,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(mapCanvas, mapOptions);
        
        // Start the request making
        generateRequests()
    }
        
    // Get the ball rolling and trigger our init() on 'load'
    //google.maps.event.addDomListener(window, 'load', init);
}

function loadCompleteInspectionOrders() {

    var strStartDate = $('#startdate').val();
    var strEndDate = $('#enddate').val();

    var URL = "Get_Inspection_Completed_Orders.aspx?StartDate=" + strStartDate + "&EndDate=" + strEndDate;

    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var strCurDivision = "";
            var strCurCompany = "";
            var strCurSubdivision = "";
            var strHTML = "";
            var iRowCount = 0;

            var iDivisionTotal = 0;
            var iCompanyTotal = 0;
            $('#insp_stats').html('');
            $.each(data, function (i) {
                var item = data[i];
                if (strCurDivision != item.Division_Id) {

                    if (strCurDivision != "") {
                        $("#" + strCurDivision + "_count").html(iDivisionTotal);
                        $("#" + strCurDivision + "_" + strCurCompany + "_count").html(iCompanyTotal);
                    } 

                    if (iRowCount != 0) {
                        $('#' + strCurDivision + '_stats').append("<div style='clear:both'></div>");
                        $('#' + strCurDivision + '_stats').append("<div style='height:80px;'></div>");
                        iRowCount = 0;
                    }
                    iRowCount = 0;

                    $('#insp_stats').append("<div style='margin-bottom:10px;border-bottom:1px solid #000000; font-size:28px;width:100%;'>" + item.Division_Desc + " <span id='" + item.Division_Id + "_count'></span></div>");
                    $('#insp_stats').append("<div id='" + item.Division_Id + "_stats'></div>");

                    strCurDivision = item.Division_Id;

                    console.log($('#' + strCurDivision + '_stats').html());
                    iDivisionTotal = 0;


                }

                if (strCurCompany != item.Client_Short_Name) {

                    if (strCurCompany != "") {
                        $("#" + strCurDivision + "_" + strCurCompany + "_count").html(iCompanyTotal);
                    }

                    $('#' + strCurDivision + '_stats').append("<div id='" + strCurDivision + "_" + item.Client_Short_Name + "_stats' style='float:left;width:300px;'><span style='font-size:18px;font-weight:600;'>" + item.Client_Short_Name + ":</span> <span style='font-size:18px;font-weight:600;' id='" + strCurDivision + "_" + item.Client_Short_Name + "_count'></span></div>");
                    iRowCount = iRowCount + 1;

                    if (iRowCount == 3) {
                        $('#' + strCurDivision + '_stats').append("<div style='clear:both'></div>");
                        $('#' + strCurDivision + '_stats').append("<div style='height:20px;'></div>");
                        iRowCount = 0;
                    }

                    strCurCompany = item.Client_Short_Name;
                    iCompanyTotal = 0;

                }


                $("#" + strCurDivision + "_" + strCurCompany + "_stats").append("<div>");
                $("#" + strCurDivision + "_" + strCurCompany + "_stats").append("<div style='margin-left:5px;float:left;width:180px;'>" + item.Subdivision_Name + "</div>");
                $("#" + strCurDivision + "_" + strCurCompany + "_stats").append("<div style='margin-left:5px;float:left;'>" + item.Count + "</div>");
                $("#" + strCurDivision + "_" + strCurCompany + "_stats").append("<div style='clear:both'></div>");
                $("#" + strCurDivision + "_" + strCurCompany + "_stats").append("</div>");

                iDivisionTotal = iDivisionTotal + item.Count;
                iCompanyTotal = iCompanyTotal + item.Count;
            });

            $("#" + strCurDivision + "_count").html(iDivisionTotal);
            $("#" + strCurDivision + "_" + strCurCompany + "_count").html(iCompanyTotal);

        },
        error: function (e) {
            console.log(e.message);
        }
    });

}

function doHover(el) {

    $('#' + el.id).css('border', '1px solid #000000');
    $('#' + el.id + ' .schedule_move').css('background', '#000000');
    $('#' + el.id + ' .schedule_move').html('<div style="height:16px;">move</div><div style="height:16px;">re-assign</div>');
}

function unHover(el) {

    $('#' + el.id).css('border', '1px solid #696969');
    $('#' + el.id + ' .schedule_move').css('background', '#696969');
    $('#' + el.id + ' .schedule_move').html('');
}



function manageScheduleItem(el) {
    alert(el.id);
}

function loadInspectionOrders() {

    var startDate = $('#flt_begin_date').val();
    var endDate = $('#flt_end_date').val();
    var statuses = '';
    var types = '';
   
    var URL = "Get_Inspection_Order_Slice.aspx?StartDate=" + startDate + "&EndDate=" + endDate + "&Types=" + types + "&Statuses=" + statuses;
    console.log(URL);

    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrInspectionOrders = data;
            resort_ins_orders('Order_Date');
            //renderInspectionOrders();
        }
    });
}

function loadInspectionAccountingOrders() {
  
    var startDate = $('#flt_begin_date').val();
    var endDate = $('#flt_end_date').val();
    var statuses = '3';
    var types = '';

    var URL = "../Inspection_Portal/Get_Inspection_Order_Slice.aspx?StartDate=" + startDate + "&EndDate=" + endDate + "&Types=" + types + "&Statuses=" + statuses;
    console.log(URL);

    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            arrInspectionOrders = data;
            resort_ins_orders('Order_Date');
            //renderInspectionOrders();
        }
    });
}

function renderInspectionOrders() {
    console.log(JSON.stringify(arrInspectionOrders));
    $('#inspection_order_list').html('');
    $('#foundation_order_grid').html('');

    var fltClient = $('#flt_client').val();
    var fltType = $('#flt_type').val();
    var fltSubdivision = $('#flt_subdivision').val();
    var fltStatus = $('#flt_status').val();
    var fltAddress = $('#flt_address').val();

    var isAccounting = false;

    var url = window.location.href;

    if (url.indexOf('AccountingPortal') > -1) {
        isAccounting = true;
    }


    $.each(arrInspectionOrders, function (i) {
        var item = arrInspectionOrders[i];
 
        var bDoRow = true;

        if ((fltClient != "" && item.Client_Short_Name.toLowerCase().indexOf(fltClient.toLowerCase()) == -1)
            || (fltType != "" && item.Inspection_Type.toLowerCase().indexOf(fltType.toLowerCase()) == -1)
            || (fltStatus != "" && item.Inspection_Status.toLowerCase().indexOf(fltStatus.toLowerCase()) == -1)
            || (fltSubdivision != "" && item.Subdivision_Name.toLowerCase().indexOf(fltSubdivision.toLowerCase()) == -1)
            || (fltAddress != "" && item.Address.toLowerCase().indexOf(fltAddress.toLowerCase()) == -1)
        ) 
        {
            var bDoRow = false;
        }


        if(bDoRow == true)
        {
            var strHTML = "<div style='padding-top:5px;padding-bottom:2px;background:ffff4c;border-bottom:1px solid #eeeeee;'>";
            strHTML = strHTML + "<div class='lst_clickable' style='width:170px;font-weight:600;padding-left:10px;'><div onclick='editInspectionOrder(\"" + item.Inspection_Order_Id + "\")' style='float:left;'>" + item.MLAW_Number + "</div><div style='clear:both'></div></div>";
            strHTML = strHTML + "<div class='lst_item' style='width:104px;'>" + item.Client_Short_Name + " </div>";
            strHTML = strHTML + "<div class='lst_item' style='width:140px;'>" + item.Subdivision_Name + " </div>";
            strHTML = strHTML + "<div class='lst_item' style='width:260px;'>" + item.Address + "</div>";
            strHTML = strHTML + "<div class='lst_item' style='width:130px;'>" + item.Inspection_Type + "</div>";
            //strHTML = strHTML + "<div class='lst_item' style='width:80px;'>" + item.Lot + "/" + item.Block + "/" + item.Section + " </div>";
            strHTML = strHTML + "<div class='lst_item' style='width:110px;'>" + item.Order_Date + "</div>";

            if (isAccounting == false) {
                strHTML = strHTML + "<div class='lst_item' style='width:110px;'>" + item.Inspection_Status + "</div>";
            } else {
                strHTML = strHTML + "<div class='lst_item' style='width:110px;'>" + item.Amount + "</div>";
            }

            strHTML = strHTML + "<div style='clear:both'></div>";
            strHTML = strHTML + "</div>";
       
            if (isAccounting == true) {
                $('#foundation_order_grid').append(strHTML);
            } else {
                $('#inspection_order_list').append(strHTML);
            }
        }

    });

}

function resort_ins_orders(col) {

    currentSort = col;
    $('#ins_order_list_titles div').removeClass('header_button_on');
    $('#ins_order_list_title_' + col).addClass('header_button_on');


    if (col == "Order_Date") {
        arrInspectionOrders.sort(function (el1, el2) {
            return datecompare(el1, el2, col)
        });
    } else {

        arrInspectionOrders.sort(function (el1, el2) {
            return compare(el1, el2, col)
        });
    }

    renderInspectionOrders();

}


function editInspectionOrder(InspectionOrderId) {
    
    toggleInspectionView('insp_edit_order');
    var stateObj = { id: InspectionOrderId };
    history.pushState(stateObj, "order number:" + InspectionOrderId, InspectionOrderId + ".html");

    var URL = "Get_Inspection_Order.aspx?Inspection_Order_Id=" + InspectionOrderId;
    
    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            item = data[0];

            $('#ord_edit_insp_order_id').val(InspectionOrderId);
            $('#ord_edit_status').val(item.Order_Status_Id);
            $('#ord_edit_inspection_type').val(item.Inspection_Type_Id);
            $('#ord_edit_pass_fail').val(item.Inspection_Result);
            $('#ord_edit_insp_comments').val(item.Inspection_Notes);

            $('#ord_edit_company').text(item.Client_Short_Name);
            $('#ord_edit_subdivision').text(item.Subdivision_Name);
            $('#ord_edit_comments').text(item.Comments);

            $('#ord_edit_mlaw_number').text(item.MLAW_Number);
            $('#ord_edit_section').text(item.Section);
            $('#ord_edit_phase').text(item.Phase);
            $('#ord_edit_lot').text(item.Lot);
            $('#ord_edit_block').text(item.Block);
            $('#ord_edit_contact').text(item.Contact);
            $('#ord_edit_phone').text(item.Phone);
            $('#ord_edit_elevation').text(item.Elevation);
            $('#ord_edit_plan_name').text(item.Plan_Name);
            $('#ord_edit_plan_number').text(item.Plan_Number);

            $('#ord_edit_address').text(item.Address);
            $('#ord_edit_company_holder').text(item.Client_Short_Name);
            $('#ord_edit_company').text(item.Client_Id);
            $('#ord_edit_subdivision_holder').text(item.Subdivision_Name);

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

            $('#ord_edit_date_received').text(item.Order_Date_String);


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
    });


}

function updateInspectionOrder() {

    var status = $('#ord_edit_status').val();
    var type = $('#ord_edit_inspection_type').val();
    var result = $('#ord_edit_pass_fail').val();
    var comments = $('#ord_edit_insp_comments').val();
    var insp_order_id = $('#ord_edit_insp_order_id').val();

    var URL = "Update_Inspection_Order.aspx?Inspection_Order_Id=" + insp_order_id + "&Status=" + status + "&Type=" + type + "&Result=" + result + "&Result_Notes=" + encodeURIComponent(comments);


    $.ajax({
        url: URL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
                loadInspectionOrders();
              toggleInspectionView('insp_order_grid');
        }
    });
}