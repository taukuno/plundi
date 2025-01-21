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
                    labels: [],
                    datasets: [
                        {
                            label: 'Damage done',
                            backgroundColor: '#f8717120',
                            borderColor: '#f87171',
                            cubicInterpolationMode: 'monotone'
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
                            display: false,
                        },
                        tooltip: {
                            bodySpacing: 4,
                            cornerRadius: 4,
                            boxPadding: 4,
                            callbacks: {
                                title: function (tooltipItems) {
                                    return 'After ' + tooltipItems[0].label;
                                }
                            }
                        }
                    },
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: false,
                                text: 'Time',
                                color: '#e5e7eb'
                            },
                            ticks: {
                                color: '#e5e7eb'
                            },
                            grid: {
                                color: '#4b5563'
                            },
                        },
                        y: {
                            type: 'linear',
                            min: 0,
                            display: true,
                            position: 'left',
                            ticks: {
                                color: '#e5e7eb'
                            },
                            grid: {
                                color: '#4b5563'
                            }
                        },
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
            if (charts[canvasId] === undefined)
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
 * @param {string} timeLabels The time labels.
 * @param {object} stackingDamageData The stacking damage data to feed into the chart.
 * @param {boolean} smoothLines The value indicating whether to smooth the chart lines.
 */
export function updateChart(canvasId, timeLabels, stackingDamageData, smoothLines) {
    if (!charts[canvasId])
        return;

    charts[canvasId].data.labels = timeLabels;
    charts[canvasId].data.datasets[0].data = stackingDamageData;

    if (smoothLines) {
        charts[canvasId].data.datasets[0].cubicInterpolationMode = 'monotone';
        charts[canvasId].data.datasets[0].stepped = false;
    } else {
        charts[canvasId].data.datasets[0].cubicInterpolationMode = undefined;
        charts[canvasId].data.datasets[0].stepped = true;
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
