<template>
  <div class="container mt-5">
    <div class="row text-center">
      <div class="col p-4 bg-light border rounded-3 m-1">
        <BerlinClock tz="Europe/Berlin"/>
      </div>
      <div class="col p-4 bg-light border rounded-3 m-1">
        <BerlinClock tz="America/New_York"/>
      </div>
      <div class="col p-4 bg-light border rounded-3 m-1">
        <BerlinClock tz="Asia/Hong_Kong"/>
      </div>
    </div>
  </div>
</template>

<script>
import BerlinClock from './components/BerlinClock.vue'

export default {
  name: 'App',
  components: {
    BerlinClock
  }
}
</script>
