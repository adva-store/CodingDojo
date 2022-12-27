$( document ).ready(function() {
    // helper function - calculate time
    function calculateTime(time, step = 5) {
        return [
            Math.floor(time/step),
            time % step
        ];
    }
    // helper function - print time
    function printTime(selector, count) {
        let allItems = $(selector);
        allItems.slice(0, count).each(function(index, element) {
            $(element).addClass('opacity-1');
        });
    }
    // main function
    function generateBerlinClock() {
        var today = new Date();
        // calculate hours
        calculatedHours = calculateTime(today.getHours());
        // calculate minutes
        calculatedMinutes = calculateTime(today.getMinutes());
        // reset all
        $('div').removeClass('opacity-1');
        // print hours
        printTime('.ho5 div', calculatedHours[0]);
        printTime('.ho1 div', calculatedHours[1]);
        // print minutes
        printTime('.m5 div', calculatedMinutes[0]);
        printTime('.m1 div', calculatedMinutes[1]);
        // print seconds
        if (today.getSeconds() % 2) {
            $('.sec div').addClass('opacity-1');
        }
    }

    generateBerlinClock();

    setInterval(function() {
        generateBerlinClock();
    }, 1000);

});

