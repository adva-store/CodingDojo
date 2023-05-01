<template>
  <div class="max-w-sm mx-auto my-6">
      <div class="grid grid-cols-4">
          <div class="col-span-4 flex justify-center">
              <div class="w-8 h-8 rounded-full border"
                   :class="{ 'bg-slate-300': seconds > 0 }">
                  &nbsp;
              </div>
          </div>

          <div class="border h-8" :class="{ 'bg-slate-600': fiveHours > 0 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': fiveHours > 1 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': fiveHours > 2 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': fiveHours > 3 }">&nbsp;</div>

          <div class="border h-8" :class="{ 'bg-slate-600': hours > 0 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': hours > 1 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': hours > 2 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': hours > 3 }">&nbsp;</div>
      </div>

      <div class="grid grid-cols-11">
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 0 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 1 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': fiveMinutes > 2 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 3 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 4 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': fiveMinutes > 5 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 6 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 7 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-600': fiveMinutes > 8 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 9 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': fiveMinutes > 10 }">&nbsp;</div>
      </div>

      <div class="grid grid-cols-4">
          <div class="border h-8" :class="{ 'bg-slate-300': minutes > 0 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': minutes > 1 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': minutes > 2 }">&nbsp;</div>
          <div class="border h-8" :class="{ 'bg-slate-300': minutes > 3 }">&nbsp;</div>
      </div>
  </div>
</template>

<script setup lang="ts">
const seconds = ref(0)
const fiveHours = ref(0)
const hours = ref(0)
const fiveMinutes = ref(0)
const minutes = ref(0)

setDateTimeData()
setInterval(() => setDateTimeData(), 1000)

function setDateTimeData(): void {
    const dateTime = new Date()

    seconds.value = dateTime.getSeconds() % 2
    fiveHours.value = Math.floor(dateTime.getHours() / 5)
    hours.value = dateTime.getHours() % 5
    fiveMinutes.value = Math.floor(dateTime.getMinutes() / 5)
    minutes.value = dateTime.getMinutes() % 5
}
</script>
