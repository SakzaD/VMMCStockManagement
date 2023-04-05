var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#department',
    created: function () {
        //this.getProvinces();
        this.getDepartments();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        departments: [],
        operationId: '',
        department: {
            id: 0,
            name: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedDepartment: {
            id: 0,
            name: '',
            userId: ''
        }
    },
    validations: {
        department: {
            name: {
                required: required
            },
           
        },
        selectedDepartment: {
            name: {
                required: required
            },

        }
    },
    methods: {

        clearDapartment: function () {
            this.department = {
                id: 0,
                name: '',
                userId: ''
            };
        },
        save: function () {

            this.$v.department.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.department.$pending || this.$v.department.$error) {
                this.hasError = true;
                return;
            }

            this.department.userId = this.userId;
            this.create();
        },

        edit: function () {

            this.$v.selectedDepartment.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedDepartment.$pending || this.$v.selectedDepartment.$error) {
                this.hasError = true;
                return;
            }

            this.selectedDepartment.userId = this.userId;
            this.update();
        },
        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/department/update', this.selectedDepartment)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getDepartments();

                        //title, message, type
                        me.notify("Success","Department successfully updated.", "success");

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
            this.createData(this.ApiUrl + '/api/department/create', this.department)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getDepartments();
                        me.notify("Success", "Department successfully added.", 'success');

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
        deleteDepartment: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedDepartment.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/department/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getDepartments();

                        //title, message, type
                        me.notify("Success", "Department was successfully deleted.", "success");

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
  
        getDepartments: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/department/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                   
                        var dT = $('#tblDepartment').DataTable();
                        dT.destroy();

                        me.departments = response.data.data;

                        me.$nextTick(function () {
                            $('#tblDepartment').DataTable(
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
