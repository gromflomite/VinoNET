/*Comprobación de datos*/
console.log(colourNames);
console.log(colourScores);
console.log(topTenName);
console.log(topTenScore);



/*Gráfico D.O MÁS VISITADAS POR LA ACTIVIDAD DE LOS USUARIOS, vista general de tintos, blancos y rosados de cada una*/
Highcharts.chart(document.getElementById('sources'), {
    colors: ['#882641', '#e4db97', '#ec97ae'],
    chart: {
        type: 'column',
        inverted: true,
        polar: true
    },
    title: {
        text: 'Denominaciones de origen más visitadas por la actividad de los usuarios (%)'
    },
    tooltip: {
        outside: true
    },
    pane: {
        size: '100%',
        endAngle: 270
    },
    xAxis: {
        tickInterval: 1,
        labels: {
            align: 'right',
            useHTML: true,
            allowOverlap: true,
            step: 1,
            y: 4,
            style: {
                fontSize: '10px'
            }
        },
        lineWidth: 0,
        categories: sourceNames,
            
    },
    yAxis: {
        lineWidth: 0,
        tickInterval: 25,
        reversedStacks: false,
        endOnTick: true,
        showLastLabel: true
    },
    plotOptions: {
        column: {
            stacking: 'normal',
            borderWidth: 0,
            pointPadding: 0,
            groupPadding: 0.15
        }
    },
    series: [{
        name: 'Tintos/D.O',
        data: tintos,
    }, {
        name: 'Blancos/D.O',
        data: blancos,
    }, {
        name: 'Rosados/D.O',
        data: rosados,
    }]
});


/*Gráfico TIPOS DE VINOS MÁS VISITADOS POR LA ACTIVIDAD DE LOS USUARIOS*/
// Build the chart
Highcharts.chart('colourTaste', {
    chart: {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    },
    title: {
        text: 'Tipos de vinos más visitados por la actividad de los usuarios (%)'
    },
    tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            colors: ['#882641', '#e4db97', '#ec97ae'],
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b><br>{point.percentage:.1f} %',
                distance: -50,
                filter: {
                    property: 'percentage',
                    operator: '>',
                    value: 4
                }
            }
        }
    },
    series: [{
        name: 'Actividad',
        data: [
            { name: 'TINTOS', y: colourScores[0] },
            { name: 'BLANCOS', y: colourScores[1] },
            { name: 'ROSADOS', y: colourScores[2] },
        ]
    }]
});



/*Gráfico TOP 5 DE LA SEMANA (Vinos mejor valorados)*/

    /*Chart data */
class wine {
    constructor(name, y, drilldown) {
        this.name = name;
        this.y = y;
        this.drilldown = drilldown;
    }
};

let data = [];
for (var i = 0; i < topFiveWeekName.length; i++) {

    topFiveWeekName[i] = new wine(topFiveWeekName[i], topFiveWeekScore[i], topFiveWeekName[i]);
    data.push(topFiveWeekName[i]);

}
    /*Create chart*/
Highcharts.chart('topWinesScore', {
    colors: ['#882641', '#4a0c2d', '#4a0c2d96', '#ec97ae','#b36d88b8'],
    chart: {
        type: 'column'
    },
    title: {
        text: 'Top 5 vinos mejor valorados la última semana'
    },
    subtitle: {
        text: ''
    },
    accessibility: {
        announceNewData: {
            enabled: true
        }
    },
    xAxis: {
        type: 'category'
    },
    yAxis: {
        title: {
            text: 'Cantidad de valoraciones recibidas'
        }

    },
    legend: {
        enabled: false
    },
    plotOptions: {
        series: {
            borderWidth: 0,
            dataLabels: {
                enabled: true,
                format: '+ {point.y:.2f}'
            }
        }
    },

    tooltip: {
        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b><br/>'
    },

    series: [
        {
            name: "Vino",
            colorByPoint: true,
            data: data,
        }
    ],
});



/*Gráfico TOP 10 (Vinos mejor valorados)*/

/*Chart data */
let data2 = [];
for (var i = 0; i < topTenName.length; i++) {

    let winename = [];
    winename.push(topTenName[i]);
    winename.push(topTenScore[i]);
    data2.push(winename);

}
Highcharts.chart('topTenWines', {
    colors: ['#882641', '#e4db97', '#ec97ae'],
    chart: {
        type: 'column'
    },
    title: {
        text: 'Top 10 vinos mejor valorados por la comunidad'
    },
    subtitle: {
        text: ''
    },
    xAxis: {
        type: 'category',
        labels: {
            rotation: -45,
            style: {
                fontSize: '13px',
                fontFamily: 'Verdana, sans-serif'
            }
        }
    },
    yAxis: {
        min: 0,
        title: {
            text: 'Cantidad de likes'
        }
    },
    legend: {
        enabled: false
    },
    tooltip: {
        pointFormat: 'Cantidad de likes: <b>{point.y:.1f} </b>'
    },
    series: [{
        name: 'Population',
        data: data2,
        dataLabels: {
            enabled: true,
            rotation: -90,
            color: '#FFFFFF',
            align: 'right',
            format: '{point.y:.1f}', // one decimal
            y: 10, // 10 pixels down from the top
            style: {
                fontSize: '13px',
                fontFamily: 'Ibarra Real Nova, serif',
            }
        }
    }]
});

















///*Gráfico TOP 5 DE LA SEMANA (Vinos mejor valorados)*/
//var ctx = document.getElementById('myCanvas').getContext('2d');
//var myCanvas = new Chart(ctx, {
//    // The type of chart we want to create
//    type: 'line',

//    // The data for our dataset
//    data: {
//        labels: topFiveWeekName ,
//        datasets: [{
//            label: 'Vino/CantidadValoraciones',
//            backgroundColor: 'transparent',
//            color: '#9a681d',
//            borderColor: '#2e2e88d4',
//            data: topFiveWeekScore,
//        }]
//    },

//    // Configuration options go here
//    options: {
//        scales: {
//            yAxes: [{
//                stacked: true
//            }]
//        }
//    }
//});






/*Gráfico TIPOS DE VINOS MÁS VISITADOS POR LA ACTIVIDAD DE LOS USUARIOS*/
//var ctx2 = document.getElementById('myDoughnutChart');
//var myDoughnutChart = new Chart(ctx2, {
//    type: 'doughnut',
//    data: {
//        labels: colourNames,
//        datasets: [{
//            label: 'TIPOS DE VINOS MÁS VISITADOS POR LA ACTIVIDAD DE LOS USUARIOS',
//            backgroundColor: [
//                'rgba(255, 99, 132, 0.2)',
//                'rgba(54, 162, 235, 0.2)',
//                'rgba(255, 206, 86, 0.2)',
//                'rgba(75, 192, 192, 0.2)',
//                'rgba(153, 102, 255, 0.2)',
//                'rgba(255, 159, 64, 0.2)'
//            ],
//            borderColor: '#2e2e88d4',
//            data: colourScores,
//        }]
//    },

//    // Configuration options go here
//    options: {}
//});
