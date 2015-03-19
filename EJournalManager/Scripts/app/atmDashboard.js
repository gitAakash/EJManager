$(function() {
    var hub = $.connection.atmHub;

    $.connection.hub.start().done(function () {
        monitr.vm.atmDashboard.loadAtms();  //hub.server.getAtms();
        monitr.vm.atmDashboard.loadRegions();
        monitr.vm.atmDashboard.loadBranches();
    });

    hub.client.updateTerminalStatuses = function (atmList) {
        monitr.vm.atmDashboard.addOrUpdateAtms(atmList);
    };
    
    ko.applyBindings(monitr.vm.atmDashboard);
});