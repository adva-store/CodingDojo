import './style.css'
import { LangtonAnt } from './src/langton-ant';
import { DownloadService } from './src/download-service';

const langtonAnt = new LangtonAnt();
const downloadService = new DownloadService();

// File
document.getElementById('file').addEventListener('change', (event) => {
  const file = event.target.files?.[0];
  if (!file) return;

  const reader = new FileReader();
  reader.onload = () => {
    langtonAnt.updateMovesFromFile(reader.result);
    langtonAnt.startGame();
  };
  reader.readAsText(file);
});

// Speed control
document.getElementById('speed').addEventListener('change', (event) => {
  const speed = parseInt(event.target.value);
  langtonAnt.updateAnimationSpeed(speed);
  if (langtonAnt.moves.length > 0) langtonAnt.startGame();
});

// Download
document.getElementById('downloadLink').addEventListener('click', async (event) => {
  event.preventDefault();
  await downloadService.downloadFile();
});

