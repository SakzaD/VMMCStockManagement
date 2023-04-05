var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#asset-category',
    created: function () {
       
        this.getCategories();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {

        categories: [],
        operationId: '',
        category: {
            id: 0,
            name: '',
            description: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedCategory: {
            id: 0,
            name: '',
            description: '',
            userId: ''
        }
    },
    validations: {
        category: {
            name: {
                required: required
            },

            description: {
                required: required
            },
           
        },
        selectedCategory: {
            name: {
                required: required
            },

            description: {
                required: required
            },

        }
    },
    methods: {

        clearCategory: function () {
            this.category = {
                id: '',
                name: '',
                description: '',
                userId: ''
            };
        },
  
        save: function () {

            this.$v.category.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.category.$pending || this.$v.category.$error) {
                this.hasError = true;
                return;
            }

            this.category.userId = this.userId;
            this.create();
        },

        edit: function () {

            this.$v.selectedCategory.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedCategory.$pending || this.$v.selectedCategory.$error) {
                this.hasError = true;
                return;
            }

            this.selectedCategory.userId = this.userId;
            this.update();
        },
        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnEdit'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/AssetCategory/update', this.selectedCategory)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getCategories();

                        //title, message, type
                        me.notify("Success","Category successfully updated.", "success");

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                         else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        create: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnSave'));
            btnSave.start();

            this.createData(this.ApiUrl + '/api/AssetCategory/create', this.category)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getCategories();
                        me.notify("Success", "Asset Category successfully added.", 'success');

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
        deleteCategory: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedCategory.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/AssetCategory/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getCategories();

                        //title, message, type
                        me.notify("Success", "Asset was successfully deleted.", "success");

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
  
        getCategories: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/asset-category/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#tblAssetType').DataTable();
                        dT.destroy();

                        me.categories = response.data.data;

                        me.$nextTick(function () {
                            $('#tblAssetType').DataTable(
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
