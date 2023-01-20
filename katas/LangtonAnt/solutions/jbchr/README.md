# Janek's Langton Ant

I chose the tech stack I'm most familiar with (NestJs was new for me). I asumed an infinite grid,
so if the ant moves out on the east side it will show up in the west.

## Tech Stack

- NX
- NestJs (Backend)
- Angular (Frontend)

## Installation

Install npm dependencies:

```
npm install
```

## Usage

The following command will run both frontend and backend:

```
npm run start:all
```

To download a csv file for a new game go to the following url and modify the params to your liking:

```
http://localhost:3333/api/game.csv?boardLength=5&antPosX=2&antPosY=2&antDirection=n&turns=20
```

The frontend is available under:

```
http://localhost:4200/
```

There you can upload the file you just downloaded or choose a file from the `samples` folder.

## Potential Improvements

- Add tests for the frontend
- Style the frontend
- Error handling in the frontend
-
