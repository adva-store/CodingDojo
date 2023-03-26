const { CellState, Direction } = require('./constants');

const getRow = (position, boardSize) => Math.floor(position / boardSize);
const getColumn = (position, boardSize) => position % boardSize;
const positionFromCoordinates = (x, y, boardLength) => y * boardLength + x;

class Ant {
  constructor(board, startX, startY, initialDirection) {
    this.board = board;
    this.position = positionFromCoordinates(+startX, +startY, +this.board.boardSize);
    this.initialDirection = initialDirection;
  }

  toggleCellState(cell) {
    return cell[0] + (cell[1].endsWith(CellState.WHITE) ? CellState.BLACK : CellState.WHITE);
  }

  removeAnt(cell) {
    return ` ${cell[1]}`;
  }

  addAnt(cell, direction) {
    return `${direction}${cell[1]}`;
  }

  newPosition(position, direction) {
    const col = getColumn(position, this.board.boardSize);
    const row = getRow(position, this.board.boardSize);

    let newRow = row;
    let newCol = col;

    switch (direction) {
      case Direction.NORTH:
        newRow = (row - 1 + this.board.boardSize) % this.board.boardSize;
        break;
      case Direction.EAST:
        newCol = (col + 1) % this.board.boardSize;
        break;
      case Direction.SOUTH:
        newRow = (row + 1) % this.board.boardSize;
        break;
      case Direction.WEST:
        newCol = (col - 1 + this.board.boardSize) % this.board.boardSize;
        break;
    }

    return positionFromCoordinates(newCol, newRow, this.board.boardSize);
  }

  updateDirection(currentDirection, cell) {
    const directions = Object.values(Direction);
    const directionIndex = directions.indexOf(currentDirection);
    const offset = cell === CellState.WHITE ? 1 : -1;
    const newIndex = (directionIndex + offset + directions.length) % directions.length;
    return directions[newIndex];
  }

  move(steps) {
    let currentCellState = CellState.WHITE;
    let currentDirection = this.initialDirection;
  
    this.board.setCell(0, this.position, `${this.initialDirection}${CellState.WHITE}`);
  
    for (let i = 1; i < steps; i++) {
      currentDirection = this.updateDirection(currentDirection, currentCellState);
      
      this.board.board[i] = [...this.board.board[i - 1]];
      this.board.setCell(i, this.position, this.toggleCellState(this.board.getCell(i, this.position)));
      this.board.setCell(i, this.position, this.removeAnt(this.board.getCell(i, this.position)));
  
      this.position = this.newPosition(this.position, currentDirection);
      this.board.setCell(i, this.position, this.addAnt(this.board.getCell(i, this.position), currentDirection));
  
      currentCellState = this.board.getCell(i, this.position)[1];
    }
  
    return this.board.board;
  }
}

module.exports = Ant;