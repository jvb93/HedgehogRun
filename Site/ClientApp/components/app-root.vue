<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-sm-2">
                <nav-menu params="route: route"></nav-menu>
            </div>
            <div class="col-sm-10" style="height:100vh;">
                <router-view></router-view>
            </div>
        </div>

    </div>

</template>

<script>
    import Vue from 'vue'
    import Dashboard from './dashboard'
    import FetchData from './fetch-data'
    import HomePage from './home-page'
    import NavMenu from './nav-menu'

    Vue.component('dashboard', Dashboard);
    Vue.component('fetch-data', FetchData);
    Vue.component('home-page', HomePage);
    Vue.component('nav-menu', NavMenu);
    import VueHighcharts from 'vue-highcharts';
    import Highcharts from 'highcharts';
    import VTooltip from 'v-tooltip'

    Vue.use(VTooltip)
    Vue.use(VueHighcharts);

    export default {
        data() {
            return {
            }
        },

    }

    Highcharts.setOptions({
        colors: ['#d4ff2c', '#24CBE5', '#ED561B', '#FFF', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
        global: {
            useUTC: false
        },
        chart: {
            backgroundColor: 'transparent',
            style: {
                fontFamily: 'sans-serif',
                color:'white'
            },
          
            zoomType: 'x'
            
        },

        legend: {
           
        },
        tooltip: {
            shared: true,
            backgroundColor: '#333',
            style: {
                color:'white'
            }
            
        },
        legend: {
           
            itemStyle: {
                color:'white'
            },
            itemHiddenStyle: {
                color:'#222'
            }
        
        },
        
    });

</script>

<style>
</style>
