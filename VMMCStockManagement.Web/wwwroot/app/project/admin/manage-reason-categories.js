var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#reason-categories',
    created: function () {
        //this.getProvinces();
        this.getReasonCategories();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        reasons: [],
        reasonCategories: [],
        reasonCategory: {
            id: 0,
            description: '',
            userId: '',

        }
        ,
        hasError: false,
        selectedReasonCategory: {
            id: 0,
            description: '',
            userId: ''
        }
    },
    validations: {
        reasonCategory: {
            description: {
                required: required
            },
           
           
        },
        selectedReasonCategory: {
            description: {
                required: required
            },
           

        }
    },
    methods: {

        clearReasoncategory: function () {
            this.reasonCategory = {
                id: 0,
                description: '',
                userId: ''
            };
        },
        save: function () {

            this.$v.reasonCategory.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.reasonCategory.$pending || this.$v.reasonCategory.$error) {
                this.hasError = true;
                return;
            }

            this.reasonCategory.userId = this.userId;
            this.create();
        },

        edit: function () {

            this.$v.selectedReasonCategory.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedReasonCategory.$pending || this.$v.selectedReasonCategory.$error) {
                this.hasError = true;
                return;
            }

            this.selectedReasonCategory.userId = this.userId;
            this.update();
        },
        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/ReasonCategory/update', this.selectedReasonCategory)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getReasonCategories();

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
            this.createData(this.ApiUrl + '/api/ReasonCategory/create', this.reasonCategory)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getReasonCategories();
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
        deleteReasonCategory: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedReasonCategory.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/ReasonCategory/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getReasonCategories();

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
     
         getReasonCategories: function () {
            let me = this;
             me.readData(this.ApiUrl + '/api/Lookup/reason-category/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                       
                        var dT = $('#tblReasonCategory').DataTable();
                        dT.destroy();

                     
                        me.reasonCategories = response.data.data;

                        me.$nextTick(function () {
                            $('#tblReasonCategory').DataTable(
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
