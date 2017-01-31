function loadChartData() {

    var width = 500,
        height = 500,
        radius = Math.min((width - 50), (height - 50)) / 2;

    var x = d3.scale.linear()
        .range([0, 2 * Math.PI]);

    var y = d3.scale.linear()
        .range([0, radius]);

    var color = d3.scale.category20c();

    var svg = d3.select("#chrt_open_orders").append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + (height / 2 + 10) + ")");

    var partition = d3.layout.partition()
        .value(function (d) { return d.size; });

    var arc = d3.svg.arc()
        .startAngle(function (d) { return Math.max(0, Math.min(2 * Math.PI, x(d.x))); })
        .endAngle(function (d) { return Math.max(0, Math.min(2 * Math.PI, x(d.x + d.dx))); })
        .innerRadius(function (d) { return Math.max(0, y(d.y)); })
        .outerRadius(function (d) { return Math.max(0, y(d.y + d.dy)); });

    d3.json("/Charting/CHRT_Status_Open_Orders.aspx", function (error, root) {
      
        var g = svg.selectAll("g")
            .data(partition.nodes(root))
            .enter().append("g");

        var path = g.append("path")
            .attr("d", arc)
            .style("fill", function (d) { return color((d.children ? d : d.parent).name); })
            .on("click", click);

        var text = g.append("text")
            .attr("transform", function (d) { return "rotate(" + computeTextRotation(d) + ")"; })
            .attr("x", function (d) { return y(d.y); })
            .attr("dx", "6") // margin
            .attr("dy", ".35em") // vertical-align
            .text(function (d) { return d.name; });

        function click(d) {
            // fade out all text elements
            text.transition().attr("opacity", 0);

            path.transition()
                .duration(750)
                .attrTween("d", arcTween(d))
                .each("end", function (e, i) {
                    // check if the animated element's data e lies within the visible angle span given in d
                    if (e.x >= d.x && e.x < (d.x + d.dx)) {
                        // get a selection of the associated text element
                        var arcText = d3.select(this.parentNode).select("text");
                        // fade in the text element and recalculate positions
                        arcText.transition().duration(750)
                        .attr("opacity", 1)
                        .attr("transform", function () { return "rotate(" + computeTextRotation(e) + ")" })
                        .attr("x", function (d) { return y(d.y); });
                    }
                });
        }
    });

    d3.select(self.frameElement).style("height", height + "px");

    // Interpolate the scales!
    function arcTween(d) {
        var xd = d3.interpolate(x.domain(), [d.x, d.x + d.dx]),
            yd = d3.interpolate(y.domain(), [d.y, 1]),
            yr = d3.interpolate(y.range(), [d.y ? 20 : 0, radius]);
        return function (d, i) {
            return i
                ? function (t) { return arc(d); }
                : function (t) { x.domain(xd(t)); y.domain(yd(t)).range(yr(t)); return arc(d); };
        };
    }

    function computeTextRotation(d) {
        return (x(d.x + d.dx / 2) - Math.PI / 2) / Math.PI * 180;
    }

}

