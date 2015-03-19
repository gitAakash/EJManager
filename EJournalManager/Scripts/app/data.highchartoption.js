monitr.model.PieOptions = {
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
    }]
}

//var options = {
//    chart: {
//        renderTo: 'container',
//        type: 'spline'
//    },
//    series: [{}]
//};