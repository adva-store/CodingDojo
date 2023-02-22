namespace bvJagiolka.Shared
{
  public class BarcodeListManager
  {
    public static List<string> GetValideBarcodes(List<string> barcodeList)
    {
      List<string> valideBarcodes = new();

      barcodeList.ForEach(
        curBarcode =>
        {
          if (BarcodeListManager.IsGTIN(curBarcode) || BarcodeListManager.IsEan8(curBarcode))
          {
            valideBarcodes.Add(curBarcode);
          }
        });

      return valideBarcodes;
    }

    public static bool IsGTIN(string barcode)
    {
      // 13 stellig und numerisch
      var isNumeric = long.TryParse(barcode, out long result);
      if (barcode.Length == 13 && isNumeric)
      {
        //var ländercode = barcode.Substring(0, 2);
        //var herstellercode = barcode.Substring(2, 5);
        //var artikelnummer = barcode.Substring(7, 5);
        var prüfzifferOrg = int.Parse(barcode.Substring(12, 1));

        int prüfsumme = BarcodeListManager.CalculateGtinPrüfsumme(barcode);
        int roundUpPrüfsumme = BarcodeListManager.CalculateGtinAufgerundetePrüfsumme(prüfsumme);
        int prüfzifferCalculated = roundUpPrüfsumme - prüfsumme;

        return prüfzifferOrg == prüfzifferCalculated;
      }

      return false;
    }

    public static bool IsEan8(string barcode)
    {
      // 8 stellig und numerisch
      var isNumeric = int.TryParse(barcode, out int result);
      if (barcode.Length == 8 && isNumeric)
      {
        return true;
      }

      return false;
    }

    // Prüfsumme => barcode wird von hinten nach vorne abwechselnd mit dem Faktor 3, dann 1 multipliziert.
    //              Die Summe all dieser Ergebnisse ist die Prüfsumme.
    public static int CalculateGtinPrüfsumme(string barcode)
    {
      int prüfsumme = 0;
      bool isFaktor3 = true;
      for (int i = 11; i >= 0; i--)
      {
        int singleInt = int.Parse(barcode.Substring(i, 1));
        int faktor = isFaktor3 ? 3 : 1;
        prüfsumme += singleInt * faktor;

        isFaktor3 = !isFaktor3;
      }

      return prüfsumme;
    }

    // Die Prüfsumme wird an der Zehnerstelle aufgerundet
    public static int CalculateGtinAufgerundetePrüfsumme(int prüfsumme)
    {
      decimal prüfsummeDecimal = ((decimal)prüfsumme) / 10;

      return (int)(Math.Ceiling(prüfsummeDecimal) * 10);
    }
  }
}
