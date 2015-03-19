// atmDataService.js
// Depends on scripts:
//                         ajaxservice.js
// 
(function (monitr) {
    "use strict";
    monitr.services.terminalDataService = {
        getAtms: function (callback) {
            //my.ajaxService.ajaxGetJsonp("GetSaleItems", null, callback);
            monitr.services.ajaxService.ajaxGetJson("api/v1/terminals", null, callback);
        },
        
        getTerminal: function (callback, terminalId) {
            var url = "api/v1/terminals/" + terminalId;
            monitr.services.ajaxService.ajaxGetJson(url, null, callback);
        },
        
        getTerminalCassettes: function (callback, terminalId) {
            var url = "api/v1/terminals/" + terminalId + "/cassettes";
            monitr.services.ajaxService.ajaxGetJson(url, null, callback);
        },
        
        getTerminalTransactionSummary: function (callback, type, terminalId, fromDate, toDate) {
            var url = "api/v1/transactions/summaries/" + type + "?terminalId=" + terminalId + "&fromDate=" + fromDate + "&toDate=" + toDate;
            monitr.services.ajaxService.ajaxGetJson(url, null, callback);
        },
        
        getTerminalHardwareStatus: function (callback, terminalId) {
            var url = "api/v1/terminals/" + terminalId + "/hardwarestatuses";
            monitr.services.ajaxService.ajaxGetJson(url, null, callback);
        },
        
        getTerminalEventSummary: function (callback, terminalId, fromDate, toDate) {
            var url = "api/v1/events/summaries?terminalId=" + terminalId + "&fromDate=" + fromDate + "&toDate=" + toDate;
            monitr.services.ajaxService.ajaxGetJson(url, null, callback);
        },
        
    };
}(monitr));