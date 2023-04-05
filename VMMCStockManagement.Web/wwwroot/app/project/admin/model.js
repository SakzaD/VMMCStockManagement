var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#model',

    created: function () {

        this.getModel();
        this.getMakes();

    },

    data: {

        models: [],
        makes: [],

        model: {

            id: 0,
            makeId: '',
            name: '',
            userId: '',

        },

        selectedModel: '',
        hasError: false,
    },

    validations: {
        model: {
            name: {
                required: required
            },
            makeId: {
                required: required
            },
        },
    },

    methods: {
        clearModel: function () {
            this.model = {

                id: 0,
                makeId: '',
                name: '',
                userId: '',

            };
        },
        save: function () {

            this.$v.model.$touch();

            if (this.$v.model.$pending || this.$v.model.$error) {
                this.hasError = true;
                return;
            }

            this.model.userId = this.userId;
            if (this.model.id == '' || this.model.id == undefined) {
                this.create();
            } else {
                this.update();
            }

        },

        create: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/model/create', this.model)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalEditAdd').modal('hide');
                        me.getModel();
                        me.notify("Success", "model successfully added.", 'success');

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
            this.createData(this.ApiUrl + '/api/model/update', this.model)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalEditAdd').modal('hide');
                        me.getModel();

                        me.notify("Success", "Grant successfully updated.", "success");

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

        deleteModel: function () {
            let me = this;

            var request = {
                id: this.selectedModel.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/model/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getModel();

                        //title, message, type
                        me.notify("Success", "model was successfully deleted.", "success");

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

        getModel: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/model/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#tblModel').DataTable();
                        dT.destroy();

                        me.models = response.data.data;

                        me.$nextTick(function () {
                            $('#tblModel').DataTable(
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

        getMakes: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/make/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.makes = response.data.data;
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
