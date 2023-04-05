var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#implementingpartners',
    created: function () {
        this.getImplementingPartners();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
  
        implementingpartners: [],
        implementingpartner: {
            id: 0,
            name: '',
            userId: ''
        }
        ,
        hasError: false,
        selectedImplementingpartner: ''
    },
    validations: {
        implementingpartner: {
            name: {
                required: required,
            },
           
        }
    },
    methods: {


        save: function () {

            this.$v.implementingpartner.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.implementingpartner.$pending || this.$v.implementingpartner.$error) {
                this.hasError = true;
                return;
            }

            this.implementingpartner.userId = this.userId;
            //alert('UserId: ' + this.userId);
            if (this.implementingpartner.id && this.implementingpartner.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },
        update: function () {
            let me = this;

            this.createData(this.ApiUrl + '/api/implementingpartner/update', this.implementingpartner)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        $('#createModal').modal('hide');
                        me.successNotify("Successfully updated");
                        me.getImplementingPartners();

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
            this.createData(this.ApiUrl + '/api/implementingpartner/create', this.implementingpartner)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        //me.successNotify("Successfully created");

                        $('#createModal').modal('hide');

                        if (me.subDistrictId) {
                            me.getImplementingPartners();
                        }


                        me.getOperations();


                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },
        deleteImplementingpartner: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedOperation.id,
                userId: this.userId
            }

            this.createData(this.ApiUrl + '/api/implementingpartner/delete', request)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        $('#deleteModal').modal('hide');
                        //me.successNotify("Successfully deleted");
                        me.getImplementingPartners();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    alert('Exception: ' + error);
                });
        },


        getImplementingPartners: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/implementingpartner/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.implementingpartners = response.data.data;
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
