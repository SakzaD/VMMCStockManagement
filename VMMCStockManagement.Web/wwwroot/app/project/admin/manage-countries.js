var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#manage-countries',
    created: function () {
    
        this.getCountries();
    
    },
    mounted: function () {
        //this.userId = $('#userId').val();
        //this.reportingData.userId = $('#userId').val();
    },
    data: {
        userId: '',
     
        countries: [],
       
        country: {
            id: 0,
            name: '',
            userId: ''
        },
        selectedCountry: '',
        hasError: false,
    },
    validations: {
        country: {
            name: {
                required: required,
            }
        }
    },
    methods: {

        clearCountry: function () {
            this.country = {
                id: 0,
                name: '',
                userId: ''
            };
        },
        save: function () {
            this.$v.country.$touch();
            // if its still pending or an error is returned do not submit


            if (this.$v.country.$pending || this.$v.country.$error) {
                this.hasError = true;
                return;
            }

            this.country.userId = this.userId;

            if (this.country.id && this.country.id > 0) {
                this.update();
            } else {
                this.create();
            }
          
        },
        update: function () {
            let me = this;

            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();

            this.createData(this.ApiUrl +'/api/Country/update', this.country)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalAddNew').modal('hide');
                        me.notify("Success", "Country successfully updated.", 'success');
                        me.getCountries();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },

        create: function () {
            let me = this;

            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();

            this.createData(this.ApiUrl + '/api/Country/create', this.country)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalAddNew').modal('hide');
                        me.notify("Success", "Country successfully added.", 'success');
                        me.getCountries();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },

        deleteCountry: function () {
            let me = this;
            //this.reportingData.userId = this.userId;

            var request = {
                id: this.selectedCountry.id,
                userId: this.userId
            }

            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData('/api/Country/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalDelete').modal('hide');
                        me.notify("Success", "Country successfully deleted.", 'success');
                        me.getCountries();

                    } else {
                        me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },
      

        getCountries: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/country/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                  
           
                        var dT = $('#tblCountry').DataTable();
                        dT.destroy();

                        me.countries = response.data.data;

                        me.$nextTick(function () {
                            $('#tblCountry').DataTable(
                                {
                                    dom: "Bfrtip", buttons: ["copy", "csv", "excel", "print", "pdf"]
                                }
                            );
                        })
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
