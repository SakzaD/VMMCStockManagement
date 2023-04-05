var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#manage-provinces',
    created: function () {

        //this.getCountries();
        this.getProvinces();


    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
        userId: '',
        countryId: '',
        countries: [],
        provinces: [],
        province: {
            id: 0,
            name: '',
            countryId : 1,
            userId : '',
        },
        hasError: false,
        selectedProvince: '',
    },
    validations: {
        province: {
            name: {
                required: required,
            },
            countryId: {
                required: required,
            }
        }
    },
    methods: {

        clearProvince: function () {
            province.id = 0;
            province.name = '';
            province.countryId = 1;
        },
        save: function () {
            this.$v.province.$touch();
            // if its still pending or an error is returned do not submit


            if (this.$v.province.$pending || this.$v.province.$error) {
                this.hasError = true;
                return;
            }

            this.province.userId = this.userId;
            //alert('UserId: ' + this.userId);
            if (this.province.id && this.province.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/Province/update', this.province)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        //me.successNotify("Successfully updated");
                        //me.getProvinces();

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
            this.createData(this.ApiUrl + '/api/Province/create', this.province)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#addNew').modal('hide');
                        //me.successNotify("Successfully created");

                        //if (me.countryId) {
                        //    me.getProvinces();
                        //}

                        me.getProvinces();
                      
                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        deleteProvince: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedProvince.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/province/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getProvinces();

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
                        console.log(me.reportList);
                    }
                })
                .catch(function (error) {
                    console.log(error);
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

        onCountryChange: function (event) {
            this.countryId = event.target.value;
            this.provinces = [];
            this.getProvinces();

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
