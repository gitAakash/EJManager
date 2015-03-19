monitr.model.Cassette = function () {
    var self = this;
    self.currency = ko.observable();
    self.denomination = ko.observable();
    self.available = ko.observable();
    self.total = ko.observable();
    self.description = ko.computed(function () {
        return this.currency() + " " + this.denomination();
    }, self);
    self.percentage = ko.computed(function() {
        return this.available() && this.total() ? Math.round((this.available() / this.total()) * 100) : 0;
    }, self),
    self.statusMessage = ko.computed(function() {
        return this.percentage() + "% (" + this.available() + " of " + this.total() + ")";
    }, self),
    self.percentageStyle = ko.computed(function() {
        return this.percentage() ? this.percentage() + "%" : '0%';
    }, self);
};