const Board = require('./board');
const Ant = require('./ant');

const langtonAnt = (boardSize, startX, startY, initialDirection, steps) => {
  const board = new Board(boardSize, steps);
  const ant = new Ant(board, startX, startY, initialDirection);
  return ant.move(steps);
};
  
module.exports = langtonAnt;