function loadDesignerChartData() {

    var width = 500,
        height = 500,
        radius = Math.min((width-50), (height-50)) / 2;

    var x = d3.scale.linear()
        .range([0, 2 * Math.PI]);

    var y = d3.scale.linear()
        .range([0, radius]);

    var color = d3.scale.category20c();

    var svg = d3.select("#chrt_designer_open_orders").append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + (height / 2 + 10) + ")");

    var partition = d3.layout.partition()
        .value(function (d) { return d.size; });

    var arc = d3.svg.arc()
        .startAngle(function (d) { return Math.max(0, Math.min(2 * Math.PI, x(d.x))); })
        .endAngle(function (d) { return Math.max(0, Math.min(2 * Math.PI, x(d.x + d.dx))); })
        .innerRadius(function (d) { return Math.max(0, y(d.y)); })
        .outerRadius(function (d) { return Math.max(0, y(d.y + d.dy)); });

    d3.json("Charting/CHRT_designer_open_orders.aspx", function (error, root) {
        var g = svg.selectAll("g")
            .data(partition.nodes(root))
            .enter().append("g");

        var path = g.append("path")
            .attr("d", arc)
            .style("fill", function (d) { return color((d.children ? d : d.parent).name); })
            .on("click", click);

        var text = g.append("text")
            .attr("transform", function (d) { return "rotate(" + computeTextRotation(d) + ")"; })
            .attr("x", function (d) { return y(d.y); })
            .attr("dx", "6") // margin
            .attr("dy", ".35em") // vertical-align
            .text(function (d) { return d.name; });

        function click(d) {
            // fade out all text elements
            text.transition().attr("opacity", 0);

            path.transition()
                .duration(750)
                .attrTween("d", arcTween(d))
                .each("end", function (e, i) {
                    // check if the animated element's data e lies within the visible angle span given in d
                    if (e.x >= d.x && e.x < (d.x + d.dx)) {
                        // get a selection of the associated text element
                        var arcText = d3.select(this.parentNode).select("text");
                        // fade in the text element and recalculate positions
                        arcText.transition().duration(750)
                        .attr("opacity", 1)
                        .attr("transform", function () { return "rotate(" + computeTextRotation(e) + ")" })
                        .attr("x", function (d) { return y(d.y); });
                    }
                });
        }
    });

    d3.select(self.frameElement).style("height", height + "px");

    // Interpolate the scales!
    function arcTween(d) {
        var xd = d3.interpolate(x.domain(), [d.x, d.x + d.dx]),
            yd = d3.interpolate(y.domain(), [d.y, 1]),
            yr = d3.interpolate(y.range(), [d.y ? 20 : 0, radius]);
        return function (d, i) {
            return i
                ? function (t) { return arc(d); }
                : function (t) { x.domain(xd(t)); y.domain(yd(t)).range(yr(t)); return arc(d); };
        };
    }

    function computeTextRotation(d) {
        return (x(d.x + d.dx / 2) - Math.PI / 2) / Math.PI * 180;
    }
}

function loadCompletedOrders() {
    var startDate = $('#comp_order_begin').val();
    var endDate = $('#comp_order_end').val();
    var strURL = "/Charting/CHRT_Get_Completed_Orders.aspx?Start_Date=" + startDate + "&End_Date=" + endDate + "&Chart_Type=0";
    console.log(strURL);
    $('#completed_orders').html('');
  
    var margin = { top: 100, right: 50, bottom: 50, left: 40 },
        width = 1200 - margin.left - margin.right,
        height = 500 - margin.top - margin.bottom;

    // scale to ordinal because x axis is not numerical
    var x = d3.scale.ordinal().rangeRoundBands([0, width], .1);

    //scale to numerical value by height
    var y = d3.scale.linear().range([height, 0]);

    var chart = d3.select("#completed_orders")
                  .append("svg")  //append svg element inside #chart
                  .attr("width", width + (2 * margin.left) + margin.right)    //set width
                  .attr("height", height + margin.top + margin.bottom);  //set height
    var xAxis = d3.svg.axis()
                  .scale(x)
                  .tickSize(0)
                  .orient("bottom");  //orient bottom because x-axis will appear below the bars

    var yAxis = d3.svg.axis()
                  .scale(y)
                  .orient("left");

    d3.json(strURL, function (error, data) {
        loadCompletedOrdersDetails();

        x.domain(data.map(function (d) { return d.letter }));
        y.domain([0, d3.max(data, function (d) { return d.frequency }) + 10]);

        $('#total_orders').text("Total Orders Completed: " + d3.sum(data, function (d) { return d.frequency; }));

        var bar = chart.selectAll("g")
                          .data(data)
                        .enter()
                          .append("g")
                          .attr("transform", function (d, i) {
                              return "translate(" + x(d.letter) + ", 0)";
                          });

        bar.append("rect")
            .attr("y", function (d) {
                return y(d.frequency);
            })
            .attr("x", function (d, i) {
                return x.rangeBand() + (margin.left / 4);
            })
            .attr("height", function (d) {
                return height - y(d.frequency);
            })
            .attr("width", x.rangeBand());  //set width base on range on ordinal data

        bar.append("text")
            .attr("x", x.rangeBand() + margin.left - 12)
            .attr("y", function (d) { return y(d.frequency) - 10; })
            .attr("dy", ".75em")
            .text(function (d) { return d.frequency; });

        chart.append("g")
              .attr("class", "x axis")
              .attr("transform", "translate(" + margin.left + "," + height + ")")
                  .call(xAxis)
                  .selectAll("text")
                    .attr("transform", function (d, i) {
                        return "translate(-" + (this.getBBox().width/2) +" ," + this.getBBox().width + ")rotate(-90)";
                    })
        ;


        chart.append("g")
              .attr("class", "y axis")
              .attr("transform", "translate(" + margin.left + ",0)")
              .call(yAxis)
              .append("text")
              .attr("transform", "rotate(-90)")
              .attr("y", 6)
              .attr("dy", ".71em")
              .style("text-anchor", "end")
              .text("Homes");


    });

    function type(d) {
        d.letter = +d.letter; // coerce to number
        return d;
    }
}


