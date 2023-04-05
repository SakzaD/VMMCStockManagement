
var dashboard = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#profile-layout',
    created: function () {
    
    },
    mounted: function () {
        //this.showLoader(true);
    },
    data: {
        profile: {
            currentPassword: '',
            newPassword: '',
            confirmPassword: ''
            
        },
        status: {
            hasError: false,
            message: ''
        }
    
    },
    methods: {
     
       changePassword: function() {
            let me = this;

            var formData = new FormData();

            for (var propt in this.profile) {

                formData.append(propt, this.profile[propt]);
            }

            var url = '/api/Account/ChangePassword/ChangePassword?currentPassword=' +
                this.profile.currentPassword + '&newPassword=' + this.profile.newPassword +
                '&confirmPassword=' + this.profile.confirmPassword;
            let button = Ladda.create(document.querySelector('#changePasswordButton'));
            button.start();
            me.createData(url)
                .then(function (response) {
                    button.stop();
                    me.status = response.data;
                    if (response.status === 200) {
                        if (response.data.hasError === false) {
                            me.profile.currentPassword = '';
                            me.profile.newPassword = '';
                            me.profile.confirmPassword = '';
                            showNotification('Success', 'Password was updated successfully.', 'success');
                        } else {
                            showNotification('Error', response.data.message, 'danger');
                        }
                      
                    } else {
                        showNotification('Error', 'Something wrong happended, trying to update password.', 'danger');
                    }
                })
                .catch(function (error) {
                    button.stop();
                    console.log(error);
                    showNotification('Error', 'Something wrong happended, trying to update password.', 'danger');
                });
        },


        getParameterByName(name, url) {

            if (!url) url = window.location.href;

            name = name.replace(/[\[\]]/g, '\\$&');

            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);

            if (!results) return null;

            if (!results[2]) return '';

            return decodeURIComponent(results[2].replace(/\+/g, ' '));
            
        },
        showLoader: function (isLoading) {
            showLoader(isLoading);
        }
    }
});
