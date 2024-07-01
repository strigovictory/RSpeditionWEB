/* Диаграмма-Воронка */
function RenderPlotlyChart_funnel(ref, title, groups, dataListNames, dataListValues) {
    var data = [];

    for (i = 0; i < groups.length; i++)
    {
        data.push
            (
                {
                    type: 'funnel',
                    name: groups[i],
                    y: dataListNames,
                    x: dataListValues[i],
                    textposition: "inside",
                    textinfo: "value+percent total"
                }
            )
    }

    var layout =
    {
        title: { position: "top center", text: title},
        margin: { l: 300, r: 0 },
        width: 1200,
        height: 600,
        funnelmode: "stack",
        showlegend: 'true',
        paper_bgcolor: '#878787',
        plot_bgcolor: '#878787'
    }

    var config =
    {
        modeBarButtonsToRemove: ['pan2d', 'lasso2d'],
        responsive: true,
        scrollZoom: true,
        displaylogo: false
    }

    Plotly.newPlot(ref, data, layout, config);
}

/* Диаграмма с заполненной площадью */
function RenderPlotlyChart_area(ref, title, groups, dataListNames, dataListValues) {
    var data = [];

    for (i = 0; i < groups.length; i++) {
        data.push
            (
                {
                    fill: 'tonexty',
                    type: 'scatter',
                    name: groups[i],
                    y: dataListValues[i],
                    x: dataListNames,
                    textposition: "inside",
                    textinfo: "value+percent total"
                }
            )
    }


    var layout =
    {
        title: { position: "top center", text: title },
        margin: { l: 130, r: 0 },
        width: 1200,
        height: 600,
        showlegend: 'true',
        paper_bgcolor: '#878787',
        plot_bgcolor: '#878787'
    }

    var config =
    {
        modeBarButtonsToRemove: ['pan2d', 'lasso2d'],
        responsive: true,
        scrollZoom: true,
        displaylogo: false
    }

    Plotly.newPlot(ref, data, layout, config);
}

/* Столбиковая диаграмма */
function RenderPlotlyChart_bar(ref, title, dataListNames, dataListValues) {
    var data = [
        {
            x: dataListNames,
            y: dataListValues,
            type: 'bar'
        }
    ];

    var layout =
    {
        title: { position: "top center", text: title },
        margin: { l: 130, r: 0 },
        width: 1200,
        height: 600,
        funnelmode: "stack",
        showlegend: 'true',
        paper_bgcolor: '#878787',
        plot_bgcolor: '#878787'
    }

    var config =
    {
        modeBarButtonsToRemove: ['pan2d', 'lasso2d'],
        responsive: true,
        scrollZoom: true,
        displaylogo: false
    }

    Plotly.newPlot(ref, data, layout, config);
}

/* Круговая диаграмма */
function RenderPlotlyChart_pie(ref, title, dataListNames, dataListValues) {
    var data = [
        {
            labels: dataListNames,
            values: dataListValues,
            type: 'pie'
        }
    ];
    
    var layout =
    {
        title: { position: "top center", text: title },
        margin: { l: 130, r: 0 },
        width: 1200,
        height: 600,
        funnelmode: "stack",
        showlegend: 'true',
        paper_bgcolor: '#878787',
        plot_bgcolor: '#878787'
    }

    var config =
    {
        modeBarButtonsToRemove: ['pan2d', 'lasso2d'],
        responsive: true,
        scrollZoom: true,
        displaylogo: false
    }

    Plotly.newPlot(ref, data, layout, config);
}

/* Линейная диаграмма */
function RenderPlotlyChart_line(ref, title, dataListNames, dataListValues) {
    var data = [
        {
            x: dataListNames,
            y: dataListValues,
            type: 'scatter',
            mode: 'lines+markers',
            line: { color: '#09bd70', width: 5, shape: 'vh'},
            marker: { size: 9, color: 'black' }
        }
    ];

    var layout =
    {
        title: { position: "top center", text: title },
        margin: { l: 130, r: 0 },
        width: 1200,
        height: 600,
        funnelmode: "stack",
        showlegend: 'true',
        paper_bgcolor: '#878787',
        plot_bgcolor: '#878787'
    }

    var config =
    {
        modeBarButtonsToRemove: ['pan2d', 'lasso2d'],
        responsive: true,
        scrollZoom: true,
        displaylogo: false
    }

    Plotly.newPlot(ref, data, layout, config);
}