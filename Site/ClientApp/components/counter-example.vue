<template>
    <div>
        <h1>Counter</h1>
        <p>This is a simple example of a Vue.js component & Vuex</p>
        <p>
            Current count (Vuex): <strong>{{ currentCount }}</strong>
        </p>
        <p>
            Auto count: <strong>{{ autoCount }}</strong>
        </p>
        <button @click="incrementCounter()">Increment</button>
        <button @click="resetCounter()">Reset</button>

        <radial-progress-bar :diameter="200"
                             :startColor="'#D4FF2C'"
                             :completed-steps="completedSteps"
                             :animateSpeed="250"
                             :total-steps="totalSteps">
            <p>Total steps: {{ totalSteps }}</p>
            <p>Completed steps: {{ completedSteps }}</p>
        </radial-progress-bar>
    </div>
   
</template>

<script>
    import { mapActions, mapState } from 'vuex'
    import RadialProgressBar from 'vue-radial-progress/dist/vue-radial-progress.min.js'
  export default {
  data() {
  return {
      autoCount: 0,
      totalSteps: 10,
      completedSteps:5
  }
        },
    components: {
        RadialProgressBar
    },


  computed: {
  ...mapState({
  currentCount: state => state.counter
  })
  },

  methods: {
  ...mapActions(['setCounter']),

  incrementCounter: function() {
      var counter = this.currentCount + 1;
      this.completedSteps++;
  this.setCounter({counter: counter});
  },
  resetCounter: function() {
      this.setCounter({ counter: 0 });
      this.completedSteps = 0;
  this.autoCount = 0;
  }
  },

  created() {
  setInterval(() => {
  this.autoCount += 1
  }, 1000)
  }
  }
</script>

<style>
</style>
