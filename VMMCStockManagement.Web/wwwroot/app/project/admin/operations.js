var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#operations',
    created: function () {
        this.getProvinces();
        this.getOperations();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        provinces: [],
        districts: [],
        subDistricts: [],
        operations: [],
        countryId: 1,
        provinceId: '',
        districtId: '',
        subDistrictId: '',
        operationId: '',
        operation: {
            id: 0,
            name: '',
            userId: '',
            subDistrictId: '',
            districtId: '',
            provinceId: '',
        }
        ,
        hasError: false,
        selectedOperation: ''
    },
    validations: {
        operation: {
            name: {
                required: required,
            },
            subDistrictId: {
                required: required,
            }
        }
    },
    methods: {


        save: function () {

            this.$v.operation.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.operation.$pending || this.$v.operation.$error) {
                this.hasError = true;
                return;
            }

            this.operation.userId = this.userId;
            //alert('UserId: ' + this.userId);
            if (this.operation.id && this.operation.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/operation/update', this.operation)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        me.successNotify("Successfully updated");
                        me.getFacilities();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        create: function () {
            let me = this;
            this.createData(this.ApiUrl + '/api/operation/create', this.operation)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");

                        $('#createModal').modal('hide');

                        if (me.subDistrictId) {
                            me.getOperations();
                        }


                        me.getOperations();


                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        deleteOperation: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedOperation.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/operation/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getOperations();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
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
        getOperations: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/operation/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.operations = response.data.data;
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
