
function toggleFilterHeader(el) {

    $('#order_types').children().removeClass('header_button_on');
    $('#flt_' + el).addClass('header_button_on');

}


function load800Orders()
{
    if ($('#filters').is(":visible") == true) {
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
        statuses = '9,10';
        date_type = '3';

        end_date = sdf.format(dtNow);
        begin_date = sdf.format(dtBegin);

        $('#flt_begin_date').val(begin_date);
        $('#flt_end_date').val(end_date);
        $('#flt_date_type').val(3);

        $('#order_types .header_button').removeClass('header_button_on');
        $('#flt_complete').addClass('header_button_on');
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

    if (option == 4) {
        statuses = '10, 11';
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



    var strURL = "../Get_800_Order_Slice.aspx?Statuses=" + statuses + "&Date_Type=" + date_type + "&Start_Date=" + begin_date + "&End_Date=" + end_date;
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