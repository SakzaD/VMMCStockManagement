var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#grant',
    created: function () {
       
        this.getGrants();
        this.getCountries();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        grants: [],
        countries: [],
        operationId: '',
        grant: {
            id: 0,
            name: '',
            countryId: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedGrant: {
            id: 0,
            name: '',
            countryId: '',
            userId: ''
        }
    },
    validations: {
        grant: {
            name: {
                required: required
            },

            countryId: {
                required: required
            },
           
        },
        selectedGrant: {
            name: {
                required: required
            },

            countryId: {
                required: required
            },

        }
    },
    methods: {

        clearGrant: function () {
            this.grant = {
                id: 0,
                name: '',
                countryId: '',
                userId: ''
            };
        },
        save: function () {

            this.$v.grant.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.grant.$pending || this.$v.grant.$error) {
                this.hasError = true;
                return;
            }

            this.grant.userId = this.userId;
            this.create();
        },

        edit: function () {

            this.$v.selectedGrant.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedGrant.$pending || this.$v.selectedGrant.$error) {
                this.hasError = true;
                return;
            }

            this.selectedGrant.userId = this.userId;
            this.update();
        },

        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/grant/update', this.selectedGrant)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getGrants();

                        //title, message, type
                        me.notify("Success","Grant successfully updated.", "success");

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                         else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },

        create: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/grant/create', this.grant)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getGrants();
                        me.notify("Success", "Grant successfully added.", 'success');

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

        deleteGrant: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedGrant.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/grant/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getGrants();

                        //title, message, type
                        me.notify("Success", "Grant was successfully deleted.", "success");

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
  
        getGrants: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/grant/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {


                        var dT = $('#tblGrants').DataTable();
                        dT.destroy();

                        me.grants = response.data.data;

                        me.$nextTick(function () {
                            $('#tblGrants').DataTable(
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
