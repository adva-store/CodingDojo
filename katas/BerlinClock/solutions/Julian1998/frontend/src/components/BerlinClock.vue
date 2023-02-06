<template>
  <div id="berlinclock">
    <div class="timezone">{{ tz }}</div>
    <div v-if="clock.length != 0" class="mx-auto clock">
      <div class="mx-auto dot" />
      <div id="hour1">
        <div class="hour" v-for="(hour1, index) in clock.hour1" :key="index" :class="{ 'empty': !hour1}"></div>
      </div>
      <div id="hour2">
        <div class="hour" v-for="(hour2, index) in clock.hour2" :key="index" :class="{ 'empty': !hour2}"></div>
      </div>
      <div id="minute1">
        <div class="minute-small" v-for="(minute1, index) in clock.minute1" :key="index"
             :class="{ 'empty': !minute1, 'red': (index + 1) % 3 == 0}"></div>
      </div>
      <div id="minute2">
        <div class="minute-big" v-for="(minute2, index) in clock.minute2" :key="index" :class="{ 'empty': !minute2}"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BerlinClock',
  props: {
    tz: String
  },
  data() {
    return {
      clock: []
    }
  },
  methods: {
    getClock() {
      this.axios.get(`${process.env.VUE_APP_API_URL}/api/clock/${this.tz}`).then((response) => {
        this.clock = response.data
      })
    }
  },
  mounted() {
    this.getClock()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.clock {
  width: 352px;
}

.dot {
  width: 4em;
  height: 4em;
  border-radius: 50%;
  border: .6em solid black;
  background-color: #FAD02C;
  margin-bottom: .5em;
  position: relative;
  animation: blink 1s infinite;
  align-self: center;
}

@keyframes blink {
  from {background-color: #FAD02C;}
  to {background-color: white;}
}

.hour, .minute-big, .minute-small {
  background-color: red;
  border: .6em solid black;
  width: 88px;
  height: 50px;
  display: inline-block;
}

.minute-big {
  background-color: #FAD02C;
}

.minute-small {
  background-color: #FAD02C;
  width: 32px;
}

.red {
  background-color: red;
}

.empty {
  background-color: white !important;
}

.timezone {
  font-size: 33px;
  text-align: center;
  margin-bottom: 10px;
}
</style>
