
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#notification',
    created: function () {
       
    },
    mounted: function () {

        this.getNotifications();
    },
    data: {

        notification: {
            numberOfRequest : 0
        },
  

    },
    methods: {


        getNotifications: function () {
            let me = this;

            me.readData(this.ApiUrl + '/api/Dashboard/notification')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        me.notification = response.data.data;

                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }
});
