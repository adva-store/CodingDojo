# Langton Ant

Langton's Ant is a cellular automaton that simulates an ant moving on a grid.

## Tech Stack

- Node.js
- Express.js (API)
- Vite (Frontend)

## Installation

To install the necessary npm dependencies, run the following command:

```
npm run install:all
```

## Usage

To run both the frontend and API concurrently, execute the following command:

```
npm run start:all
```

Run only the API:

```
npm run start:api
```

Run only the frontend (Note: File download from the API is disabled in this mode):

```
npm run start:frontend
```

### Generating Simulation Data

You can generate and export new simulation data in two ways:

1. Send a request to the API with the desired parameters. The API will return the CSV data in the response.

```
http://localhost:3000/simulate?boardSize=10&startX=1&startY=1&direction=n&steps=10

```

2. Use the frontend interface to download the simulation data as a CSV file.

The frontend can be accessed at:

```
http://localhost:5173
```
