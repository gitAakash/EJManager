// atmDataService.js
// Depends on scripts:
//                         ajaxservice.js
// 
(function (monitr) {
    "use strict";
    monitr.services.AdminDataService = {
        getAllRegions: function (callback) {
            //my.ajaxService.ajaxGetJsonp("GetSaleItems", null, callback);
            monitr.services.ajaxService.ajaxGetJson("api/v1/regions", null, callback);
        },
        
        getRegionBranches: function (callback, region) {
            //my.ajaxService.ajaxGetJsonp("GetSaleItems", null, callback);
            var regionBranchUrl = '';
            if (region)
                regionBranchUrl = "api/v1/regions/" + region + "/branches";
            else
                regionBranchUrl = "api/v1/branches";
            monitr.services.ajaxService.ajaxGetJson(regionBranchUrl, null, callback);
        }
    };
}(monitr));