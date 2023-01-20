# Janek's Berlin Clock

![alt text](/docs/images/preview.png)

I choose react and three.js for this project because I don't have much experience in it so I wanted to try it. I chose Next.js because the kata's description was asking for a server and this was an easy way to expose an api route. Honestly, I read over the description that the kata should follow TDD, so this application was not developed test-driven.

The client will query the server every second about the current berlin clock time. In a real application this computation might done on the client as it is inexpensive. Furthermore, the request might be delayed leading in the clock not "blinking" regularily but in different intervals.

## Tech Stack

- Next.js / React
- React Three Fiber / Three.js

## Usage

First install dependencies:

```
npm install
```

then start dev server

```
npm run dev
```

The website will be served on http://localhost:3000 and the server's only api route is available on http://localhost:3000/api/berlin-clock-time

## Notes / Todo / Improvements

The following things should be implemented in the future:

- Server should return time in the client's timezone
- Error handling, when server response is invalid, or server failed to answer
