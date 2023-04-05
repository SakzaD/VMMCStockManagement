var vue = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#client-layout',
    created: function () {
        this.getBranchList();
    },
    mounted: function () {
        //this.showLoader(true);
    },
    data: {
        branchList: [],
        selectedBranch: {
            id: '',
            name: '',
            address: '',
            location: '',
            phone: '',

        },
        client: {
            firstName: '',
            lastName: '',
            dob: '',
            gender: '',
            clientId: '',
            phone: '',
            email: '',
            dor: '',
            branch: '',
            address: '',
            booking: {
                treatment : '',
                treatmentDate : '',
                amount : '',
                branch : '',
            },
        },
        booking: {
            treatment: {
                treatmentName: ''
            },
            createdAt: '',
            bookedDate: '',
            price: '',
            branch: {
                name: ''
            }
        },
        skinResults: {
            skinTone: '',
            muscleTone: '',
            moistureContent: '',
            sensitivity: '',
            colour: '',
            circulation: '',
            pore: '',
            superficialLine: '',
            generalHealth: '',
            comment: '',
            therapist: '',
        },
        medicalHistory: {
            isAllergies: false,
            isSkinDeseases: false,
            isConstipation: false,
            isHighLowBloodPressure: false,
            isDepression: false,
            isRheumatism: false,
            isDiabetes: false,
            isEpilepsy: false,
            isMetal: false,
            isBrokenCapilaries: false,
            isHeadaches: false,
            isAsthma: false,
            isCarbohydrates: false,
            isVitamin: false,
            isFat: false,
            isProtein: false,
            isTea: false,
            isAlcohol: false,
            isWater: false,
            isJuice: false,
            isCoffee: false,
            isAcidicFood: false,
            isMedication: false,
            lastVisitDoctor: '',
            sleepQuality: '',
            exercise: '',
            isSmoking: false,
           
        },
        error: {
            hasError: false,
            title: '',
            message: ''
        },
        clientId: '',

    },
    methods: {
        getBranchList: function () {
            let me = this;
            me.readData('/Branch/getall')
                .then(function (response) {
                   
                    if (response.data) {

                        me.branchList = response.data;

                    }
                }).catch(function (error) {

                });
        },
        getPersonalInfo: function (id) {
            let me = this;
            console.log("Id: "+ id);
            $('#client-details').modal('toggle');
            var loader = $('#loadingSpinner');
            var itemsLayout = $('#information');
            loader.removeClass('d-none');
            itemsLayout.addClass('d-none');
            me.error.hasError = false;
            me.readData('/client/' + id)
                .then(function (response) {
                  
                    if (response.data.isSuccessful) {
                        loader.addClass('d-none');
                        me.client = response.data.data;
                        if (response.data && response.data.booking) {
                            me.booking = response.data.booking;
                        }

                        itemsLayout.removeClass('d-none');
                    } else {
                        me.showError();
                    }

                }).catch(function (error) {
                    loader.addClass('d-none');
                    me.showError();
                });
        },
        getMedicalInfo: function (clientId) {
            let me = this;
            $('#medical-history').modal('toggle');
            var loader = $('.loadingSpinner');
            var itemsLayout = $('.information');
            loader.removeClass('d-none');
            itemsLayout.addClass('d-none');
            me.error.hasError = false;

            me.readData('/Client/medicalhistory?clientId=' + clientId)
                .then(function (response) {
                    loader.addClass('d-none');
                    if (response.status == 200) {
                        if (response.data && response.data.isSuccess) {
                            me.medicalHistory = response.data.data;
                            console.log(me.medicalHistory);
                            itemsLayout.removeClass('d-none');
                        } else {
                            me.showNotFound('Oops sorry client medical information was not found in the system.');
                        }

                    } else {
                        me.showError();
                    }
                }).catch(function (error) {
                 
                    loader.addClass('d-none');
                    me.showError();
                });
        },
        clearSkinData: function () {
            this.skinResults = {
                skinTone: '',
                muscleTone: '',
                moistureContent: '',
                sensitivity: '',
                colour: '',
                circulation: '',
                pore: '',
                superficialLine: '',
                generalHealth: '',
                comment: '',
                therapist: 'Unknown',
            }
        },
        getSkinInfo: function (clientId) {
            let me = this;

            $('#skin-test').modal('toggle');
            var loader = $('.loadingSpinner');
            var itemsLayout = $('.information');
            loader.removeClass('d-none');
            itemsLayout.addClass('d-none');
            me.error.hasError = false;
            me.readData('/Client/skintest?clientId=' + clientId)
                .then(function (response) {
                
                    if (response.status == 200) {
                        loader.addClass('d-none');
                        if (response.data && response.data.isSuccess) {

                            me.skinResults = response.data.data;
                            itemsLayout.removeClass('d-none');
                        } else {
                            me.clearSkinData();
                            me.showNotFound('Oops sorry client skin test was not found in the system.');
                        }
                    } else {
                        me.showError();
                    }
                }).catch(function (error) {
                    loader.addClass('d-none');
                    me.showError();
                });
        },

        showError: function () {
            this.error = {
                hasError: true,
                title: 'Failed',
                message: 'We could not fetch the client information, please try again later.'
            }
        },
        showNotFound: function(msg) {
            this.error = {
                hasError: true,
                title: 'Not found',
                message: msg
            }
        }
    }
});




