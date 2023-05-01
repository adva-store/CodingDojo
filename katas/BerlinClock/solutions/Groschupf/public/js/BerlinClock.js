class BerlinClock{
    #secondsLightOn = false;
    #fiveHours = 0;
    #hours = 0;
    #fiveMinutes = 0;
    #minutes = 0;
    #currentTime = new Date();
  
    getFiveHoursLights(){
        return Math.floor(this.#currentTime.getHours() / 5);
    }
  
    getHoursLights(){
      return this.#currentTime.getHours() % 5;
    }
  
    getFiveMinutesLights(){
      return Math.floor(this.#currentTime.getMinutes() / 5);
    }
  
    getMinutesLights(){
      return this.#currentTime.getMinutes() % 5;
    }
  
    getSecondsLight(){
      return this.#currentTime.getSeconds() % 2;
    }

    getData(){
      return [
        this.getFiveHoursLights(),
        this.getHoursLights(),
        this.getFiveMinutesLights(), 
        this.getMinutesLights(),
        this.getSecondsLight()
      ]
    }
  }

  module.exports = BerlinClock