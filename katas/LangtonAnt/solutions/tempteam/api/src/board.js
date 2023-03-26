const { CellState } = require('./constants');

class Board {
  constructor(boardSize, steps) {
    this.boardSize = parseInt(boardSize);
    this.steps = parseInt(steps);
    this.board = this.createBoard();
  }

  createBoard() {
    return new Array(this.steps)
      .fill(null)
      .map(() => Array(this.boardSize * this.boardSize).fill(` ${CellState.WHITE}`));
  }

  setCell(step, position, value) {
    this.board[step][position] = value;
  }

  getCell(step, position) {
    return this.board[step][position];
  }
}

module.exports = Board;