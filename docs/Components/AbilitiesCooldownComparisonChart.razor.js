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
                type: 'bar',
                data: {
                    labels: ['Common', 'Uncommon', 'Rare', 'Epic']
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
                            cornerRadius: 0,
                            boxPadding: 4,
                            callbacks: {
                                label: function (context) {
                                    let label = context.dataset.label || '';

                                    if (label) {
                                        label += ': ';
                                    }

                                    if (context.dataset.minData && context.dataset.minData[context.dataIndex] !== undefined && context.dataset.minData[context.dataIndex] !== context.raw) {
                                        label += `${context.dataset.minData[context.dataIndex]} - ${context.raw}s`;
                                    } else {
                                        label += context.raw + 's';
                                    }

                                    return label;
                                }
                            }
                        },
                    },
                    scales: {
                        x: {
                            display: true,
                            ticks: {
                                color: '#e5e7eb'
                            },
                            grid: {
                                color: '#4b5563'
                            }
                        },
                        y: {
                            type: 'linear',
                            min: 0,
                            display: true,
                            position: 'left',
                            ticks: {
                                color: '#e5e7eb',
                                callback: function (value, index) {
                                    return value + 's';
                                },
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
            if(charts[canvasId] === undefined)
                return;

            if (mediaQueries[0].matches) {
                charts[canvasId].options.aspectRatio = 1;
            } else if (mediaQueries[1].matches) {
                charts[canvasId].options.aspectRatio = 2;
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
 * @param {object} data The data to feed into the chart.
 */
export function updateChart(canvasId, data) {
    if (!charts[canvasId])
        return;

    let abilityColors =
        {
            'Aura of Zealotry': '#fb923c',
            'Call Galefeather': '#a5f3fc',
            'Celestial Barrage': '#6d28d9',
            'Earthbreaker': '#a16207',
            'Explosive Caltrops': '#71717a',
            'Fade to Shadow': '#fde68a',
            'Faeform': '#0ea5e9',
            'Fire Whirl': '#f87171',
            'Void Tear': '#818cf8',
            'Holy Shield': '#fde047',
            'Hunter\'s Chains': '#84cc16',
            'Lightning Bulwark': '#67e8f9',
            'Mana Sphere': '#c084fc',
            'Quaking Leap': '#713f12',
            'Repel': '#7e22ce',
            'Rime Arrow': '#a855f7',
            'Searing Axe': '#dc2626',
            'Slicing Winds': '#bfdbfe',
            'Snowdrift': '#a5b4fc',
            'Star Bomb': '#1e40af',
            'Steel Traps': '#7f1d1d',
            'Storm Archon': '#2563eb',
            'Toxic Smackerel': '#16a34a',
            'Windstorm': '#0e7490'
        };

    charts[canvasId].data.datasets = [];
    let index = 0;

    data.forEach(item => {
        let dataset = {
            label: item.label,
            backgroundColor: abilityColors[item.label],
            data: item.data,
        }

        charts[canvasId].data.datasets.push(dataset);
        index++;
    });

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

    charts[canvasId].data.datasets = [];
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
