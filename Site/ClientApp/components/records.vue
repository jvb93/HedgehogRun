<template>
    <div>
      <div class="row mt50">
          <div class="col-md-6">
              <div class="panel text-center">
                  <h1 class="font-pathway font-75">{{roundToTwoPlaces(topSpeed)}}<small>mph</small></h1>
                  <h4 class="text-muted">Top Speed since {{prettyDate(oldestTick, 'L')}}</h4>
              </div>
              <div class="panel">
                  <div class="panel-heading">Top 10 Fastest Intervals</div>
                  <div class="panel-body">
                      <table class="table table-striped table-borderless">
                          <thead>
                              <tr>
                                  <th>Timestamp</th>
                                  <th>Speed (mph)</th>
                              </tr>
                          </thead>
                          <tbody>
                              <tr v-for="(record, index) in fastestIntervals" :key="index">
                                  <td>
                                      {{prettyDate(record.date, 'MM/DD/YYYY hh:mm A')}}
                                  </td>
                                  <td>
                                      {{roundToTwoPlaces(record.speed)}}
                                  </td>
                              </tr>
                          </tbody>
                      </table>
                  </div>
              </div>
          </div>
          <div class="col-md-6">
              <div class="panel text-center">
                  <h1 class="font-pathway font-75">{{roundToTwoPlaces(totalMiles)}}<small> miles</small></h1>
                  <h4 class="text-muted">Miles Ran since {{prettyDate(oldestTick, 'L')}}</h4>
              </div>
              <div class="panel">
                  <div class="panel-heading">Top 10 Longest Nights</div>
                  <div class="panel-body">
                      <table class="table table table-striped table-borderless">
                          <thead>
                              <tr>
                                  <th>Day</th>
                                  <th>Distance (miles)</th>
                              </tr>
                          </thead>
                          <tbody>
                              <tr v-for="(record, index) in longestNights" :key="index">
                                  <td>
                                      {{prettyDate(record.date, 'L')}}
                                  </td>
                                  <td>
                                      {{roundToTwoPlaces(record.distance)}}
                                  </td>
                              </tr>
                          </tbody>
                      </table>
                  </div>
              </div>
          </div>
      </div>

       
    </div>

   
</template>

<script>
    import { mapActions, mapState } from 'vuex'
    import moment from 'moment'
   
    export default {
        data: function() {
            return {
                topSpeed: 0,
                totalMiles: 0,
                oldestTick: new Date(),
                longestNights:[],
                fastestIntervals:[]

            }
        },
       
        methods: {
            getRecords: function () {
                this.$http.get('/api/records').then(response => {
                    // get body data
                    this.topSpeed = convertTicksToMph(response.data.maxTicks);      
                    this.totalMiles = convertTicksToMiles(response.data.totalTicks);
                    this.oldestTick = response.data.firstTick;

                    for (var x = 0; x < response.data.topTenFastestPeriods.length; x++) {
                        this.fastestIntervals.push({ date: response.data.topTenFastestPeriods[x].Date, speed: convertTicksToMph(response.data.topTenFastestPeriods[x].Ticks) })
                    }

                    for (var x = 0; x < response.data.topTenNights.length; x++) {
                        this.longestNights.push({ date: response.data.topTenNights[x].Date, distance: convertTicksToMiles(response.data.topTenNights[x].Ticks) })
                    }



                }, response => {
                    console.log(response);
                });        
            },
            roundToTwoPlaces: function (value) {
                return value.toFixed(2);
            },
            prettyDate: function (date, fmat) {
                
                if (!isNaN(parseFloat(date)) && isFinite(date)) {

                    var d = new Date(date);
                    return moment(d).format(fmat);
                }
                return date;
            }
        
        },

        mounted: function () {
            this.getRecords();     
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


