using bvJagiolka.Console;
using bvJagiolka.Shared;

Console.WriteLine("Barcode Validator gestartet");
Console.WriteLine("===========================");

// Barcodeliste setzen, aus den übergebenen Argumenten oder die gestellten Testdaten
List<string> barcodeList = args.ToList();
if (barcodeList.Count == 0)
{
  barcodeList.AddRange(TestData.GetBarcodeList_TestData());
  Console.WriteLine("Es wurden keine Daten übergeben, daher werden die Testdaten geladen:");
}
else
{
  Console.WriteLine("Folgende Übergaben wurden erkannt:");
}
barcodeList.ForEach(barcode => Console.WriteLine(barcode));
Console.WriteLine("\n\n\n");

// valide Barcodes filtern
var validBarcodes = BarcodeListManager.GetValideBarcodes(barcodeList);

// valide Barcodes ausgeben
Console.WriteLine("Valide Barcodes:");
validBarcodes.ForEach(validBarcode => Console.WriteLine(validBarcode));