
var linechartcustomerColors = ["rgba(64, 81, 137, 1)"];
linechartcustomerColors && (options = {
    series: [ {
        name: "Records",
        type: "bar",
        data: [89.25, 98.58, 68.74, 108.87, 77.54, 84.03, 51.24, 28.57, 92.57, 42.36, 88.51, 36.57]
    }],
    chart: {
        height: 370,
        type: "line",
        toolbar: {
            show: !1
        }
    },
    stroke: {
        curve: "straight",
        dashArray: [0, 0, 8],
        width: [2, 0, 2.2]
    },
    fill: {
        opacity: [.1, .9, 1]
    },
    markers: {
        size: [0, 0, 0],
        strokeWidth: 2,
        hover: {
            size: 4
        }
    },
    xaxis: {
        categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
        axisTicks: {
            show: !1
        },
        axisBorder: {
            show: !1
        }
    },


    plotOptions: {
        bar: {
            columnWidth: "30%",
            barHeight: "70%"
        }
    },
    colors: linechartcustomerColors,
    tooltip: {
        shared: !0,
        y: [{
            formatter: function (e) {
                return void 0 !== e ? e.toFixed(0) : e
            }
        }, {
            formatter: function (e) {
                return void 0 !== e ? "$" + e.toFixed(2) + "k" : e
            }
        }, {
            formatter: function (e) {
                return void 0 !== e ? e.toFixed(0) + " Sales" : e
            }
        }]
    }
}, (chart = new ApexCharts(document.querySelector("#customer_impression_charts"), options)).render());

