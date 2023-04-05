var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#locations',
    created: function () {
        //this.getProvinces();
        this.getLocations();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        locations: [],
        operationId: '',
        location: {
            id: 0,
            name: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedLocation: {
            id: 0,
            name: '',
            userId: ''
        }
    },
    validations: {
        location: {
            name: {
                required: required
            },
           
        },
        selectedLocation: {
            name: {
                required: required
            },

        }
    },
    methods: {


        save: function () {

            this.$v.location.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.location.$pending || this.$v.location.$error) {
                this.hasError = true;
                return;
            }

            this.location.userId = this.userId;
            this.create();
        },

        edit: function () {

            this.$v.selectedLocation.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedLocation.$pending || this.$v.selectedLocation.$error) {
                this.hasError = true;
                return;
            }

            this.selectedLocation.userId = this.userId;
            this.update();
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/location/update', this.selectedLocation)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getLocations();

                        //title, message, type
                        me.notify("Success","Location successfully updated.", "success");

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                         else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        create: function () {
            let me = this;
            this.createData(this.ApiUrl + '/api/Location/create', this.location)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getLocations();
                        me.notify("Success", "Location successfully added.", 'success');

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                    }
                })
                .catch(function (error) {
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        deleteLocation: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedLocation.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/location/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getLocations();

                        //title, message, type
                        me.notify("Success", "Location was successfully deleted.", "success");

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        getProvinces: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/province/filter?countryId=' + this.countryId)
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
        getLocations: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/location/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                       
                         $('#tblLocation').DataTable().clear().destroy();
                        //var table = $('#tblLocation');

                     
                        me.locations = response.data.data;

                        me.$nextTick(function () {
                            $('#tblLocation').DataTable(
                                {
                                    dom: "Bfrtip", buttons: ["copy", "csv", "excel", "print"]
                                }
                            );
                        })

                        //$('#tblLocation').DataTable(/*{
                        //    dom: 'Bfrtip',
                        //    buttons: [
                        //        'copy', 'csv', 'excel'
                        //    ],
                        //    order: false,
                        //    "pageLength": 50
                        //}*/);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        onProvinceChange: function (event) {
            this.provinceId = event.target.value;
            this.districts = [];
            this.subDistricts = [];
            //this.operations = [];
            //this.facilities = [];
            this.getDistricts();

        },

        onDistrictChange: function (event) {
            this.districtId = event.target.value;
            this.subDistricts = [];
            //this.operations = [];
            //this.facilities = [];
            this.getSubDistricts();

        },
        onSubDistrictChange: function (event) {
            this.subDistrictId = event.target.value;
            //this.operations = [];
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
