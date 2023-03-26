import { Board } from './board';
import { GameInfo } from './game-info';
import { DataParser } from './data-parser';

export class LangtonAnt {
  constructor() {
    this.board = new Board(document.getElementById('grid'));
    this.gameInfo = new GameInfo();
    this.dataParser = new DataParser();
    this.moves = [];
    this.currentTurn = 0;
    this.animationSpeed = document.getElementById('speed').value || 500;
    this.animation = null;
  }

  startGame() {
    clearInterval(this.animation);
    this.currentTurn = 0;
    this.gameInfo.updateInfo(
      this.dataParser.boardSize,
      this.dataParser.startPosition,
      this.dataParser.viewingDirection,
      this.dataParser.moveCount
    );

    this.animation = setInterval(() => {
      if (this.currentTurn < this.moves.length) {
        this.board.renderTurn(this.moves[this.currentTurn]);
        this.currentTurn++;
      } else {
        clearInterval(this.animation);
      }
    }, this.animationSpeed);
  }

  updateMovesFromFile(fileContent) {
    this.moves = this.dataParser.parseData(fileContent);
  }

  updateAnimationSpeed(speed) {
    this.animationSpeed = parseInt(speed);
  }
}
