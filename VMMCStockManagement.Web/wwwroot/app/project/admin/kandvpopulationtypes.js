var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#kandvpopulationtypes',
    created: function () {
        this.getKAndVpoPulationTypes();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
  
        kandvpopulationtypes: [],
        kandvpopulationtype: {
            id: 0,
            name: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedKAndVPopulationType: ''
    },
    validations: {
        kandvpopulationtype: {
            name: {
                required: required,
            },
           
        }
    },
    methods: {

        clearKAndVPopulationType: function () {
            this.kandvpopulationtype.id = 0;
            this.kandvpopulationtype.name = '';
        },
        save: function () {

            this.$v.kandvpopulationtype.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.kandvpopulationtype.$pending || this.$v.kandvpopulationtype.$error) {
                this.hasError = true;
                return;
            }

            this.kandvpopulationtype.userId = this.userId;

            console.log(this.kandvpopulationtype.id);
            if (this.kandvpopulationtype.id && this.kandvpopulationtype.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/KAndVPopType/update', this.kandvpopulationtype)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        //me.successNotify("Successfully updated");
                        me.getKAndVpoPulationTypes();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        create: function () {
            let me = this;
            this.createData(this.ApiUrl + '/api/KAndVPopType/create', this.kandvpopulationtype)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");

                        $('#createModal').modal('hide');

                        me.getKAndVpoPulationTypes();


                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        deleteKAndVPopulationType: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedKAndVPopulationType.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/KAndVPopType/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getKAndVpoPulationTypes();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },


        getKAndVpoPulationTypes: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/kabdvtypes/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.kandvpopulationtypes = response.data.data;
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
