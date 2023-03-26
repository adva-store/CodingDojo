export class Ant {
  static create(direction) {
    const antImage = document.createElement('img');
    antImage.src = 'ant.png';
    antImage.height = 50;
    antImage.width = 50;
    antImage.style.transform = `rotate(${this.getRotationForDirection(direction)}deg)`;

    return antImage;
  }

  static getRotationForDirection(direction) {
    const directionRotationMap = { o: 90, s: 180, w: 270 };
    return directionRotationMap[direction] || 0;
  }
}
