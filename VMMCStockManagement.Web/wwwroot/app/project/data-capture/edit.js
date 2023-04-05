
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#edit-page',

    watch: function () {
        this.calculateTotal();

    },
    created: function () {
        //this.getReportList();
        var id = this.getParameterByName('id');

   
        if (id) {
            this.getReportById(id);
        }

        this.getProvinces();
        this.getActivities();
        this.getDeliveryModels();
        this.getTechnicalAreas();
        this.getModalities();
        this.getUsers();
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        this.reportingData.userId = this.userId;
    },
    data: {
        reportingData: {
            id: 0,
            userId: '',
            noOfFacilitiesUSAIDSupported: '',
            noOfFacilitiesReceivingTechncalAssistance: '',


            implementingPartner: '',
            responsiblePersonId: '',
            facilityId: '',
            subDistrictId: '',
            districtId: '',
            provinceId: '',
            trainingTitle: '',
            activities: [],
            deliveryModels: [],
            technicalAreas: [],
            modalities: [],
            otherActivity: '',
            otherModality: '',
            otherTechnicalArea: '',

            adverseEventFollowingImmunization: {
                minor: '',
                moderate: '',
                serious: '',
                severe: '',
                total: ''
            },
            booster: {
                noReceivedBoosterPfizer: '',
                noReceivedBoosterJandJ: '',
                total: ''
            },
            elderlySixtyPlus: {
                firstDosePfizer: '',
                secondDosePfizer2ndOrSingleJandJ: '',
                boosterPfizerOrJandJ: ''
            },
            janssenSingleDose: {
                male: '',
                female: ''
            },
            numberOfCovidVaccineMultisectoral: {
                national: '',
                provincial: '',
                district: '',
                total: ''
            },
            numberOfHealthcareWorkersTrainedRCCE: {
                male: '',
                female: '',
                unknown: '',
                total: ''
            },
            numberOfHelathWorkersTrainedCovidCaseManagement: {
                male: '',
                female: '',
                unknown: '',
                total: ''
            },
            numberOfHelathWorkersTrainedCovidCaseManagement2: {
                male: '',
                female: '',
                unknown: '',
                total: ''
            },
            numberOfNoneHealthcareWorkersTrainedRCCE: {
                male: '',
                female: '',
                unknown: '',
                total: ''
            },
            numberOfNonHealthWorkersReceivedCovidTraining: {
                male: '',
                female: '',
                unknown: '',
                total: ''
            },
            numberOfPeopleTrained: {
                male: '',
                female: '',
                unknown: '',
                target: '',
                total: ''
            },
            numberOfPoliciesProtocols: {
                rcce: '',
                surveillanceRapidResponse: '',
                caseManagement: '',
                ipc: '',
                coordinationAndOperation: '',
                vaccines: '',
                total: ''
            },
            numberOfToolsForPlanning: {
                establishingSurveillanceSystems: '',
                monitoringAndRespondingToAEFI: '',
                monitoringAndRespondingToAESI: '',
                safetyDataManagementSystems: '',
                covid19VaccineSafetyCommunication: ''
            },
            numberOfVaccinationSitesEstablished: {
                fixedAndOutreachSites: '',
                temporaryOrMobileClinics: '',
                massVaccinationOrCampains: '',
                total: ''
            },

            recommendedDoseUsaidDirectSupport: {
                receivedPfizerSecondAndJJSingleDose: '',
                pfizerSecondDose: '',
                male: '',
                female: '',
                jjSingleDose: '',
            },
            numberOfWorkersReceivedCovidTraining: {
                male: '',
                female: '',
                unknown: '',
                total: ''
            },
            pfizerFirstDoze: {
                male: '',
                female: '',
                total: ''
            },
            pfizerSecondDoze: {
                male: '',
                female: '',
                total: ''
            },
            targetExpectedHealthAndNonHealthcareRCCE: {
                healthCareWorkers: '',
                nonHealthCareWorkers: '',
                total: ''
            },
            numberOfPeopleTrainedOnSurveillance: {
                male: '',
                female: '',
                unknown: '',
                target: '',
                total: ''
            }
      

        },


        countries : [],
        provinces : [],
        districts : [],
        subdistricts: [],
        facilities: [],
        modalities: [],
        technicalAreas: [],
        deliveryModels: [],
        activities: [],
        users: [],
        hasError: false

    },
    validations: {
        reportingData: {
            provinceId: {
                required: required
            },
            districtId: {
                required: required
            },
            subDistrictId: {
                required: required
            }
            ,
            facilityId: {
                required: required
            }
            ,
            implementingPartner: {
                required: required
            },
            trainingTitle: {
                required: required
            },
            responsiblePersonId: {
                required: required
            },
            activities: {
                required: required
            }, deliveryModels: {
                required: required
            }, technicalAreas: {
                required: required
            }, modalities: {
                required: required
            },
            elderlySixtyPlus: {
                firstDosePfizer: {
                    required,
                    isGreater(value) {
                        var total = 0;

                        console.log('Passed Value: ' + value);
                        var male = this.reportingData.pfizerFirstDoze.male;
                        var female = this.reportingData.pfizerFirstDoze.female;

                        if (male != '') {
                            total += Number(male);
                        }

                        if (female != '') {
                            total += Number(female);
                        }

                        console.log('Total: ' + total);
                        if (value != '') {
                            if (Number(value) > total)
                                return false;
                        }

                        return true;
                    }
                },
                secondDosePfizer2ndOrSingleJandJ: {
                    required,
                    isGreater(value) {
                        var totalPfizer2nd = 0;
                        var male = this.reportingData.pfizerSecondDoze.male;
                        var female = this.reportingData.pfizerSecondDoze.female;

                        if (male != '') {
                            totalPfizer2nd += Number(male);
                        }

                        if (female != '') {
                            totalPfizer2nd += Number(female);
                        }

                        var totalJanssenSingle = 0;
                        var male = this.reportingData.janssenSingleDose.male;
                        var female = this.reportingData.janssenSingleDose.female;

                        if (male != '') {
                            totalJanssenSingle += Number(male);
                        }

                        if (female != '') {
                            totalJanssenSingle += Number(female);
                        }

                        if (Number(value) > (totalPfizer2nd + totalJanssenSingle)) {
                            return false;
                        }

                        return true;
                    }
                },
                boosterPfizerOrJandJ: {
                    isGreater(value) {
                        var total = 0;
                        var noReceivedBoosterPfizer = this.reportingData.booster.noReceivedBoosterPfizer;
                        var noReceivedBoosterJandJ = this.reportingData.booster.noReceivedBoosterJandJ;

                        if (value == '')
                            return true;


                        if (this.isNumeric(noReceivedBoosterPfizer)) {
                            total += Number(noReceivedBoosterPfizer);
                        }

                        if (this.isNumeric(noReceivedBoosterJandJ)) {
                            total += Number(noReceivedBoosterJandJ);
                        }

                        if (this.isNumeric(value)) {

                            if (Number(value) > total)
                                return false;

                        }

                        return true;
                    }
                }
            },


        }
    },
    methods: {

        isNumeric: function (n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        },

        save: function () {
            this.$v.reportingData.$touch();
    
            if (this.$v.reportingData.$pending || this.$v.reportingData.$error) {
                this.hasError = true;
                $('#errorModal').modal('show');
                return;
            }

            this.update();
        },

        update: function () {
            let me = this;
            this.reportingData.userId = this.userId;

            this.createData(this.ApiUrl + '/api/DataCapture/update-data', this.reportingData)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.successNotify("Successfully updated");

                        var url = me.ApiUrl + '/DataCapture';

                        me.redirectAfterwaiting(url);
                 
                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },

        getReportById: function (id) {
            let me = this;
            me.readData(this.ApiUrl + '/api/DataCapture/get-by-id?id='+id)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.reportingData = response.data.data;
                       
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getContries: function () {
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

        getProvinces: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/province/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.provinces = response.data.data;
                        me.getDistricts(me.reportingData.provinceId);
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
                        me.getSubDistricts(me.reportingData.districtId);
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
                        me.getFacilities(me.reportingData.subDistrictId);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getFacilities: function (subDistrictId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/facility/filter?subDistrictId=' + subDistrictId)
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

        getActivities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/activity/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.activities = response.data.data;
                        console.log(me.activities);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getModalities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/modality/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.modalities = response.data.data;
                        console.log(me.modalities);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getDeliveryModels: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/delivery-model/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.deliveryModels = response.data.data;
                        console.log(me.deliveryModels);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getTechnicalAreas: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/technical-area/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.technicalAreas = response.data.data;
                        console.log(me.technicalAreas);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getUsers: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/user/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.users = response.data.data;
                        console.log(me.technicalAreas);
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
            this.getDistricts(provinceId);
            
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

        getParameterByName: function(name, url){

            if (!url) url = window.location.href;

            name = name.replace(/[\[\]]/g, '\\$&');

            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                results = regex.exec(url);

            if (!results) return null;

            if (!results[2]) return '';

            return decodeURIComponent(results[2].replace(/\+/g, ' '));

        },

        calculateTotal: function () {

            this.reportData.recommendedDoseUsaidDirectSupport.receivedPfizerSecondAndJJSingleDose = parseInt(this.reportingData.pfizerSecondDoze.total) + parseInt(this.reportingData.janssenSingleDose.total);

            this.reportData.recommendedDoseUsaidDirectSupport.male = parseInt(this.reportingData.janssenSingleDose.male) + parseInt(this.reportingData.pfizerSecondDoze.male);

            this.reportData.recommendedDoseUsaidDirectSupport.female = parseInt(this.reportingData.janssenSingleDose.female) + parseInt(this.reportingData.pfizerSecondDoze.female);

            this.reportData.recommendedDoseUsaidDirectSupport.pfizerSecondDose = parseInt(this.reportingData.pfizerSecondDoze.total);

            this.reportData.recommendedDoseUsaidDirectSupport.jjSingleDose = parseInt(this.reportingData.janssenSingleDose.total);
        }


    }
});
