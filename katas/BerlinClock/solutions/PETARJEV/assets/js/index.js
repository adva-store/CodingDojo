(function () {
    //Die Funktion
    function paintTheClock() {
        var time = new Date();
        var hours = time.getHours();
        var minutes = time.getMinutes();
        var seconds = time.getSeconds();
        //Variablen, die nötig sind
        var fiveHours = 0;
        var oneHours = 0;
        var fiveMinutes = 0;
        var oneMinutes = 0;
        //Stunden fur die erste Reihe
        if (hours / 5 > 1) {
            fiveHours = Math.floor(hours / 5);
            hours = hours - (fiveHours * 5);
            var fiveHoursTiles = document.querySelectorAll(".five-hour");
            var fiveHoursTilesLength = fiveHoursTiles.length;
            for (var i = 0; i < fiveHours; i++) {
                fiveHoursTiles[i].className = "five-hour five-hour-filled";
            }
            for (var i = fiveHours; i < fiveHoursTilesLength; i++) {
                fiveHoursTiles[i].className = "five-hour";
            }
        }
        //Stunden fur die zweite Reihe
        if (hours > 0) {
            var oneHoursTiles = document.querySelectorAll(".one-hour");
            var oneHoursTilesLength = oneHoursTiles.length;
            for (var i = 0; i < hours; i++) {
                oneHoursTiles[i].className = "one-hour one-hour-filled";
            }
            for (var i = hours; i < oneHoursTilesLength; i++) {
                oneHoursTiles[i].className = "one-hour";
            }
        }
        //Minuten fur die dritte Reihe
        if (minutes / 5 > 1) {
            fiveMinutes = Math.floor(minutes / 5);
            minutes = minutes - (fiveMinutes * 5);
            var fiveMinutesTiles = document.querySelectorAll(".five-minute");
            var fiveMinutesTilesLength = fiveMinutesTiles.length;
            for (var i = 0; i < fiveMinutes; i++) {
                fiveMinutesTiles[i].className = "five-minute five-minute-filled";
            }
            for (var i = fiveMinutes; i < fiveMinutesTilesLength; i++) {
                fiveMinutesTiles[i].className = "five-minute";
            }
        }
        //Minuten fur die vierte Reihe
        if (minutes > 0) {
            var oneMinutsTiles = document.querySelectorAll(".one-minute");
            var oneMinutsTilesLength = oneMinutsTiles.length;
            for (var i = 0; i < minutes; i++) {
                oneMinutsTiles[i].className = "one-minute one-minute-filled";
            }
            for (var i = minutes; i < oneMinutsTilesLength; i++) {
                oneMinutsTiles[i].className = "one-minute";
            }
        }
        //Sekunden
        var secondsTile = document.getElementById("seconds");
        if (seconds % 2 == 0) {
            secondsTile.className = "";
        }
        else {
            secondsTile.className = "seconds-filled";
        }
    }
    window.setInterval(paintTheClock,1000);
})();
