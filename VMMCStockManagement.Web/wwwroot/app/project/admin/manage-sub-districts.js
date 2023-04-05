var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#manage-sub-districts',
    created: function () {
       
        this.getProvinces();
        this.getSubDistricts();
       
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
 
        countries: [],
        provinces: [],
        districts: [],
        subDistricts: [],
       
        countryId: '',
        provinceId: '',
        districtId: '',
        subDistrict: {
            id: 0,
            name: '',
            districtId: '',
            userId: '',
            countryId: '',
            provinceId: '',
        },
        hasError: false,
        selectedSubDistrict: '',
       
    },
    validations: {
        subDistrict: {
            name: {
                required: required,
            },
            provinceId: {
                required: required,
            },
            districtId: {
                required: required,
            }
        }
    },
    methods: {


        save: function () {
            this.$v.subDistrict.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.subDistrict.$pending || this.$v.subDistrict.$error) {
                this.hasError = true;
                return;
            }

            this.subDistrict.userId = this.userId;
            //alert('UserId: ' + this.userId);
            if (this.subDistrict.id && this.subDistrict.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/SubDistrict/update', this.subDistrict)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        //me.successNotify("Successfully updated");
                        me.getSubDistricts();

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
            this.createData(this.ApiUrl + '/api/SubDistrict/create', this.subDistrict)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");


                        $('#createModal').modal('hide');

                        //if (me.districtId) {
                            me.getSubDistricts();
                        //}


                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        deleteSubDistrict: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedSubDistrict.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/subdistrict/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getSubDistricts();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
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

        getCountries: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/country/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.countries = response.data.data;
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
            this.getProvinces();

        },

        onProvinceChange: function (event) {
            this.provinceId = event.target.value;
            this.districts = [];
            //this.subDistricts = [];
            this.getDistricts();

        },

        onDistrictChange: function (event) {
            this.districtId = event.target.value;
            //this.subDistricts = [];
            //this.getSubDistricts();

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
