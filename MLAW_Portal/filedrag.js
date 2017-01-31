
var orderFile = "";

function handleFileUpload(files, obj, isDesigner) {

    for (var i = 0; i < files.length; i++) {
        var fd = new FormData();
        fd.append('file', files[i]);

        if (orderFile == "") {
            orderFile = files[i].name;
        } else {
            orderFile = orderFile + ";" + files[i].name;
        }
       
        var status = new createStatusbar(obj); //Using this we can set progress.
        status.setFileNameSize(files[i].name, files[i].size);

        if (files[i].size > 500000 && isDesigner == null) {
            if (confirm("This file is very large and make take a long time to process. Would you like to process the form, or simply upload it?")) {
                sendFileToServer(fd, status);
            } else {
                uploadToServer(fd, status);
            }

        } else {
            sendFileToServer(fd, status);
        }

    }
}


function handleFileUpload2(files, obj, isDesigner) {

    for (var i = 0; i < files.length; i++) {
        var fd = new FormData();
        fd.append('file', files[i]);

        if (orderFile == "") {
            orderFile = files[i].name;
        } else {
            orderFile = orderFile + ";" + files[i].name;
        }
        
        var status = new createStatusbar(obj); //Using this we can set progress.
        status.setFileNameSize(files[i].name, files[i].size);
        uploadToServer(fd, status);

    }
}


function uploadToServer(formData, status) {
    
    var uploadURL = "upload_form.aspx"; //Upload URL
    var extraData = {}; //Extra Data.
    var jqXHR = $.ajax({
        xhr: function () {
            var xhrobj = $.ajaxSettings.xhr();
            if (xhrobj.upload) {
                xhrobj.upload.addEventListener('progress', function (event) {
                    var percent = 0;
                    var position = event.loaded || event.position;
                    var total = event.total;
                    if (event.lengthComputable) {
                        percent = Math.ceil(position / total * 100);
                    }
                    //Set progress
                    status.setProgress(percent);
                }, false);

            }
            return xhrobj;

        },
        url: uploadURL,
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: formData,
        success: function (data) {
            status.setProgress(100);
            console.log(data);
            //$('#test_results').val(data);
            //$("#status1").append("File upload Done<br>");           
        }
    });

    status.setAbort(jqXHR);
}

var tempRevisionComments;
var subIsSet = 0;

function sendFileToServer(formData, status) {

    $('#ord_company').val('');
    $('#ord_subdivision').val('');
    //$('#ord_date_received').val('');
    $('#ord_address').val('');
    $('#ord_city').val('');
    $('#ord_type').val('');

    var uploadURL = "../process_form.aspx"; //Upload URL
    var extraData = {}; //Extra Data.
    var jqXHR = $.ajax({
        xhr: function () {
            var xhrobj = $.ajaxSettings.xhr();
            if (xhrobj.upload) {
                xhrobj.upload.addEventListener('progress', function (event) {
                    var percent = 0;
                    var position = event.loaded || event.position;
                    var total = event.total;
                    if (event.lengthComputable) {
                        percent = Math.ceil(position / total * 75);
                    }
                    //Set progress
                    status.setProgress(percent);
                }, false);

            }
            return xhrobj;
           
        },
        url: uploadURL,
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: formData,
        success: function (data) {
            status.setProgress(100);
            console.log(data);
        
            var obj = $.parseJSON(data);
        
            if (obj.isRevision == 1) {
             
                $('#ord_address').val(obj.strAddress);
                $('input[name="ord_rev"][value="1"]').prop('checked', true);
                tempRevisionComments = obj.strComments;
                findMLAWNum();
            } else {
              
                subIsSet = 0;
                setCompanyName(obj.strCompany);
                handleSubdivision(obj.strSubdivision);

                //$('#ord_date_received').val(obj.strDate);
                $('#ord_address').val(obj.strAddress);
                $('#ord_city').val(obj.strCity);
                $('#ord_type').val(obj.strOrderType);
                $('#ord_contact').val(obj.strContact);
                $('#ord_phone').val(obj.strPhone);
                $('#ord_lot').val(obj.strLot);
                $('#ord_block').val(obj.strBlock);
                $('#ord_section').val(obj.strSection);
                $('#ord_phase').val(obj.strPhase);
                $('#ord_city').val(obj.strCity);
                $('#ord_county').val(obj.strCounty);
                $('#ord_plan_name').val(obj.strPlanName);
                $('#ord_plan_number').val(obj.strPlanNumber);
                $('#ord_elevation').val(obj.strElevation);
                $('#ord_comments').val(obj.strComments);
                //$('#ord_customer_job_num').val(obj.strCustomerJobNumber);

                if (obj.strGarage != null && obj.strGarage.length > 0) {
                    var GarageSetting = obj.strGarage.substring(0, 1).toUpperCase();
                    var subGS = GarageSetting.substring(0, 1);

                    if (subGS == "R") {
                        GarageSetting = "Right Hand";
                    } else {
                        GarageSetting = "Left Hand";
                    }

                    $('input[name="ord_garage"][value="' + GarageSetting + '"]').prop('checked', true);
                }


                if (obj.strPatio != null && obj.strPatio.length > 0) {
                    var PSetting = obj.strPatio.substring(0, 1).toUpperCase();
                    var subPatio = PSetting.substring(0, 1);

                    if (subPatio.toLowerCase() == "y") {
                        PSetting = "Yes";
                    } else {
                        pSetting = "No";
                    }

                    $('input[name="ord_patio"][value="' + PSetting + '"]').prop('checked', true);
                }

                $('input[name="ord_masonry"][value="' + obj.strMasonrySides + '"]').prop('checked', true);

                //$('#test_results').val(data);
                //$("#status1").append("File upload Done<br>");     
            }
        }
    });

    status.setAbort(jqXHR);
}

function handleSubdivision(subdivision) {
    
    if ($('#ord_company').val().trim() == '')
    {
        setTimeout(function (){
            handleSubdivision(subdivision);
        },1000);
    }else{
        setSubdivision(subdivision);
    }
}

var rowCount = 0;
function createStatusbar(obj) {
    rowCount++;
    var row = "odd";
    if (rowCount % 2 == 0) row = "even";
    this.statusbar = $("<div class='statusbar " + row + "'></div>");
    this.filename = $("<div class='filename'></div>").appendTo(this.statusbar);
    this.size = $("<div class='filesize'></div>").appendTo(this.statusbar);
    this.progressBar = $("<div class='progressBar'><div></div></div>").appendTo(this.statusbar);
    this.abort = $("<div class='abort'>Abort</div>").appendTo(this.statusbar);
    obj.after(this.statusbar);

    this.setFileNameSize = function (name, size) {
        var sizeStr = "";
        var sizeKB = size / 1024;
        if (parseInt(sizeKB) > 1024) {
            var sizeMB = sizeKB / 1024;
            sizeStr = sizeMB.toFixed(2) + " MB";
        }
        else {
            sizeStr = sizeKB.toFixed(2) + " KB";
        }

        this.filename.html(name);
        this.size.html(sizeStr);
    }
    this.setProgress = function (progress) {
        var progressBarWidth = progress * this.progressBar.width() / 100;
        this.progressBar.find('div').animate({ width: progressBarWidth }, 10).html(progress + "% ");
        if (parseInt(progress) >= 100) {
            this.abort.hide();
        }
    }
    this.setAbort = function (jqxhr) {
        var sb = this.statusbar;
        this.abort.click(function () {
            jqxhr.abort();
            sb.hide();
        });
    }
}





