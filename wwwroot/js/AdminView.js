/*Comprobación de datos*/
console.log(colourNames);
console.log(colourScores);
console.log(topTenName);
console.log(topTenScore);



/*Gráfico TOP 5 DE LA SEMANA (Vinos mejor valorados)*/
var ctx = document.getElementById('myCanvas').getContext('2d');
var myCanvas = new Chart(ctx, {
    // The type of chart we want to create
    type: 'line',

    // The data for our dataset
    data: {
        labels: topFiveWeekName ,
        datasets: [{
            label: 'Vino/CantidadValoraciones',
            backgroundColor: 'transparent',
            color: '#9a681d',
            borderColor: '#2e2e88d4',
            data: topFiveWeekScore,
        }]
    },

    // Configuration options go here
    options: {
        scales: {
            yAxes: [{
                stacked: true
            }]
        }
    }
});


/*Gráfico TIPOS DE VINOS MÁS VISITADOS POR LA ACTIVIDAD DE LOS USUARIOS*/
var ctx2 = document.getElementById('myDoughnutChart');
var myDoughnutChart = new Chart(ctx2, {
    type: 'doughnut',
    data: {
        labels: colourNames,
        datasets: [{
            label: 'TIPOS DE VINOS MÁS VISITADOS POR LA ACTIVIDAD DE LOS USUARIOS',
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: '#2e2e88d4',
            data: colourScores,
        }]
    },

    // Configuration options go here
    options: {}
});


/*Gráfico D.O MÁS VISITADAS POR LA ACTIVIDAD DE LOS USUARIOS, vista general de tintos, blancos y rosados de cada una*/
Highcharts.chart(document.getElementById('container'), {
    colors: ['#882641', '#e4db97', '#ec97ae'],
    chart: {
        type: 'column',
        inverted: true,
        polar: true
    },
    title: {
        text: 'D.O MÁS VISITADAS'
    },
    tooltip: {
        outside: true
    },
    pane: {
        size: '85%',
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
                fontSize: '12px'
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
        name: 'Tintos',
        data: tintos,
    }, {
        name: 'Blancos',
        data: blancos,
    }, {
        name: 'Rosados',
        data: rosados,
    }]
});