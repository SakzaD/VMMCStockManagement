
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#report-page',
  
    created: function () {
        //this.getReportList();

/*
        var id = this.getParameterByName('id');

        if (id) {
            this.getReportById(id);
        }*/
        this.getAgeGroup();
        this.getProvinces();
        this.getKAndVTypes();
        this.getPackageService();
        this.getImplementingPartners();
        this.getEthnicities();
        this.getOperations();
        //this.getActivities();
        //this.getDeliveryModels();
        //this.getTechnicalAreas();
        //this.getModalities();
        //this.getUsers();
    
    },

    data: {
        userId: '',
        capturedData: {
            id: 0,
            userId: '',
            uuId: '',
            captureDate: '',
            ageGroupId: '',
            genderId: '',
            kAndVPopTypeId: '',
            servicePackageId: '',
            implementingPartnerId: '',
            ethnicityId: '',
            provinceId: '',
            districtId: '',
            subDistrictId: '',
            operationId: '',
            homeTown: '',
            catchmentClinicId: '',
            additionalNeeds: '',
            referredOnward: ''
        },
        ageGroups: [],
        kAndVTypes: [],
        packageService: [],
        implementingPartners: [],
        ethnicities: [],
        provinces: [],
        districts: [],
        subdistricts: [],
        operations: [],
        catchmentClinics: [],
        facilities: [],
        report: {
            startDate: '',
            endDate: '',
            angeGroupId: '',
            genderId: '',
            kAndVTypesId: '',
            implementingPartnerId: '',
            userId: '',
        },
        reportData: []

    },

  
    methods: {

        isNumeric: function (n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        },

        generateReport: function () {
            let me = this;

            me.report.userId = this.userId;
            console.log(this.ApiUrl);
            me.readData(this.ApiUrl + '/api/report/filter?startDate=' +
                me.report.startDate + '&endDate=' + me.report.endDate
                + "&angeGroupId=" + me.report.angeGroupId + "&genderId=" + me.report.genderId
                + "&kAndVTypesId=" + me.report.kAndVTypesId + "&implementingPartnerId=" +
                me.report.implementingPartnerId + "&userId=" + me.userId)
                .then(function (response) {


                    if (response.data.status == 'success') {

                        $('#example1').DataTable().clear().destroy();
                       
                        me.reportData = response.data.data;

                        var table = $("#example1").DataTable({
                            "responsive": true, "lengthChange": false, "autoWidth": true,
                            "buttons": ["copy", "csv", "excel", "pdf", "print"]
                        });
                        
                        table.buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getReportById: function (id) {
            let me = this;


            me.readData(this.ApiUrl + '/api/DataCapture/get-by-id?id=' + id)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.reportingData = response.data.data;
                        console.log(me.reportList);


                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getEthnicities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/ethnicity/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.ethnicities = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },getOperations: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/operation/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.operations = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }, getAgeGroup: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/agegroup/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.ageGroups = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getKAndVTypes: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/kabdvtypes/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.kAndVTypes = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }, getPackageService: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/packageservice/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.packageService = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }, getImplementingPartners: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/implementingpartner/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.implementingPartners = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getProvinces: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/province/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.provinces = response.data.data;
                        console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getDistricts: function (provinceId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/district/filter?provinceId=' + provinceId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.districts = response.data.data;
                        console.log(me.districts);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getSubDistricts: function (districtId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/sub-district/filter?districtId=' + districtId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.subdistricts = response.data.data;
                        console.log(me.subdistricts);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getFacilities: function (operationId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/facility/filter?operationId=' + operationId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.facilities = response.data.data;
                        console.log(me.deliveryModels);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },


        onProvinceChange: function (event) {
            var provinceId = event.target.value;
            this.districts = [];
            this.subdistricts = [];
            this.facilities = [];
            this.getDistricts(provinceId);

        },

        onOperationChange: function (event) {
            var operationId = event.target.value;
            this.facilities = [];
            this.getFacilities(operationId);

        },

        onDistrictChange: function (event) {
            var districtId = event.target.value;
            this.subdistricts = [];
            this.facilities = [];
            this.getSubDistricts(districtId);

        },

        onSubDistrictChange: function (event) {
            var subDistrictId = event.target.value;
            this.facilities = [];
            this.getFacilities(subDistrictId);

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
