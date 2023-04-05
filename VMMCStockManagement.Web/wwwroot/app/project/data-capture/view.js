
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#create-page',
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
    },
    data: {
    
        reportingData: {
            id: 0,
            userId: '',
            noOfFacilitiesUSAIDSupported: '',
            noOfFacilitiesReceivingTechncalAssistance: '',


            implementingPartner: '',
            responsiblePersonId: '',
            responsiblePerson: '',
            capturedBy: '',
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
            activityNames: [],
            deliveryModelNames: [],
            technicalAreaNames: [],
            modalityNames: [],

            adverseEventFollowingImmunization: {
                minor: '',
                moderate: '',
                serious: '',
                severe: ''
            },
            booster: {
                noReceivedBoosterPfizer: '',
                noReceivedBoosterJandJ: ''
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
                district: ''
            },
            numberOfHealthcareWorkersTrainedRCCE: {
                male: '',
                female: '',
                unknown: ''
            },
            numberOfHelathWorkersTrainedCovidCaseManagement: {
                male: '',
                female: '',
                unknown: ''
            },
            numberOfNoneHealthcareWorkersTrainedRCCE: {
                male: '',
                female: '',
                unknown: ''
            },
            numberOfNonHealthWorkersReceivedCovidTraining: {
                male: '',
                female: '',
                unknown: ''
            },
            numberOfPeopleTrained: {
                male: '',
                female: '',
                unknown: '',
                target: ''
            },
            numberOfPoliciesProtocols: {
                rcce: '',
                surveillanceRapidResponse: '',
                caseManagement: '',
                ipc: '',
                coordinationAndOperation: '',
                vaccines: ''
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
                massVaccinationOrCampains: ''
            },
            numberOfWorkersReceivedCovidTraining: {
                male: '',
                female: '',
                unknown: ''
            },
            pfizerFirstDoze: {
                male: '',
                female: ''
            },
            pfizerSecondDoze: {
                male: '',
                female: ''
            },
            recommendedDoseUsaidDirectSupport: {
                noReceivedPfizerSecondAndJJSingleDose: '',
                male: '',
                female: '',
                pfizerSecondDose: '',
                jjSingleDose: ''
            },
            targetExpectedHealthAndNonHealthcareRCCE: {
                healthCareWorkers: '',
                nonHealthCareWorkers: ''
            },
            numberOfPeopleTrainedOnSurveillance: {
                male: '',
                female: '',
                unknown: '',
                target: ''
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

    },
    methods: {


        save: function () {
            alert('Clicked saved');
            if (this.reportingData.id && this.reportingData.id > 0 ) {
                this.update();
            } else {
                this.create();
            }
        },

        create: function () {
            let me = this;
            this.createData(this.ApiUrl + '/api/DataCapture/save-captured-data', this.reportingData)
                .then(function (response) {
               
                    if (response.data.status == 'success') {

                        me.successNotify("Successfully created");


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
            me.isloading = true;
            me.readData(this.ApiUrl + '/api/DataCapture/get-by-id?id='+id)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.reportingData = response.data.data;
                        console.log(me.reportList);
                        me.isloading = false;
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


    }
});
