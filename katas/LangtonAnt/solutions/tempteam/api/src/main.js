const express = require('express');
const cors = require('cors');
const app = express();
const langtonAnt = require('./langton');
const CSVExporter = require('./exporter/csv-exporter');

app.use(cors({
  origin: '*'
}));

app.get('/simulate', (req, res) => {
  const { boardSize, startX, startY, direction, steps } = req.query;

  const result = langtonAnt(boardSize, startX, startY, direction, steps);

  const fileName = 'silumation.csv';
  const csvExporter = new CSVExporter(fileName);
  // csvExporter.export(result);

  // Set the content type and disposition headers for the CSV response
  res.setHeader('Content-Type', 'text/csv');
  res.setHeader('Content-Disposition', `attachment; filename=${fileName}`);

  // Send the CSV content as the response
  res.send(csvExporter.formatData(result));
})

app.listen(3000, () => {
  console.log('Server listening on port 3000')
})