$(function () {
    // Handler for .ready() called.
    showLoader(true);
    loadData();
});

function loadData()
{

   $.ajax({
        url: '/client/all',
        method: "GET",
        success: function (data) {
            var table = lookUpBaseTable();
            //table.DataTable().
            $.each(data.data, function (a, b) {
                table.row.add(b);
                
            });
         
            table.draw();
            showLoader(false);
        }
    });
}

function lookUpBaseTable() {

    return $('#clientTable').DataTable({
        autoWidth: false,
        dom: '<"datatable-header"fl><"datatable-scroll"t><"datatable-footer"ip>',
        language: {
            search: '<span>Filter:</span> _INPUT_',
            lengthMenu: '<span>Show:</span> _MENU_',
            paginate: { 'first': 'First', 'last': 'Last', 'next': '→', 'previous': '←' }
        },
        "columnDefs": [{
            "targets": 0,
            "render": function (data, type, row, meta) {
                return row.firstName;
            }
        }, {
            "targets": 1,
            "render": function (data, type, row, meta) {
                return row.lastName;
            }
        }, {
            "targets": 2,
            "render": function (data, type, row, meta) {
                return row.email;
            }
        },
        {
            "targets": 3,
            "render": function (data, type, row, meta) {
                return row.phone;
            }
            },
            {
            "targets": 4,
            "render": function (data, type, row, meta) {
                return row.branch;
            }
        }, {
            "targets": 5,
            "render": function (data, type, row, meta) {
                return row.dor;
            }
        },
       {
            "targets": 6,
            "render": function (data, type, row, meta) {
                return '<div class="ml-3"> <div class="list-icons"> <a href="#" class="list-icons-item dropdown-toggle" data-toggle="dropdown"><i class="icon-more2"></i></a> <div class="dropdown-menu dropdown-menu-right"> <button onclick="getPersonalInfo(' + row.id + ');"  class="dropdown-item"><i class="icon-profile"></i>Personal Info</button> <div class="dropdown-divider"></div> <a target="_blank" href="history?id=' + row.id + '" class="dropdown-item"><i class="icon-eye"></i>Visit History</a> <button onclick="getSkinInfo(' + row.id + ')"  class="dropdown-item"><i class="icon-stack-text"></i>Skin Info</button> <button onclick="getMedicalInfo(' + row.id + ')"  class="dropdown-item"><i class="icon-drawer3"></i>Medical Info</button> </div> </div>';
            }
        }
        ]
    });
}

function getPersonalInfo(clientId) {
    vue.getPersonalInfo(clientId);
}

function getMedicalInfo(clientId) {
    vue.getMedicalInfo(clientId);
}

function getSkinInfo(clientId) {
    vue.getSkinInfo(clientId);
}

function openConfirmDialog(clientId) {
    vue.openConfirmDialog(clientId);
}

function makeClientNew() {
    vue.makeClientNew();
}