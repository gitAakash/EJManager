// ajaxService
// John Papa http://johnpapa.net
// Depends on scripts:
//                         jQuery
(function (monitr) {
    "use strict";
    //TODO:  put your hosting server here
    // var serviceBase = 'http://localhost:51015/',

    //var serviceBase = 'http://atmonitor.aspnetdevelopment.in/',

    var serviceBase = monitr.util.Configuration.baseUrl,
    
        getSvcUrl = function (method) { return serviceBase + method; };

    monitr.services.ajaxService = (function () {
        var ajaxGetJson = function (method, jsonIn, callback) {
            $.ajax({
                url: getSvcUrl(method),
                type: "GET",
                data: ko.toJSON(jsonIn),
                dataType: "json",
                contentType: "application/json",
                success: function (json) {
                    callback(json);
                }
            });
        },
        ajaxPostJson = function (method, jsonIn, callback) {
            $.ajax({
                url: getSvcUrl(method),
                type: "POST",
                data: ko.toJSON(jsonIn),
                dataType: "json",
                contentType: "application/json",
                success: function (json) {
                    callback(json);
                }
            });
        };
        return {
            ajaxGetJson: ajaxGetJson,
            ajaxPostJson: ajaxPostJson
        };
    })();
}(monitr));