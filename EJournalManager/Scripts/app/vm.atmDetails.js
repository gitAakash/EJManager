(function(monitr) {
    monitr.vm.atmDetails = function () {
        var self = this;
        var terminalInfo = new monitr.model.Atm(),
            terminalName = ko.observable(),
            terminalFullName = ko.observable(),
            terminalId = ko.observable(),
            location = ko.observable(),
            branchName = ko.observable(),
            regionName = ko.observable(),
            cassettes = ko.observableArray([]),
            activities = ko.observableArray([]),
            terminalInfos = ko.observableArray([]),
            hardwareInfos = ko.observableArray([]),
            transSummaryData = ko.observableArray([]),
            transSummaryData2 = ko.observableArray([]),
            transSummaryData3 = ko.observableArray([]),
            cassetteTotalAmount = ko.observable(),
            cassetteTotalAvailable = ko.observable(),
            cassetteCurrency = ko.observable(),
            loadTerminalInfoCallback = function(terminal) {
                terminalInfos.push(
                    new monitr.model.Atm()
                        .location(terminal.location)
                        .uptimeStatus(terminal.uptimeStatus)
                        .terminalId(terminal.terminalId)
                        .branchId(terminal.branchId)
                        .regionId(terminal.regionId)
                        .terminalName(terminal.terminalName)
                        .branchName(terminal.branchName)
                        .regionName(terminal.regionName)
                        .terminalFullName(terminal.terminalFullName)
                );

                $('#atmInfoAjaxLoading').hide();
                $('#atmInfoPanel').show();
            },
            loadTerminalInfo = function() {
                var terminalId = $("#hdfTerminalId").val();
                $('#' + 'atmInfoPanel').hide();
                $('#atmInfoAjaxLoading').show();

                monitr.services.terminalDataService.getTerminal(monitr.vm.atmDetails.loadTerminalInfoCallback, terminalId);
            },
            loadCassettesCallback = function(json) {
                if (json) {
                    cassetteTotalAmount(json.total);
                    cassetteTotalAvailable(json.avail);
                    cassetteCurrency(json.curr);

                    if (json.cassettes) {
                        $.each(json.cassettes, function(i, cassette) {
                            cassettes.push(
                                new monitr.model.Cassette()
                                    .currency(cassette.curr)
                                    .denomination(cassette.denom)
                                    .available(cassette.avail)
                                    .total(cassette.total)
                            );
                        });
                    }
                }

                $('#' + 'cassettePanel').show();
                $('#cassetteAjaxLoading').hide();
            },
            loadCassettes = function() {
                var terminalId = $("#hdfTerminalId").val();
                $('#' + 'cassettePanel').hide();
                $('#cassetteAjaxLoading').show();
                monitr.services.terminalDataService.getTerminalCassettes(monitr.vm.atmDetails.loadCassettesCallback, terminalId);
            },
            loadTransactionSummayCallback = function(transSummary) {
                var terminalId = $("#hdfTerminalId").val();
                var fromDate = new Date($("#FromDate").val()).getTime() / 1000;
                var toDate = new Date($("#ToDate").val()).getTime() / 1000;
                var transUrl = $("#hdfTransUrl").val() + '?TerminalId=' + terminalId + '&FromDateX=' + fromDate + '&ToDateX=' + toDate;

                activities.push(
                    new monitr.model.AtmActivity()
                        .name('Transaction Count')
                        .description((transSummary[0] && transSummary[0].freq && transSummary[0].freq > 0) ? transSummary[0].freq : 0)
                        .misc(transUrl)
                );
                activities.push(                    
                    new monitr.model.AtmActivity()
                        .name('Transaction Volume')
                        .description('NGN ' + ((transSummary[0] && transSummary[0].volume && transSummary[0].volume > 0) ? transSummary[0].volume.toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : '0.00'))
                        .misc(transUrl)
                );

                $('#' + 'statsPanel').show();
                $('#statsAjaxLoading').hide();
            },
            loadTransactionSummay = function() {
                var terminalId = $("#hdfTerminalId").val();
                var fromDate = new Date($("#FromDate").val()).getTime() / 1000;
                var toDate = new Date($("#ToDate").val()).getTime() / 1000;
                $('#' + 'statsPanel').hide();
                $('#statsAjaxLoading').show();
                monitr.services.terminalDataService.getTerminalTransactionSummary(monitr.vm.atmDetails.loadTransactionSummayCallback, 1, terminalId, fromDate, toDate);
            },
            loadHardwareInfosCallback = function(json) {
                $.each(json, function(i, hardwareInfo) {
                    hardwareInfos.push(
                        new monitr.model.HardwareInfo()
                            .name(hardwareInfo.name)
                            .status(hardwareInfo.status)
                    );
                });

                $('#' + 'hardwareInfoPanel').show();
                $('#hardwareInfoAjaxLoading').hide();
            },
            loadHardwareInfos = function() {
                var terminalId = $("#hdfTerminalId").val();
                $('#' + 'hardwareInfoPanel').hide();
                $('#hardwareInfoAjaxLoading').show();
                monitr.services.terminalDataService.getTerminalHardwareStatus(monitr.vm.atmDetails.loadHardwareInfosCallback, terminalId);
            },
            loadTransactionSummaryDataByResponseCodeCallback = function(data) {
                var chart = $('#responseChart').highcharts();

                var categoriesArray = [];
                var seriesData = [];
                for (var i = 0; i < data.length; i++) {
                    categoriesArray.push(data[i].category);
                    seriesData.push([data[i].category, data[i].freq]);

                }
                chart.xAxis[0].update({ categories: categoriesArray }, false);
                chart.series[0].setData(seriesData, true);
            },
            loadTransactionSummaryDataByResponseCode = function() {
                var terminalId = $("#hdfTerminalId").val();
                //alert($("#FromDate").val().getTime());
                var fromDate = new Date($("#FromDate").val()).getTime() / 1000;
                var toDate = new Date($("#ToDate").val()).getTime() / 1000;
                monitr.services.terminalDataService.getTerminalTransactionSummary(monitr.vm.atmDetails.loadTransactionSummaryDataByResponseCodeCallback, 2, terminalId, fromDate, toDate);
            },
            loadTransactionSummaryDataByTransTypeCallback = function(data) {
                var chart = $('#transTypeChart').highcharts();

                var categoriesArray = [];
                var seriesData = [];
                for (var i = 0; i < data.length; i++) {
                    categoriesArray.push(data[i].category);
                    seriesData.push([data[i].category, data[i].freq]);

                }
                chart.xAxis[0].update({ categories: categoriesArray }, false);
                chart.series[0].setData(seriesData, true);

                //    //monitr.model.PieOptions.series[0].setData(transSummaryData2);
                //    //monitr.model.PieOptions.chart = new Highcharts.Chart(monitr.model.PieOptions);

            },
            loadTransactionSummaryDataByTransType = function() {
                var terminalId = $("#hdfTerminalId").val();
                var fromDate = new Date($("#FromDate").val()).getTime() / 1000;
                var toDate = new Date($("#ToDate").val()).getTime() / 1000;
                monitr.services.terminalDataService.getTerminalTransactionSummary(monitr.vm.atmDetails.loadTransactionSummaryDataByTransTypeCallback, 3, terminalId, fromDate, toDate);
            },
            loadEventSummayCallback = function(eventSummary) {
                var terminalId = $("#hdfTerminalId").val();
                var fromDate = new Date($("#FromDate").val()).getTime() / 1000;
                var toDate = new Date($("#ToDate").val()).getTime() / 1000;
                var transUrl = $("#hdfEventUrl").val() + '?TerminalId=' + terminalId + '&FromDateX=' + fromDate + '&ToDateX=' + toDate;

                activities.push(
                    new monitr.model.AtmActivity()
                        .name('Event Count')
                        .description((eventSummary[0] && eventSummary[0].freq && eventSummary[0].freq > 0) ? eventSummary[0].freq : 0)
                        .misc(transUrl)
                );

            },
            loadEventSummay = function() {
                var terminalId = $("#hdfTerminalId").val();
                var fromDate = new Date($("#FromDate").val()).getTime() / 1000;
                var toDate = new Date($("#ToDate").val()).getTime() / 1000;
                monitr.services.terminalDataService.getTerminalEventSummary(monitr.vm.atmDetails.loadEventSummayCallback, terminalId, fromDate, toDate);
            };

        return {
            terminalName: terminalName,
            terminalFullName: terminalFullName,
            terminalInfo: terminalInfo,
            terminalId: terminalId,
            location: location,
            branchName: branchName,
            regionName: regionName,
            loadTerminalInfo: loadTerminalInfo,
            loadTerminalInfoCallback: loadTerminalInfoCallback,
            loadCassettes: loadCassettes,
            loadCassettesCallback: loadCassettesCallback,
            cassettes: cassettes,
            loadTransactionSummayCallback: loadTransactionSummayCallback,
            loadTransactionSummay: loadTransactionSummay,
            activities: activities,
            terminalInfos: terminalInfos,
            loadHardwareInfosCallback: loadHardwareInfosCallback,
            loadHardwareInfos: loadHardwareInfos,
            hardwareInfos: hardwareInfos,
            transSummaryData: transSummaryData,
            transSummaryData2: transSummaryData2,
            transSummaryData3: transSummaryData3,
            loadTransactionSummaryDataByResponseCodeCallback: loadTransactionSummaryDataByResponseCodeCallback,
            loadTransactionSummaryDataByResponseCode: loadTransactionSummaryDataByResponseCode,
            loadTransactionSummaryDataByTransTypeCallback: loadTransactionSummaryDataByTransTypeCallback,
            loadTransactionSummaryDataByTransType: loadTransactionSummaryDataByTransType,
            loadEventSummayCallback: loadEventSummayCallback,
            loadEventSummay: loadEventSummay,
            cassetteTotalAmount: cassetteTotalAmount,
            cassetteTotalAvailable: cassetteTotalAvailable,
            //cassetteDescription: cassetteDescription,
            cassetteCurrency: cassetteCurrency
        };
    }();

    monitr.vm.atmDetails.cassetteDescription = ko.computed(function () {
        var currency = (this.cassetteCurrency() ? this.cassetteCurrency() : 'NGN');
        var avail = (this.cassetteTotalAvailable() ? this.cassetteTotalAvailable().toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : '0.00');
        var total = (this.cassetteTotalAmount() ? this.cassetteTotalAmount().toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : '0.00');
        return " [" + currency + " " + avail + " of " + currency + " " + total + "]";
    }, monitr.vm.atmDetails);
}(monitr));



