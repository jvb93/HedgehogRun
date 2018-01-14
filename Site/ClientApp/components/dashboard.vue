<template>
    <div>
        <h1 class="text-center font-pathway font-50">Dashboard</h1> 
        <div class="row">
            <div class="clearfix">
                <div class="col-xs-12 col-md-6 col-md-push-3">
                    <radial-progress-bar :diameter="350"
                                         :startColor="'#D4FF2C'"
                                         :completed-steps="speedData.currentSpeed"
                                         :animateSpeed="250"
                                         :total-steps="speedData.topSpeed" style="left:50%; margin-left:-175px;">
                        <h1 class="font-anton font-66">{{ roundToTwoPlaces(speedData.currentSpeed) }} MPH</h1>
                        <small class="text-muted">Current Speed</small>
          
                    </radial-progress-bar>
                </div>
    
                <div class="col-xs-6 col-md-3 col-md-pull-6 text-center">
                    <h1 class="font-pathway font-75 centerHack">{{prettyTemperature}}&deg;F</h1>
                    <small class="text-muted">Current Temperature</small>
                </div>    
    
                <div class="col-xs-6 col-md-3 text-center">
                    <h1 class="font-pathway font-75 centerHack">{{prettyHumidity}}%</h1>
                    <small class="text-muted">Current Humidity</small>
                </div>
    
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h1 class="font-pathway">Speed Data, Last 12h</h1>

                <div class="panel">
                    <div class="panel-heading"></div>
                    <div class="panel-body">
                        <highcharts :options="speedOptions" ref="highcharts"></highcharts>

                    </div>
                </div>

            </div>

        </div>
        <div class="row">
            <div class="col-md-6 col-xs-12">
                <h1 class="font-pathway">Temperature, Last 12h</h1>

                <div class="panel">
                    <div class="panel-heading"></div>
                    <div class="panel-body">
                        <highcharts :options="temperatureOptions" ref="highcharts"></highcharts>

                    </div>
                </div>

            </div>
            <div class="col-md-6 col-xs-12">
                <h1 class="font-pathway">Humidity, Last 12h</h1>

                <div class="panel">
                    <div class="panel-heading"></div>
                    <div class="panel-body">
                        <highcharts :options="humidityOptions" ref="highcharts"></highcharts>

                    </div>
                </div>

            </div>
        </div>
        <div class="col-md-12 text-right"><small class="text-muted">Data is current as of: {{prettyDate}}</small></div>
    </div>

   
</template>

