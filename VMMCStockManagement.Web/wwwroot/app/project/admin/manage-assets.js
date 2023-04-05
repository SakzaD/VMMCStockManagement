var assetManager = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#asset',
    created: function () {
       
        this.getAssets();
        this.getAssetCategories();
        this.getMakes();
        //this.getModels();
        this.getGrants();
        this.getSuppliers();
        this.getFacilities();
        this.getDepartments();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();

        $(this.$refs.warrantStartDateRef).pickadate({
            format: 'yyyy-mm-dd'
        })
            .on("change", e => {
                this.onWarrantStartDateChange(e.target.value);
            });

        $(this.$refs.warrantEndDateRef).pickadate({
            format: 'yyyy-mm-dd'
        })
            .on("change", e => {
                this.onWarrantEndDateChange(e.target.value);
            });


        $(this.$refs.purchaseOrReceivedDateRef).pickadate({
            format: 'yyyy-mm-dd'
        })
            .on("change", e => {
                this.onPurchaseOrReceivedDateChange(e.target.value);
            });


        $(this.$refs.transferDateRef).pickadate({
            format: 'yyyy-mm-dd'
        })
            .on("change", e => {
                this.onTransferDateChange(e.target.value);
            });
    },

    data: {
        grants: [],
        facilities: [],
        assets: [],
        makes: [],
        departments: [],
        models: [],
        suppliers: [],
        assetCategories: [],
        operationId: '',
        asset: {
            id: 0,
            name: '',
            description: '',
            registrationNumber: '',
            serialNumber: '',
            assetCategoryId: '',
            makeId: '',
            modelId: '',
            grantId: '',
            referenceNumber: '',
            userId: '',
            isAddedToRequest: true,
            referenceOrTicketNumber: '',
            invoiceNumber: '',
            supplierId: '',
            warrantyType: '',
            warrantStartDate: '',
            warrantEndDate: '',
            purchaseOrReceivedDate: '',
            transferDate: '',
            purchasePrice: '',
            locationId: '',
            departmentId: '',
        }
        ,
        hasError: false,

        selectedAsset: {
            id: 0,
            name: '',
            description: '',
            registrationNumber: '',
            serialNumber: '',
            assetCategoryId: '',
            makeId: '',
            modelId: '',
            grantId: '',
            referenceNumber: '',
            userId: '',
            isAddedToRequest: true,
            referenceOrTicketNumber: '',
            invoiceNumber: '',
            supplierId: '',
            warrantyType: '',
            warrantStartDate: '',
            warrantEndDate: '',
            purchaseOrReceivedDate: '',
            transferDate: '',
            purchasePrice: '',
            locationId: '',
            departmentId: '',
            
        },
        messageInfo: '',
        assetFile: '',
        fileError: false,
        modalDateError: {
            header: '',
            message: ''
        }
    },
    validations: {
        asset: {
            name: {
                required: required
            },

            description: {
                required: required
            },

            registrationNumber: {
                required: required
            },

            serialNumber: {
                required: required
            },

            assetCategoryId: {
                required: required
            },

            makeId: {
                required: required
            },

            modelId: {
                required: required
            },

            grantId: {
                required: required
            },
            supplierId: {
                required: required
            }, warrantyType: {
                required: required
            }
            ,
            locationId: {
                required: required
            },
            departmentId: {
                required: required
            }
        },
        selectedAsset: {
            name: {
                required: required
            },

            description: {
                required: required
            },

            registrationNumber: {
                required: required
            },

            serialNumber: {
                required: required
            },

            assetCategoryId: {
                required: required
            },

            makeId: {
                required: required
            },

            modelId: {
                required: required
            },

            grantId: {
                required: required
            },
            supplierId: {
                required: required
            },
            warrantyType: {
                required: required
            },
            locationId: {
                required: required
            },
            departmentId: {
                required: required
            }
        },
        assetFile: {
            required: required
        }
    },


    methods: {

        onWarrantStartDateChange: function (value) {

            if (this.asset.warrantEndDate) {
                var isDateAfter = moment(value).isSameOrAfter(moment(this.asset.warrantEndDate));
                if (isDateAfter) {

                    $('#warrantStartDate').val('');
                    this.asset.warrantStartDate = '';

                    this.modalDateError.header = 'Warrant start date';
                    this.modalDateError.message = 'Warrant start date cannot be after end date.';

                    $('#modalInfo').modal('show');
                } else {

                    this.asset.purchaseOrReceivedDate = value;

                }
            } else {
                this.asset.warrantStartDate = value;
            }

        },
        onWarrantEndDateChange: function (value) {
            this.asset.warrantEndDate = value;

            if (this.asset.warrantStartDate) {
                var isDateAfter = moment(value).isSameOrAfter(moment(this.asset.warrantEndDate));
                if (isDateAfter == false) {

                    $('#warrantEndDate').val('');
                    this.asset.warrantEndDate = '';

                    this.modalDateError.header = 'Warrant end date';
                    this.modalDateError.message = 'Warrant end date cannot be date before start date.';

                    $('#modalInfo').modal('show');
                } else {
                    this.asset.warrantEndDate = value;

                }
            } else {
                this.asset.warrantEndDate = value;
            }
        },
        onPurchaseOrReceivedDateChange: function (value) {

            var now = moment();
            console.log('Date: ' + value);
            var isDateAfter = moment(value).isSameOrAfter(moment());

            if (isDateAfter) {

                $('#purchaseOrReceivedDate').val('');
                this.asset.purchaseOrReceivedDate = '';

                this.modalDateError.header = 'Purchase/Received Date';
                this.modalDateError.message = 'You cannot select date that is after current date.';

                $('#modalInfo').modal('show');
            } else {

                this.asset.purchaseOrReceivedDate = value;

            }
          
        },

        onTransferDateChange: function (value) {

            var isDateAfter = moment(value).isSameOrAfter(moment(this.asset.purchaseOrReceivedDate));

            if (this.asset.purchaseOrReceivedDate && isDateAfter == false) {

                $('#transferDate').val('');
                this.asset.warrantEndDate = '';

                this.modalDateError.header = 'Transfer date';
                this.modalDateError.message = 'Transfer date cannot be before purchase/received date.';

                $('#modalInfo').modal('show');
            } else {
                this.asset.warrantEndDate = value;

            }
        },
        onReferenceChange: function (event) {
            var referenceNumber = event.target.value;

            if (referenceNumber && referenceNumber.length >= 3) {
                console.log("Reference Number: "+referenceNumber);
                this.getReferenceGrant(referenceNumber);
            }

        },
        getReferenceGrant: function (referenceNumber) {
            let me = this;
            me.readData(this.ApiUrl + '/api/RequestAsset/get-request-grant-by-reference?referenceNumber=' + referenceNumber)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        console.log(response.data.data);
                        me.asset.grantId = response.data.data.grantId;
                    } else {
                        me.asset.grantId = '';
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        clearAsset: function () {
            this.asset = {
                id: 0,
                name: '',
                description: '',
                registrationNumber: '',
                serialNumber: '',
                assetCategoryId: '',
                makeId: '',
                modelId: '',
                grantId: '',
                referenceNumber: '',
                referenceNumber: '',
                userId: '',
                isAddedToRequest: true,
                referenceOrTicketNumber: '',
                invoiceNumber: '',
                supplierId: '',
                warrantyType: '',
                warrantStartDate: '',
                warrantEndDate: '',
                purchaseOrReceivedDate: '',
                transferDate: '',
                purchasePrice: '',
                locationId: '',
                departmentId: '',
            };
        },
        clearFile: function () {
            this.$refs.file.value = '';
            this.assetFile = '';
        },

        onFileChange: function () {

            var selectedFile = this.$refs.file.files[0];

            console.log(selectedFile);
            if (selectedFile) {
                this.assetFile = selectedFile;
            } else {
                this.assetFile = '';
            }

        },
        uploadFile: async function () {
            this.fileError = false;
            this.$v.assetFile.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.assetFile.$pending || this.$v.assetFile.$error) {
                this.fileError = true;
                return;
            }

            let me = this;

            let btnUpload = Ladda.create(document.querySelector('#btnUpload'));
            btnUpload.start();

            var formData = new FormData();
            formData.append("userId", this.userId);
            formData.append("assetsFile", this.assetFile);

            console.log(this.assetFile.name);
            await me.uploadBlob('/api/asset/upload-bulk', formData)

                .then(async function (response) {
                    btnUpload.stop();
                    if (response.data.status == 'success') {
                        $('#modalAddBulk').modal('hide');

                        me.notify("Success", "File was successfully uploaded.", "success");
                        me.getAssets();
                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }

                })
                .catch(function (error) {
                    btnUpload.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        save: function () {

            this.$v.asset.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.asset.$pending || this.$v.asset.$error) {
                this.hasError = true;
                return;
            }

            this.asset.userId = this.userId;
            this.create();
        },
        edit: function () {

            this.$v.selectedAsset.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedAsset.$pending || this.$v.selectedAsset.$error) {
                this.hasError = true;
                return;
            }

            this.selectedAsset.userId = this.userId;
            this.update();
        },
        update: function () {
            let me = this;
            let btnUpload = Ladda.create(document.querySelector('#btnUpdate'));
            btnUpload.start();
            this.createData(this.ApiUrl + '/api/asset/update', this.selectedAsset)
                .then(function (response) {
                    btnUpload.stop();
                    if (response.data.status == 'success') {
                        $('#modalEdit').modal('hide');
                        me.getAssets();
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
                    btnUpload.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        create: function () {
            let me = this;

            let btnUpload = Ladda.create(document.querySelector('#btnAdd'));
            btnUpload.start();

            this.createData(this.ApiUrl + '/api/asset/create', this.asset)
                .then(function (response) {
                    btnUpload.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getAssets();
                        me.notify("Success", "Asset successfully added.", 'success');

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                    }
                })
                .catch(function (error) {
                    btnUpload.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        getModels: function (makeId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/model/filter?makeId=' + makeId)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.models = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getGrants: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/grant/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.grants = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getMakes: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/make/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.makes = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        deleteAsset: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedAsset.id,
                userId: this.userId
            }

            let btnDelete = Ladda.create(document.querySelector('#btnDelete'));
            btnDelete.start();

            this.createData(this.ApiUrl + '/api/asset/delete', request)
                .then(function (response) {
                    btnDelete.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getAssets();

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
                    btnDelete.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
        getAssets: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/asset/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#tblAsset').DataTable();
                        dT.destroy();


                        me.assets = response.data.data;

                        me.$nextTick(function () {
                            $('#tblAsset').DataTable(
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
        getSuppliers: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/supplier/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.suppliers = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getAssetCategories: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/asset-category/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.assetCategories = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getFacilities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/facility/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.facilities = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getDepartments: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/department/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.departments = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        onMakeChange: function (event) {
            var makeId = event.target.value;
            this.models = [];
            console.log("MakeId: " + makeId);
            this.getModels(makeId);
        },
        getRequestById: function (id) {
            var userId = this.userId;
            let me = this;
            me.readData(this.ApiUrl + '/api/asset/get-by-id?id=' + id)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        var data = response.data.data;
                        me.selectedAsset = data;

                        console.log(data);
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
        updateCategoryId: function (categoryId) {
            this.asset.assetCategoryId = categoryId;
            console.log("Category Id: " + this.asset.assetCategoryId);
        },

        updateMakeId: function (id) {
            this.asset.makeId = id;
            console.log("makeId Id: " + this.asset.makeId);
            this.models = [];
            this.asset.modelId = '';
            this.getModels(id);
        },
        updateModelId: function (id) {
            this.asset.modelId = id;
            console.log("modelId Id: " + this.asset.modelId);
        }
        , updateSupplierId: function (id) {
            this.asset.supplierId = id;
            console.log("supplierId Id: " + this.asset.supplierId);
        }, updateLocationId: function (id) {
            this.asset.locationId = id;
            console.log("locationId Id: " + this.asset.locationId);
        }, updateDepartmenteId: function (id) {
            this.asset.departmentId = id;
            console.log("departmentId Id: " + this.asset.departmentId);
        },
        updateGrantId: function (id) {
            this.asset.grantId = id;
            console.log("departmentId Id: " + this.asset.grantId);
        }
    }
});


$(document).on('change', '#assetCategoryId', function () {
    var categoryId = $(this).val();
    assetManager.updateCategoryId(categoryId);
});

$(document).on('change', '#makeId', function () {
    var categoryId = $(this).val();
    assetManager.updateMakeId(categoryId);
});

$(document).on('change', '#modelId', function () {
    var categoryId = $(this).val();
    assetManager.updateModelId(categoryId);
});

$(document).on('change', '#supplierId', function () {
    var categoryId = $(this).val();
    assetManager.updateSupplierId(categoryId);
});

$(document).on('change', '#locationId', function () {
    var categoryId = $(this).val();
    assetManager.updateLocationId(categoryId);
});

$(document).on('change', '#departmentId', function () {
    var categoryId = $(this).val();
    assetManager.updateDepartmenteId(categoryId);
});

$(document).on('change', '#grantId', function () {
    var grantId = $(this).val();
    assetManager.updateGrantId(grantId);
});