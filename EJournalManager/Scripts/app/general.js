var monitr = monitr || {
    services: {},
    model: {},
    vm: {},
    util: {}
};


(function(monitr) {
    monitr.util.Configuration = function () {
        var config = {
            'OFF_LINE_SORT_ORDER': 1,
            'IN_SERVICE_SORT_ORDER': 2,
            'SUPERSVISOR_MODE_SORT_ORDER': 3,
            'LOW_CASH_SORT_ORDER': 4,
            'CLOSED_SORT_ORDER': 5,
            'BASE_URL': 'http://localhost/AtmMonitoring/'
        },
            get = function(name) { return config[name]; },
            offlineAtmSortOrder = config['OFF_LINE_SORT_ORDER'],
            inServiceAtmSortOrder = config['IN_SERVICE_SORT_ORDER'],
            supervisorModeAtmSortOrder = config['SUPERSVISOR_MODE_SORT_ORDER'],
            lowCashAtmSortOrder = config['LOW_CASH_SORT_ORDER'],
            closedAtmSortOrder = config['CLOSED_SORT_ORDER'],
            baseUrl = config['BASE_URL'];
        return {
            get: get,
            offlineAtmSortOrder: offlineAtmSortOrder,
            inServiceAtmSortOrder: inServiceAtmSortOrder,
            supervisorModeAtmSortOrder: supervisorModeAtmSortOrder,
            lowCashAtmSortOrder: lowCashAtmSortOrder,
            closedAtmSortOrder: closedAtmSortOrder,
            baseUrl: baseUrl
        };
        //var offlineAtmSortOrder = 1,
        //    inServiceAtmSortOrder = 2,
        //    supervisorModeAtmSortOrder = 3,
        //    lowCashAtmSortOrder = 4,
        //    closedAtmSortOrder = 5;
        //return {            
        //    offlineAtmSortOrder: 1,
        //    inServiceAtmSortOrder: inServiceAtmSortOrder,
        //    supervisorModeAtmSortOrder: supervisorModeAtmSortOrder,
        //    lowCashAtmSortOrder: lowCashAtmSortOrder,
        //    closedAtmSortOrder: closedAtmSortOrder
        //};
    }();
}(monitr));


function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}