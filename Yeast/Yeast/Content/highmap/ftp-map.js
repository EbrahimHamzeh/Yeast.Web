$(document).ready(function(){

var my_url = "http://www.cloudsat.cira.colostate.edu/embed/maps/data.json";
var my_url2 = "http://www.cloudsat.cira.colostate.edu/embed/maps/pdata.json";
var cdata = (function () {
    var cdata = null;
    $.ajax({
        'async': false,
        'global': false,
        'url': my_url,
        'dataType': "json",
        'success': function (data) {
            cdata = data;
        }
    });
    return cdata;
})(); 

var tdata = (function () {
    var tdata = null;
    $.ajax({
        'async': false,
        'global': false,
        'url': my_url2,
        'dataType': "json",
        'success': function (data) {
            tdata = data;
        }
    });
    return tdata;
})();

var pdata = {};
for(jj=0;jj<tdata.length;jj++) {
 pdata[tdata[jj].cname] = [tdata[jj].value[0],tdata[jj].value[1],tdata[jj].value[2],tdata[jj].value[3],tdata[jj].value[4],tdata[jj].value[5],tdata[jj].value[6],tdata[jj].value[7]];
}

 function addCommas(nStr) {
  nStr += '';
  var x = nStr.split('.');
  var x1 = x[0];
  var x2 = x.length > 1 ? '.' + x[1] : '';
  var rgx = /(\d+)(\d{3})/;
  while (rgx.test(x1)) {
   x1 = x1.replace(rgx, '$1' + ',' + '$2');
  }
  return x1 + x2;
 }

 var chtml = "";
 var cback = "";
 for(ii=0;ii<cdata.length;ii++) {
  if(cdata[ii].value=="1") tfiles="File";
  else tfiles="Files";
  if(parseInt(cdata[ii].value)>1000000) cback = "background-color:#D5FCD4;font-weight:bold;";
  else cback = "";
  if(cdata[ii].cname=="United States of America") chtml += "<div style=\"float:left;width:140px;border-bottom: 1px solid #CCC;margin-bottom:3px;"+cback+"padding-left:3px;\">United States<br>"+addCommas(cdata[ii].value)+" Total "+tfiles+"</div>";
  else chtml += "<div style=\"float:left;width:140px;border-bottom: 1px solid #CCC;"+cback+"padding-left:3px;\">"+cdata[ii].cname+"<br>"+addCommas(cdata[ii].value)+" Total "+tfiles+"</div>";
 }
 $(".country-class").html(chtml);

    $.getJSON('http://www.cloudsat.cira.colostate.edu/embed/maps/world-highres.geo.json', function (geojson) {
     var mapChart;
     var countryChart;
     Highcharts.wrap(Highcharts.Point.prototype, 'select', function (proceed) {
             proceed.apply(this, Array.prototype.slice.call(arguments, 1));

            var points = mapChart.getSelectedPoints();
            if (points.length) {
             if (points.length === 1) {
              $('#info h2').html(points[0].name);
             } else {
              $('#info h2').html('Comparing countries');
             }
             $('#info .subheader').html('<h4>Historical population</h4><small><em>Shift + Click on map to compare countries</em></small>')
if (!countryChart) {
                    countryChart = $('#country-chart').highcharts({
                        chart: {
                            height: 250,
                            spacingLeft: 0
                        },
                        credits: {
                            enabled: false
                        },
                        title: {
                            text: null
                        },
                        subtitle: {
                            text: null
                        },
                        xAxis: {
                            tickPixelInterval: 50,
                            crosshair: true
                        },
                        yAxis: {
                            title: null,
                            opposite: true
                        },
                        tooltip: {
                            shared: true
                        },
                        plotOptions: {
                            series: {
                                animation: {
                                    duration: 500
                                },
                                marker: {
                                    enabled: false
                                },
                                threshold: 0,
                                pointStart: 2007, //parseInt(categories[0]),
                            }
                        }
                    }).highcharts();
                }
                $.each(points, function (i) {
                 if (countryChart.series[i]) {
                  countryChart.series[i].update({
                            name: this.name,
                            data: pdata[this.name],
                            type: points.length > 1 ? 'line' : 'area'
                        }, false);
                    } else {
                        countryChart.addSeries({
                            name: this.name,
                            data: pdata[this.name],
                            type: points.length > 1 ? 'line' : 'area'
                        }, false);
                    }
                });
                while (countryChart.series.length > points.length) {
                    countryChart.series[countryChart.series.length - 1].remove(false);
                }
                countryChart.redraw();
            }

});
mapChart = $('#container').highcharts('Map', {
            
            title : {
                text : 'CloudSat Downloaded Files by Country'
            },

            mapNavigation: {
                enabled: true,
                buttonOptions: {
                    verticalAlign: 'bottom'
                }
            },
            colorAxis: {
                min: 0
            },
            tooltip: {
                footerFormat: '<span style="font-size: 10px">(Click for details)</span>'
            },

            series : [{
                data : cdata,
                mapData: geojson,
                joinBy: ['name', 'cname'],
                name: 'Files Downloaded',
                allowPointSelect: true,
                cursor: 'pointer',
                states: {
                    hover: {
                        color: '#BADA55'
                    },
                    select: {
                        color: '#a4edba',
                        borderColor: 'black',
                        dashStyle: 'shortdot'
                    }
                },
                dataLabels: {
                    enabled: true,
                    format: '{point.properties.postal}'
                }
            }]
        }).highcharts();
    });
});
