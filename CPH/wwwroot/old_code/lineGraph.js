import { Chart } from '../js/chart.js';

// TODO: UNUSED
const LineGraph = {
    __proto__: Chart,
    xType: d3.scaleLinear(this.xDomain, this.xRange),
    yType: d3.scaleLinear(this.yDomain, this.yRange),
    xLabel: 'X Axis Label',
    yLabel: 'Y Axis Label',
    xAxis: d3.axisBottom(this.xType),
    yAxis: d3.axisLeft(this.yType)
}

export { LineGraph };