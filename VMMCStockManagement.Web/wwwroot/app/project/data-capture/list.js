
var captureData = new Vue({
    mixins: [myMixin, notification, adaptconfig],
    el: '#report-list',
    created: function () {
        this.getReportList();
    },
    mounted: function () {
   
        //this.userId = $('#userId').val();
    },
    data: {
    
        reportList: [],
        selectedItem: {}

    },
    methods: {

        setSelectedState: function (item) {
            this.selectedItem = item;
        },

        getReportList: function () {
            let me = this;
            me.isloading = true;
            me.readData(this.ApiUrl + '/api/DataCapture/filter')
                .then(function (response) {

                    if (response.data.status == 'success') {

                        var dT = $('#buttons-datatables2').DataTable();
                        dT.destroy();

                        me.reportList = response.data.data;

                        me.$nextTick(function () {
                            $('#buttons-datatables2').DataTable(
                                {
                                    dom: "Bfrtip", buttons: ["copy", "csv", "excel", "print", "pdf"]
                                }
                            );
                        })

                        me.isloading = false;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        deleteRecord() {
            let me = this;

            var request = {
                id: this.selectedItem.id,
                userId: this.userId
            }

            this.delete(this.ApiUrl + '/api/DataCapture/delete', request )
                .then(function (response) {
              
                    if (response.data.status == 'success') {

                        $('.delete').modal('hide');

                        me.successNotify("Successfully deleted");

                        me.getReportList();
                    
                } else {
                    me.failureNotify("Ooops something went wrong, try again later.");
                  }
                })
                .catch(function (error) {
                   
                });
        },
    }
});
