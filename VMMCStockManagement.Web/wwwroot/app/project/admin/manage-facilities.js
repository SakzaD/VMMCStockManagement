var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#facility',
    created: function () {
        //this.getReportList();
        //this.getProvinces();
        this.getFacilities();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
  
        facilities: [],

        facility: {
            id: 0,
            name: '',
            countryId: '',
            provinceId: '',
            districtId: '',
            subDistrictId: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedFacility: {
            id: 0,
            name: '',
            countryId: '',
            provinceId: '',
            districtId: '',
            subDistrictId: '',
            userId: ''
        }
    },
    validations: {
        facility: {
            name: {
                required: required,
            }
        },
        selectedFacility: {
            name: {
                required: required,
            }
        }
    },
    methods: {

        clearFacility: function () {
            this.facility = {
                id: 0,
                name: '',
                countryId: '',
                provinceId: '',
                districtId: '',
                subDistrictId: '',
                userId: ''
            }
        },
        save: function () {

            this.$v.facility.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.facility.$pending || this.$v.facility.$error) {
                this.hasError = true;
                return;
            }

            this.facility.userId = this.userId;
            this.create();
        },
        edit: function () {

            this.$v.selectedFacility.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedFacility.$pending || this.$v.selectedFacility.$error) {
                this.hasError = true;
                return;
            }
            this.selectedFacility.userId = this.userId;
            this.update();

        },
        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/Facility/update', this.selectedFacility)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.notify("Success", "Facility was successfully updated.", "success");
                        me.getFacilities();

                    } else {
                        me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                    }
                })
                .catch(function (error) {
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        create: function () {
            let me = this;

            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();

            this.createData(this.ApiUrl + '/api/Facility/create', this.facility)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        me.notify("Success", "Facility was successfully added.", "success");

                        $('#modalAddNew').modal('hide');

                        //if (me.subDistrictId) {
                        //    me.getFacilities();
                        //}


                        me.getFacilities();


                    } else {
                        me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        deleteFacility: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedFacility.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/facility/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalDelete').modal('hide');
                        me.notify("Success", "Facility was successfully deleted.", "success");
                        me.getFacilities();

                    } else {
                        me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
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
        getOperations: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/operation/filter?subDistrictId='+this.subDistrictId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.operations = response.data.data;
                        console.log(me.operations);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getCountries: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/country/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.countries = response.data.data;
                        console.log(me.reportList);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getProvinces: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/province/filter?countryId='+this.countryId)
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

        getDistricts: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/district/filter?provinceId=' + this.provinceId)
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
        getSubDistricts: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/sub-district/filter?districtId=' + this.districtId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.subDistricts = response.data.data;
                        console.log(me.subdistricts);
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

                        var dT = $('#tblFacility').DataTable();
                        dT.destroy();

                        me.facilities = response.data.data;
                     
                        me.$nextTick(function () {
                            $('#tblFacility').DataTable(
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

        onCountryChange: function (event) {
            this.countryId = event.target.value;
            this.provinces = [];
            this.districts = [];
            this.subDistricts = [];
            this.facilities = [];
            this.getProvinces();

        },

        onProvinceChange: function (event) {
            this.provinceId = event.target.value;
            this.districts = [];
            this.subDistricts = [];
            this.operations = [];
            //this.facilities = [];
            this.getDistricts();

        },

        onDistrictChange: function (event) {
            this.districtId = event.target.value;
            this.subDistricts = [];
            this.operations = [];
            //this.facilities = [];
            this.getSubDistricts();

        },
        onSubDistrictChange: function (event) {
            this.subDistrictId = event.target.value;
            this.operations = [];
            this.getOperations();

            console.log('Sub District Change');
            //this.getFacilities();

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
