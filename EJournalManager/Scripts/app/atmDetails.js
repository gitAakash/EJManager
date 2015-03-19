$(function () {
    initialView();
    
    monitr.vm.atmDetails.loadTerminalInfo();
    ko.applyBindings(monitr.vm.atmDetails);
    
    monitr.vm.atmDetails.loadCassettes();
    monitr.vm.atmDetails.loadTransactionSummay();
    monitr.vm.atmDetails.loadHardwareInfos();
    monitr.vm.atmDetails.loadTransactionSummaryDataByResponseCode();
    monitr.vm.atmDetails.loadTransactionSummaryDataByTransType();
    monitr.vm.atmDetails.loadEventSummay();

    $("#btnApply").click(function () {
        monitr.vm.atmDetails.activities([]); // = ko.observableArray([]);
        monitr.vm.atmDetails.loadTransactionSummay();
        monitr.vm.atmDetails.loadTransactionSummaryDataByResponseCode();
        monitr.vm.atmDetails.loadTransactionSummaryDataByTransType();
        monitr.vm.atmDetails.loadEventSummay();
    });
});


function initialView() {
    //var fromDate = $('#FromDate').datepicker({
    //    format: 'dd/mm/yyyy'
    //}).on('changeDate', function (ev) {
    //    fromDate.hide();
    //}).data('datepicker');

    //var toDate = $('#ToDate').datepicker({
    //    format: 'dd/mm/yyyy'
    //}).on('changeDate', function (ev) {
    //    toDate.hide();
    //}).data('datepicker');

    $('#FromDate').datetimepicker();
    $('#ToDate').datetimepicker();
    $('#responseChart').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: null
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        legend: {
            itemStyle: {
                color: '#000000',
                fontWeight: 'bold',
                fontSize: '9px'
            }
        },
        series: [{
            type: 'pie',
            name: 'Frequency',
            data: []
        }]
    });
    
    $('#transTypeChart').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: null
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        legend: {
            itemStyle: {
                color: '#000000',
                fontWeight: 'bold',
                fontSize: '9px'
            }
        },
        series: [{
            type: 'pie',
            name: 'Frequency',
            data: []
        }]
    });
}