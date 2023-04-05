
var myMixin = {
  
  data () {
      return {

         selectedFile: null
    }
  },
    methods: {
        uploadBlob: async function (url, data) {
            var promise = new Promise(function (resolve, reject) {
                axios({
                    method: 'post',
                    url: url,
                    data: data,
                    config: {
                        headers: { 'Content-Type': 'multipart/form-data' }
                    }
                })
                    .then(function (response) {
                        resolve(response);
                    })
                    .catch(function (response) {
                        reject(response);
                    });

            });
            return promise;
        },
    createData: function (url,data) {
 var promise = new Promise(function(resolve, reject) {
         axios({
                method: 'post',
                url: url,
                data: data,
                config: {
                         headers: {'Content-Type': 'multipart/form-data' }}
                        })
                .then(function (response) {
                        resolve(response);
                    })
                 .catch(function (response) {
                 reject(response);
                });

  });
     return promise;
        }
      ,
      uploadData: function (url, data) {
          var promise = new Promise(function (resolve, reject) {
              axios.post(url,data)
                  .then(function (response) {
                      resolve(response);
                  })
                  .catch(function (response) {
                      reject(response);
                  });

          });
          return promise;
      },

      delete: function (url, data) {
          var promise = new Promise(function (resolve, reject) {
              axios({
                  method: 'delete',
                  url: url,
                  data: data,
                  config: {
                      headers: {
                          'Content-Type': 'multipart/form-data',
                          'Access-Control-Allow-Origin': '*',
                          'Access-Control-Allow-Methods': 'DELETE',
                          'Access-Control-Allow-Headers': 'Content-Type'
                      },

                  }
              })
                  .then(function (response) {
                      resolve(response);
                  })
                  .catch(function (response) {
                      reject(response);
                  });
          });
          return promise;
      },
 updateData: function (url,data) {
   var promise = new Promise(function(resolve, reject) {
         axios({
                method: 'put',
                url: url,
                data: data,
             config: {
                 headers: {
                     'Content-Type': 'multipart/form-data',
                     'Access-Control-Allow-Origin': '*',
                     'Access-Control-Allow-Methods': 'PUT',
                     'Access-Control-Allow-Headers': 'Content-Type'
                 }
             }
                        })
                .then(function (response) {
                        resolve(response);
                    })
                 .catch(function (response) {
                 reject(response);
                });
  });
     return promise;
    }, deleteUrl: function (url) {
   var promise = new Promise(function(resolve, reject) {
         axios.delete(url)
                .then(function (response) {
                        resolve(response);
                    })
                 .catch(function (response) {
                 reject(response);
                });
  });
     return promise;
    },
 readData: function (url) {
   var promise = new Promise(function(resolve, reject) {
          axios.get(url)
          .then(function (response) {
            resolve(response);
          })
          .catch(function (error) {
            reject(error);
          });
  });
     return promise;
 },

 exportToPdf:function(title,url){

            this.readData(url).then(function (data) {

            var a = document.body.appendChild(
                    document.createElement("a")
                );
                a.download =title + ".pdf";
                a.href = data.data;
                a.click()

            });
 
 },
getGrowerTypes:function(){

    var promise = new Promise(function(resolve, reject) {

        this.getData('/api/lookup/GetByCategory/5b66a716aeafea09614a68d9').then(function(respose){
                         resolve(response);
            }).catch(function(error){
                         reject(error);
            });
    });
        return promise;            
        
    }
  }
}
