// imports
const express = require('express');
const app = express();
const port = 3000;


// static files
app.use(express.static('public'));
app.use('/css', express.static(__dirname + 'public/css'));
app.use('/js', express.static(__dirname + 'public/js'));

// set views
app.set('views', './views')
app.set('view engine', 'ejs')

app.get('', (request, response) => {
  
  response.render('index')
})

app.get('/berlinClockData', (request, response) => {
  var BerlinClock = require('./public/js/BerlinClock.js')
  var clock = new BerlinClock()
  response.json(clock.getData())
})




app.listen(port, () => console.info(`listening on port ${port}`))
