import { Cell } from './cell'
export class Board {
  constructor(gridContainer) {
    this.gridContainer = gridContainer;
  }

  renderTurn(turnData) {
    this.clearGridContainer();
    this.setGridContainerSize(turnData);
    turnData.forEach((cellData) => this.gridContainer.appendChild(Cell.create(cellData)));
  }

  clearGridContainer() {
    this.gridContainer.innerHTML = '';
  }

  setGridContainerSize(turnData) {
    this.gridContainer.style.setProperty('--fields', Math.sqrt(turnData.length));
  }
}
