$( document ).ready(function() {
    function generateBerlinClock() {
        var today = new Date();
        var hours = today.getHours();
        var minutes = today.getMinutes();
        var seconds = today.getSeconds();
    
        var ho5 = 0;
        while (hours >= 5) {
            hours -= 5;
            ho5++;
        }
    
        var ho1 = 0;
        while (hours > 0) {
            hours -= 1;
            ho1++;
        }
    
        var m5 = 0;
        while (minutes >= 5) {
            minutes -= 5;
            m5++;
        }
    
        var m1 = 0;
        while (minutes > 0) {
            minutes -= 1;
            m1++;
        }
    
console.log(ho5, ho1, m5, m1);
        // reset all
        $('div').removeClass('opacity-1');
        
        var m5divs = $('.m5 div');
        m5divs.slice(0, m5).each(function(index, element) {
            $(element).addClass('opacity-1');
        });
    
        var m1divs = $('.m1 div');
        m1divs.slice(0, m1).each(function(index, element) {
            $(element).addClass('opacity-1');
        });
    
        var ho5divs = $('.ho5 div');
        ho5divs.slice(0, ho5).each(function(index, element) {
            $(element).addClass('opacity-1');
        });
    
        var ho1divs = $('.ho1 div');
        ho1divs.slice(0, ho1).each(function(index, element) {
            $(element).addClass('opacity-1');
        });
    
        $('.sec div').removeClass('opacity-1');
        if (seconds % 2) {
            $('.sec div').addClass('opacity-1');
        }
    }
    generateBerlinClock();


    setInterval(function() {
        generateBerlinClock();
    }, 1000);

});

