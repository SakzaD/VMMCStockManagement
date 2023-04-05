var notification = {

    data() {
    },

    methods: {

         notify: function(title, message, type) {

            new PNotify({
                title: title,
                text: message,
                type: type,
                icon: true,
                addclass: 'alert bg-' + type + ' border-' + type + ' alert-styled-left'
            });
        },

        successNotify: function (message) {

            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr.success(message);
        },

        failureNotify: function (message) {

            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr.error(message);
        },

                redirectAfterwaiting2: function (url) {

            setInterval(() => {
                window.location.href = url;
            }, 2000)
        }
    }
}


