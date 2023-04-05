var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#asset-report',
    created: function () {

        this.getAssets();
        this.getGrants();
        this.getDepartments();
        this.getLocations();
        this.getUsers();
    },

    mounted: function () {
        //this.userId = $('#userId').val();
    },

    data: {

        locations: [],
        grants: [],
        departments: [],
        assetCategories: [],
        users: [],

        reportFilter: {

            locationId: '',
            grantId: '',
            departmentId: '',
            assetCategoryId: '',
            userId: '',
            userAssignedId: '',
          
            startDate: '',
            endDate: '',
            registrationNumber: '',
            serialNumber: '',
        },
        reportData: []
    },

    methods: {

        generateReport: function () {
            let me = this;

            this.reportFilter.userId = this.userId;

            console.log(me.reportFilter.userId);
            let btnGenerate = Ladda.create(document.querySelector('#btnGenerateReport'));
            btnGenerate.start();

            me.reportData = [];


            me.readData(this.ApiUrl + '/api/Report/generate-asset-report?locationId=' + me.reportFilter.locationId + '&grantId=' + me.reportFilter.locationId + '&departmentId=' + me.reportFilter.locationId
                + '&assetCategoryId=' + me.reportFilter.assetCategoryId + '&userId=' + me.reportFilter.userId + '&userAssignedId=' + me.reportFilter.userAssignedId + '&startDate=' + me.reportFilter.startDate
                + '&endDate=' + me.reportFilter.endDate + '&registrationNumber=' + me.reportFilter.registrationNumber + '&serialNumber=' + me.reportFilter.serialNumber)
                .then(function (response) {
                    btnGenerate.stop();

                    if (response.data.status == 'success') {

                        var dT = $('#tblReport').DataTable();
                        dT.destroy();

                        me.reportData = response.data.data;

                        me.$nextTick(function () {
                            $('#tblReport').DataTable(
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

        getAssets: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/asset/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.assetCategories = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getUsers: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/users/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.users = response.data.data;
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

        getGrants: function () {
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

        getLocations: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/facility/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.locations = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },


        getParameterByName: function (name, url) {

            if (!url) url = window.location.href;

            name = name.replace(/[\[\]]/g, '\\$&');

            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                results = regex.exec(url);

            if (!results) return null;

            if (!results[2]) return '';

            return decodeURIComponent(results[2].replace(/\+/g, ' '));

        },
    }
});
