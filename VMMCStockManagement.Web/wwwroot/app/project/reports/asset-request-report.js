
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#asset-request-report',
    created: function () {

        this.getGrant();
        this.getDepartment();
        this.getJobTitle();
    },
    mounted: function () {
        //this.userId = $('#userId').val();
    },
    data: {     
        reportData: [],
        jobTitles: [],
        departments: [],
        grants: [],

        reportFilters: {

            departmentId: '',
            grantId: '',
            jobTitleId: '',
            startDate: '',
            endDate: ''
        }
    },

    methods: {
 
        generateReport: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/report/request-asset-report', this.reportFilters)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        var dT = $('#buttons-datatables').DataTable();
                        dT.destroy();

                        me.reportData = response.data.data;

                        me.$nextTick(function () {
                            $('#buttons-datatables').DataTable(
                                {
                                    dom: "Bfrtip", buttons: ["copy", "csv", "excel", "print", "pdf"]
                                }
                            );
                        })
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getGrant: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/grant/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.grants = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getDepartment: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/department/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.departments = response.data.data;
                        console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getJobTitle: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/jobtitle/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.jobTitles = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
    }
});

