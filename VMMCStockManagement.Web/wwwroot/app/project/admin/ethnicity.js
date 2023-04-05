var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#ethnicity',
    created: function () {
        this.getEthnicities();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
  
        ethnicities: [],
        ethnicity: {
            id: 0,
            name: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedEthnicity: ''
    },
    validations: {
        ethnicity: {
            name: {
                required: required,
            },
           
        }
    },
    methods: {

        clearEthnicity: function () {
            this.ethnicity.id = 0;
            this.ethnicity.name = '';
        },
        save: function () {

            this.$v.ethnicity.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.ethnicity.$pending || this.$v.ethnicity.$error) {
                this.hasError = true;
                return;
            }

            this.ethnicity.userId = this.userId;

            console.log(this.ethnicity.id);
            if (this.ethnicity.id && this.ethnicity.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/ethnicity/update', this.ethnicity)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        //me.successNotify("Successfully updated");
                        me.getEthnicities();

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
            this.createData(this.ApiUrl + '/api/ethnicity/create', this.ethnicity)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");

                        $('#createModal').modal('hide');

                        me.getEthnicities();


                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        deleteEthnicity: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedEthnicity.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/ethnicity/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getEthnicities();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },


        getEthnicities: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/ethnicity/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.ethnicities = response.data.data;
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
