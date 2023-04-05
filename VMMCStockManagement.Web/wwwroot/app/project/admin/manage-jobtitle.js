var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#jobTitle',
    created: function () {
       
        this.getJobTitles();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        jobTitles: [],
        operationId: '',
        jobTitle: {
            id: 0,
            name: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedJobTitle: {
            id: 0,
            name: '',
            userId: ''
        }
    },
    validations: {
        jobTitle: {
            name: {
                required: required
            },
           
        },
        selectedJobTitle: {
            name: {
                required: required
            },

        }
    },
    methods: {

        clearJobTitle: function () {
            this.jobTitle = {
                id: 0,
                name: '',
                userId: ''
            };
        },
        save: function () {

            this.$v.jobTitle.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.jobTitle.$pending || this.$v.jobTitle.$error) {
                this.hasError = true;
                return;
            }

            this.jobTitle.userId = this.userId;
            this.create();
        },

        edit: function () {

            this.$v.selectedJobTitle.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedJobTitle.$pending || this.$v.selectedJobTitle.$error) {
                this.hasError = true;
                return;
            }

            this.selectedJobTitle.userId = this.userId;
            this.update();
        },
        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/JobTitle/update', this.selectedJobTitle)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getJobTitles();

                        //title, message, type
                        me.notify("Success","Job Title successfully updated.", "success");

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
            this.createData(this.ApiUrl + '/api/JobTitle/create', this.jobTitle)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getJobTitles();
                        me.notify("Success", "Job Title successfully added.", 'success');

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
        deleteJobTitle: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedJobTitle.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/JobTitle/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getJobTitles();

                        //title, message, type
                        me.notify("Success", "Job Title was successfully deleted.", "success");

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
  
        getJobTitles: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/JobTitle/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                       
                        var dT = $('#tblJobTitle').DataTable();
                        dT.destroy();

                        me.jobTitles = response.data.data;

                        me.$nextTick(function () {
                            $('#tblJobTitle').DataTable(
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
