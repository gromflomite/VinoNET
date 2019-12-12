
console.log(colourNames);
console.log(colourScores);
console.log(topTenName);
console.log(topTenScore);




var ctx = document.getElementById('myCanvas').getContext('2d');
var myCanvas = new Chart(ctx, {
    // The type of chart we want to create
    type: 'line',

    // The data for our dataset
    data: {
        labels: topFiveWeekName ,
        datasets: [{
            label: 'Planes/día',
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


var ctx2 = document.getElementById('myDoughnutChart');
var myDoughnutChart = new Chart(ctx2, {
    type: 'doughnut',
    data: {
        labels: colourNames,
        datasets: [{
            label: 'PLANES POR TIPO',
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