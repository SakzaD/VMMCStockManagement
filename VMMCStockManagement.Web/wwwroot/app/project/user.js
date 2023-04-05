
var userContext = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#users',
  
    created: function () {

        this.currentUserId = this.getParameterByName('id');

        this.getAllUsers();
        this.getAllRoles();
        this.getManagers();

    },

    data: {
        users: [],
        managers: [],
        roles: [],
        userRoles: [],
        selectedRole: '',
        currentUserId: '',

        user: {
            id: '',
            firstName: '',
            lastName: '',
            email: '',
            phoneNumber: '',
            roles: [],
            userId: '',
            employeeNumber: ''
        },

        selectedUser: {
            id: '',
            firstName: '',
            lastName: '',
            email: '',
            phoneNumber: '',
            roles: [],
            userId: '',
            employeeNumber: ''
        },

        hasError: false,
        messageInfo: '',
        userFile: '',
        fileError: false,

        modalDateError: {
            header: '',
            message: ''
        }
    },
    validations: {
        user: {
            firstName: {
                required: required
            },

            lastName: {
                required: required
            },

            email: {
                required: required
            },

            //roles: {
            //    required: required
            //},
            //managerId: {
            //    required: required
            //}

        },
        selectedUser: {
            firstName: {
                required: required
            },

            lastName: {
                required: required
            },

            email: {
                required: required
            },

            //roles: {
            //    required: required
            //},
            //managerId: {
            //    required: required
            //}

        },
        userFile: {
            required: required
        }
    },
  
    methods: {

        clearFile: function () {
            this.$refs.file.value = '';
            this.userFile = '';
        },

        onFileChange: function () {
            var selectedFile = this.$refs.file.files[0];
            console.log(selectedFile);
            if (selectedFile) {
                this.userFile = selectedFile;
            } else {
                this.userFile = '';
            }
        },

        clearUser: function () {
            this.user = {
                id: '',
                firstName: '',
                lastName: '',
                email: '',
                phoneNumber: '',
                roleId: '',
                userId: '',
                employeeNumber: ''
            };
        },

        uploadFile: async function () {
            this.fileError = false;
            this.$v.userFile.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.userFile.$pending || this.$v.userFile.$error) {
                this.fileError = true;
                return;
            }

            let me = this;

            let btnUpload = Ladda.create(document.querySelector('#btnUpload'));
            btnUpload.start();

            var formData = new FormData();
            formData.append("userId", this.userId);
            formData.append("userFile", this.userFile);

            console.log(this.userFile.name);
            await me.uploadBlob('/api/user/upload-users', formData)

                .then(async function (response) {
                    btnUpload.stop();
                    if (response.data.status == 'success') {
                        $('#modalAddBulk').modal('hide');

                        me.notify("Success", "File was successfully uploaded.", "success");
                        //me.getAssets();
                        //me.getAllUsers();
                    } else {
                        if (response.data.message)
                            me.notify("Error", response.data.message, "danger");
                        else
                            me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                        //me.failureNotify("Ooops something went wrong, try again later.");
                    }

                })
                .catch(function (error) {
                    btnUpload.stop();
                    me.notify("Error", "Ooops something went wrong, try again later.", "danger");
                });
        },

        save: function () {

            this.$v.user.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.user.$pending || this.$v.user.$error) {
                this.hasError = true;
                return;
            }

            this.user.userId = this.userId;
            this.create();
        },
        edit: function () {

            this.$v.selectedUser.$touch();
            // if its still pending or an error is returned do not submit

            if (this.$v.selectedUser.$pending || this.$v.selectedUser.$error) {
                this.hasError = true;
                return;
            }

            this.selectedUser.userId = this.userId;
            this.update();
        },
        create: function () {
            let me = this;
            let btnSave = Ladda.create(document.querySelector('#btnAdd'));
            btnSave.start();

            this.createData(this.ApiUrl + '/api/authentication/create-account', this.user)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalAddNew').modal('hide');
                        me.getAllUsers();
                        me.notify("Success", "User was successfully added.", 'success');

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
            let btnSave = Ladda.create(document.querySelector('#btnUpdate'));
            btnSave.start();

            this.createData(this.ApiUrl + '/api/authentication/update-account', this.selectedUser)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {

                        $('#modalEdit').modal('hide');
                        me.getAllUsers();
                        me.notify("Success", "User was successfully updated.", 'success');

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
        isNumeric: function (n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        },

        onRoleChange: function (event) {

            console.log(event);

            this.selectedRole = event.target.value;

            console.log(this.selectedRole);
        },
        getUserRoles: function (editId) {
            let me = this;
            me.readData(this.ApiUrl + '/api/lookup/user/roles/filter?userId=' + editId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.userRoles = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        deleteUser: function () {
            let me = this;

            var request = {
                id: this.selectedUser.id,
                userId: this.userId
            }
            let btnSave = Ladda.create(document.querySelector('#btnDelete'));
            btnSave.start();
            this.createData(this.ApiUrl + '/api/user/delete', request)
                .then(function (response) {
                    btnSave.stop();
                    if (response.data.status == 'success') {
                        $('#modalDelete').modal('hide');
                        me.getAllUsers();

                        //title, message, type
                        me.notify("Success", "User was successfully deleted.", "success");

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
        getAllUsers: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/lookup/users/filter?userId='+this.userId)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#table-users').DataTable();
                        dT.destroy();

                        me.users = response.data.data;

                        me.$nextTick(function () {
                            $('#table-users').DataTable(
                                {
                                    dom: "Bfrtip",
                                    buttons: ["copy", "csv", "excel", "print", "pdf"],

                                }
                            );
                        })
                       
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getManagers: function () {
            let me = this;
            me.readData(this.ApiUrl + '/api/lookup/managers/filter?role=Manager&userId=' + this.userId)
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.managers = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getAllRoles: function () {
            console.log("UserId2: " + this.userId);

            let me = this;
            me.readData(this.ApiUrl + '/api/Lookup/all-roles/filter?userId=' + me.userId)
                .then(function (response) {

                    if (response.data.status == 'success') {
                        me.roles = response.data.data;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        addUserRole: function () {
            //console.log("UserId2: " + this.userId);

            let me = this;

            var roleDeleteRequest = {
                roleId: me.selectedRole,
                userId: me.currentUserId
            };

            console.log(roleDeleteRequest);

            me.createData(this.ApiUrl + '/api/account/add-user-role', roleDeleteRequest)
                .then(function (response) {
                   
                    if (response.data.status == 'success') {
                        me.getUserRoles(me.currentUserId);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        deleteRole: function () {
            //console.log("UserId2: " + this.userId);

            let me = this;

            var roleDeleteRequest = {
                roleId: me.selectedRole.id,
                userId: me.currentUserId
            };

            me.createData(this.ApiUrl + '/api/account/remove-role', roleDeleteRequest)
                .then(function (response) {
                    $('#deleteConfirmation').modal('hide');
                    if (response.data.status == 'success') {
                        me.getUserRoles(me.currentUserId);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        getUserRole: function (userRole) {

            this.user.roles = userRole;
        },
        getUserRoleUpdate: function (userRole) {

            console.log(userRole);
            this.selectedUser.roles = userRole;
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
$('#selectedRoles').on('change', function () {

    var userRole = $('#selectedRoles').val();

    if (userRole.length > 0) {
        userContext.getUserRole(userRole);
    }
});

$('#selectedRolesUpdate').on('change', function () {

    var userRole = $('#selectedRolesUpdate').val();

    if (userRole.length > 0) {
        userContext.getUserRoleUpdate(userRole);
    }
});