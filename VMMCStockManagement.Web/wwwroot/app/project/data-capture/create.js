
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#create-page',
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
        //this.reportingData.userId = $('#userId').val();
    },

    data: {
        userId: '',
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
                female: '',
                total: ''
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
        countries: [],
        provinces: [],
        districts: [],
        subdistricts: [],
        facilities: [],
        modalities: [],
        technicalAreas: [],
        deliveryModels: [],
        activities: [],
        users: [],
        hasError: false,
        reportData: {

            recommendedDoseUsaidDirectSupport: {

                receivedPfizerSecondAndJJSingleDose:'',
                pfizerSecondDose:'',
                male:'',
                female:'',
                jjSingleDose:'',
            }
        }

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
            // if its still pending or an error is returned do not submit


            if (this.$v.reportingData.$pending || this.$v.reportingData.$error) {
                this.hasError = true;

                $('#errorModal').modal('show');
                return;
            }

            this.reportingData.userId = this.userId;
            //alert('UserId: ' + this.userId);
            this.isloading = true;

            this.create();
        },

        create: function () {
            let me = this;
        
            this.createData(this.ApiUrl + '/api/DataCapture/save-captured-data', this.reportingData)
                .then(function (response) {

                    me.isloading = false;

                    if (response.data.status == 'success') {

                        me.successNotify("Successfully created");

                        var url = me.ApiUrl + '/DataCapture';

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
            this.reportingData.userId = this.userId;

            this.updateData(this.ApiUrl + '/api/DataCapture/update-data', this.reportingData)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.successNotify("Successfully updated");

                        var url = '/DataCapture';

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
                        console.log(me.reportList);

                    
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

        getReasons: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/reason/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.reasons = response.data.data;

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

            this.reportingData.pfizerFirstDoze.total = 0;
            this.reportingData.pfizerSecondDoze.total = 0;
            this.reportingData.booster.total = 0;
            this.reportingData.janssenSingleDose.total = 0;
            this.reportingData.adverseEventFollowingImmunization.total = 0;
            this.reportingData.numberOfPeopleTrained.total = 0;
            this.reportingData.numberOfVaccinationSitesEstablished.total = 0;
            this.reportingData.numberOfCovidVaccineMultisectoral.total = 0;
            this.reportingData.numberOfHealthcareWorkersTrainedRCCE.total = 0;
            this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.total = 0;
            this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.total = 0;
            this.reportingData.numberOfPeopleTrainedOnSurveillance.total = 0;
            this.reportingData.numberOfWorkersReceivedCovidTraining.total = 0;
            this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.total = 0;
            this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.total = 0;
            this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.total = 0;
            this.reportingData.numberOfPoliciesProtocols.total = 0;
            this.reportData.recommendedDoseUsaidDirectSupport.male = 0;
            this.reportData.recommendedDoseUsaidDirectSupport.female = 0;

            if (this.reportingData.pfizerFirstDoze.male != '') {
                this.reportingData.pfizerFirstDoze.total += parseInt(this.reportingData.pfizerFirstDoze.male);
            }
            if (this.reportingData.pfizerFirstDoze.female != '') {
                this.reportingData.pfizerFirstDoze.total += parseInt(this.reportingData.pfizerFirstDoze.female);
            }
            if (this.reportingData.pfizerFirstDoze.male != '' && this.reportingData.pfizerFirstDoze.female != '') {
                this.reportingData.pfizerFirstDoze.total = parseInt(this.reportingData.pfizerFirstDoze.male) + parseInt(this.reportingData.pfizerFirstDoze.female);
            }

            if (this.reportingData.pfizerSecondDoze.male != '') {
                this.reportingData.pfizerSecondDoze.total += parseInt(this.reportingData.pfizerSecondDoze.male);
            }
            if (this.reportingData.pfizerSecondDoze.female != '') {
                this.reportingData.pfizerSecondDoze.total += parseInt(this.reportingData.pfizerSecondDoze.female);
            }
            if (this.reportingData.pfizerSecondDoze.male != '' && this.reportingData.pfizerSecondDoze.female != '') {
                this.reportingData.pfizerSecondDoze.total = parseInt(this.reportingData.pfizerSecondDoze.male) + parseInt(this.reportingData.pfizerSecondDoze.female);
            }

            if (this.reportingData.booster.noReceivedBoosterPfizer != '') {
                this.reportingData.booster.total += parseInt(this.reportingData.booster.noReceivedBoosterPfizer);
            }
            if (this.reportingData.booster.noReceivedBoosterJandJ != '') {
                this.reportingData.booster.total += parseInt(this.reportingData.booster.noReceivedBoosterJandJ);
            }
            if (this.reportingData.booster.noReceivedBoosterPfizer != '' && this.reportingData.booster.noReceivedBoosterJandJ != '') {
                this.reportingData.booster.total = parseInt(this.reportingData.booster.noReceivedBoosterPfizer) + parseInt(this.reportingData.booster.noReceivedBoosterJandJ);
            }

            if (this.reportingData.janssenSingleDose.male != '') {
                this.reportingData.janssenSingleDose.total += parseInt(this.reportingData.janssenSingleDose.male)
            }
            if (this.reportingData.janssenSingleDose.female != '') {
                this.reportingData.janssenSingleDose.total += parseInt(this.reportingData.janssenSingleDose.female);
            }
            if (this.reportingData.janssenSingleDose.male != '' && this.reportingData.janssenSingleDose.female != '') {
               this.reportingData.janssenSingleDose.total = parseInt(this.reportingData.janssenSingleDose.male) + parseInt(this.reportingData.janssenSingleDose.female);

            }

            if (this.reportingData.adverseEventFollowingImmunization.minor != '') {
                this.reportingData.adverseEventFollowingImmunization.total += parseInt(this.reportingData.adverseEventFollowingImmunization.minor);
            }
            if (this.reportingData.adverseEventFollowingImmunization.moderate != '') {
                this.reportingData.adverseEventFollowingImmunization.total += parseInt(this.reportingData.adverseEventFollowingImmunization.moderate);
            }
            if (this.reportingData.adverseEventFollowingImmunization.serious != '') {
                this.reportingData.adverseEventFollowingImmunization.total += parseInt(this.reportingData.adverseEventFollowingImmunization.serious);
            }
            if (this.reportingData.adverseEventFollowingImmunization.severe != '') {
                this.reportingData.adverseEventFollowingImmunization.total += parseInt(this.reportingData.adverseEventFollowingImmunization.severe);
            }
            if (this.reportingData.adverseEventFollowingImmunization.minor != '' &&
                this.reportingData.adverseEventFollowingImmunization.moderate != '' &&
                this.reportingData.adverseEventFollowingImmunization.serious != '' &&
                this.reportingData.adverseEventFollowingImmunization.severe != '') {

                this.reportingData.adverseEventFollowingImmunization.total = parseInt(this.reportingData.adverseEventFollowingImmunization.minor) + parseInt(this.reportingData.adverseEventFollowingImmunization.moderate) + parseInt(this.reportingData.adverseEventFollowingImmunization.serious) + parseInt(this.reportingData.adverseEventFollowingImmunization.severe);
            }
        
            if (this.reportingData.numberOfPeopleTrained.male != '') {
                this.reportingData.numberOfPeopleTrained.total += parseInt(this.reportingData.numberOfPeopleTrained.male);
            }
            if (this.reportingData.numberOfPeopleTrained.female != '') {

                this.reportingData.numberOfPeopleTrained.total += parseInt(this.reportingData.numberOfPeopleTrained.female);

            }
            if (this.reportingData.numberOfPeopleTrained.unknown != '') {
                this.reportingData.numberOfPeopleTrained.total +=  parseInt(this.reportingData.numberOfPeopleTrained.unknown);

            }
            if (this.reportingData.numberOfPeopleTrained.male != '' && this.reportingData.numberOfPeopleTrained.female != '' && this.reportingData.numberOfPeopleTrained.unknown) {
                this.reportingData.numberOfPeopleTrained.total = parseInt(this.reportingData.numberOfPeopleTrained.male) + parseInt(this.reportingData.numberOfPeopleTrained.female) + parseInt(this.reportingData.numberOfPeopleTrained.unknown);
            }

            if (this.reportingData.numberOfVaccinationSitesEstablished.fixedAndOutreachSites != '') {
                this.reportingData.numberOfVaccinationSitesEstablished.total += parseInt(this.reportingData.numberOfVaccinationSitesEstablished.fixedAndOutreachSites);
            }
            if (this.reportingData.numberOfVaccinationSitesEstablished.temporaryOrMobileClinics != '') {
                this.reportingData.numberOfVaccinationSitesEstablished.total += parseInt(this.reportingData.numberOfVaccinationSitesEstablished.temporaryOrMobileClinics);
            }
            if (this.reportingData.numberOfVaccinationSitesEstablished.massVaccinationOrCampains != '') {
                this.reportingData.numberOfVaccinationSitesEstablished.total += parseInt(this.reportingData.numberOfVaccinationSitesEstablished.massVaccinationOrCampains);
            }
            if (this.reportingData.numberOfVaccinationSitesEstablished.fixedAndOutreachSites != '' &&
                this.reportingData.numberOfVaccinationSitesEstablished.temporaryOrMobileClinics != '' &&
                this.reportingData.numberOfVaccinationSitesEstablished.massVaccinationOrCampains != '') {
                this.reportingData.numberOfVaccinationSitesEstablished.total = parseInt(this.reportingData.numberOfVaccinationSitesEstablished.fixedAndOutreachSites) + parseInt(this.reportingData.numberOfVaccinationSitesEstablished.temporaryOrMobileClinics) + parseInt(this.reportingData.numberOfVaccinationSitesEstablished.massVaccinationOrCampains);

            }

            if (this.reportingData.numberOfCovidVaccineMultisectoral.national != '') {

                this.reportingData.numberOfCovidVaccineMultisectoral.total += parseInt(this.reportingData.numberOfCovidVaccineMultisectoral.national);
            }
            if (this.reportingData.numberOfCovidVaccineMultisectoral.provincial != '') {

                this.reportingData.numberOfCovidVaccineMultisectoral.total += parseInt(this.reportingData.numberOfCovidVaccineMultisectoral.provincial);
            }
            if (this.reportingData.numberOfCovidVaccineMultisectoral.district != '') {

                this.reportingData.numberOfCovidVaccineMultisectoral.total += parseInt(this.reportingData.numberOfCovidVaccineMultisectoral.district);

            }
            if (this.reportingData.numberOfCovidVaccineMultisectoral.national != '' && this.reportingData.numberOfCovidVaccineMultisectoral.provincial != '' && this.reportingData.numberOfCovidVaccineMultisectoral.district != '') {
                this.reportingData.numberOfCovidVaccineMultisectoral.total = parseInt(this.reportingData.numberOfCovidVaccineMultisectoral.national) + parseInt(this.reportingData.numberOfCovidVaccineMultisectoral.provincial) + parseInt(this.reportingData.numberOfCovidVaccineMultisectoral.district);
            }
            this.reportingData.numberOfHealthcareWorkersTrainedRCCE.total = parseInt(this.reportingData.numberOfHealthcareWorkersTrainedRCCE.male) + parseInt(this.reportingData.numberOfHealthcareWorkersTrainedRCCE.female) + parseInt(this.reportingData.numberOfHealthcareWorkersTrainedRCCE.unknown);

            if (this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.male != '') {
                this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.total += parseInt(this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.male);
            }
            if (this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.female != '') {
                this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.total += parseInt(this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.female);
            }
            if (this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.unknown != '') {
                this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.total += parseInt(this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.unknown);
            }
            if (this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.male != '' && this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.female != '' && this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.unknown != '') {

                this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.total = parseInt(this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.male) + parseInt(this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.female) + parseInt(this.reportingData.numberOfNoneHealthcareWorkersTrainedRCCE.unknown);
            }

            if (this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.healthCareWorkers != ''){

                this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.total += parseInt(this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.healthCareWorkers);
            }
            if (this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.nonHealthCareWorkers != '') {

                this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.total += parseInt(this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.nonHealthCareWorkers);

            }
            if (this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.healthCareWorkers != '' &&
                this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.nonHealthCareWorkers != '') {

                this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.total = parseInt(this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.healthCareWorkers) + parseInt(this.reportingData.targetExpectedHealthAndNonHealthcareRCCE.nonHealthCareWorkers);

            }

            if (this.reportingData.numberOfPeopleTrainedOnSurveillance.male != '') {
                this.reportingData.numberOfPeopleTrainedOnSurveillance.total += parseInt(this.reportingData.numberOfPeopleTrainedOnSurveillance.male);
            }
            if (this.reportingData.numberOfPeopleTrainedOnSurveillance.female != '') {
                this.reportingData.numberOfPeopleTrainedOnSurveillance.total += parseInt(this.reportingData.numberOfPeopleTrainedOnSurveillance.female);
            }
            if (this.reportingData.numberOfPeopleTrainedOnSurveillance.unknown != '') {

                this.reportingData.numberOfPeopleTrainedOnSurveillance.total += parseInt(this.reportingData.numberOfPeopleTrainedOnSurveillance.unknown);

            }
            if (this.reportingData.numberOfPeopleTrainedOnSurveillance.male != '' && this.reportingData.numberOfPeopleTrainedOnSurveillance.female != '' && this.reportingData.numberOfPeopleTrainedOnSurveillance.unknown != '' ) {
                this.reportingData.numberOfPeopleTrainedOnSurveillance.total = parseInt(this.reportingData.numberOfPeopleTrainedOnSurveillance.male) + parseInt(this.reportingData.numberOfPeopleTrainedOnSurveillance.female) + parseInt(this.reportingData.numberOfPeopleTrainedOnSurveillance.unknown);
            }
  
            if (this.reportingData.numberOfWorkersReceivedCovidTraining.male != '') {
                this.reportingData.numberOfWorkersReceivedCovidTraining.total += parseInt(this.reportingData.numberOfWorkersReceivedCovidTraining.male);
            }
            if (this.reportingData.numberOfWorkersReceivedCovidTraining.female != '') {
                this.reportingData.numberOfWorkersReceivedCovidTraining.total += parseInt(this.reportingData.numberOfWorkersReceivedCovidTraining.female);
            }
            if (this.reportingData.numberOfWorkersReceivedCovidTraining.unknown != '') {
                this.reportingData.numberOfWorkersReceivedCovidTraining.total += parseInt(this.reportingData.numberOfWorkersReceivedCovidTraining.unknown);
            }
            if (this.reportingData.numberOfWorkersReceivedCovidTraining.male != '' && this.reportingData.numberOfWorkersReceivedCovidTraining.female != '' && this.reportingData.numberOfWorkersReceivedCovidTraining.unknown != '') {
                this.reportingData.numberOfWorkersReceivedCovidTraining.total = parseInt(this.reportingData.numberOfWorkersReceivedCovidTraining.male) + parseInt(this.reportingData.numberOfWorkersReceivedCovidTraining.female) + parseInt(this.reportingData.numberOfWorkersReceivedCovidTraining.unknown);
            }

            if (this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.male != '') {
                this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.total += parseInt(this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.male);
            }
            if (this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.female != '') {
                this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.total += parseInt(this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.female);
            }
            if (this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.unknown != '') {
                this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.total += parseInt(this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.unknown);
            }
            if (this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.male != '' && this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.female != '' && this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.unknown != '' ) {
                this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.total = parseInt(this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.male) + parseInt(this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.female) + parseInt(this.reportingData.numberOfNonHealthWorkersReceivedCovidTraining.unknown);
            }

            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.male != '') {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.total += parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.male);
            }
            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.female != '') {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.total += parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.female);
            }
            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.unknown != '') {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.total +=  parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.unknown);
            }
            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.male != '' && this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.female != '' && this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.unknown != '' ) {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.total = parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.male) + parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.female) + parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement.unknown);
            }

            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.male != '') {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.total += parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.male);
            }
            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.female != '') {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.total += parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.female) 
            }
            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.unknown != '') {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.total += parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.unknown);
            }
            if (this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.male != '' && this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.female != '' && this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.unknown != '') {
                this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.total = parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.male) + parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.female) + parseInt(this.reportingData.numberOfHelathWorkersTrainedCovidCaseManagement2.unknown);

            }

            if (this.reportingData.numberOfPoliciesProtocols.rcce != '') {
                this.reportingData.numberOfPoliciesProtocols.total += parseInt(this.reportingData.numberOfPoliciesProtocols.rcce);
            }
            if (this.reportingData.numberOfPoliciesProtocols.surveillanceRapidResponse != '') {
                this.reportingData.numberOfPoliciesProtocols.total += parseInt(this.reportingData.numberOfPoliciesProtocols.surveillanceRapidResponse);
            }
            if (this.reportingData.numberOfPoliciesProtocols.caseManagement != '') {
                this.reportingData.numberOfPoliciesProtocols.total += parseInt(this.reportingData.numberOfPoliciesProtocols.caseManagement);
            }
            if (this.reportingData.numberOfPoliciesProtocols.ipc != '') {
                this.reportingData.numberOfPoliciesProtocols.total += parseInt(this.reportingData.numberOfPoliciesProtocols.ipc);
            }
            if (this.reportingData.numberOfPoliciesProtocols.coordinationAndOperation != '') {
                this.reportingData.numberOfPoliciesProtocols.total += parseInt(this.reportingData.numberOfPoliciesProtocols.coordinationAndOperation);
            }
            if (this.reportingData.numberOfPoliciesProtocols.vaccines != '') {
                this.reportingData.numberOfPoliciesProtocols.total += parseInt(this.reportingData.numberOfPoliciesProtocols.vaccines);

            }
            if (this.reportingData.numberOfPoliciesProtocols.rcce != '' &&
                this.reportingData.numberOfPoliciesProtocols.surveillanceRapidResponse != '' &&
                this.reportingData.numberOfPoliciesProtocols.caseManagement != '' &&
                this.reportingData.numberOfPoliciesProtocols.ipc != '' &&
                this.reportingData.numberOfPoliciesProtocols.coordinationAndOperation != '' &&
                this.reportingData.numberOfPoliciesProtocols.vaccines != '') {
                this.reportingData.numberOfPoliciesProtocols.total = parseInt(this.reportingData.numberOfPoliciesProtocols.rcce) + parseInt(this.reportingData.numberOfPoliciesProtocols.surveillanceRapidResponse) + parseInt(this.reportingData.numberOfPoliciesProtocols.caseManagement) + parseInt(this.reportingData.numberOfPoliciesProtocols.ipc) + parseInt(this.reportingData.numberOfPoliciesProtocols.coordinationAndOperation) + parseInt(this.reportingData.numberOfPoliciesProtocols.vaccines);

            }


            if (this.reportingData.elderlySixtyPlus.firstDosePfizer <= this.reportingData.pfizerFirstDoze.total || this.reportingData.elderlySixtyPlus.firstDosePfizer == '' ) {
                console.log("Right validation");
            } else {
                console.log("Wrong validation");
            }

            if (this.reportingData.elderlySixtyPlus.secondDosePfizer2ndOrSingleJandJ <= this.reportingData.pfizerSecondDoze.total || this.reportingData.elderlySixtyPlus.secondDosePfizer2ndOrSingleJandJ == '') {
                console.log("Right validation");
            } else {
                console.log("Wrong validation");
            }

            if (this.reportingData.booster.total >= this.reportingData.elderlySixtyPlus.boosterPfizerOrJandJ) {
                $('.hidden').hide();
            } else {

                $('.hidden').show();
            }

            this.reportData.recommendedDoseUsaidDirectSupport.receivedPfizerSecondAndJJSingleDose = parseInt(this.reportingData.pfizerSecondDoze.total) + parseInt(this.reportingData.janssenSingleDose.total);

            this.reportData.recommendedDoseUsaidDirectSupport.male = parseInt(this.reportingData.janssenSingleDose.male) + parseInt(this.reportingData.pfizerSecondDoze.male);

            this.reportData.recommendedDoseUsaidDirectSupport.female = parseInt(this.reportingData.janssenSingleDose.female) + parseInt(this.reportingData.pfizerSecondDoze.female);

            this.reportData.recommendedDoseUsaidDirectSupport.pfizerSecondDose = parseInt(this.reportingData.pfizerSecondDoze.total);

            this.reportData.recommendedDoseUsaidDirectSupport.jjSingleDose =parseInt(this.reportingData.janssenSingleDose.total);
        },
    }
});
