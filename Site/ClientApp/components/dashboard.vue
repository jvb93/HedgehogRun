<template>
    <div>
        <h1 class="text-center font-pathway font-50">Dashboard</h1> 
        <div class="row">
            <div class="clearfix">
                <div class="col-xs-12 col-md-6 col-md-push-3">
                    <radial-progress-bar :diameter="400"
                                         :startColor="'#D4FF2C'"
                                         :completed-steps="speedData.currentSpeed"
                                         :animateSpeed="250"
                                         :total-steps="speedData.topSpeed" style="left:50%; margin-left:-200px;">
                        <h1 class="font-anton font-75">{{ speedData.currentSpeed }} MPH</h1>
                        <small class="text-muted">Current Speed</small>
          
                    </radial-progress-bar>
                </div>
    
                <div class="col-xs-6 col-md-3 col-md-pull-6 text-center">
                    <h1 class="font-pathway font-50">{{prettyTemperature}}&deg;F</h1>
                    <small class="text-muted">Current Temperature</small>
                </div>    
    
                <div class="col-xs-6 col-md-3 text-center">
                    <h1 class="font-pathway font-50">{{prettyHumidity}}%</h1>
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
            <div class="col-md-12">
                <h1 class="font-pathway">Atmospheric Data, Last 12h</h1>

                <div class="panel">
                    <div class="panel-heading"></div>
                    <div class="panel-body">
                        <highcharts :options="options" ref="highcharts"></highcharts>

                    </div>
                </div>

            </div>

        </div>
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
                speedData: {
                    topSpeed: 0,
                    currentSpeed:0
                },
                options: {
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
                                format: '{value}°C',
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
                                opposite: true

                            }, { // Secondary yAxis
                                title: {
                                    text: 'Humidity',
                                    style: {
                                        color: Highcharts.getOptions().colors[0]
                                    }
                                },
                                labels: {
                                    format: '{value} %',
                                    style: {
                                        color: Highcharts.getOptions().colors[0]
                                    }
                                }
                            }
                    ],                 
                    series: [],            
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
                        format: '{value} MPH',
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
                    opposite: true
                },                 
                series: [],             
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
            }
        },

        methods: {
            getAtmosphericData: function () {
                this.$http.get('/api/currentatmospheric').then(response => {
                    // get body data
                    this.currentHumidity = response.data.humidity;
                    this.currentTemperature = response.data.temperature;

                }, response => {
                    console.log(response);
                });        
            },
            getHistoricalAtmosphericData: function () {
                this.$http.get('/api/historicalatmospheric').then(response => {
                // get body data
                this.options.series = [];
                this.options.series.push(response.data.humidity);
                this.options.series.push(response.data.temperature);

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
            }
        },

        mounted: function () {
            this.getAtmosphericData();
            this.getHistoricalAtmosphericData();
            this.getHistoricalSpeedData();
            setInterval(this.getAtmosphericData, 60000);
            setInterval(this.getHistoricalAtmosphericData, 60000);
            setInterval(this.getHistoricalSpeedData, 60000);
            
        }
    }
</script>


