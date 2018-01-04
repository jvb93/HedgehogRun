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

export default {
    data() {
        return {
        }
    }
}
</script>

<style>
</style>
