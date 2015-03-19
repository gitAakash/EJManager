monitr.model.Atm = function () {
    var self = this;
    self.terminalName = ko.observable();
    self.terminalFullName = ko.observable();
    self.terminalId = ko.observable();
    self.location = ko.observable();
    self.branchId = ko.observable();
    self.regionId = ko.observable();
    self.branchName = ko.observable();
    self.regionName = ko.observable();
    self.uptimeStatus = ko.observable();
    self.terminalShortName = ko.computed(function () {
        if (this.terminalFullName()) {
            var name = this.terminalFullName();
            var index = name.indexOf('_');

            if (index > -1) {
                name = name.substring(index + 1);
            }
            
            if (name.length > 16) {
                name = name.substring(0, 16);
            }

            return name;
        }
        
        return this.terminalFullName();
    }, self);
    self.atmDetailUrl = ko.computed(function() {
         return 'Details/' + this.terminalId();
    }, self);
    self.isOnline = ko.computed(function() {
        return this.uptimeStatus() ? this.uptimeStatus() === 'In Service' : false;
    }, self),
    self.isOffline = ko.computed(function() {
        return this.uptimeStatus() ? this.uptimeStatus() === 'Off-line' : false;
    }, self),
    self.isInSupervisorMode = ko.computed(function() {
        return this.uptimeStatus() ? this.uptimeStatus() === 'Supervisor' : false;
    }, self),
    self.isLowCashMode = ko.computed(function() {
        return this.uptimeStatus() ? this.uptimeStatus() === 'Low-Cash' : false;
    }, self),
    self.isClosedMode = ko.computed(function() {
        return this.uptimeStatus() ? this.uptimeStatus() === 'Closed' : false;
    }, self),
    self.uptimeStatusCss = ko.computed(function() {
        var css = "";
        if (this.isOnline() === true) {
            css = "tiles-primary"; //"tiles-green";
        } else if (this.isInSupervisorMode() === true) {
            css = "tiles-indigo";  //"tiles-orange";
        } else if (this.isOffline() === true) {
            css = "tiles-danger";
        } else if (this.isLowCashMode() === true) {
            css = "tiles-sky";  //"tiles-warning";
        } else if (this.isClosedMode() === true) {
            css = "tiles-closed"; //"tiles-inverse";
        }

        return css;
    }, self),
    self.uptimeStatusLabelCss = ko.computed(function() {
        var css = "";
        if (this.isOnline() === true) {
            css = "label-success";
        } else if (this.isInSupervisorMode() === true) {
            css = "label-warning";
        } else if (this.isOffline() === true) {
            css = "label-danger";
        } else if (this.isLowCashMode() === true) {
            css = "label-sky";
        } else if (this.isClosedMode() === true) {
            css = "label-black";
        }

        return css;
    }, self),
    self.statusText = ko.computed(function() {
        var css = "";
        if (this.isOnline() === true) {
            css = "In Service";
        } else if (this.isInSupervisorMode() === true) {
            css = "Supervisor Mode";
        } else if (this.isOffline() === true) {
            css = "Offline";
        } else if (this.isLowCashMode() === true) {
            css = "Low Cash";
        } else if (this.isClosedMode() === true) {
            css = "Closed";
        }

        return css;
    }, self),
    self.nameAndLocation = ko.computed(function() {
        return this.terminalName + " (" + this.location + ")";
    }, self),
    self.sortOrder = ko.computed(function() {
        var order = 99999;
        if (this.isOnline() === true) {
            order = monitr.util.Configuration.inServiceAtmSortOrder;
        } else if (this.isInSupervisorMode() === true) {
            order = monitr.util.Configuration.supervisorModeAtmSortOrder;
        } else if (this.isOffline() === true) {
            order = monitr.util.Configuration.offlineAtmSortOrder;
        } else if (this.isLowCashMode() === true) {
            order = monitr.util.Configuration.lowCashAtmSortOrder;
        } else if (this.isClosedMode() === true) {
            order = monitr.util.Configuration.closedAtmSortOrder;
        }

        return order;
    }, self);
    //self.sortOrder = ko.computed(function () {
    //    var order = 99999;
    //    if (this.isOnline() === true) {
    //        order = monitr.util.Constants.inServiceAtmSortOrder;
    //    } else if (this.isInSupervisorMode() === true) {
    //        order = monitr.util.Constants.supervisorModeAtmSortOrder;
    //    } else if (this.isOffline() === true) {
    //        order = monitr.util.Constants.offlineAtmSortOrder;
    //    } else if (this.isLowCashMode() === true) {
    //        order = monitr.util.Constants.lowCashAtmSortOrder;
    //    } else if (this.isClosedMode() === true) {
    //        order = monitr.util.Constants.closedAtmSortOrder;
    //    }

    //    return order;
    //}, self);

};