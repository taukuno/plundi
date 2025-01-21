export var charts = {};

/**
 * Draws the chart.
 *
 * @param {string} canvasId The id of the canvas.
 */
export function drawChart(canvasId) {
    let canvas = document.getElementById(canvasId);
    if (canvas) {
        charts[canvasId] = new Chart(canvas, {
                type: 'line',
                data: {
                    labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
                    datasets: [
                        {
                            label: 'Hit points',
                            backgroundColor: '#f8717120',
                            borderColor: '#f87171',
                            cubicInterpolationMode: 'monotone',
                            yAxisID: 'y1'
                        },
                        {
                            label: 'Attack power',
                            backgroundColor: '#facc1520',
                            borderColor: '#facc15',
                            cubicInterpolationMode: 'monotone',
                            yAxisID: 'y2'
                        }
                    ]
                },
                options: {
                    aspectRatio: 2,
                    datasets: {
                        line: {
                            pointRadius: 0,
                            pointHoverRadius: 0
                        }
                    },
                    interaction: {
                        mode: 'index',
                        intersect: false
                    },
                    plugins: {
                        legend: {
                            position: 'bottom',
                            labels: {
                                color: '#e5e7eb'
                            }
                        },
                        tooltip: {
                            bodySpacing: 4,
                            cornerRadius: 4,
                            boxPadding: 4,
                            callbacks: {
                                title: function (tooltipItems) {
                                    if(tooltipItems[0].label === '')
                                    {
                                        return 'Level 10';
                                    }

                                    return 'Level ' + tooltipItems[0].label;
                                }
                            }
                        }
                    },
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true,
                                text: 'Level',
                                color: '#e5e7eb'
                            },
                            ticks: {
                                color: '#e5e7eb'
                            },
                            grid: {
                                color: '#4b5563'
                            }
                        },
                        y1: {
                            type: 'linear',
                            display: true,
                            position: 'left',
                            min: 100,
                            max: 220,
                            ticks: {
                                stepSize: 50,
                                color: '#f87171'
                            },
                            grid: {
                                color: '#4b5563'
                            }
                        },
                        y2: {
                            type: 'linear',
                            display: true,
                            min: 34,
                            max: 84,
                            position: 'right',
                            ticks: {
                                stepSize: 17,
                                color: '#facc15'
                            },
                            grid: {
                                drawOnChartArea: false
                            },
                        }
                    }
                }
            }
        );

        let mediaQueries = [
            window.matchMedia('(max-width: 767px)'),
            window.matchMedia('(min-width: 768px) and (max-width: 1023px)'),
            window.matchMedia('(min-width: 1024px)')
        ];

        function handleMediaChange() {
            if(charts[canvasId] === undefined)
                return;

            if (mediaQueries[0].matches) {
                charts[canvasId].options.aspectRatio = 1;
            } else if (mediaQueries[1].matches) {
                charts[canvasId].options.aspectRatio = 2;
            } else if (mediaQueries[2].matches) {
                charts[canvasId].options.aspectRatio = 4;
            }

            charts[canvasId].update();
        }

        mediaQueries.forEach(mq => mq.addEventListener('change', handleMediaChange));

        handleMediaChange();
    }
}

/**
 * Updates the chart.
 *
 * @param {string} canvasId The id of the canvas.
 * @param {object} hitPointsData The hit-points data to feed into the chart.
 * @param {object} attackPowerData The attack-power data to feed into the chart.
 * @param {boolean} smoothLines The value indicating whether to smooth the chart lines.
 */
export function updateChart(canvasId, hitPointsData, attackPowerData, smoothLines) {
    if (!charts[canvasId])
        return;

    charts[canvasId].data.datasets[0].data = hitPointsData;
    charts[canvasId].data.datasets[1].data = attackPowerData;

    if (smoothLines) {
        charts[canvasId].data.datasets[0].cubicInterpolationMode = 'monotone';
        charts[canvasId].data.datasets[1].cubicInterpolationMode = 'monotone';
        charts[canvasId].data.datasets[0].stepped = false;
        charts[canvasId].data.datasets[1].stepped = false;

        charts[canvasId].data.labels = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];
    } else {
        charts[canvasId].data.datasets[0].cubicInterpolationMode = undefined;
        charts[canvasId].data.datasets[1].cubicInterpolationMode = undefined;
        charts[canvasId].data.datasets[0].stepped = true;
        charts[canvasId].data.datasets[1].stepped = true;

        if (hitPointsData.length === 10) {
            charts[canvasId].data.datasets[0].data.push(hitPointsData[9]);
        }

        if (attackPowerData.length === 10) {
            charts[canvasId].data.datasets[1].data.push(attackPowerData[9]);
        }

        charts[canvasId].data.labels = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', ''];
    }

    charts[canvasId].update();
}

/**
 * Clears the chart.
 *
 * @param {string} canvasId The id of the canvas.
 */
export function clearChart(canvasId) {
    if (!charts[canvasId])
        return;

    charts[canvasId].data.datasets[0].data = null;
    charts[canvasId].data.datasets[1].data = null;
    charts[canvasId].update();
}

/**
 * Deletes the chart.
 *
 * @param {string} canvasId The id of the canvas.
 */
export function deleteChart(canvasId) {
    delete charts[canvasId];
}
