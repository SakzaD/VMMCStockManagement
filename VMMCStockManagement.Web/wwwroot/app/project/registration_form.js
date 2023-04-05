

var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#create-page',

    created: function () {
        //this.getReportList();


        var id = this.getParameterByName('id');

        if (id) {
            this.getAllDistricts();
            this.getAllSubDistricts();
            this.getAllFacilities();
            this.getCapturedDataById(id);
        } else {
            var date = new Date().toLocaleDateString('en-CA');;
            this.capturedData.captureDate = date;
            //const capturedDate = document.querySelector('[name=capturedDate]');
            //capturedDate.value = date;
        }

        this.getAgeGroup();
        this.getProvinces();
        this.getKAndVTypes();
        this.getPackageService();
        this.getImplementingPartners();
        this.getEthnicities();
        this.getOperations();

    },
    mounted: function () {
        //$(this.$refs.datepickerCapturedDate).pickadate({
        //    format: 'yyyy-mm-dd'
        //}).on("change", e => {
        //    this.updateDate(e.target.value);
        //});
    },
    data: {
        userId: '',
        selectedCapturedDataId: '',
        capturedData: {
            id: 0,
            userId: '',
            uuId: '',
            captureDate: null,
            ageGroupId: '',
            genderId: '',
            kAndVPopTypeId: '',
            servicePackageId: '',
            implementingPartnerId: '',
            ethnicityId: '',
            provinceId: '',
            districtId: '',
            subDistrictId: '',
            operationId: '',
            homeTown: '',
            catchmentClinicId: '',
            additionalNeeds: '',
            referredOnward: ''
        },
        ageGroups: [],
        kAndVTypes: [],
        packageService: [],
        implementingPartners: [],
        ethnicities: [],
        provinces: [],
        districts: [],
        subdistricts: [],
        operations: [],
        catchmentClinics: [],
        facilities: [],
        hasError: false,

    },
    validations: {
        capturedData: {
            ageGroupId: {
                required: required,
            },

            genderId: {
                required: required,
            },

            kAndVPopTypeId: {
                required: required,
            },

            servicePackageId: {
                required: required,
            },

            implementingPartnerId: {
                required: required,
            },

            ethnicityId: {
                required: required,
            },
            provinceId: {
                required: required,
            },

            districtId: {
                required: required,
            },

            subDistrictId: {
                required: required,
            },
            operationId: {
                required: required,
            },
            catchmentClinicId: {
                required: required,
            },

            additionalNeeds: {
                required: required,
            },
            referredOnward: {
                required: required,
            },

            uuId: {
                required: required,
            },

        }
    },
    methods: {
        onCapturedDateChnage: function (event) {
            var selectedValue = event.target.value;
    
            if (selectedValue && selectedValue != '') {
                var selectedDate = moment(selectedValue, 'YYYY-MM-DD');
          
                var isvalid = selectedDate.isValid();
    
                if (!isvalid) {
                    return;
                }

                var nowDate = moment(moment(), 'YYYY-MM-DD')

                if (nowDate.isAfter(selectedDate, 'day') ) {
          
                    $('#backdatemodal').modal('show');
                }

            }
           
        },
        updateDate: function ( val) {
            this.capturedData.captureDate = val;
        },
        setSelectedData: function (rowId) {
            this.selectedCapturedDataId = rowId
        },
        isNumeric: function (n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        },

        getHumanDate: function () {

            var date = moment(moment(), 'YYYY-MM-DD').format('DD/MM/YYYY');
            console.log("Current Date: " + date);
            return date;
        },

        deleteRecord: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedCapturedDataId,
                userId: this.userId
            }

            console.log(request);

            this.createData(this.ApiUrl + '/api/DataCapture/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#form_id_' + request.id).remove();
                        $('#deleteConfirmation').modal('hide');
                  
                        //me.getProvinces();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },

        save: function () {

            this.$v.capturedData.$touch();
            //// if its still pending or an error is returned do not submit


            if (this.$v.capturedData.$pending || this.$v.capturedData.$error) {
                this.hasError = true;

                //$('#errorModal').modal('show');
                return;
            }

            this.capturedData.userId = this.userId;

            //this.create();

            if (this.capturedData.id && this.capturedData.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },

        create: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/DataCapture/create', this.capturedData)
                .then(function (response) {

                    me.isloading = false;

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");

                        var url = me.ApiUrl + '/RegistrationForm/List';

                        console.log("Url: " + url);

                        me.redirectAfterwaiting(url);


                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");

                        me.isloading = false;
                    }
                })
                .catch(function (error) {

                    alert('Exception: ' + error);
                });
        },

        update: function () {
            let me = this;
            this.capturedData.userId = this.userId;

            this.createData(this.ApiUrl + '/api/datacapture/update', this.capturedData)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully updated");

                        var url = '/registrationform/list';

                        me.redirectAfterwaiting(url);

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }

                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },

        getCapturedDataById: function (id) {
            let me = this;


            me.readData(this.ApiUrl + '/api/DataCapture/get-by-id?id=' + id)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.capturedData = response.data.data;
                        console.log(me.capturedData);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getEthnicities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/ethnicity/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.ethnicities = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }, getOperations: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/operation/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.operations = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getAgeGroup: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/agegroup/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.ageGroups = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getKAndVTypes: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/kabdvtypes/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.kAndVTypes = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getPackageService: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/packageservice/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.packageService = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getImplementingPartners: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/implementingpartner/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.implementingPartners = response.data.data;
                        //console.log(me.provinces);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getProvinces: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/province/filter')
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

        getDistricts: function (provinceId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/district/filter?provinceId=' + provinceId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.districts = response.data.data;
                        console.log(me.districts);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getAllDistricts: function (provinceId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/district/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.districts = response.data.data;
                        console.log(me.districts);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getAllSubDistricts: function (districtId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/sub-district/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.subdistricts = response.data.data;
                        console.log(me.subdistricts);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getSubDistricts: function (districtId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/sub-district/filter?districtId=' + districtId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.subdistricts = response.data.data;
                        console.log(me.subdistricts);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getAllFacilities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/facility/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.facilities = response.data.data;
                        console.log(me.deliveryModels);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        getFacilities: function (operationId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/facility/filter?operationId=' + operationId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.facilities = response.data.data;
                        console.log(me.deliveryModels);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },


        onProvinceChange: function (event) {
            var provinceId = event.target.value;
            this.districts = [];
            this.subdistricts = [];
            this.facilities = [];

            if ( provinceId != '' && provinceId > 0 ) {
                this.getDistricts(provinceId);
            }
         

        },

        onOperationChange: function (event) {
            var operationId = event.target.value;
            this.facilities = [];
            this.getFacilities(operationId);

        },

        onDistrictChange: function (event) {
            var districtId = event.target.value;
            this.subdistricts = [];
            this.facilities = [];
            this.getSubDistricts(districtId);

        },

        onSubDistrictChange: function (event) {
            var subDistrictId = event.target.value;
            this.facilities = [];
            this.getFacilities(subDistrictId);

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

function setRowId(id) {
    console.log('About to write id');
    console.log(id);
    console.log('End to write id');
    captureData.setSelectedData(id);
}