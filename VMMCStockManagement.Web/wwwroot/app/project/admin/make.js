var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#make',
    created: function () {

        this.getMakes();

    },

    data: {

        makes: [],
        make: {
            id: 0,
            name: '',
            userId: '',

        },

        selectedMake: '',
        hasError: false,

    },
    validations: {
        make: {
            name: {
                required: required
            },
        },
    },
    methods: {

        clearMake: function () {
            this.make = {
                id: 0,
                name: '',
                userId: '',

            };
        },
        save: function () {

            this.$v.make.$touch();

            if (this.$v.make.$pending || this.$v.make.$error) {
                this.hasError = true;
                return;
            }

            this.make.userId = this.userId;
            if (this.make.id == '' || this.make.id == undefined) {
                this.create();
            } else {
                this.update();
            }
         
        },

        create: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();

            this.createData(this.ApiUrl + '/api/make/create', this.make)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalEditAdd').modal('hide');
                        me.getMakes();
                        me.notify("Success", "make successfully added.", 'success');

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },

        update: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/make/update', this.make)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEditAdd').modal('hide');
                        me.getMakes();

                        //title, message, type
                        me.notify("Success", "Grant successfully updated.", "success");

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },

        deleteMake: function () {
            let me = this;

            var request = {
                id: this.selectedMake.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/make/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getMakes();

                        //title, message, type
                        me.notify("Success", "make was successfully deleted.", "success");

                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }
                })
                .catch(function (error) {
                    btnSave.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },

        getMakes: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/make/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#tblMake').DataTable();
                        dT.destroy();

                        me.makes = response.data.data;

                        me.$nextTick(function () {
                            $('#tblMake').DataTable(
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
