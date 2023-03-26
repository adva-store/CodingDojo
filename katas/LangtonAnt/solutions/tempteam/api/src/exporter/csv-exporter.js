const Exporter = require('./exporter');

class CSVExporter extends Exporter {
  delimiter = ';'

  format(data) {
    const formattedRows = data.map(row => {
      const formattedFields = row.map(field => field.trim()).join(',');
      return formattedFields + this.delimiter;
    });

    // Remove the delimiter from the last row
    const lastRowIndex = formattedRows.length - 1;
    formattedRows[lastRowIndex] = formattedRows[lastRowIndex].slice(0, -1);

    return formattedRows.join('\n');
  }
}

module.exports = CSVExporter;