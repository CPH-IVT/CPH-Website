import { LineGraph } from '../js/lineGraph.js';

const PercentileChart = {
    __proto__: LineGraph,
    curve: d3.curveMonotoneX,
    yTicks: this.yAxis.ticks(5),
    set setyTicks(numberOfTicks) {
        this.yTicks = this.yAxis.ticks(numberOfTicks);
    },
    xTicks: this.xAxis.ticks(10),
    set setxTicks(numberOfTicks) {
        this.xTicks = this.xAxis.ticks(numberOfTicks);
    },
    yTickFormat: this.yAxis.tickFormat(),
    set setyTickFormat(dataArray) {
        this.yTickFormat = this.yAxis.tickFormat(dataArray);
    },
    xTickFormat: this.xAxis.tickFormat(["0", "5th", "10th", "15th", "20th", "25th", "30th", "35th", "40th", "45th", "50th", "55th", "60th", "65th", "70th", "75th", "80th", "85th", "90th", "95th", "100th"]),
    line: d3.line().defined(i => D[i]).curve(this.curve).x(i => this.)
}