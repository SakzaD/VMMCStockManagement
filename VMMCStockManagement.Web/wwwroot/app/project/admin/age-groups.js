var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#agegroups',
    created: function () {
        this.getAgeGroups();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
  
        ageGroups: [],
        ageGroup: {
            id: 0,
            name: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedAgeGroup: ''
    },
    validations: {
        ageGroup: {
            name: {
                required: required,
            },
           
        }
    },
    methods: {

        clearAgeGroup: function () {
            this.ageGroup.id = 0;
            this.ageGroup.name = '';
        },
        save: function () {

            this.$v.ageGroup.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.ageGroup.$pending || this.$v.ageGroup.$error) {
                this.hasError = true;
                return;
            }

            this.ageGroup.userId = this.userId;

            console.log(this.ageGroup.id);
            if (this.ageGroup.id && this.ageGroup.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/AgeGroup/update', this.ageGroup)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        //me.successNotify("Successfully updated");
                        //me.getAgeGroups();

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
            this.createData(this.ApiUrl + '/api/AgeGroup/create', this.ageGroup)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");

                        $('#createModal').modal('hide');

                        me.getAgeGroups();


                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        deleteAgeGroup: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedAgeGroup.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/AgeGroup/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getAgeGroups();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },


        getAgeGroups: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/AgeGroup/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.ageGroups = response.data.data;
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
