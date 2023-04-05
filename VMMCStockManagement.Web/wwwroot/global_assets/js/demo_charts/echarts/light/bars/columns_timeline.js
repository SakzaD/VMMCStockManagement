/* ------------------------------------------------------------------------------
 *
 *  # Echarts - Columns timeline example
 *
 *  Demo JS code for columns timeline chart [light theme]
 *
 * ---------------------------------------------------------------------------- */


// Setup module
// ------------------------------

var EchartsColumnsTimelineLight = function() {


    //
    // Setup module components
    //

    // Columns timeline chart
    var _columnsTimelineLightExample = function() {
        if (typeof echarts == 'undefined') {
            console.warn('Warning - echarts.min.js is not loaded.');
            return;
        }

        // Define element
        var columns_timeline_element = document.getElementById('columns_daily');

        //
        // Charts configuration
        //

        if (columns_timeline_element) {

            // Initialize chart
            var columns_timeline = echarts.init(columns_timeline_element);


            //
            // Chart config
            //

        
            // Options
            columns_timeline.setOption({

                // Setup timeline
                timeline: {
                    axisType: 'category',
                    data: ['2010-01-01', '2011-01-01', '2012-01-01', '2013-01-01', '2014-01-01'],
                    left: 0,
                    right: 0,
                    bottom: 0,
                    label: {
                        normal: {
                            fontFamily: 'Roboto, Arial, Verdana, sans-serif',
                            fontSize: 11
                        }
                    },
                    autoPlay: true,
                    playInterval: 3000
                },

                // Config
                options: [
                    {

                        // Global text styles
                        textStyle: {
                            fontFamily: 'Roboto, Arial, Verdana, sans-serif',
                            fontSize: 13
                        },

                        // Chart animation duration
                        animationDuration: 750,

                        // Setup grid
                        grid: {
                            left: 10,
                            right: 10,
                            top: 35,
                            bottom: 60,
                            containLabel: true
                        },

                        // Add legend
                        legend: {
                            data: ['Daily Stats'],
                            itemHeight: 8,
                            itemGap: 20
                        },

                        // Tooltip
                        tooltip: {
                            trigger: 'axis',
                            backgroundColor: 'rgba(0,0,0,0.75)',
                            padding: [10, 15],
                            textStyle: {
                                fontSize: 13,
                                fontFamily: 'Roboto, sans-serif'
                            },
                            axisPointer: {
                                type: 'shadow',
                                shadowStyle: {
                                    color: 'rgba(0,0,0,0.025)'
                                }
                            }
                        },

                        // Horizontal axis
                        xAxis: [{
                            type: 'category',
                            data: ['Sunday','Monday','Tuesday','Wednesday', 'Thursday', 'Friday','Sartuday'],
                            axisLabel: {
                                color: '#333'
                            },
                            axisLine: {
                                lineStyle: {
                                    color: '#999'
                                }
                            },
                            splitLine: {
                                show: true,
                                lineStyle: {
                                    color: '#eee',
                                    type: 'dashed'
                                }
                            },
                            splitArea: {
                                show: true,
                                areaStyle: {
                                    color: ['rgba(250,250,250,0.1)', 'rgba(0,0,0,0.015)']
                                }
                            }
                        }],

                        // Vertical axis
                        yAxis: [
                            {
                                type: 'value',
                                name: 'GDP（million)',
                                max: 53500,
                                axisLabel: {
                                    color: '#333'
                                },
                                axisLine: {
                                    lineStyle: {
                                        color: '#999'
                                    }
                                },
                                splitLine: {
                                    show: true,
                                    lineStyle: {
                                        color: '#eee'
                                    }
                                }
                            },
                            {
                                type: 'value',
                                name: 'Other（million)',
                                axisLabel: {
                                    color: '#333'
                                },
                                axisLine: {
                                    lineStyle: {
                                        color: '#999'
                                    }
                                },
                                splitLine: {
                                    show: true,
                                    lineStyle: {
                                        color: '#f5f5f5'
                                    }
                                }
                            }
                        ],

                        // Add series
                        series: [
                            {
                                name: 'GDP',
                                type: 'bar',
                                markLine: {
                                    symbol: ['arrow','none'],
                                    symbolSize: [4, 2],
                                    itemStyle: {
                                        normal: {
                                            lineStyle: {color: 'orange'},
                                            barBorderColor: 'orange',
                                            label: {
                                                position: 'left',
                                                formatter: function(params) {
                                                    return Math.round(params.value);
                                                },
                                                textStyle: {color: 'orange'}
                                            }
                                        }
                                    },
                                    data: [{type: 'average', name: 'Average'}]
                                },
                                data: dataMap.dataGDP['2010']
                            },
                            {
                                name: 'Financial',
                                yAxisIndex: 1,
                                type: 'bar',
                                data: dataMap.dataFinancial['2010']
                            },
                            {
                                name: 'Real Estate',
                                yAxisIndex: 1,
                                type: 'bar',
                                data: dataMap.dataEstate['2010']
                            }
                        ]
                    },

                    // 2011 data
                    {
                        series: [
                            {data: dataMap.dataGDP['2011']},
                            {data: dataMap.dataFinancial['2011']},
                            {data: dataMap.dataEstate['2011']}
                        ]
                    },

                    // 2012 data
                    {
                        series: [
                            {data: dataMap.dataGDP['2012']},
                            {data: dataMap.dataFinancial['2012']},
                            {data: dataMap.dataEstate['2012']}
                        ]
                    },

                    // 2013 data
                    {
                        series: [
                            {data: dataMap.dataGDP['2013']},
                            {data: dataMap.dataFinancial['2013']},
                            {data: dataMap.dataEstate['2013']}
                        ]
                    },

                    // 2014 data
                    {
                        series: [
                            {data: dataMap.dataGDP['2014']},
                            {data: dataMap.dataFinancial['2014']},
                            {data: dataMap.dataEstate['2014']}
                        ]
                    }
                ]
            });
        }


        //
        // Resize charts
        //

        // Resize function
        var triggerChartResize = function() {
            columns_timeline_element && columns_timeline.resize();
        };

        // On sidebar width change
        var sidebarToggle = document.querySelector('.sidebar-control');
        sidebarToggle && sidebarToggle.addEventListener('click', triggerChartResize);

        // On window resize
        var resizeCharts;
        window.addEventListener('resize', function() {
            clearTimeout(resizeCharts);
            resizeCharts = setTimeout(function () {
                triggerChartResize();
            }, 200);
        });
    };


    //
    // Return objects assigned to module
    //

    return {
        init: function() {
            _columnsTimelineLightExample();
        }
    }
}();


// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function() {
    EchartsColumnsTimelineLight.init();
});
