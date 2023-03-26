const fs = require('fs');

class Exporter {
  constructor(filename) {
    this.filename = filename;
  }

  export(data) {
    const formattedData = this.format(data);
    fs.writeFileSync(this.filename, formattedData);
  }

  formatData(data) {
    return this.format(data);
  }

  format(data) {
    throw new Error('format method must be implemented in the derived class.');
  }
}

module.exports = Exporter;