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
                    <h1 class="font-pathway font-75 centerHack">
                        <i-odometer class="font-pathway" :value="prettyTemperature"></i-odometer>&deg;F
                    </h1>
                    <small class="text-muted">Current Temperature</small>
                </div>    
    
                <div class="col-xs-6 col-md-3 text-center">
                    <h1 class="font-pathway font-75 centerHack">
                        <i-odometer class="font-pathway" :value="prettyHumidity"></i-odometer>%
                    </h1>
                    <small class="text-muted">Current Humidity</small>
                </div>
    
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h1 class="font-pathway">Speed + Distance, Last 12h</h1>

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
    import IOdometer from 'vue-odometer';
    export default {
        data: function() {
            return {
                currentHumidity: 0,
                currentTemperature: 0,
                lastUpdated: '',
                speedData: {
                    topSpeed: 0,
                    currentSpeed: 0,
                    milesRanLastTwelevHours: 0
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
                            labels: {
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
                    yAxis: [{ // Primary yAxis
                        labels: {
                            formatter: function () {
                                return this.value + " MPH";
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
                    }, {
                        labels: {
                            formatter: function () {
                                return this.value + " Miles";
                            },

                            style: {
                                color: Highcharts.getOptions().colors[3]
                            }
                        },
                        title: {
                            text: 'Distance',
                            style: {
                                color: Highcharts.getOptions().colors[3]
                            }
                        },
                        min: 0,
                    }],
                    series: [],
                    tooltip: {
                        pointFormatter: function () {
                            return '<span style="color:' + this.color + '">\u25CF</span> ' + this.series.name + ': <b>' + this.y.toFixed(2) + ' ' + this.series.tooltipOptions.valueSuffix + '</b> <br />'

                        }
                    },
                    responsive: {
                        rules: [{
                            condition: {
                                maxWidth: 400
                            },
                            chartOptions: {
                                yAxis: [{
                                    labels: {
                                        enabled: false
                                    }
                                },
                                    {
                                        labels: {
                                            enabled: false
                                        }
                                    }
                                ]
                            }

                        }]
                    }
                }
            }
        },
        components: {
            RadialProgressBar,
            IOdometer
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
                    this.speedOptions.series.push(response.data.distance);

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
            getTicksForLastTwelveHours: function () {
                this.$http.get('/api/TicksLastTwelveHours').then(response => {
                    this.speedData.milesRanLastTwelevHours = convertTicksToMiles(response.data.ticks);
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
            this.getTicksForLastTwelveHours();

            //refresh every minute for 100 minutes.
            var refreshCount = 100;
            var interval = setInterval(function (atmosphericDataFunction, historicalAtmosphericDataFunction, historicalSpeedFunction, currentSpeedFunction, ticksForLastTwelveHoursfunction) {

                atmosphericDataFunction();
                historicalAtmosphericDataFunction();
                historicalSpeedFunction();
                currentSpeedFunction();
                ticksForLastTwelveHoursfunction();
                refreshCount--;
                console.log("Refreshing Data: " +  refreshCount + " Remaining until reload needed");
                if (refreshCount < 1) {
                    console.log("Script has expired. Please reload the page.");

                    clearInterval(interval);

                }

            }, 60000, this.getAtmosphericData, this.getHistoricalAtmosphericData, this.getHistoricalSpeedData, this.getCurrentSpeed, this.getTicksForLastTwelveHours)
          

            
        }
    }

    function convertTicksToMph(ticks) {
        var distance = ((ticks * 2 * 3.14 * 5.25) / 12);
        return distance * 0.0113636;
    }
    function convertTicksToMiles(ticks) {
        return ((ticks * 2 * 3.14 * 5.25) / 12) / 5280;
    }
</script>


