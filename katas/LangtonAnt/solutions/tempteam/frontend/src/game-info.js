export class GameInfo {
  constructor() {
    this.edgeLength = document.getElementById('edgeLength');
    this.startingPosition = document.getElementById('startingPosition');
    this.viewingDirection = document.getElementById('viewingDirection');
    this.moves = document.getElementById('moves');
  }

  updateInfo(boardSize, startPosition, viewingDirection, moves) {
    this.edgeLength.textContent = boardSize;
    this.startingPosition.textContent = `${startPosition.x}, ${startPosition.y}`;
    this.viewingDirection.textContent = viewingDirection;
    this.moves.textContent = moves;
  }
}
