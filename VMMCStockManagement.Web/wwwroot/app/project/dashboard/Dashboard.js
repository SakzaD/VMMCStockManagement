
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#dashboard',
    created: function () {
        this.getDashboardStats();
    },
    mounted: function () {

        this.getNotifications();
        this.getFacilities();
        this.getDepartments();
        this.getAssetCategories();
    },
    data: {
        departments: [],
        facilities: [],
        assetCategories: [],
        dashboard: {
            totalRequest: 0,
            totalReturn: 0,
            totalTransfer: 0,
            totalRepair: 0,
            totalDamageOrLoss: 0,
            totalDisposal: 0,
            totalReplacement: 0,
            totalAllAssets: 0,
            dailyStats: [],
            monthlyStats: []
        },
        notification: {
            numberOfRequest : 0
        },
        selectedItem: {}

    },
    methods: {
        filterDashboard: function () {

           

          
        },
        filterDashboard: function () {
            let me = this;
            //let btnFilterReport = Ladda.create(document.querySelector('#btnFilterDashboard'));
            //btnFilterReport.start();
            me.getDashboardStats();
        },
        getAssetCategories: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/asset-category/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.assetCategories = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getFacilities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/facility/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.facilities = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getDepartments: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/department/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.departments = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getDashboardStats: function () {
            let me = this;
            let btnFilterReport = Ladda.create(document.querySelector('#btnFilterDashboard'));
            btnFilterReport.start();
            me.readData(this.ApiUrl + '/api/Dashboard/stats')
                .then(function (response) {
                    btnFilterReport.stop();
                    if (response.data.status == 'success') {

                        me.dashboard = response.data.data;

                        if (me.dashboard.dailyStats)
                            me.updateMonthlyStats(me.dashboard.monthlyStats);

                    }
                })
                .catch(function (error) {
                    console.log(error);
                    btnFilterReport.stop();
                });
        },

        getNotifications: function () {
            let me = this;

            me.readData(this.ApiUrl + '/api/Dashboard/notification')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        console.log(response.data.data);
                        me.notification = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        updateDailyStats: function (dataList) {
            var chart = echarts.init(document.getElementById('columns_daily'));

            var totals = [];
            var days = [];

            for (i = 0; i < dataList.length; i++) {
                days.push(dataList[i].month);
                totals.push(dataList[i].total);
            }

            chart.setOption({
                grid3D: {},
                xAxis: [
                    {
                        type: 'category',
                        data: days
                    }
                ],
                yAxis3D: {},
                zAxis3D: {},
                series: [{
                    type: 'bar',
                    symbolSize: 50,
                    data: totals,
                    itemStyle: {
                        opacity: 1
                    }
                }]
            });
        },
        updateMonthlyStats: function (dataList) {
            var chart = echarts.init(document.getElementById('columns_monthly'));

            var totals = [];
            var months = [];

            for (i = 0; i < dataList.length; i++) {
                months.push(dataList[i].month);
                totals.push(dataList[i].total);
            }

            chart.setOption({
                grid3D: {},
                xAxis: [
                    {
                        type: 'category',
                        data: months
                    }
                ],
                yAxis3D: {},
                zAxis3D: {},
                series: [{
                    type: 'bar',
                    symbolSize: 50,
                    data: totals,
                    itemStyle: {
                        opacity: 1
                    }
                }]
            });
        },
    }
});
