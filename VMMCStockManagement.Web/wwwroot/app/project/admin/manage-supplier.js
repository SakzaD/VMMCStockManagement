var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#supplier',
    created: function () {

        this.getSuppliers();

    },

    data: {

        suppliers: [],
        supplier: {
            id: 0,
            name: '',
            description: '',
            userId: '',

        },

        selectedSupplier:  {
            id: 0,
            name: '',
            description: '',
            userId: '',

        },
        hasError: false,

    },
    validations: {
        supplier: {
            name: {
                required: required
            },
        },
        selectedSupplier: {
            name: {
                required: required
            },
        },
    },
    methods: {

        clearSupplier: function () {
            this.supplier = {
                id: 0,
                name: '',
                description: '',
                userId: '',

            };
        },
        save: function () {

            this.$v.supplier.$touch();

            if (this.$v.supplier.$pending || this.$v.supplier.$error) {
                this.hasError = true;
                return;
            }

            this.supplier.userId = this.userId;
          
            this.create();
            
         
        },

        edit: function () {

            this.$v.selectedSupplier.$touch();

            if (this.$v.selectedSupplier.$pending || this.$v.selectedSupplier.$error) {
                this.hasError = true;
                return;
            }

            this.selectedSupplier.userId = this.userId;
       
            this.update();
            
         
        },

        create: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();

            this.createData(this.ApiUrl + '/api/supplier/create', this.supplier)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAdd').modal('hide');
                        me.getSuppliers();
                        me.notify("Success", "Supplier successfully added.", 'success');

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

        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/supplier/update', this.selectedSupplier)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getSuppliers();

                        //title, message, type
                        me.notify("Success", "Supplier successfully updated.", "success");

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

        deleteSupplier: function () {
            let me = this;

            var request = {
                id: this.selectedSupplier.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/supplier/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getSuppliers();

                        //title, message, type
                        me.notify("Success", "Supplier was successfully deleted.", "success");

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

        getSuppliers: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/supplier/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                   

                        var dT = $('#tblSupplier').DataTable();
                        dT.destroy();

                        me.suppliers = response.data.data;

                        me.$nextTick(function () {
                            $('#tblSupplier').DataTable(
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
