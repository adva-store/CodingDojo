// format time
function formatHour(hour) {
    return hour + ' hour' + ((hour != 1) ? 's' : '');
}

function formatMinute(minute) {
    return minute + ' minute' + ((minute != 1) ? 's' : '');
}

// draw clock 
function drawClock() {
    window.setInterval(function () {
        // time instance
        var time = new Date();
        // variables
        var seconds = time.getSeconds();
        var minutes = time.getMinutes();
        var hours = time.getHours();
        // current time
        var currentTime = hours + ':' + ((minutes < 10) ? '0' : '') + minutes;
        $('#currentTime').html(currentTime);

        //transform lights
        setLight('second', seconds % 2);
        setLight('hour05', hours >= 5);
        setLight('hour10', hours >= 10);
        setLight('hour15', hours >= 15);
        setLight('hour20', hours >= 20);
        setLight('hour01', (hours % 5) >= 1);
        setLight('hour02', (hours % 5) >= 2);
        setLight('hour03', (hours % 5) >= 3);
        setLight('hour04', (hours % 5) >= 4);
        setLight('minute05', minutes >= 5);
        setLight('minute10', minutes >= 10);
        setLight('minute15', minutes >= 15);
        setLight('minute20', minutes >= 20);
        setLight('minute25', minutes >= 25);
        setLight('minute30', minutes >= 30);
        setLight('minute35', minutes >= 35);
        setLight('minute40', minutes >= 40);
        setLight('minute45', minutes >= 45);
        setLight('minute50', minutes >= 50);
        setLight('minute55', minutes >= 55);
        setLight('minute01', (minutes % 5) >= 1);
        setLight('minute02', (minutes % 5) >= 2);
        setLight('minute03', (minutes % 5) >= 3);
        setLight('minute04', (minutes % 5) >= 4);

        // info rows
        $('#row0').html('1st row::: ' + seconds +' seconds')
        $('#row1').html('2nd row::: 5 hours &times; ' + Math.floor(hours / 5)
            + ' = ' + formatHour(5 * Math.floor(hours / 5)));
        $('#row2').html('3rd row::: ' + formatHour(hours % 5));
        $('#row3').html('4th row::: ' + ' 5 minutes &times; ' + Math.floor(minutes / 5)
            + ' = ' + formatMinute(5 * Math.floor(minutes / 5)));
        $('#row4').html('5th row::: ' + formatMinute(minutes % 5));
        $('#row5').html('<h1>' + currentTime + '</h1><p>CURRENT TIME</p>');
    }, 100);
}

// set light
function setLight(id, visible) {
    if (visible) {
        $('#' + id).show();
    } else {
        $('#' + id).hide();
    }
}
