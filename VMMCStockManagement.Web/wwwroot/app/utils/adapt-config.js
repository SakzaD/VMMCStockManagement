Vue.use(window.vuelidate.default);
const { required, requiredIf, minLength, maxLength, numeric, email } = window.validators;

var adaptconfig = {
    mounted() {
        if (this.$refs.userId) {
            this.userId = this.$refs.userId.value;
        }
    },
    data() {
        return {
            ApiUrl: "https://localhost:5001",
            //ApiUrl: "https://vmmcstockmanagement.righttocare.org",
            ApiUrlUat: "http://10.7.2.74/adapt",
            userId: '',
            isloading: false

        }
    }, methods: {

        //formatServerDateTimeTwo: function (date) {

        //    var unixTimestamp = Date.parse(date);
        //    var dateObject = new Date(unixTimestamp)

        //    var year = dateObject.getFullYear();
        //    var month = dateObject.getMonth() + 1;
        //    var day = dateObject.getDate();

        //    var monthtodisplay = month.toString();
        //    if (month.toString().length == 1) {
        //        monthtodisplay = '0' + monthtodisplay;
        //    }


        //    var daytodisplay = day.toString();
        //    if (day.toString().length == 1) {
        //        daytodisplay = '0' + daytodisplay;
        //    }

        //    return year + '-' + monthtodisplay + '-' + daytodisplay;
        //},
        //redirectAfterwaiting: function (url) {

        //    setInterval(() => {
        //        window.location.href = url;
        //    }, 2000)
        //},
        downloadFile: function (fileName) {

            var filePath = "https://localhost:5001/attachments/Specification/"+fileName;
            //var filePath = "https://itasset.righttocare.org/attachments/Specification/" + fileName;
            var link = document.createElement("a");
            link.href = filePath;

            link.style = "visibility:hidden";
            link.download = fileName;

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        },
        redirectToPage: function (url) {

            setInterval(() => {
                window.location.href = url;
            }, 2000)
        },

    }
}