function loadCompletedOrdersDetails() {
    var startDate = $('#comp_order_begin').val();
    var endDate = $('#comp_order_end').val();
    var strURL = "/Charting/Get_Completed_Orders.aspx?Start_Date=" + startDate + "&End_Date=" + endDate + "&Chart_Type=0";
    console.log(strURL);
    $('#completed_orders_details').html('');

    $.ajax({
        url: strURL,
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var curCompany = "";
            var curDivision = "";
            var strHTML = "";
            var iCount = 0;
            var iTotal = 0;
            var iDivisionTotal = 0;

            //console.log(JSON.stringify(data));
            $.each(data, function (i) {
                var item = data[i];
              
                if (curDivision != item.Division_Desc) {
                    if (curDivision != "") {
                        strHTML = strHTML + "</div>";
                        strHTML = strHTML.replace("[" + curDivision + "_total]", iDivisionTotal);
                    }
                    if (iCount != 0) {
                        
                        strHTML = strHTML + "<div style='clear:both'></div>";
                    }
                    
                    strHTML = strHTML.replace("<span id='" + curCompany + "_total'></span>", iTotal);
                    
                    strHTML = strHTML + "<div style='font-size:24px;margin-top:100px;width:1200px;border-bottom:1px solid;'>" + item.Division_Desc + ": [" + item.Division_Desc + "_total] orders</div>";
                    curDivision = item.Division_Desc;
                    curCompany = "";
                    iCount = 0;
                    iTotal = 0;
                    iDivisionTotal = 0;
                }
                
                if (curCompany != item.Client_Short_Name) {

                    if (curCompany != "") {
                        strHTML = strHTML + "</div>";
                        strHTML = strHTML.replace("<span id='" + curCompany + "_total'></span>", iTotal);
                        iTotal = 0;
                    }
                    
                    if (iCount == 3) {
                        strHTML = strHTML + "<div style='clear:both'></div>";
                        iCount = 0;
                    }
                    
                    strHTML = strHTML + "<div style='margin-top:50px;float:left;width:400px;background:transparent'><div style='font-size:18px;font-weight:600;'>" + item.Client_Short_Name + ": <span id='" + item.Client_Short_Name + "_total'></span></div>";
                    
                    iCount = iCount + 1
                    curCompany = item.Client_Short_Name;
                }

                
                strHTML = strHTML + "<div style='padding-left:5px;width:250px;float:left'>" + item.Subdivision_Name + "</div>";
                strHTML = strHTML + "<div style='width:100px;float:left'>" + item.Count + "</div>";
                strHTML = strHTML + "<div style='clear:both'></div>";
                iTotal = iTotal + parseInt(item.Count);
                iDivisionTotal = iDivisionTotal + parseInt(item.Count);
            });
            strHTML = strHTML.replace("[" + curDivision + "_total]", iDivisionTotal);
            
           
            $('#completed_orders_details').html(strHTML);
            
            
        }
    });


}