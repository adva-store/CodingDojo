export class DownloadService {
  constructor() {
    this.apiUrl = 'http://localhost:3000/simulate?boardSize=5&startX=1&startY=1&direction=n&steps=5';
    this.fileName = 'silumation.csv';
  }

  async downloadFile() {
    try {
      const response = await fetch(this.apiUrl);
      const blob = await response.blob();
      const downloadUrl = URL.createObjectURL(blob);
      const temporaryLink = document.createElement('a');
      temporaryLink.href = downloadUrl;
      temporaryLink.download = this.fileName;
      document.body.appendChild(temporaryLink);
      temporaryLink.click();
      document.body.removeChild(temporaryLink);
      URL.revokeObjectURL(downloadUrl);
    } catch (error) {
      console.error('Error downloading the CSV file:', error);
    }
  }
}
