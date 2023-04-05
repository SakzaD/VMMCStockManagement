
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#manage-user',
    created: function () {

        this.getUserList();
  
    },
    mounted: function () {
        //this.showLoader(true);
        this.manageUser.userId = this.userId;
        this.getRoles();
    },

    data: {

        manageUser: {

            id: 0,
            userId: '',
            firstName: '',
            lastName: '',
            phone: '',
            email: '',
            facility: [],
            roles: [],
            password: ''

        },

        usertList: [],
        roles: [],
        hasError: false
    },

    validations: {

        manageUser: {

            firstName: {
                required: required
            },
            lastName: {
                required: required
            },
            phone: {
                required: required,
                numeric,
                minLength: minLength(10),
                maxLength: maxLength(10)
            },
            email: {
                required: required,
                email
            },
            password: {
                required: required,
                minLength: minLength(8),
            },
            roles: {
                required: required,
            },
        },
    },

    methods: {


        save: function () {

            this.$v.manageUser.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.manageUser.$pending || this.$v.manageUser.$error) {
                this.hasError = true;
                $('.hidden').show();
                return;
            }

            this.manageUser.userId = this.userId;

            this.isloading = true;
            if (this.manageUser.id && this.manageUser.id > 0) {
                this.update();
            } else {
                this.create();
            }
        },

        create: function () {
            let me = this;
            this.createData(this.ApiUrl + '/api/Account/create-account', this.manageUser)
                .then(function (response) {

                    me.isloading = false;

                    if (response.data.status == 'success') {

                        me.successNotify("Successfully created");

                        var url = me.ApiUrl + '/ManageUser';

                        me.redirectAfterwaiting(url);
                    } else if (response.data.status == 'fail') {
                        me.failureNotify(response.data.message);
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

        },

        getRoles: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/roles/filter?userId='+this.userId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.roles = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getUserList: function () {
            let me = this;
            me.isloading = true;
            me.readData(this.ApiUrl + '/api/Account/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.usertList = response.data.data;

                        $('.hidden').show();

                        me.$nextTick(function () {
                            $('#buttons-datatables2').DataTable(
                                {
                                    dom: "Bfrtip", buttons: ["copy", "csv", "excel", "print", "pdf"]
                                }
                            );
                        }),
                     
                        me.isloading = false;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        deleteTreatment() {
            let me = this;
            let button = Ladda.create(document.querySelector('#deleteTreatment'));
            button.start();
            this.updateData('/Treatment/Delete?id=' + this.selectedTreatment.id)
                .then(function (response) {
                    button.stop();
                    if (response.status === 200) {
                        $('#deleteTreatmentModal').modal('toggle');
                        showNotification('Success', 'Treatment was deleted successfully.', 'success');
                        me.getTreatmentList();
                    } else {
                        showNotification('Error: err', 'Something wrong happended, trying to delete treatment.', 'danger');
                    }
                })
                .catch(function (error) {
                    button.stop();
                    showNotification('Error: catch', 'Something wrong happended, trying to delete treatment.', 'danger');
                });

        },

    }
});