<script>
    import { mapActions, mapState } from 'vuex'
    import RadialProgressBar from 'vue-radial-progress/dist/vue-radial-progress.min.js'
    import VueHighcharts from 'vue-highcharts';
    import Highcharts from 'highcharts';
    export default {
        data: function() {
            return {
                currentHumidity: 0,
                currentTemperature: 0,
                lastUpdated :'',
                speedData: {
                    topSpeed: 0,
                    currentSpeed:0
                },
                temperatureOptions: {
                    title: {
                        text: '',
                        x: -20 //center
                    },
                    
                    subtitle: {
                        text: '',
                        x: -20
                    },
                    xAxis: {
                        type: 'datetime'
                    },
                    yAxis: [
                            { // Primary yAxis
                            labels:{
                                format: '{value}°F',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                                },
                                title: {
                                    text: 'Temperature',
                                    style: {
                                        color: Highcharts.getOptions().colors[1]
                                    }
                                },
                                opposite: false

                            },
                    ],                 
                    series: [],
                    plotOptions: {
                        series: {
                            color: Highcharts.getOptions().colors[1]
                        }
                    },
                },
                humidityOptions: {
                    title: {
                        text: '',
                        x: -20 //center
                    },

                    subtitle: {
                        text: '',
                        x: -20
                    },
                    xAxis: {
                        type: 'datetime',
                       
                    },
                    yAxis: [
                        { // Primary yAxis
                            labels: {
                                format: '{value}%',
                                style: {
                                    color: Highcharts.getOptions().colors[2]
                                }
                            },
                            title: {
                                text: 'Humidity',
                                style: {
                                    color: Highcharts.getOptions().colors[2]
                                }
                            },
                            opposite: false

                        },
                    ],
                    series: [],
                    plotOptions: {
                        series: {
                            color: Highcharts.getOptions().colors[2]
                        }
                    },
                 
                },
                speedOptions: {
                    chart: {
                        type: 'area'
                    },
                    title: {
                        text: '',
                        x: -20 //center
                    },                 
                    subtitle: {
                        text: '',
                        x: -20
                    },
                    xAxis: {
                        type: 'datetime'

                    },
                    plotOptions: {
                        fillColor: Highcharts.getOptions().colors[0]
                    },
                yAxis:{ // Primary yAxis
                    labels:{
                        formatter: function () {
                            return convertTicksToMph(this.value).toFixed(2) + " MPH";
                        },
                       
                        style: {
                            color: Highcharts.getOptions().colors[0]
                        }
                    },
                    title: {
                        text: 'Speed',
                        style: {
                            color: Highcharts.getOptions().colors[0]
                        }
                    },
                    min: 0,
                },                 
                series: [],
                tooltip: {
                    pointFormatter: function () {
                        return '<span style="color:' + this.color + '">\u25CF</span> ' + this.series.name + ': <b>' + convertTicksToMph(this.y).toFixed(2) + ' MPH</b> <br />'

                    }
                }
                }
            }
        },
        components: {
            RadialProgressBar
        },
        computed: {
            prettyHumidity: function () {
                return Math.round(this.currentHumidity);
            },
            prettyTemperature: function () {
                return Math.round(this.currentTemperature);
            },
            prettyDate: function () {
                var time = this.lastUpdated;
                if (!isNaN(parseFloat(time)) && isFinite(time)) {

                    var d = new Date(time);
                    return d.toLocaleString();
                }
                return time;
            }
        },

        methods: {
            getAtmosphericData: function () {
                this.$http.get('/api/currentatmospheric').then(response => {
                    // get body data
                    this.currentHumidity = response.data.humidity;
                    this.currentTemperature = response.data.temperature;
                    this.lastUpdated = response.data.lastUpdated;

                }, response => {
                    console.log(response);
                });        
            },
          
            getHistoricalAtmosphericData: function () {
                this.$http.get('/api/historicalatmospheric').then(response => {
                // get body data
                this.temperatureOptions.series = [];
                this.humidityOptions.series = [];
                this.humidityOptions.series.push(response.data.humidity);
                this.temperatureOptions.series.push(response.data.temperature);

                }, response => {
                    console.log(response);
                });
            },
            getHistoricalSpeedData: function () {
                this.$http.get('/api/historicalspeed').then(response => {
                    this.speedOptions.series = [];
                    this.speedOptions.series.push(response.data.ticks);

                }, response => {
                    console.log(response);
                });
            },
            getCurrentSpeed: function () {
                this.$http.get('/api/currentspeed').then(response => {
                    this.speedData.currentSpeed = convertTicksToMph(response.data.current);
                    this.speedData.topSpeed = convertTicksToMph(response.data.max);

                }, response => {
                    console.log(response);
                });
            },
            setRandomSpeed: function () {
                this.speedData.currentSpeed = parseInt((Math.random() * (10 - 0) + 0).toFixed(2));
            },
            roundToTwoPlaces: function (value) {
                return value.toFixed(2);
            }
           
        },

        mounted: function () {
            this.getAtmosphericData();
            this.getHistoricalAtmosphericData();
            this.getHistoricalSpeedData();
            this.getCurrentSpeed();
            setInterval(this.getAtmosphericData, 60000);
            setInterval(this.getHistoricalAtmosphericData, 60000);
            setInterval(this.getHistoricalSpeedData, 60000);
            setInterval(this.getCurrentSpeed,60000);

            
        }
    }

    function convertTicksToMph(ticks) {
        var distance = ((ticks * 2 * 3.14 * 5.25) / 12);
        return distance * 0.0113636;
    }
</script>


