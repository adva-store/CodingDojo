import { Ant } from './ant';

export class Cell {
  static create(cellData) {
    const cellElement = document.createElement('span');
    cellElement.style.backgroundColor = this.getBackgroundColor(cellData);
    if (cellData[1]) {
      cellElement.appendChild(Ant.create(cellData[0]));
    }

    return cellElement;
  }

  static getBackgroundColor(cellData) {
    return cellData[0] === 'w' ? '#eeeeee' : '#323232d4';
  }
}
