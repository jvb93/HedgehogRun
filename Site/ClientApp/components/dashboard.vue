<template>
    <div>
    <h1>{{prettyTemperature}}&deg;F</h1>

        <radial-progress-bar :diameter="200"
                             :startColor="'#D4FF2C'"
                             :completed-steps="prettyHumidity"
                             :animateSpeed="250"
                             :total-steps="100">
            <p>Current Humidity: {{ prettyHumidity }}%</p>
          
        </radial-progress-bar>
    </div>
   
</template>

<script>
    import { mapActions, mapState } from 'vuex'
    import RadialProgressBar from 'vue-radial-progress/dist/vue-radial-progress.min.js'
    export default {
        data: function() {
            return {
                currentHumidity: 0,
                currentTemperature: 0
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
                this.$http.get('/api/hoglog').then(response => {
                    console.log(response);
                    // get body data
                    this.currentHumidity = response.data.humidity;
                    this.currentTemperature = response.data.temperature;

                }, response => {
                    // error callback
                });
            }

        },

        mounted: function () {
           this.getAtmosphericData();
            setInterval(this.getAtmosphericData, 6000);
            
        }
    }
</script>

<style>
</style>
