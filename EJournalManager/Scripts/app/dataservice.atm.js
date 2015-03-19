// atmDataService.js
// Depends on scripts:
//                         ajaxservice.js
// 
(function (monitr) {
    "use strict";
    monitr.services.atmDataService = {
        getAtms: function (callback) {
            //my.ajaxService.ajaxGetJsonp("GetSaleItems", null, callback);
            monitr.services.ajaxService.ajaxGetJson("Atms/GetAtms", null, callback);
        }
    };
}(monitr));