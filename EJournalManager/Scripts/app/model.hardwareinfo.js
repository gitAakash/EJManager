monitr.model.HardwareInfo = function () {
    var self = this;
    self.name = ko.observable();
    self.status = ko.observable();
    self.statusCss = ko.computed(function () {
        var css = "";
        if (this.status() === 'OK') {
            css = "label-success";
        } else if (this.status() === 'SUSPECT' || this.status() === 'LOW') {
            css = "label-info";
        } else if (this.status() === 'FAULTY' || this.status() === 'OUT' || this.status() === 'NOT CONFIGURED') {
            css = "label-danger";
        }
        return css;
    }, self);

};