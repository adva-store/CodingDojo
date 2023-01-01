# Readme Berlin clock

this solves this Berlin Clock kata ov adva-store.

## installation:

Run `npm i` to install all required dependencies.

## Start

To use the dev version execute `npm run dev`.  The Berlin Clock is available under `localhost:5173`.

To check the prod build first run `nom run build` to build the app and afterwards `npn run preview` to serve it on `localhost:4173`


## Code organisation

The main component cna be found in `src/clock` while the serice providing the time can be found `src/timeService.ts`

## Notes

Obbiously this challange could also be completed using vanilla JS (or any other framework), but Idecided to refresh my React 
kwnoledge because I didn't use it for quite a while.
Also using RxJs may look over-engineered and the number of external depenncies (or for some additional complexety) could be 
reduced by relying on `setInterval` instead.