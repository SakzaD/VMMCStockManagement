var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#reasons',
    created: function () {
        //this.getProvinces();
        this.getReasons();
        this.getCategoryReasons();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        reasons: [],
        reasonCategories: [],
        reason: {
            id: 0,
            description: '',
            userId: '',
            reasonCategoryId: ''

        }
        ,
        hasError: false,
        selectedReason: {
            id: 0,
            description: '',
            userId: '',
            reasonCategoryId: ''
        }
    },
    validations: {
        reason: {
            description: {
                required: required
            },
            reasonCategoryId: {
                required: required
            },
           
        },
        selectedReason: {
            description: {
                required: required
            },
            reasonCategoryId: {
                required: required
            },

        }
    },
    methods: {

        clearReason: function () {
            this.reason = {
                id: 0,
                description: '',
                userId: ''
            };
        },
        save: function () {

            this.$v.reason.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.reason.$pending || this.$v.reason.$error) {
                this.hasError = true;
                return;
            }

            this.reason.userId = this.userId;
            this.create();
        },

        edit: function () {

            this.$v.selectedReason.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedReason.$pending || this.$v.selectedReason.$error) {
                this.hasError = true;
                return;
            }

            this.selectedReason.userId = this.userId;
            this.update();
        },
        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/Reason/update', this.selectedReason)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getReasons();

                        //title, message, type
                        me.notify("Success","Reason successfully updated.", "success");

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
            this.createData(this.ApiUrl + '/api/Reason/create', this.reason)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getReasons();
                        me.notify("Success", "Reason successfully added.", 'success');

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        deleteReason: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedReason.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/Reason/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getReasons();

                        //title, message, type
                        me.notify("Success", "Reason was successfully deleted.", "success");

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
     
        getCategoryReasons: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/reason-category/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.reasonCategories = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
         getReasons: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/reason/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                       
                        var dT = $('#tblReason').DataTable();
                        dT.destroy();

                     
                        me.reasons = response.data.data;

                        me.$nextTick(function () {
                            $('#tblReason').DataTable(
                                {
                                    dom: "Bfrtip", buttons: ["copy", "csv", "excel", "print"]
                                }
                            );
                        })

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
