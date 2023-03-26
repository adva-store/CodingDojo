export class DataParser {
  constructor() {
    this.boardSize = 0;
    this.startPosition = { x: 0, y: 0 };
    this.viewingDirection = '';
    this.moveCount = 0;
  }

  parseData(data) {
    const parsedData = data.replace(/[\s\n]/g, '').split(';').map((turn) => turn.split(','));
    this.boardSize = Math.sqrt(parsedData[0].length);

    parsedData[0].forEach((cell, i) => {
      if (cell[1]) {
        this.startPosition = { x: i % this.boardSize, y: Math.floor(i / this.boardSize) };
        this.viewingDirection = cell[0];
      }
    });

    this.moveCount = parsedData.length;
    return parsedData;
  }
}
