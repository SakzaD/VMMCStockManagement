﻿var assetManager = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#requestManager',
    created: function () {

        var requestId = this.getParameterByName('requestId');

        if (requestId) {
            this.getRequestById(requestId);
        }

        this.getAssetCategories();
        this.getMakes();
        //this.getModels();
        this.getGrants();
        this.getSuppliers();
        this.getFacilities();
        this.getDepartments();
    },
    mounted: function () {

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
        this.getNoneCompletedRequest();
    },
    data: {
        requests: [],
        selectedRequest: {},
        selectedRequestedCategory: {},
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
        },
        modalDateError: {
            header: '',
            message: ''
        },
        hasError: false,
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

            supplierId: {
                required: required
            }, warrantyType: {
                required: required
            },
            locationId: {
                required: required
            },
            departmentId: {
                required: required
            }

        },
    },
    methods: {
        onReferenceChange: function (event) {
            var referenceNumber = event.target.value;

            if (referenceNumber && referenceNumber.length >= 3) {
                console.log("Reference Number: " + referenceNumber);
                this.getReferenceGrant(referenceNumber);
            }

        },
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
        getRequestById: function (requestId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/RequestAsset/get-request-manager-by-id?requestId=' + requestId)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#tblRequests').DataTable();
                        dT.destroy();

                        me.selectedRequest = response.data.data;

                        if (me.selectedRequest.referenceNumber) {
                            me.asset.referenceOrTicketNumber = me.selectedRequest.referenceNumber;
                            me.asset.grantId = me.selectedRequest.grantId;
                        }
                        me.$nextTick(function () {
                            $('#tblRequests').DataTable(
                                {
                                    order: false,
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
         getNoneCompletedRequest: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/RequestAsset/get-all-none-completed-requests')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#tblRequests').DataTable();
                        dT.destroy();

                        me.requests = response.data.data;

                        me.$nextTick(function () {
                            $('#tblRequests').DataTable(
                                {
                                    order: false,
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
                       // me.getAssets();
                        //title, message, type
                        me.notify("Success", "Grant successfully updated.", "success");

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
                        var requestId = me.getParameterByName('requestId');
                        me.getRequestById(requestId);
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
            console.log("Category Id: "+this.asset.assetCategoryId);
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

        }
        , updateSupplierId: function (id) {
            this.asset.supplierId = id;

        }, updateLocationId: function (id) {
            this.asset.locationId = id;

        }, updateDepartmenteId: function (id) {
            this.asset.departmentId = id;
      
        }
    }
});


$(document).on('change', '#assetCategory', function () {
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