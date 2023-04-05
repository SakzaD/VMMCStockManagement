var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#manage-districts',
    created: function () {
        //this.getReportList();
 
        this.getProvinces();
        this.getDistricts();
        
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
   

        countries: [],
        provinces: [],
        districts: [],
        subdistricts: [],
        facilities: [],

        countryId: 1,
        provinceId: '',

        district: {
            id: 0,
            name: '',
            provinceId: '',
            countryId: '',
            userId: ''
        },
        selectedDistrict: '',
        hasError: false
   
    },
    validations: {
        district: {
            name: {
                required: required,
            },
            provinceId: {
                required: required,
            }
        }
    },
    methods: {

        deleteDistrict: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedDistrict.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/district/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getDistricts();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        save: function () {
            this.$v.district.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.district.$pending || this.$v.district.$error) {
                this.hasError = true;
                return;
            }
            this.district.userId = this.userId;
            //alert('UserId: ' + this.userId);
         
            if (this.district.id && this.district.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/District/update-data', this.district)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        //me.successNotify("Successfully updated");
                        me.getDistricts();

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
            this.createData(this.ApiUrl + '/api/District/create', this.district)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");

                        $('#addNew').modal('hide');

                        //if (me.provinceId) {
                            me.getDistricts();
                        //}


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
                        console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getDistricts: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/district/filter')
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

        onCountryChange: function (event) {
            this.countryId = event.target.value;
            this.provinces = [];
            this.districts = [];
            this.getProvinces(this.countryId);

        },
        onProvinceChange: function (event) {
            this.provinceId = event.target.value;
            this.districts = [];
            this.getDistricts(this.provinceId);

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
